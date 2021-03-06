﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Bridge.Translator.Tests
{
    static class FileHelper
    {
        public static string CombineRelativePath(string absolutePath, string relativePath)
        {
            var di = new DirectoryInfo(absolutePath + relativePath);

            return di.FullName;
        }

        public static string GetRelativeToCurrentDirPath(string relativePath)
        {
            return FileHelper.CombineRelativePath(Directory.GetCurrentDirectory(), relativePath);
        }

        public static string GetRelativeToCurrentDirPath(params string[] relativePaths)
        {
            return FileHelper.GetRelativeToCurrentDirPath(string.Join("\\", relativePaths));
        }

        public static string ReadProjectOutputFolder(string configurationName, string projectFileFullName)
        {
            var doc = XDocument.Load(projectFileFullName, LoadOptions.SetLineInfo);

            var opnodes = from n in doc.Descendants() where n.Name.LocalName == "OutputPath" select n;
            var nodes = from n in doc.Descendants()
                        where n.Name.LocalName == "OutputPath" &&
                              n.Parent.Attribute("Condition").Value.Contains(configurationName)
                        select n;

            if (nodes.Count() != 1)
            {
                return null;
            }

            var path = nodes.First().Value;
            return path;
        }
    }
}
