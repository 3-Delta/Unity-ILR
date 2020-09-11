namespace UnityEngine.Rendering
{
    /// <summary>
    /// Bit depths of a Depth render texture.
    /// Some values may not be supported on all platforms.
    /// 深度纹理bit枚举
    /// </summary>
    public enum DepthBits
    {
        /// <summary>No Depth Buffer.</summary>
        None = 0,
        /// <summary>8 bits Depth Buffer.</summary>
        Depth8 = 8,
        /// <summary>16 bits Depth Buffer.</summary>
        Depth16 = 16,
        /// <summary>24 bits Depth Buffer.</summary>
        Depth24 = 24,
        /// <summary>32 bits Depth Buffer.</summary>
        Depth32 = 32
    }
}
