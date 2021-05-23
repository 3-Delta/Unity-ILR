using System;
using System.Collections.Generic;
using System.Text;

namespace UnityEditor.ShaderProfiler
{
    public static class ShaderProfilerUtils
    {
        public static string ReadString(byte[] stream, ref int offset)
        {
            int startPos = offset;
            while (stream[offset] != 0)
            {
                offset++;
            }
            string result = Encoding.ASCII.GetString(stream, startPos, offset - startPos); 
            offset += (4 - (offset % 4));

            return result;
        }

        public static uint ReadUInt(byte[] stream, ref int offset)
        {
            uint result = (uint)(stream[offset++] | (stream[offset++] << 8) | (stream[offset++] << 16) | (stream[offset++] << 24));
            return result;

        }

        public static ulong ReadUInt64(byte[] stream, ref int offset)
        {
            ulong result = (ulong)stream[offset++];
            result |= (ulong)stream[offset++] << 8;
            result |= (ulong)stream[offset++] << 16;
            result |= (ulong)stream[offset++] << 24;
            result |= (ulong)stream[offset++] << 32;
            result |= (ulong)stream[offset++] << 40;
            result |= (ulong)stream[offset++] << 48;
            result |= (ulong)stream[offset++] << 54;
            return result;
        }

        public static List<uint> ReadIntArray(byte[] stream, ref int offset)
        {
            List<uint> result = new List<uint>();
            return result;
        }
        
        public static string GetKeywords(uint[] keywordset, Dictionary<uint, Tuple<uint, string>> keywordDict)
        {
            string result = "";
            for (int setIndex = 0; setIndex < keywordset.Length; setIndex++)
            {
                if(keywordset[setIndex] == 0) continue;
                for (int bitIndex = 0; bitIndex < 32; bitIndex++)
                {
                    if (TestKeyword(bitIndex, (int) keywordset[setIndex]))
                    {
                        result += keywordDict[(uint)(bitIndex + setIndex * 32)].Item2;
                        result += " ";
                    }
                }
            }
            return result.Trim();
        }
        
        private static bool TestKeyword(int index, int bitMap)
        {
            return (bitMap & (1 << (index & (32 - 1)))) != 0;
        }
    }
}