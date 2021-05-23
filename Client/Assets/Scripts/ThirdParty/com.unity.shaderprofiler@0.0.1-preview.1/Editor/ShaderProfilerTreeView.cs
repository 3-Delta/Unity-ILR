using UnityEditor.IMGUI.Controls;
using UnityEditorInternal;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace UnityEditor.ShaderProfiler
{
    public class ShaderProfilerTreeView : TreeView
    {
        // private MultiColumnHeader m_MultiColumnHeader;
        // ShaderProfileInfo[] m_CurrentProfilerInfos;
        // ShaderProfileGlobalInfo m_CurrentGlobalInfo;

        System.Random rand = new System.Random();
        
        private int GetItemId()
        {
            return rand.Next();
        }

        public PackedShaderData CurrentPackedShaderData;

        public ShaderProfilerTreeView(TreeViewState state) : base(state)
        {
            Reload();
        }

        protected override TreeViewItem BuildRoot()
        {
            TreeViewItem tvi = new TreeViewItem(0,-1);
            if(CurrentPackedShaderData == null)
            {
                tvi.children = new List<TreeViewItem>();
            }
            else
            {
                tvi.children = GetChildren(CurrentPackedShaderData.ShaderInfoList, 0);
            }
            
            return tvi;
        }

        private List<TreeViewItem> GetChildren(List<PackedShaderData.ShaderInfo> shaderInfos, int level)
        {
            List<TreeViewItem> result = new List<TreeViewItem>();
            foreach(PackedShaderData.ShaderInfo currentInfo in shaderInfos)
            {
                TreeViewItem newItem = new TreeViewItem();
                newItem.id = GetItemId();
                newItem.depth = level;
                newItem.displayName = currentInfo.ShaderName + "(Subshader : " + currentInfo.SubshaderInfos.Count + ")";
                newItem.children = GetChildren(currentInfo.SubshaderInfos, currentInfo.ActiveSubshaderIndex,level + 1);
                result.Add(newItem);
            }
            return result;
        }

        private List<TreeViewItem> GetChildren(List<PackedShaderData.SubShaderInfo> subShaderInfos, uint activeIndex,int level)
        {
            List<TreeViewItem> result = new List<TreeViewItem>();
            foreach(PackedShaderData.SubShaderInfo currentInfo in subShaderInfos)
            {
                TreeViewItem newItem = new TreeViewItem();
                newItem.id = GetItemId();
                newItem.depth = level;
                newItem.displayName = "Subshader("+ (result.Count == activeIndex ? " active " : "") +" LOD: " + currentInfo.SubshaderLOD + " Passes: " + currentInfo.PassInfos.Count + ")";
                newItem.children = GetChildren(currentInfo.PassInfos, level + 1);
                result.Add(newItem);
            }
            return result;
        }

        private List<TreeViewItem> GetChildren(List<PackedShaderData.PassInfo> passInfos, int level)
        {
            List<TreeViewItem> result = new List<TreeViewItem>();
            foreach(PackedShaderData.PassInfo currentInfo in passInfos)
            {
                TreeViewItem newItem = new TreeViewItem();
                newItem.id = GetItemId();
                newItem.depth = level;
                newItem.displayName = currentInfo.PassName + "(" + (currentInfo.IsValid ? "Valid" : "Invalid")  + " Programs: " +currentInfo.ProgramInfos.Count + ")";
                newItem.children = GetChildren(currentInfo.ProgramInfos, level + 1);
                result.Add(newItem);
            }
            return result;
        }

        private List<TreeViewItem> GetChildren(Dictionary<PackedShaderData.ProgramType, PackedShaderData.ProgramInfo> programInfos, int level)
        {
            List<TreeViewItem> result = new List<TreeViewItem>();
            foreach(PackedShaderData.ProgramType currentInfo in programInfos.Keys)
            {
                TreeViewItem newItem = new TreeViewItem();
                newItem.id = GetItemId();
                newItem.depth = level;
                newItem.displayName = Enum.GetName(typeof(PackedShaderData.ProgramType), currentInfo) + "(Variants: " + programInfos[currentInfo].VariatInfos.Count + ")";
                newItem.children = GetChildren(programInfos[currentInfo].VariatInfos, level + 1);
                result.Add(newItem);
            }
            return result;
        }

        private List<TreeViewItem> GetChildren(List<PackedShaderData.VariantInfo> variantInfos, int level)
        {
            List<TreeViewItem> result = new List<TreeViewItem>();
            foreach(PackedShaderData.VariantInfo currentInfo in variantInfos)
            {
                TreeViewItem newItem = new TreeViewItem();
                newItem.id = GetItemId();
                newItem.depth = level;
                newItem.displayName = (currentInfo.IsWarmup ? "Warmuped ":"") + currentInfo.Keywords;
                
                result.Add(newItem);
            }
            return result;
        }

        public void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            var treeRect = EditorGUILayout.GetControlRect(GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            OnGUI(treeRect);
            
            EditorGUILayout.EndVertical();
        }
    }
}