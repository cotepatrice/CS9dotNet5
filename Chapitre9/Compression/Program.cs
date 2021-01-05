using System;
using System.IO;
using System.IO.Compression;
using System.Xml;

namespace Compression
{
    class Program
    {

        static string[] callsigns = new string[] 
        { 
            "Husker", "Starbuck", "Apollo", "Boomer", 
            "Bulldog", "Athena", "Helo", "Racetrack" 
        };

        static void Main(string[] args)
        {
            WorkWithCompression();
            WorkWithCompression(true);
        }

        static void WorkWithCompression(bool useBrotli = false) 
        { 
            string fileExt = useBrotli ? "brotli" : "gzip";
            // compress the XML output
            string filePath = Path.Combine(Environment.CurrentDirectory, $"streams.{fileExt}");
            FileStream file = File.Create( filePath);
            Stream compressor;
            if (useBrotli)
            {
                compressor = new BrotliStream(file, CompressionMode.Compress);
            }
            else
            {
                compressor = new GZipStream(file, CompressionMode.Compress);
            }
            
            using (compressor)
            {
                using (XmlWriter xml = XmlWriter.Create(compressor))
                {
                    xml.WriteStartDocument(); 
                    xml.WriteStartElement("callsigns");
                    foreach (string item in callsigns)
                    {
                        xml.WriteElementString("callsign", item);
                    }
                }
            } // also closes the underlying stream
            
            // output all the contents of the compressed file
            Console.WriteLine($"{filePath} contains {new FileInfo(filePath).Length} bytes.");
            Console.WriteLine(File.ReadAllText( filePath)); // read a compressed file
            Console.WriteLine($"Reading the {fileExt} compressed XML file:");
            file = File.Open(filePath, FileMode.Open);
            Stream decompressor;
            if (useBrotli)
            {
                decompressor = new BrotliStream(file, CompressionMode.Decompress);
            }
            else
            {
                decompressor = new GZipStream(file, CompressionMode.Decompress);
            }
            
            using (decompressor)
            {
                using (XmlReader reader = XmlReader.Create(decompressor))
                {
                    while (reader.Read())
                    {
                        // check if we are on an element node named callsign
                        if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "callsign"))
                        {
                            reader.Read(); // move to the text inside element
                            Console.WriteLine($"{reader.Value}"); // read its value
                        }
                    }
                }
            }
        }
    }
}
