using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using MvDependencyGrapher.RpgMv;

namespace MvDependencyGrapher
{
    class Program
    {
        static void Main(string[] args)
        {
            // Write program name, version, and copyright to console
            string appTitle = string.Format("{0} {1}", getAssemblyTitle(), getAssemblyVersion());
            Console.Title = appTitle;
            Console.WriteLine(appTitle);
            Console.WriteLine(getAssemblyCopyright());
            Console.WriteLine();

            if (args.Length == 0)
            {
                Console.WriteLine("No project path provided, exiting.");
                Environment.Exit(1);
            }

            // Check GraphViz
            string dotPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "gv", "dot.exe");
            if (!File.Exists(dotPath))
            {
                Console.Error.WriteLine("Cannot find GraphViz dot. Make sure you've extracted the tool files properly.");
                Environment.Exit(3);
            }

            string projectDir = Path.GetDirectoryName(args[0]);
            const string gvName = "dependency.gv";
            const string pngName = "dependency.png";

            // Load map infos (so we know which maps to load and their names)
            List<MapInfo> mapInfos = null;
            Console.WriteLine("Loading map infos");
            try
            {
                mapInfos = MapInfo.FromJson(File.ReadAllText(Path.Combine(projectDir, "data", "MapInfos.json")));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error loading map infos: {0}", ex.Message);
                Environment.Exit(2);
            }

            Dictionary<long, MapNode> nodes = new Dictionary<long, MapNode>();
            Dictionary<long, string> mapNames = new Dictionary<long, string>();
            foreach (var mapInfo in mapInfos)
            {
                if (mapInfo == null) continue;
                MapNode node = GetOrCreateNode(nodes, mapInfo.Id);
                mapNames.Add(mapInfo.Id, mapInfo.Name.Replace("\"", "\\\"")); // Escape quotes ahead of the time
                try
                {
                    Console.WriteLine("Processing map {0:d3}", mapInfo.Id);
                    // Load map
                    Map map = Map.FromJson(File.ReadAllText(Path.Combine(projectDir, "data", string.Format("Map{0:d3}.json", mapInfo.Id))));
                    // Enumerate all events and pages
                    foreach (var @event in map.Events)
                    {
                        if (@event == null) continue;
                        foreach (var page in @event.Pages)
                        {
                            // Find all transfer commands
                            foreach (var cmd in page.List.Where(c => c.Code == 201))
                            {
                                // Link nodes
                                var targetNode = GetOrCreateNode(nodes, (long)cmd.Parameters[1]);
                                targetNode.TransfersFromNodes.Add(node);
                                node.TransfersToNodes.Add(targetNode);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Error processing map {0:d3}: {1}", mapInfo.Id, ex.Message);
                    Environment.Exit(2);
                }
            }

            // Write the dot file
            try
            {
                Console.WriteLine("Writing graph file");
                using (StreamWriter sw = File.CreateText(Path.Combine(projectDir, gvName)))
                {
                    sw.WriteLine("digraph Dependency {");
                    foreach (var node in nodes.Values)
                    {
                        string nodeName = string.Format("{0:d3}: {1}", node.Id, mapNames[node.Id]);
                        if (node.TransfersToNodes.Count > 0)
                        {
                            // Connected nodes, don't need to specifically declare the node
                            foreach (var toNode in node.TransfersToNodes)
                            {
                                string toNodeName = string.Format("{0:d3}: {1}", toNode.Id, mapNames[toNode.Id]);
                                sw.WriteLine("\"{0}\" -> \"{1}\";", nodeName, toNodeName);
                            }
                        }
                        else
                        {
                            // No connections, need to declare node for it to show up on graph
                            sw.WriteLine("\"{0}\";", nodeName);
                        }
                        sw.WriteLine();
                    }
                    sw.WriteLine("}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error writing graph file: {0}", ex.Message);
                Environment.Exit(4);
            }

            // Generate graph PNG with dot
            Console.WriteLine("Generating image");
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = dotPath,
                    Arguments = string.Format("-Tpng -o \"{0}\" \"{1}\"", Path.Combine(projectDir, pngName), Path.Combine(projectDir, gvName)),
                    UseShellExecute = false
                }).WaitForExit();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Failed to generate graph image: {0}", ex.Message);
                Environment.Exit(5);
            }

            // Open the image
            try
            {
                Process.Start(Path.Combine(projectDir, "dependency.png"));
                Console.WriteLine("Opened image");
            }
            catch
            {
                Console.Error.WriteLine("Couldn't open image, please open {0} in your project folder manually.", pngName);
            }
        }

        static MapNode GetOrCreateNode(Dictionary<long, MapNode> nodes, long id)
        {
            if (!nodes.TryGetValue(id, out var node))
            {
                node = new MapNode { Id = id };
                nodes.Add(id, node);
            }
            return node;
        }

        static string getAssemblyTitle()
        {
            // http://stackoverflow.com/a/10203668
            var attribute = Assembly.GetExecutingAssembly()
                            .GetCustomAttributes(typeof(AssemblyTitleAttribute), false)
                            .Cast<AssemblyTitleAttribute>().FirstOrDefault();
            if (attribute != null)
            {
                return attribute.Title;
            }
            else
            {
                return "(Please add assembly title)";
            }
        }

        static string getAssemblyVersion()
        {
            return AssemblyName.GetAssemblyName(Assembly.GetExecutingAssembly().Location).Version.ToString();
        }

        static string getAssemblyCopyright()
        {
            var attribute = Assembly.GetExecutingAssembly()
                            .GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)
                            .Cast<AssemblyCopyrightAttribute>().FirstOrDefault();
            if (attribute != null)
            {
                return attribute.Copyright.Replace("©", "(C)");
            }
            else
            {
                return string.Format("Copyright (C) {0}", DateTime.Today.Year);
            }
        }

    }
}
