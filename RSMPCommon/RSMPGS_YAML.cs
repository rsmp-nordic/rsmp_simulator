using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace nsRSMPGS
{

    public class cYAMLParser
    {

        public static cYAMLMapping GetYAMLMappings(List<string> sLines)
        {
            int iLineIndex = 0;

            return GetYAMLMappings(sLines, ref iLineIndex, null, "", -1);
        }

        private static cYAMLMapping GetYAMLMappings(List<string> sLines, ref int iLineIndex, cYAMLMapping Parent, string sMappingName, int iParentIndention)
        {

            string sKey;
            string sValue;

            cYAMLMapping YAMLMapping = new cYAMLMapping();

            YAMLMapping.sMappingName = sMappingName;
            YAMLMapping.Parent = Parent;

            while (iLineIndex < sLines.Count)
            {

                if (sLines[iLineIndex].Contains("Detector forced on/off"))
                {

                }

                // Comments?
                if (UseFul.StringLeft(sLines[iLineIndex], 2) == " #")
                {
                    iLineIndex++;
                    continue;
                }

                int iLineIndention = IndentionLength(sLines[iLineIndex]);

                // Back to previous level
                if (iLineIndention < iParentIndention)
                {
                    break;
                }

                // New section on same level?
                if (iLineIndention == iParentIndention && sLines[iLineIndex].IndexOf(":") == sLines[iLineIndex].Length - 1 && sLines[iLineIndex].Length > 0)
                {
                    break;
                }

                // New scalar on same level?
                if (iLineIndention == iParentIndention && sLines[iLineIndex].IndexOf(": ") > 0)
                {
                    break;
                }

                if (sLines[iLineIndex].IndexOf(": ") >= 0)
                {

                    sKey = UseFul.StringLeft(sLines[iLineIndex], sLines[iLineIndex].IndexOf(": ")).Trim(new char[] { ' ', '\'', '\"' });
                    sValue = UseFul.StringMid(sLines[iLineIndex], sLines[iLineIndex].IndexOf(": ") + 2).Trim(new char[] { ' ', '\'', '\"' });
                    //        description: Traffic Light Controller is in fail safe mode; e.g. yellow flash
                    //          or dark mode

                    bool bPreserveLFs = false;
                    bool bFoldLFs = false;
                    bool bAddEndingLF = false;
                    bool bTextIsQuoted = false;
                    if (UseFul.StringLeft(sValue, 1) == "|")
                    {
                        bPreserveLFs = true;
                    }
                    else if (UseFul.StringLeft(sValue, 1) == ">")
                    {
                        bFoldLFs = true;
                    }
                    if (bPreserveLFs || bFoldLFs)
                    {
                        bAddEndingLF = UseFul.StringMid(sValue, 1, 1) == "-" ? false : true;
                        sValue = "";
                        if (iLineIndex < sLines.Count - 1)
                        {
                            bTextIsQuoted = sLines[iLineIndex + 1].StartsWith("\"") || sLines[iLineIndex + 1].StartsWith("'");
                        }
                    }
                    else
                    {
                        bTextIsQuoted = sValue.StartsWith("\"") || sValue.StartsWith("'");
                    }
                    if (bTextIsQuoted)
                    {

                    }
                    while (iLineIndex < sLines.Count - 1)
                    {
                        // Buggish YAML could contain empty line in text flow?
                        if (iLineIndention >= IndentionLength(sLines[iLineIndex + 1]) && sLines[iLineIndex + 1].Length > 0)
                        {
                            break;
                        }
                        if (bTextIsQuoted == false)
                        {
                            if (sLines[iLineIndex + 1].IndexOf(": ") >= 0)
                            {
                                //  break;
                            }
                        }
                        else
                        {

                        }

                        iLineIndex++;
                        if (sLines[iLineIndex].Trim() == "")
                        {
                            sValue += "\n";
                        }
                        else
                        {
                            if (UseFul.StringRight(sValue, 1) != "\n")
                            {
                                sValue += " ";
                            }
                            sValue += sLines[iLineIndex].Trim();
                            if (bPreserveLFs)
                            {
                                sValue += "\n";
                            }
                        }
                    }
                    sValue = sValue.Trim(new char[] { '\"', ' ', '\n', '\'' });
                    if (bAddEndingLF)
                    {
                        sValue += "\n";
                    }
                    if (YAMLMapping.YAMLScalars.ContainsKey(sKey) == false)
                    {
                        YAMLMapping.YAMLScalars.Add(sKey, sValue);
                    }
                    else
                    {
                        Debug.WriteLine("Key already exists in section: " + YAMLMapping.GetFullPath() + ": " + sLines[iLineIndex].Trim());
                    }
                    iLineIndex++;
                }
                else if (sLines[iLineIndex].IndexOf(":") == sLines[iLineIndex].Length - 1 && sLines[iLineIndex].Length > 0)
                {
                    string sChildSectionName = UseFul.StringLeft(sLines[iLineIndex], sLines[iLineIndex].IndexOf(":")).Trim();
                    iLineIndex++;
                    cYAMLMapping YAMLChildSection = GetYAMLMappings(sLines, ref iLineIndex, YAMLMapping, sChildSectionName, iLineIndention);
                    if (YAMLMapping.YAMLMappings.ContainsKey(sChildSectionName) == false)
                    {
                        YAMLMapping.YAMLMappings.Add(sChildSectionName, YAMLChildSection);
                    }
                    else
                    {
                        Debug.WriteLine("Key already exists in section: " + YAMLMapping.GetFullPath() + ": " + sLines[iLineIndex].Trim());
                    }
                }
                else if (iLineIndention == 0 && sLines[iLineIndex] == "---")
                {
                    iLineIndex++;
                }
                else
                {
                    Debug.WriteLine("Invalid line: " + iLineIndex.ToString() + ": " + sLines[iLineIndex].Trim());
                    iLineIndex++;
                }

            }

            return YAMLMapping;

        }

        public static int GetKeyAndValueAndReturnIndentionLength(string sString, out string sKey, out string sValue)
        {
            if (sString.IndexOf(":") >= 0)
            {
                sKey = UseFul.StringLeft(sString, sString.IndexOf(":")).Trim();
                sValue = UseFul.StringMid(sString, sString.IndexOf(":") + 1).Trim();
            }
            else
            {
                sKey = sString.Trim();
                sValue = "";
            }

            return IndentionLength(sString);

        }

        public static int IndentionLength(string sString)
        {

            for (int iIndentionLength = 0; iIndentionLength < sString.Length; iIndentionLength++)
            {
                if (sString.Substring(iIndentionLength, 1) != " ")
                {
                    return iIndentionLength;
                }
            }

            return sString.Length;

        }


    }

    public class cYAMLMapping
    {

        public string sMappingName;
        public cYAMLMapping Parent = null;

        public Dictionary<string, string> YAMLScalars = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        public Dictionary<string, cYAMLMapping> YAMLMappings = new Dictionary<string, cYAMLMapping>(StringComparer.OrdinalIgnoreCase);

        public string GetScalar(string sKey)
        {
            string sValue;

            YAMLScalars.TryGetValue(sKey, out sValue);

            if (sValue == null)
            {
                sValue = "";
            }

            return sValue.Trim();
        }

        public string GetFullPath()
        {

            string sPath = sMappingName;

            cYAMLMapping pParent;

            for (pParent = Parent; pParent != null; pParent = pParent.Parent)
            {
                sPath = pParent.sMappingName + "\\" + sPath;
            }
            return sPath;

        }
    }

}