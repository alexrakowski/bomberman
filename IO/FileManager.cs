using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
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
            if (!File.Exists(mapPath))
            {
                throw new BombermanException("Map File " + mapPath + " does not exist");
            }
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
        const string SAVED_GAMES_EXT = ".sav";
        public static void SaveGameFile(IXmlSerializable gameInfo, string playerName)
        {
            string dirPath = SAVES_PATH + playerName;
            Directory.CreateDirectory(dirPath);

            string savePath;
            int saveIndex = 1;
            do
            {
                // find first unused index for file name
                savePath = dirPath + "/" + saveIndex.ToString() + SAVED_GAMES_EXT;
                saveIndex++;
            } while (File.Exists(savePath));

            WriteXmlSerializable(gameInfo, savePath);
        }
        public static Object LoadGameFile(string playerName)
        {
            string dirPath = SAVES_PATH + playerName;
            string loadPath;
            
            int saveIndex = 0;
            do
            {
                // find last used index of file name
                saveIndex++;
                loadPath = dirPath + "/" + saveIndex.ToString() + SAVED_GAMES_EXT;
            } while (File.Exists(loadPath));

            loadPath = dirPath + "/" + (saveIndex - 1).ToString() + SAVED_GAMES_EXT;

            var result = ReadXmlSerializable(loadPath);

            return result;
        }

        private static void WriteXmlSerializable(IXmlSerializable xmlSerializable, string filePath)
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream(filePath, FileMode.Create);
                var serializer = new XmlSerializer(xmlSerializable.GetType());
                serializer.Serialize(stream, xmlSerializable);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //TODO: remove file if error
        }
        private static Object ReadXmlSerializable(string filePath)
        {
            Object obj;
            XmlSerializer serializer = new XmlSerializer(typeof(Bomberman.Game.Serialization.GameState));

            using (Stream reader = new FileStream(filePath, FileMode.Open))
            {
                obj = (Bomberman.Game.Serialization.GameState)serializer.Deserialize(reader);
            }

            return obj;
        }
    }
}
