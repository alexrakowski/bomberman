using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Xml.Serialization;

namespace Bomberman.IO
{
    /// <summary>
    /// Manager for Input Output File operations.
    /// Enables game saving/loading, Highscores adding/loading.
    /// Manages map files.
    /// </summary>
    public static class FileManager
    {
        const string FILES_PREF = "../../../../Files";

        #region Maps
        const string MAPS_PATH = FILES_PREF + "/Maps/";
        /// <summary>
        /// Loads file containing map represantation.
        /// </summary>
        /// <param name="mapName">
        /// Name of the map to load, without the file extension.
        /// Eg. "1" to load "1.csv".
        /// </param>
        /// <returns>
        /// Array of string arrays containing values for each map cell.
        /// </returns>
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
        #endregion

        #region Save/Load Games
        const string SAVES_PATH = FILES_PREF + "/Saves/";
        const string SAVED_GAMES_EXT = ".sav";
        /// <summary>
        /// Saves current game state.
        /// </summary>
        /// <param name="gameInfo">
        /// IXmlSerializable object, representing the game's state.
        /// </param>
        /// <param name="playerName">
        /// Name of the player.
        /// </param>
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
        /// <summary>
        /// Loads game from file.
        /// </summary>
        /// <param name="playerName">
        /// Player's name</param>
        /// <param name="filename">
        /// Name of the save to load (with extension).</param>
        /// <returns></returns>
        public static Object LoadGameFile(string playerName, string filename)
        {
            string dirPath = SAVES_PATH + playerName + "/";
            string loadPath = dirPath + filename;

            if (!File.Exists(loadPath))
            {
                throw new BombermanException("Could not find save " + filename);
            }

            var result = ReadXmlSerializable(loadPath);

            return result;
        }
        /// <summary>
        /// Loads saved games for the given player.
        /// </summary>
        /// <param name="playerName">
        /// Player's name</param>
        /// <returns>
        /// Array of Saves' filenames.
        /// </returns>
        public static string[] GetSavedGames(string playerName)
        {
            string dirPath = SAVES_PATH + playerName + "/";
            var saves = Directory.GetFiles(dirPath, "*" + SAVED_GAMES_EXT);

            for (int i = 0; i < saves.Length; ++i)
            {
                var save = saves[i];
                var split = save.Split('/');
                saves[i] = split[split.Length - 1];
            }

            return saves;
        }
        #endregion

        #region Serialization
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
        #endregion

        #region HighScores
        const string HIGH_SCORES_DIR_PATH = FILES_PREF + "/HighScores/";
        const string HIGH_SCORES_FILE_PATH = HIGH_SCORES_DIR_PATH + "scores";
        const char HIGH_SCORES_DELIMITER = ':';
        const int NO_HIGH_SCORES = 10;

        /// <summary>
        /// Returns array of high scores.
        /// </summary>
        /// <returns>
        /// Array of tuples containg High Scores with players' names.
        /// </returns>
        public static Tuple<string, int>[] GetHighScores()
        {
            InitHighScoresFile();
            var lines = File.ReadAllLines(HIGH_SCORES_FILE_PATH);
            Tuple<string, int>[] scores = new Tuple<string, int>[NO_HIGH_SCORES];

            for (int i = 0; i < NO_HIGH_SCORES; ++i)
            {
                var name = lines[i].Split(HIGH_SCORES_DELIMITER)[0];
                var score = GetScoreFromLine(lines[i]);
                scores[i] = new Tuple<string, int>(name, score);
            }

            return scores;
        }
        private static void InitHighScoresFile()
        {
            if (!Directory.Exists(HIGH_SCORES_DIR_PATH))
            {
                Directory.CreateDirectory(HIGH_SCORES_DIR_PATH);
            }
            if (!File.Exists(HIGH_SCORES_FILE_PATH))
            {
                string[] lines = new string[NO_HIGH_SCORES];

                for (int i = 0; i < NO_HIGH_SCORES; ++i)
                {
                    lines[i] = "";
                }
                File.AppendAllLines(HIGH_SCORES_FILE_PATH, lines);
            }
        }
        /// <summary>
        /// Use this to add a new high score.
        /// This method automatically checks if the score is in the Top scores.
        /// If not, the score will not be added.
        /// </summary>
        /// <param name="name">
        /// Player's name
        /// </param>
        /// <param name="score">
        /// Player's score
        /// </param>
        public static void AddHighScore(string name, int score)
        {
            InitHighScoresFile();
            AddScore(name, score);
        }
        private static void AddScore(string name, int score)
        {
            int scorePosition = -1;
            var lines = File.ReadAllLines(HIGH_SCORES_FILE_PATH);

            // check if the score is in the top 10
            for (int i = 0; i < NO_HIGH_SCORES; ++i)
            {
                int lineScore = GetScoreFromLine(lines[i]);
                if (lineScore < score)
                {
                    scorePosition = i;
                }
            }
            if (scorePosition != -1)
            {
                // the score is in top 10

                // move the weaker scores down the list
                for (int i = NO_HIGH_SCORES - 1; i > scorePosition; --i)
                {
                    lines[i] = lines[i - 1];
                }
                lines[scorePosition] = name + HIGH_SCORES_DELIMITER + score.ToString() + Environment.NewLine;
            }
            File.WriteAllLines(HIGH_SCORES_FILE_PATH, lines);
        }
        private static int GetScoreFromLine(string line)
        {
            if (line.Contains(HIGH_SCORES_DELIMITER.ToString()))
            {
                var split = line.Split(HIGH_SCORES_DELIMITER);
                return Int32.Parse(split[1]);
            }
            else
            {
                return 0;
            }
        }
        #endregion
    }
}
