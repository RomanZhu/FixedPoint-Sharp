using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace LUTGenerator {
    public static class Writer {
        public const string HEADER           = "namespace FixedPoint {\n    public static partial class fixlut {\n        public static readonly";
        public const string FOOTER           = "\n        };\n    }\n}";
        public const int    ENTRIES_PER_LINE = 10;

        public static string Output     = string.Empty;
        public static int    EntryIndex = 0;
        public static int    MaxEntryIndex;

        private static readonly Dictionary<Type, string> _friendlyNames = new Dictionary<Type, string> {
            {typeof(int), "int"},
            {typeof(uint), "uint"},
            {typeof(long), "long"},
            {typeof(ulong), "ulong"},
            {typeof(short), "short"},
            {typeof(ushort), "ushort"},
            {typeof(byte), "byte"},
            {typeof(sbyte), "sbyte"},
        };

        public static void Write(IList list, string fieldName) {
            EntryIndex    = 0;
            MaxEntryIndex = list.Count - 1;
            Output        = $"{HEADER} {_friendlyNames[list[0].GetType()]}[] {fieldName} = {{";

            foreach (var i in list) {
                AddEntry(i.ToString());
            }

            Output += FOOTER;

            File.WriteAllText($"{Directory.GetCurrentDirectory()}/{fieldName}.cs", Output);
        }

        public static void AddEntry(string entry) {
            if (EntryIndex % ENTRIES_PER_LINE == 0)
                Output += "\n            ";
            else
                Output += " ";

            Output += entry;

            if (EntryIndex != MaxEntryIndex)
                Output += ",";

            EntryIndex++;
        }
    }
}