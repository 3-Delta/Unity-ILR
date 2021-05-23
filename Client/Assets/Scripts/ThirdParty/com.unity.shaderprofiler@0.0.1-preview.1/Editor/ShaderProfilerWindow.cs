using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor.IMGUI.Controls;

#if UNITY_2019_1_OR_NEWER
using UnityEngine.UIElements;
using UnityEditor.UIElements;
#else
using UnityEditor.Experimental.UIElements;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Experimental.UIElements.StyleEnums;
#endif
using UnityEngine.Experimental.Networking.PlayerConnection;
using ConnectionGUI = UnityEditor.Experimental.Networking.PlayerConnection.EditorGUI;
using ConnectionUtility = UnityEditor.Experimental.Networking.PlayerConnection.EditorGUIUtility;

using Newtonsoft.Json;


namespace UnityEditor.ShaderProfiler
{

    
    public class ShaderProfilerItem
    {
        public PackedShaderData shaderData;
        public string itemName;

        public ShaderProfilerItem()
        {
            shaderData = new PackedShaderData();
        }

        public void BuildShaderProfilerItem(byte[] stream)
        {
            itemName = GUID.Generate().ToString();
            shaderData.BuildPackedShaderData(stream);
        }
    }

    public class ShaderProfilerWindow : EditorWindow
    {
        [MenuItem("Window/Analysis/Shader Profiler")]
        public static void ShowWindow()
        {
            GetWindow<ShaderProfilerWindow>("Shader Profiler").Show();
        }

        private readonly int UIElmenentIndexCaptureList = 0;
        private readonly int UIElmenentIndexShaderInfoTree = 1;
        private readonly int UIElmenentIndexShaderCompare = 2;
        private readonly float ToolbarHeight = 18;
        private readonly float ToolbarMinWidth = 5;
        
        private List<ShaderProfilerItem> _shaderProfilerItems;
        private IConnectionState m_PlayerConnectionState = null;
        private GUIContent m_attachToPlayerCachedTargetName = new GUIContent("");
        private ProfilerMode _currentProfilerMode = ProfilerMode.Information;
        private ShaderProfilerItem _currentProfilerItem = new ShaderProfilerItem();
        private ListView _captureListView;
        private List<object> _captureListSelection = new List<object>();
        private ShaderProfilerTreeView _informationTreeView;
        private Label _informationTitle;

        public ShaderProfilerWindow()
        {
            if (_shaderProfilerItems == null)
            {
                _shaderProfilerItems = new List<ShaderProfilerItem>();    
            }
            
        }

        enum ProfilerMode
        {
            Information,
            Compare
        }

        

        void OnEnable()
        {
            if (m_PlayerConnectionState == null)
            {
                m_PlayerConnectionState = ConnectionUtility.GetAttachToPlayerState(this);
            }
            
            VisualElement rootContainer = new VisualElement();
            rootContainer.style.flexGrow = 1.0f;
            rootContainer.Add(BuildCaptureList());
            switch (_currentProfilerMode)
            {
                case ProfilerMode.Information:
                    rootContainer.Add(BuildShaderInfoTreeView());
                    break;
                case ProfilerMode.Compare:
                    rootContainer.Add(BuildShaderCompareView());
                    break;
            }
            rootContainer.style.flexDirection = FlexDirection.Row;

#if UNITY_2019_1_OR_NEWER
            rootVisualElement.Add(rootContainer);
#else
            this.GetRootVisualContainer().Add(rootContainer);
#endif
        }


        private VisualElement BuildCaptureList()
        {
            VisualElement rootContainer = new VisualElement();
            
            SetLeftPanelStyle(rootContainer);
            
            Toolbar toolbar = new Toolbar();
            toolbar.style.height = ToolbarHeight;
            
            IMGUIContainer attachTo = new IMGUIContainer(DrawAttachToPlayerDropdown);
            ToolbarSpacer spacer = new ToolbarSpacer();
            
            
            ToolbarButton capture = new ToolbarButton(delegate
            {
                ShaderSnapshot.TakeShaderSnapshot(OnRequestFinished);
            });
            capture.text = "Capture";

            ToolbarButton save = new ToolbarButton(delegate
            {
                if (_captureListSelection.Count == 1)
                {
                    ShaderProfilerItem currentItem = (ShaderProfilerItem) _captureListSelection[0];
                    string path = EditorUtility.SaveFilePanel("Save", Application.dataPath, currentItem.itemName, "json");
                    if (path != null)
                    {
                        File.WriteAllText(path, JsonConvert.SerializeObject(currentItem.shaderData));
                    }
                }
            });
            save.text = "Save";
            
            ToolbarButton load = new ToolbarButton(delegate
            {
                string path = EditorUtility.OpenFilePanel("Load", Application.dataPath, "json");
                if (path != null)
                {
                    ShaderProfilerItem currentItem = new ShaderProfilerItem();
                    currentItem.shaderData = JsonConvert.DeserializeObject<PackedShaderData>(File.ReadAllText(path));
                    currentItem.itemName = Path.GetFileNameWithoutExtension(path);
                    _shaderProfilerItems.Add(currentItem);
                    _captureListView.Refresh();
                    _captureListView.selectedIndex = 0;
                }
                

            });
            load.text = "Load";
            
            ToolbarButton delete = new ToolbarButton(delegate
            {
                if (_captureListSelection.Count != 0)
                {
                    foreach (object obj in _captureListSelection)
                    {
                        ShaderProfilerItem currentItem = (ShaderProfilerItem) obj;
                        _shaderProfilerItems.Remove(currentItem);
                    }
                    
                    if (_shaderProfilerItems.Count != 0)
                    {
                        _captureListView.selectedIndex = 0;
                    }
                    else
                    {
                        _captureListView.selectedIndex = -1;
                    }
                    _captureListView.Refresh();

                    _informationTreeView.CurrentPackedShaderData = null;
                    _informationTitle.text = "";
                    _informationTreeView.Reload();

                }
            });
            delete.text = "Remove";
            
            toolbar.Add(attachTo);
            toolbar.Add(spacer);
            toolbar.Add(capture);
            toolbar.Add(save);
            toolbar.Add(load);
            toolbar.Add(delete);
            
            _captureListView = new ListView(_shaderProfilerItems, 30,
                MakeProfilerListItem,
                BuildProfilerListItem
                );
            _captureListView.selectionType = SelectionType.Multiple;
            _captureListView.style.flexDirection = FlexDirection.Column;
            _captureListView.style.flexGrow = 1;
            _captureListView.onItemChosen += delegate(object obj)
            {
                _currentProfilerItem = (ShaderProfilerItem) obj;
                _informationTreeView.CurrentPackedShaderData = _currentProfilerItem.shaderData;
                _informationTitle.text = _currentProfilerItem.itemName;
                _informationTreeView.Reload();

            };
            _captureListView.onSelectionChanged += delegate(List<object> objs)
            {
                _captureListSelection = objs;
                save.SetEnabled(objs.Count == 1);
                delete.SetEnabled(objs.Count != 0);
            };

            rootContainer.Add(toolbar);
            rootContainer.Add(_captureListView);
            return rootContainer;
        }

        private void OnDisable()
        {
            if (m_PlayerConnectionState != null)
            {
                m_PlayerConnectionState.Dispose();
                m_PlayerConnectionState = null;
            }
        }

        private void OnRequestFinished(byte[] data, ShaderProfiler.ShaderSnapshot.ShaderProfilerError errorcode)
        {
            if (errorcode == ShaderSnapshot.ShaderProfilerError.SUCCESS)
            {
                ShaderProfilerItem newItem = new ShaderProfilerItem();
                newItem.BuildShaderProfilerItem(data);
                _shaderProfilerItems.Add(newItem);
                _captureListView.Refresh();
            }
        }

        private VisualElement MakeProfilerListItem()
        {
            VisualElement rootContainer = new VisualElement();
            rootContainer.style.height = 20;
            rootContainer.style.flexDirection = FlexDirection.Row;
            Label itemNameLabel = new Label();
            rootContainer.Add(itemNameLabel);
            
            return rootContainer;
        }

        private void BuildProfilerListItem(VisualElement element, int i)
        {
            ((Label) element.ElementAt(0)).text = _shaderProfilerItems[i].itemName;
        }

        private void SetLeftPanelStyle(VisualElement element)
        {
            //set panel style
            element.style.flexGrow = 1;
            element.style.flexDirection = FlexDirection.Column;
            element.style.borderColor = Color.white;
            element.style.borderRightWidth = 4;
        }
        
        void DrawAttachToPlayerDropdown()
        {
            if (m_attachToPlayerCachedTargetName.text != m_PlayerConnectionState.connectionName)
            {
                m_attachToPlayerCachedTargetName.text = m_PlayerConnectionState.connectionName;
            }

            float width = EditorStyles.toolbarDropDown.CalcSize(m_attachToPlayerCachedTargetName).x;
            var rect = GUILayoutUtility.GetRect(width, ToolbarHeight);
#if UNITY_2020_1_OR_NEWER
            PlayerConnectionGUI.ConnectionTargetSelectionDropdown(rect, m_PlayerConnectionState, EditorStyles.toolbarDropDown);
#else
            ConnectionGUI.AttachToPlayerDropdown(rect, m_PlayerConnectionState, EditorStyles.toolbarDropDown);
#endif
        }

        private void SetMainPanelStyle(VisualElement element)
        {
            element.style.flexGrow = 6;
            element.style.flexDirection = FlexDirection.Column;
        }
        
        private VisualElement BuildShaderInfoTreeView()
        {
            VisualElement rootContainer = new VisualElement();
            SetMainPanelStyle(rootContainer);
            Toolbar toolbar = new Toolbar();
            toolbar.style.height = ToolbarHeight;
            
            _informationTitle = new Label();
            toolbar.Add(_informationTitle);
            rootContainer.Add(toolbar);
            if (_informationTreeView == null)
            {
                _informationTreeView = new ShaderProfilerTreeView(new TreeViewState());
            }
            IMGUIContainer infoTreeViewContainer = new IMGUIContainer(_informationTreeView.OnGUI);
            infoTreeViewContainer.style.flexDirection = FlexDirection.Column;
            infoTreeViewContainer.style.flexGrow = 1;
            rootContainer.Add(infoTreeViewContainer);
            
            
            return rootContainer;
        }

        private VisualElement BuildShaderCompareView()
        {
            VisualElement rootContainer = new VisualElement();
            SetMainPanelStyle(rootContainer);
            
            return rootContainer;
        }
    }
}

