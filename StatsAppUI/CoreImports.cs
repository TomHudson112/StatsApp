using System;
using System.Runtime.InteropServices;

namespace StatsAppUI
{
    internal sealed class CoreImports
    {
        /* Holds information used by DllImport attributes, and a container for the 
           actual attributes and the methods. */
        private const string _STATS_APP_DLL_PATH = "C:\\Dev C++\\StatsApp\\StatsApp\\Debug\\bin\\StatsAppCore.dll";

        [DllImport(_STATS_APP_DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Mean(int[] items, UInt32 length);

        [DllImport(_STATS_APP_DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Median(int[] items, UInt32 length);

        [DllImport(_STATS_APP_DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Mode(int[] items, UInt32 length);

        [DllImport(_STATS_APP_DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern double Range(int[] items, UInt32 length);

        [DllImport(_STATS_APP_DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern double UpperQuartile(int[] items, UInt32 length);

        [DllImport(_STATS_APP_DLL_PATH, CallingConvention = CallingConvention.Cdecl)]
        public static extern double LowerQuartile(int[] items, UInt32 length);
    }
}
