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
        public static void SaveGameFile(IXmlSerializable gameInfo, string playerName)
        {
            string dirPath = SAVES_PATH + playerName;
            Directory.CreateDirectory(dirPath);

            string savePath;
            int saveIndex = 1;
            do
            {
                savePath = dirPath + "/" + saveIndex.ToString() + ".sav";
                saveIndex++;
            } while (File.Exists(savePath));

            WriteXmlSerializable(gameInfo, savePath);
        }
        private static void WriteSerializable(ISerializable serializable, string filePath)
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream(filePath, FileMode.Create);
                IFormatter formatter = new SoapFormatter();
                formatter.Serialize(stream, serializable);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //TODO: remove file if error
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
    }
}
