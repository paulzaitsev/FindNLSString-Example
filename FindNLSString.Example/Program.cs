using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FindNLSString.Example {
    [StructLayout(LayoutKind.Sequential)]
    public struct NLSVERSIONINFO {
        /// DWORD->int
        public int dwNLSVersionInfoSize;
        /// DWORD->int
        public int dwNLSVersion;
        /// DWORD->int
        public int dwDefinedVersion;
        /// DWORD->int
        public int dwEffectiveId;
        /// GUID->_GUID
        public Guid guidCustomVersion;
    }
    public class Program {
        [DllImport("kernel32.dll", EntryPoint = "FindNLSStringEx")]
        public static extern int FindNLSStringEx(
            [In][MarshalAs(UnmanagedType.LPWStr)] string lpLocaleName,
            int dwFindNLSStringFlags,
            [In][MarshalAs(UnmanagedType.LPWStr)] string lpStringSource,
            int cchSource,
            [In][MarshalAs(UnmanagedType.LPWStr)] string lpStringValue,
            int cchValue,
            ref int pcchFound,
            IntPtr lpVersionInformation,
            IntPtr lpReserved,
            IntPtr sortHandle);

        static void Main(string[] args) {
            #region Flags
            const string LOCALE_NAME_USER_DEFAULT = null; 
            const string LOCALE_NAME_INVARIANT  = "";
            const string LOCALE_NAME_SYSTEM_DEFAULT  = "!x-sys-default-locale";

            const int FIND_STARTSWITH = 0x00100000; // see if value is at the beginning of source
            const int FIND_ENDSWITH = 0x00200000; // see if value is at the end of source
            const int FIND_FROMSTART = 0x00400000; // look for value in source, starting at the beginning
            const int FIND_FROMEND = 0x00800000; // look for value in source, starting at the end

            const int NORM_IGNORECASE = 0x00000001; // ignore case
            const int NORM_IGNORENONSPACE = 0x00000002; // ignore nonspacing chars
            const int NORM_IGNORESYMBOLS = 0x00000004; // ignore symbols

            const int LINGUISTIC_IGNORECASE = 0x00000010; // linguistically appropriate 'ignore case'
            const int LINGUISTIC_IGNOREDIACRITIC = 0x00000020; // linguistically appropriate 'ignore nonspace'

            const int NORM_IGNOREKANATYPE = 0x00010000; // ignore kanatype
            const int NORM_IGNOREWIDTH = 0x00020000; // ignore width
            const int NORM_LINGUISTIC_CASING = 0x08000000; // use linguistic rules for casing

            const long ERROR_CAN_NOT_COMPLETE = 1003L;
            const long ERROR_INVALID_FLAGS = 1004L;
            const long ERROR_UNRECOGNIZED_VOLUME = 1005L;
            #endregion

            string source = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed porta ligula gravida nibh faucibus tincidunt.";
            string pattern = "ligula";
            int pcchFound = -1;
            int result = FindNLSStringEx(LOCALE_NAME_INVARIANT, FIND_FROMSTART, source, source.Length, pattern, pattern.Length, ref pcchFound, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            Console.WriteLine("Source:" + source);
            Console.WriteLine("Pattern:" + pattern);
            Console.WriteLine("Search result: {0}", result);
            Console.ReadKey();
        }
    }
}