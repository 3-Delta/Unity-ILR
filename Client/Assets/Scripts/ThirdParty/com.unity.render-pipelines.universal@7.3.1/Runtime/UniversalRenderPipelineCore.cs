using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine.Scripting.APIUpdating;

using UnityEngine.Experimental.GlobalIllumination;
using Lightmapping = UnityEngine.Experimental.GlobalIllumination.Lightmapping;

namespace UnityEngine.Rendering.Universal
{
    // 混合光照设置
    [MovedFrom("UnityEngine.Rendering.LWRP")] public enum MixedLightingSetup
    {
        None,
        ShadowMask,
        Subtractive,
    };

    // 渲染数据
    [MovedFrom("UnityEngine.Rendering.LWRP")] public struct RenderingData
    {
        public CullingResults cullResults;
        public CameraData cameraData;
        public LightData lightData;
        public ShadowData shadowData;
        public PostProcessingData postProcessingData;
        
        // 动态批处理
        public bool supportsDynamicBatching;
        public PerObjectData perObjectData;

        /// <summary>
        /// True if post-processing effect is enabled while rendering the camera stack.
        /// 后处理是否开启
        /// </summary>
        public bool postProcessingEnabled;
        
        // 处理最终的renderTarget
        internal bool resolveFinalTarget;
    }

    // 光照数据
    [MovedFrom("UnityEngine.Rendering.LWRP")] public struct LightData
    {
        // 主光源下表
        public int mainLightIndex;
        // 附加光源个数
        public int additionalLightsCount;
        // 最大的 逐对象附加光源个数
        public int maxPerObjectAdditionalLightsCount;
        // 可视化光源
        public NativeArray<VisibleLight> visibleLights;
        // 附加光源 逐顶点渲染 
        public bool shadeAdditionalLightsPerVertex;
        
        // 是否支持混合光照
        public bool supportsMixedLighting;
    }

    // 相机数据
    [MovedFrom("UnityEngine.Rendering.LWRP")] public struct CameraData
    {
        public Camera camera;
        public CameraRenderType renderType;
        public RenderTexture targetTexture;
        public RenderTextureDescriptor cameraTargetDescriptor;
        
        // Internal camera data as we are not yet sure how to expose View in stereo context.
        // We might change this API soon.
        // worldToCameraMatrix
        internal Matrix4x4 viewMatrix;
        internal Matrix4x4 projectionMatrix;
        
        internal Rect pixelRect;
        internal int pixelWidth;
        internal int pixelHeight;
        internal float aspectRatio;
        public float renderScale;
        public bool clearDepth;
        public bool isSceneViewCamera;
        public bool isDefaultViewport;
        public bool isHdrEnabled;
        public bool requiresDepthTexture;
        public bool requiresOpaqueTexture;

        public SortingCriteria defaultOpaqueSortFlags;

        public bool isStereoEnabled;
        internal int numberOfXRPasses;
        internal bool isXRMultipass;

        public float maxShadowDistance;
        public bool postProcessEnabled;

#if POST_PROCESSING_STACK_2_0_0_OR_NEWER
        [Obsolete("The use of the Post-processing Stack V2 is deprecated in the Universal Render Pipeline. Use the builtin post-processing effects instead.")]
        public UnityEngine.Rendering.PostProcessing.PostProcessLayer postProcessLayer;
#endif

        public IEnumerator<Action<RenderTargetIdentifier, CommandBuffer>> captureActions;

        // 这两个不知道什么用处，测试下来是后处理用的！
        public LayerMask volumeLayerMask;
        public Transform volumeTrigger;

        public bool isStopNaNEnabled;
        public bool isDitheringEnabled;
        public AntialiasingMode antialiasing;
        public AntialiasingQuality antialiasingQuality;
        internal ScriptableRenderer renderer;
    }

    // 阴影数据
    [MovedFrom("UnityEngine.Rendering.LWRP")] public struct ShadowData
    {
        public bool supportsMainLightShadows;
        public bool requiresScreenSpaceShadowResolve;
        public int mainLightShadowmapWidth;
        public int mainLightShadowmapHeight;
        public int mainLightShadowCascadesCount;
        public Vector3 mainLightShadowCascadesSplit;
        public bool supportsAdditionalLightShadows;
        public int additionalLightsShadowmapWidth;
        public int additionalLightsShadowmapHeight;
        public bool supportsSoftShadows;
        public int shadowmapDepthBufferBits;
        public List<Vector4> bias;
    }

    // 后处理数据
    public struct PostProcessingData
    {
        // LDR/HDR
        public ColorGradingMode gradingMode;
        public int lutSize;
    }

    // 相机depth排序
    class CameraDataComparer : IComparer<Camera>
    {
        public int Compare(Camera lhs, Camera rhs)
        {
            return (int)lhs.depth - (int)rhs.depth;
        }
    }

    public static class ShaderKeywordStrings
    {
        public static readonly string MainLightShadows = "_MAIN_LIGHT_SHADOWS";
        public static readonly string MainLightShadowCascades = "_MAIN_LIGHT_SHADOWS_CASCADE";
        public static readonly string AdditionalLightsVertex = "_ADDITIONAL_LIGHTS_VERTEX";
        public static readonly string AdditionalLightsPixel = "_ADDITIONAL_LIGHTS";
        public static readonly string AdditionalLightShadows = "_ADDITIONAL_LIGHT_SHADOWS";
        public static readonly string SoftShadows = "_SHADOWS_SOFT";
        public static readonly string MixedLightingSubtractive = "_MIXED_LIGHTING_SUBTRACTIVE";

        public static readonly string DepthNoMsaa = "_DEPTH_NO_MSAA";
        public static readonly string DepthMsaa2 = "_DEPTH_MSAA_2";
        public static readonly string DepthMsaa4 = "_DEPTH_MSAA_4";

        public static readonly string LinearToSRGBConversion = "_LINEAR_TO_SRGB_CONVERSION";
        [Obsolete("The _KILL_ALPHA shader keyword is deprecated in the Universal Render Pipeline.")]
        public static readonly string KillAlpha = "_KILL_ALPHA";

        public static readonly string SmaaLow = "_SMAA_PRESET_LOW";
        public static readonly string SmaaMedium = "_SMAA_PRESET_MEDIUM";
        public static readonly string SmaaHigh = "_SMAA_PRESET_HIGH";
        public static readonly string PaniniGeneric = "_GENERIC";
        public static readonly string PaniniUnitDistance = "_UNIT_DISTANCE";
        public static readonly string BloomLQ = "_BLOOM_LQ";
        public static readonly string BloomHQ = "_BLOOM_HQ";
        public static readonly string BloomLQDirt = "_BLOOM_LQ_DIRT";
        public static readonly string BloomHQDirt = "_BLOOM_HQ_DIRT";
        public static readonly string UseRGBM = "_USE_RGBM";
        public static readonly string Distortion = "_DISTORTION";
        public static readonly string ChromaticAberration = "_CHROMATIC_ABERRATION";
        public static readonly string HDRGrading = "_HDR_GRADING";
        public static readonly string TonemapACES = "_TONEMAP_ACES";
        public static readonly string TonemapNeutral = "_TONEMAP_NEUTRAL";
        public static readonly string FilmGrain = "_FILM_GRAIN";
        public static readonly string Fxaa = "_FXAA";
        public static readonly string Dithering = "_DITHERING";

        public static readonly string HighQualitySampling = "_HIGH_QUALITY_SAMPLING";
    }

    public sealed partial class UniversalRenderPipeline
    {
        // 阴影偏移, 前两位是 shadow 深度/法线 偏移
        static List<Vector4> m_ShadowBiasData = new List<Vector4>();

        /// <summary>
        /// Checks if a camera is a game camera.
        /// </summary>
        /// <param name="camera">Camera to check state from.</param>
        /// <returns>true if given camera is a game camera, false otherwise.</returns>
        public static bool IsGameCamera(Camera camera)
        {
            if (camera == null)
                throw new ArgumentNullException("camera");

            return camera.cameraType == CameraType.Game || camera.cameraType == CameraType.VR;
        }

        /// <summary>
        /// Checks if a camera is rendering in stereo mode.
        /// 立体相机,双眼模式
        /// </summary>
        /// <param name="camera">Camera to check state from.</param>
        /// <returns>Returns true if the given camera is rendering in stereo mode, false otherwise.</returns>
        public static bool IsStereoEnabled(Camera camera)
        {
            if (camera == null)
                throw new ArgumentNullException("camera");

            bool isGameCamera = IsGameCamera(camera);
            bool isCompatWithXRDimension = true;
#if ENABLE_VR && ENABLE_VR_MODULE
            isCompatWithXRDimension &= (camera.targetTexture ? camera.targetTexture.dimension == UnityEngine.XR.XRSettings.deviceEyeTextureDimension : true);
#endif
            return XRGraphics.enabled && isGameCamera && (camera.stereoTargetEye == StereoTargetEyeMask.Both) && isCompatWithXRDimension;
        }

        /// <summary>
        /// Returns the current render pipeline asset for the current quality setting.
        /// If no render pipeline asset is assigned in QualitySettings, then returns the one assigned in GraphicsSettings.
        /// </summary>
        public static UniversalRenderPipelineAsset asset
        {
            get => GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;
        }

        /// <summary>
        /// Checks if a camera is rendering in MultiPass stereo mode.
        /// </summary>
        /// <param name="camera">Camera to check state from.</param>
        /// <returns>Returns true if the given camera is rendering in multi pass stereo mode, false otherwise.</returns>
        static bool IsMultiPassStereoEnabled(Camera camera)
        {
            if (camera == null)
                throw new ArgumentNullException("camera");

            return false;
        }

        // 相机深度排序
        void SortCameras(Camera[] cameras)
        {
            if (cameras.Length <= 1)
                return;
            Array.Sort(cameras, new CameraDataComparer());
        }

        // 大致根据camera创建RenderTextureDescriptor
        static RenderTextureDescriptor CreateRenderTextureDescriptor(Camera camera, float renderScale, bool isStereoEnabled, bool isHdrEnabled, int msaaSamples, bool needsAlpha)
        {
            RenderTextureDescriptor desc;
            RenderTextureFormat renderTextureFormatDefault = RenderTextureFormat.Default;

            // NB: There's a weird case about XR and render texture
            // In test framework currently we render stereo tests to target texture
            // The descriptor in that case needs to be initialized from XR eyeTexture not render texture
            // Otherwise current tests will fail. Check: Do we need to update the test images instead?
            if (isStereoEnabled)
            {
                desc = XRGraphics.eyeTextureDesc;
                renderTextureFormatDefault = desc.colorFormat;
            }
            else if (camera.targetTexture == null)
            {
                desc = new RenderTextureDescriptor(camera.pixelWidth, camera.pixelHeight);
                desc.width = (int)((float)desc.width * renderScale);
                desc.height = (int)((float)desc.height * renderScale);
            }
            else
            {
                desc = camera.targetTexture.descriptor;
            }

            if (camera.targetTexture != null)
            {
                desc.colorFormat = camera.targetTexture.descriptor.colorFormat;
                desc.depthBufferBits = camera.targetTexture.descriptor.depthBufferBits;
                desc.msaaSamples = camera.targetTexture.descriptor.msaaSamples;
                desc.sRGB = camera.targetTexture.descriptor.sRGB;
            }
            else
            {
                bool use32BitHDR = !needsAlpha && RenderingUtils.SupportsRenderTextureFormat(RenderTextureFormat.RGB111110Float);
                RenderTextureFormat hdrFormat = (use32BitHDR) ? RenderTextureFormat.RGB111110Float : RenderTextureFormat.DefaultHDR;
            
                desc.colorFormat = isHdrEnabled ? hdrFormat : renderTextureFormatDefault;
                desc.depthBufferBits = 32;
                desc.msaaSamples = msaaSamples;
                // 如果是线性空间,就属于SRGB???
                desc.sRGB = (QualitySettings.activeColorSpace == ColorSpace.Linear);
            }

            desc.enableRandomWrite = false;
            desc.bindMS = false;
            desc.useDynamicScale = camera.allowDynamicResolution;
            return desc;
        }

        static Lightmapping.RequestLightsDelegate lightsDelegate = (Light[] requests, NativeArray<LightDataGI> lightsOutput) =>
        {
            // Editor only.
#if UNITY_EDITOR
            LightDataGI lightData = new LightDataGI();

            for (int i = 0; i < requests.Length; i++)
            {
                Light light = requests[i];
                switch (light.type)
                {
                    case LightType.Directional:
                        DirectionalLight directionalLight = new DirectionalLight();
                        LightmapperUtils.Extract(light, ref directionalLight);
                        lightData.Init(ref directionalLight);
                        break;
                    case LightType.Point:
                        PointLight pointLight = new PointLight();
                        LightmapperUtils.Extract(light, ref pointLight);
                        lightData.Init(ref pointLight);
                        break;
                    case LightType.Spot:
                        SpotLight spotLight = new SpotLight();
                        LightmapperUtils.Extract(light, ref spotLight);
                        spotLight.innerConeAngle = light.innerSpotAngle * Mathf.Deg2Rad;
                        spotLight.angularFalloff = AngularFalloffType.AnalyticAndInnerAngle;
                        lightData.Init(ref spotLight);
                        break;
                    case LightType.Area:
                        RectangleLight rectangleLight = new RectangleLight();
                        LightmapperUtils.Extract(light, ref rectangleLight);
                        rectangleLight.mode = LightMode.Baked;
                        lightData.Init(ref rectangleLight);
                        break;
                    case LightType.Disc:
                        DiscLight discLight = new DiscLight();
                        LightmapperUtils.Extract(light, ref discLight);
                        discLight.mode = LightMode.Baked;
                        lightData.Init(ref discLight);
                        break;
                    default:
                        lightData.InitNoBake(light.GetInstanceID());
                        break;
                }

                lightData.falloff = FalloffType.InverseSquared;
                lightsOutput[i] = lightData;
            }
#else
            LightDataGI lightData = new LightDataGI();

            for (int i = 0; i < requests.Length; i++)
            {
                Light light = requests[i];
                lightData.InitNoBake(light.GetInstanceID());
                lightsOutput[i] = lightData;
            }
            Debug.LogWarning("Realtime GI is not supported in Universal Pipeline.");
#endif
        };
    }

    internal enum URPProfileId
    {
        StopNaNs,
        SMAA,
        GaussianDepthOfField,
        BokehDepthOfField,
        MotionBlur,
        PaniniProjection,
        UberPostProcess,
        Bloom,
    }
}
