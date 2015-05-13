using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman
{
    /// <summary>
    /// Provides a set of public methods, 
    /// enabling communication between the main Game class,
    /// and managers, for example a Menu manager, etc.
    /// </summary>
    public interface IGame
    {
        /// <summary>
        /// Launches new game.
        /// </summary>
        void NewGame();
        /// <summary>
        /// Saves game
        /// </summary>
        void SaveGame();
        /// <summary>
        /// Load game.
        /// </summary>
        /// <param name="filename">
        /// Filename to load.
        /// </param>
        void LoadGame(string filename);
        /// <summary>
        /// Resumes the game.
        /// </summary>
        void ResumeGame();
        /// <summary>
        /// Shows saved game states.
        /// </summary>
        /// <returns>
        /// Array containing save names.
        /// </returns>
        string[] GetSavedGames();
        /// <summary>
        /// Loads map definition file.
        /// </summary>
        /// <param name="mapName">
        /// Name of the map to load.
        /// </param>
        /// <returns>
        /// Array of arrays representing map's cells.
        /// </returns>
        string[][] LoadMapFile(string mapName);
        /// <summary>
        /// Loads all high scores.
        /// </summary>
        /// <returns>
        /// Array of Tuples containing player name and his score.
        /// </returns>
        Tuple<string, int>[] GetHighScores();
        /// <summary>
        /// Updates high scores with the given score.
        /// </summary>
        /// <param name="playerName">
        /// Name of the player.
        /// </param>
        /// <param name="score">
        /// Player's score.
        /// </param>
        void UpdateHighScores(string playerName, int score);
        /// <summary>
        /// Changes setting's option.
        /// </summary>
        /// <param name="option">
        /// OptionType to change.
        /// </param>
        /// <returns>
        /// String name of the current option value.
        /// </returns>
        string ToggleOption(OptionType option);
        /// <summary>
        /// Returns current option name.
        /// </summary>
        /// <param name="option">
        /// OptionType to get name of the value.
        /// </param>
        /// <returns>
        /// String name of current option value.
        /// </returns>
        string GetOptionValue(OptionType option);
        /// <summary>
        /// Logs the player to the game.
        /// </summary>
        /// <param name="nickname">
        /// Player's name.
        /// </param>
        void Login(string nickname);
        /// <summary>
        /// Ends the game, and quits the program.
        /// </summary>
        void Quit();
    }
}
