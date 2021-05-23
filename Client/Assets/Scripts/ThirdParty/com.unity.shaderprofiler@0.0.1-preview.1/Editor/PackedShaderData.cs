using System;

using System.Collections.Generic;
using UnityEngine;
using SPUtils = UnityEditor.ShaderProfiler.ShaderProfilerUtils;
namespace UnityEditor.ShaderProfiler
{
    public class PackedShaderData
    {
        public enum ProgramType
        {
            None,
            Vertex,
            Fragment,
            Geometry,
            Hull,
            Domain,
            Count
        }
        public class VariantInfo
        {
            // public uint[] KeywordSet;
            public bool IsWarmup;
            public string Keywords;

            public void FillVariantInfo(byte[] stream, ref int offset, Dictionary<uint, Tuple<uint, string>> keywordDict)
            {
                int keywordLen = (int)SPUtils.ReadUInt(stream, ref offset);
                uint[] keywordSet = new uint[keywordLen];
                for (int index = 0; index < keywordLen; index++)
                {
                    keywordSet[index] = SPUtils.ReadUInt(stream, ref offset);
                }

                Keywords = SPUtils.GetKeywords(keywordSet, keywordDict);
                // KeywordSet = ReadIntArray(stream, ref offset);
                IsWarmup = SPUtils.ReadUInt(stream, ref offset) == 1;
            }

            
        }
        
        public class ProgramInfo
        {
            public List<VariantInfo> VariatInfos;

            public void FillProgramInfo(byte[] stream, ref int offset, Dictionary<uint, Tuple<uint, string>> keywordDict)
            {
                int variantCount = (int)SPUtils.ReadUInt(stream, ref offset);
                VariatInfos = new List<VariantInfo>(variantCount);
                for(int index = 0; index < variantCount; index++)
                {
                    VariatInfos.Add(new VariantInfo());
                    VariatInfos[index].FillVariantInfo(stream, ref offset, keywordDict);
                }
            }
        }
        
        public class PassInfo
        {
            public bool IsValid;
            public string PassName;
            public Dictionary<ProgramType, ProgramInfo> ProgramInfos;

            public void FillPassInfo(byte[] stream, ref int offset, Dictionary<uint, Tuple<uint, string>> keywordDict)
            {
                IsValid = SPUtils.ReadUInt(stream, ref offset) == 1;
                PassName = SPUtils.ReadString(stream, ref offset);
                ProgramInfos = new Dictionary<ProgramType, ProgramInfo>();
                uint typeMask = SPUtils.ReadUInt(stream, ref offset);
                for (int typeIndex = 0; typeIndex < (int)ProgramType.Count; typeIndex++)
                {
                    if ((typeMask & (1 << typeIndex)) != 0)
                    {
                        ProgramInfos[(ProgramType)typeIndex] = new ProgramInfo();
                        ProgramInfos[(ProgramType)typeIndex].FillProgramInfo(stream, ref offset, keywordDict);
                    }
                }
            }
        }
        public class SubShaderInfo
        {
            public uint SubshaderLOD;
            public List<PassInfo> PassInfos;

            public void FillSubShaderInfo(byte[] stream, ref int offset, Dictionary<uint, Tuple<uint, string>> keywordDict)
            {
                SubshaderLOD = SPUtils.ReadUInt(stream, ref offset);
                int passCount = (int) SPUtils.ReadUInt(stream, ref offset);
                PassInfos = new List<PassInfo>(passCount);
                for(int index = 0; index < passCount; index++)
                {
                    PassInfos.Add(new PassInfo());
                    PassInfos[index].FillPassInfo(stream, ref offset, keywordDict);
                }
            }
        }
        
        public class ShaderInfo
        {
            public ulong MemorySize;
            public uint ActiveSubshaderIndex;
            public string ShaderName;
            public List<SubShaderInfo> SubshaderInfos;
            #if UNITY_2019_3_OR_NEWER
            public Dictionary<uint, Tuple<uint, string>> LocalShaderKeywordMap; 
            #endif

            public void FillShaderInfo(byte[] stream, ref int offset, Dictionary<uint, Tuple<uint, string>> keywordDict)
            {
#if UNITY_2019_3_OR_NEWER
                int keywordCount = (int) SPUtils.ReadUInt(stream, ref offset);
                
                for (int keywordIndex = 0; keywordIndex < keywordCount; keywordIndex++)
                {
                    uint shaderKeywordIndex = SPUtils.ReadUInt(stream, ref offset);
                    uint shaderKeywordType = SPUtils.ReadUInt(stream, ref offset);
                    string shaderKeywordName = SPUtils.ReadString(stream, ref offset) + "(L)";
                    keywordDict[shaderKeywordIndex] = new Tuple<uint, string>(shaderKeywordType, shaderKeywordName);
                }
#endif
                MemorySize = SPUtils.ReadUInt64(stream, ref offset);
                ActiveSubshaderIndex = SPUtils.ReadUInt(stream, ref offset);
                ShaderName = SPUtils.ReadString(stream, ref offset);
                int subshaderCount = (int)SPUtils.ReadUInt(stream, ref offset);
                SubshaderInfos = new List<SubShaderInfo>(subshaderCount);
                for(int index = 0; index < subshaderCount; index++)
                {
                    SubshaderInfos.Add(new SubShaderInfo());
                    SubshaderInfos[index].FillSubShaderInfo(stream, ref offset, keywordDict);
                }

            }
        }
        private Dictionary<uint, Tuple<uint, string>> _shaderKeywordMap = null;
        public List<ShaderInfo> ShaderInfoList;

        public void BuildPackedShaderData(byte[] stream)
        {
            int offset = 0;
            int keywordCount = (int) SPUtils.ReadUInt(stream, ref offset);
            _shaderKeywordMap = new Dictionary<uint, Tuple<uint, string>>(keywordCount);
            for (int keywordIndex = 0; keywordIndex < keywordCount; keywordIndex++)
            {
                uint shaderKeywordIndex = SPUtils.ReadUInt(stream, ref offset);
                uint shaderKeywordType = SPUtils.ReadUInt(stream, ref offset);
                string shaderKeywordName = SPUtils.ReadString(stream, ref offset);
                _shaderKeywordMap[shaderKeywordIndex] = new Tuple<uint, string>(shaderKeywordType, shaderKeywordName);
            }
            
            int shaderCount = (int)SPUtils.ReadUInt(stream, ref offset);
            ShaderInfoList = new List<ShaderInfo>(shaderCount);
            for (int shaderIndex = 0; shaderIndex < shaderCount; shaderIndex++)
            {
                ShaderInfoList.Add(new ShaderInfo());
                ShaderInfoList[shaderIndex].FillShaderInfo(stream, ref offset, _shaderKeywordMap);
            }
        }
    }
}