using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bomberman.IO
{
    static class FileManager
    {
        const string MAPS_PATH = "../../../../Files/Maps/"; //TODO??
        public static string [][] LoadMapFile(string mapName)
        {
            string mapPath = MAPS_PATH + mapName + ".csv";
            var reader = new StreamReader(File.OpenRead(mapPath));

            List<string[]> lines = new List<string[]>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                lines.Add(values);
            }

            return lines.ToArray();
        }
    }
}
