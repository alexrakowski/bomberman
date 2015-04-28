using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Bomberman.IO
{
    static class FileManager
    {
        const string FILES_PREF = "../../../../Files";

        const string MAPS_PATH = FILES_PREF + "/Maps/";
        public static string[][] LoadMapFile(string mapName)
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

        const string SAVES_PATH = FILES_PREF + "/Saves/";
        public static void SaveGameFile(ISerializable gameInfo, string playerName)
        {
            string dirPath = SAVES_PATH + playerName;
            Directory.CreateDirectory(dirPath);

            string savePath;
            int saveIndex = 1;
            do
            {
                savePath = dirPath + "/" + saveIndex.ToString() + ".sav";
            } while (File.Exists(savePath));

            WriteSerializable(gameInfo, savePath);
        }
        private static void WriteSerializable(ISerializable serializable, string filePath)
        {
            System.IO.TextWriter writer = null;

            try
            {
                writer = new System.IO.StreamWriter(filePath);
                XmlSerializer serializer = new XmlSerializer(serializable.GetType());
                serializer.Serialize(writer, serializable);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
    }
}
