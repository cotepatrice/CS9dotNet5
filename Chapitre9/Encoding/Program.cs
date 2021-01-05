using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml;
using static System.Console;

namespace EncodingSamples
{
    class Program
    {
        static string[] callsigns = new string[] 
        { 
            "Husker", "Starbuck", "Apollo", "Boomer €", 
            "Bulldog", "Athena", "Helo", "Racetrack" 
        };

        static void Main(string[] args)
        {
            WriteLine("Choose compression type"); // choose a compression type 
            WriteLine("[1] GZip"); 
            WriteLine("[2] Brotli");
            Write("Press a number to choose a compression algorythm: "); 
            ConsoleKey compressNumber = ReadKey(intercept: false).Key; 
            bool useBrotli = compressNumber == ConsoleKey.D2; 
            
            WriteLine(); 
            WriteLine(); 
            
            WriteLine("Encodings"); 
            WriteLine("[1] ASCII"); 
            WriteLine("[2] UTF-8"); 
            WriteLine("[3] UTF-16 (Unicode)"); 
            WriteLine("[4] UTF-32");
            WriteLine("[5] BigEndianUnicode"); 
            WriteLine("[any other key] Default"); // choose an encoding 
            Write("Press a number to choose an encoding: "); 
            ConsoleKey number = ReadKey(intercept: false).Key; 
            
            WriteLine(); 
            WriteLine(); 
            
            Encoding encoder = number switch 
            { 
                ConsoleKey.D1 => Encoding.ASCII,  
                ConsoleKey.D2 => Encoding.UTF8, 
                ConsoleKey.D3 => Encoding.Unicode, 
                ConsoleKey.D4 => Encoding.UTF32, 
                ConsoleKey.D5 => Encoding.BigEndianUnicode,
                _ => Encoding.Default 
            }; 
            
            // define a string to encode 
            string message = "A pint of milk is £ 1.99"; // encode the string into a byte array 
            var encoded = encoder.GetBytes(message); // check how many bytes the encoding needed 
            WriteLine($"{encoder.GetType().Name} uses {encoded.Length} bytes.");

            // enumerate each byte 
            WriteLine( $"BYTE HEX CHAR"); 
            foreach (byte b in encoded) 
            {
                WriteLine($"{b, 4} {b.ToString("X"), 4} {(char) b, 5}"); 
            } 
            
            // decode the byte array back into a string and display it 
            var decoded = encoder.GetString(encoded); 
            WriteLine( decoded);

            WorkWithCompression(encoder, useBrotli);
        }

        static void WorkWithCompression(Encoding encoding, bool useBrotli = false) 
        { 
            var fileExt = useBrotli ? "brotli" : "gzip";
            var filePath = Path.Combine(Environment.CurrentDirectory, $"streams.xml.{fileExt}");
            var file = File.Create(filePath);

            // Compress with encoding
            CompressFile(file, filePath, useBrotli, encoding);

            // Decompress with encoding
            DecompressFile(file, filePath, useBrotli, encoding);
        }

        private static void CompressFile(FileStream file, string filePath, bool useBrotli, Encoding encoding)
        {
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
                var settings = new XmlWriterSettings() 
                {
                    Encoding = encoding,
                    Indent = true,
                    CloseOutput = true
                }; 

                using (var xml = XmlWriter.Create(compressor, settings))
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
            Console.WriteLine(File.ReadAllText(filePath)); // read a compressed file
        }

        private static void DecompressFile(FileStream file, string filePath, bool useBrotli, Encoding encoding)
        {
            Console.WriteLine($"Reading the {Path.GetExtension(filePath)} compressed XML file:");
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
                using (var reader = XmlReader.Create(decompressor))
                {
                    if(encoding == Encoding.Unicode || encoding == Encoding.UTF8 || encoding == Encoding.ASCII) 
                    {
                        Console.OutputEncoding = encoding;
                    }
                    else
                    {
                        Console.WriteLine($"{encoding.EncodingName} is not supported by console. Display may not be what's stored in the file.");
                    }

                    while (reader.Read())
                    {
                        // check if we are on an element node named callsign
                        if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "callsign"))
                        {
                            reader.Read(); // move to the text inside element
                            Console.WriteLine($"{reader.Value}"); // read its value
                        }
                        
                    }
                    Console.OutputEncoding = Encoding.Default;
                }
            }
        }
    }
}
