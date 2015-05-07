using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman
{
    
    public interface IGame
    {
        void NewGame();
        void SaveGame();
        void LoadGame(string filename);
        void ResumeGame();
        string[] GetSavedGames();
        string[][] LoadMapFile(string mapName);
        Tuple<string, int>[] GetHighScores();
        void UpdateHighScores(string playerName, int score);
        string ToggleOption(OptionType option);
        string GetOptionValue(OptionType option);
        void Login(string nickname);
        void Quit();
    }
}
