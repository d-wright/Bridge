﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Bridge.Contract;

namespace Bridge.Builder
{
    class Program
    {
        static void LogMessage(string level, string message)
        {
            level = level ?? "message";
            switch (level.ToLowerInvariant())
            {
                case "message":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Message: {0}", message);
                    Console.ResetColor();
                    break;
                case "warning":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Warning: {0}", message);
                    Console.ResetColor();
                    break;
                case "error":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: {0}", message);
                    Console.ResetColor();
                    break;
            }
        }

        static void Main(string[] args)
        {
            string projectLocation = null;
            string outputLocation = null;
            string bridgeLocation = null;
            bool rebuild = false;
            bool extractCore = true;
            string cfg = null;
            string source = null;
            string folder = Environment.CurrentDirectory;
            bool recursive = false;
            string lib = null;
            string def = null;

            if (args.Length == 0)
            {
                Console.WriteLine("Bridge.Builder commands:");
                Console.WriteLine("-p or -project           Path to csproj file (required)");
                Console.WriteLine("-o or -output            Output directory for generated script");
                Console.WriteLine("-cfg or -configuration   Configuration name, typically Debug/Release");
                Console.WriteLine("-r or -rebuild           Force assembly rebuilding");
                Console.WriteLine("-nocore                  Do not extract core javascript files");
                Console.WriteLine("-def or -define          Defines project constants. For example, \"CONSTANT1;CONSTANT2\" ");
#if DEBUG
                // This code and logic is only compiled in when building bridge.net in Debug configuration
                Console.WriteLine("-d or -debug             Attach the builder to an visual studio debugging instance.");
                Console.WriteLine("                         Use this to attach the process to an open Bridge.NET solution.");
                Console.WriteLine("                         This option is equivalent to Build.dll's 'AttachDebugger'.");
#endif
                Console.WriteLine("");
                return;
            }

            int i = 0;

            while (i < args.Length)
            {
                switch (args[i])
                {
                    case "-p":
                    case "-project":
                        projectLocation = args[++i];
                        break;
                    case "-b":
                    case "-bridge":
                        bridgeLocation = args[++i];
                        break;
                    case "-o":
                    case "-output":
                        outputLocation = args[++i];
                        break;
                    case "-cfg":
                    case "-configuration":
                        cfg = args[++i];
                        break;
                    case "-def":
                    case "-define":
                        def = args[++i];
                        break;
                    case "-rebuild":
                    case "-r":
                        rebuild = true;
                        break;
                    case "-nocore":
                        extractCore = false;
                        break;
                    case "-src":
                        source = args[++i];
                        break;
                    case "-folder":
                        folder = Path.Combine(Environment.CurrentDirectory, args[++i]);
                        break;
                    case "-recursive":
                        recursive = true;
                        break;
                    case "-lib":
                        lib = args[++i];
                        break;
#if DEBUG
                    case "-debug":
                    case "-attachdebugger":
                    case "-d":
                        System.Diagnostics.Debugger.Launch();
                        break;
#endif
                    default:
                        Console.WriteLine("Unknown command: " + args[i]);
                        return;
                }

                i++;
            }

            if (string.IsNullOrEmpty(outputLocation))
            {
                outputLocation = !string.IsNullOrWhiteSpace(projectLocation) ? Path.GetFileNameWithoutExtension(projectLocation) : folder;
            }

            Bridge.Translator.Translator translator = null;
            try
            {
                Console.WriteLine("Generating script...");

                if (!string.IsNullOrWhiteSpace(projectLocation))
                {
                    translator = new Bridge.Translator.Translator(projectLocation);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(lib))
                    {
                        throw new Exception("Please define path to assembly using -lib option");
                    }

                    lib = Path.Combine(folder, lib);
                    translator = new Bridge.Translator.Translator(folder, source, recursive, lib);
                }

                bridgeLocation = !string.IsNullOrEmpty(bridgeLocation) ? bridgeLocation : Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Bridge.dll");

                translator.BridgeLocation = bridgeLocation;
                translator.Rebuild = rebuild;
                translator.Log = LogMessage;
                translator.Configuration = cfg;
                if (def != null)
                {
                    translator.DefineConstants.AddRange(def.Split(';').Select(s => s.Trim()).Where(s => s != ""));
                    translator.DefineConstants = translator.DefineConstants.Distinct().ToList();
                }
                translator.Translate();

                string path = string.IsNullOrWhiteSpace(Path.GetFileName(outputLocation)) ? outputLocation : Path.GetDirectoryName(outputLocation);
                string outputPath = null;

                if (!string.IsNullOrWhiteSpace(translator.AssemblyInfo.Output))
                {
                    outputPath = Path.Combine(!string.IsNullOrWhiteSpace(projectLocation) ? Path.GetDirectoryName(projectLocation) : folder, translator.AssemblyInfo.Output);
                }
                else
                {
                    outputPath = Path.Combine(!string.IsNullOrWhiteSpace(projectLocation) ? Path.GetDirectoryName(projectLocation) : folder, !string.IsNullOrWhiteSpace(translator.AssemblyInfo.Output) ? translator.AssemblyInfo.Output : path);
                }

                if (translator.AssemblyInfo != null && translator.AssemblyInfo.CleanOutputFolderBeforeBuild)
                {
                    Console.WriteLine("Cleaning output folder before extracting scripts...");
                    CleanDirectory(outputPath);
                }

                if (extractCore)
                {
                    Console.WriteLine("Extracting core scripts...");
                    translator.ExtractCore(outputPath);
                }

                Console.WriteLine("Saving to " + outputPath);
                translator.SaveTo(outputPath, Path.GetFileName(outputLocation));
                translator.Flush(outputPath, Path.GetFileName(outputLocation));
                translator.Plugins.AfterOutput(translator, outputPath, !extractCore);

                Console.WriteLine("Done.");
            }
            catch (EmitterException e)
            {
                Console.WriteLine("Error: {2} ({3}, {4}) {0} {1}", e.Message, e.StackTrace, e.FileName, e.StartLine + 1, e.StartColumn + 1, e.EndLine + 1, e.EndColumn + 1);
            }
            catch (Exception e)
            {
                var ee = translator != null ? translator.CreateExceptionFromLastNode() : null;
                Console.ForegroundColor = ConsoleColor.Red;

                if (ee != null)
                {
                    Console.WriteLine("Error: {2} ({3}, {4}) {0} {1}", e.Message, e.StackTrace, ee.FileName, ee.StartLine + 1, ee.StartColumn + 1, ee.EndLine + 1, ee.EndColumn + 1);
                }
                else
                {
                    // Iteractively print inner exceptions
                    var ine = e;
                    var elvl = 0;
                    while (ine != null)
                    {
                        Console.WriteLine("Error: exception level: {0} - {1}\nStack trace:\n{2}", elvl++, ine.Message, ine.StackTrace);
                        ine = ine.InnerException;
                    }
                }

                Console.ResetColor();
                Console.ReadLine();
            }
        }

        private static void CleanDirectory(string outputPath)
        {
            var outputDirectory = new DirectoryInfo(outputPath);
            if (outputDirectory.Exists)
            {
                foreach (var file in outputDirectory.GetFiles())
                {
                    file.Delete();
                }
                foreach (var dir in outputDirectory.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
        }
    }
}
