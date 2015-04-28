using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Bomberman.Game.Map;

namespace Bomberman.Game
{
    partial class GamePanel : Element
    {
        enum InfoTypes
        {
            Score, Maps, Lifes, Time, Level
        }
        private static Dictionary<InfoTypes, Texture2D> Textures;
        private static Dictionary<InfoTypes, Vector2> Positions;
        private static Dictionary<InfoTypes, Vector2> FontPositions;
        private static Texture2D BackgroundTexture;
        private static SpriteFont Font;
        private static Color fontColor = Color.Black;

        public void Draw(SpriteBatch spriteBatch, GameInfo gameInfo)
        {
            DrawBackground(spriteBatch);
            foreach (var type in Enum.GetValues(typeof(InfoTypes)))
            {
                InfoTypes infoType = (InfoTypes)type;
                var text = GetInfoText(infoType, gameInfo);
                DrawInfo(spriteBatch, infoType, text);
            }
        }
        public override Texture2D GetTexture()
        {
            throw new NotImplementedException();
        }
        public override void LoadContent(ContentManager content)
        {
            GamePanel.Textures = new Dictionary<InfoTypes, Texture2D>();
            GamePanel.BackgroundTexture = content.Load<Texture2D>("textures/ui/game_panel");
            GamePanel.Textures[InfoTypes.Score] = content.Load<Texture2D>("textures/items/map_fragment");
            GamePanel.Textures[InfoTypes.Maps] = content.Load<Texture2D>("textures/items/map_fragment");
            GamePanel.Textures[InfoTypes.Lifes] = content.Load<Texture2D>("textures/items/map_fragment");
            GamePanel.Textures[InfoTypes.Time] = content.Load<Texture2D>("textures/items/map_fragment");

            GamePanel.Font = content.Load<SpriteFont>("fonts/TestFont");
        }
    }
    partial class GamePanel
    {
        private void DrawBackground(SpriteBatch spriteBatch)
        {
            var rectangle = new Rectangle(0, 0, GameConstants.SQUARE_WIDTH * GameConstants.BOARD_WIDTH, GameConstants.SQUARE_HEIGTH);
            spriteBatch.Draw(GamePanel.BackgroundTexture, rectangle, Color.AliceBlue);
        }
        private void DrawInfo(SpriteBatch spriteBatch, InfoTypes infoType, string text)
        {
            Vector2 imagePos, fontPos;
            if (Positions.TryGetValue(infoType, out imagePos))
            {
                var texture = GamePanel.Textures[infoType];
                spriteBatch.Draw(texture, imagePos, Color.White);
            }

            if (FontPositions.TryGetValue(infoType, out fontPos))
            {
                Vector2 FontOrigin = Font.MeasureString(text) / 2;
                spriteBatch.DrawString(Font, text, fontPos, fontColor,
                              0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            }
        }
        private string GetInfoText(InfoTypes infoType, GameInfo gameInfo)
        {
            switch (infoType)
            {
                case InfoTypes.Score:
                    return gameInfo.Score.ToString();
                case InfoTypes.Maps:
                    return gameInfo.foundFragments.ToString() + "/" + gameInfo.fragmentsToFind.ToString();
                case InfoTypes.Lifes:
                    return gameInfo.Lifes.ToString();
                case InfoTypes.Time:
                    string formattedTime = "";
                    int time = (int)gameInfo.Time;
                    return string.Format("{0}:{1:00}", time / 60, time % 60);
                case InfoTypes.Level:
                    return "Level: " + ((int)gameInfo.Level + 1);
                default:
                    throw new BombermanException("Unknown info type: " + infoType);
            }
        }

        private void DrawLevel(SpriteBatch spriteBatch, GameLevels level)
        {
            string text = "Poziom " + ((int)level + 1);
            Color fontColor = Color.Black;
            Vector2 fontPos = new Vector2(MapElement.WIDTH * 13, MapElement.HEIGHT / 2);
            Vector2 FontOrigin = Font.MeasureString(text) / 2;

            spriteBatch.DrawString(Font, text, fontPos, fontColor,
                          0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
        }
    }
    partial class GamePanel
    {
        private GamePanel()
        {
            Positions = new Dictionary<InfoTypes, Vector2>();
            FontPositions = new Dictionary<InfoTypes, Vector2>();

            Positions[InfoTypes.Score] = new Vector2(0, 0);
            FontPositions[InfoTypes.Score] = new Vector2(GameConstants.SQUARE_WIDTH * 1.8f, GameConstants.SQUARE_HEIGTH / 2);
            Positions[InfoTypes.Maps] = new Vector2(GameConstants.SQUARE_WIDTH * 3, 0);
            FontPositions[InfoTypes.Maps] = new Vector2(GameConstants.SQUARE_WIDTH * 4.5f, GameConstants.SQUARE_HEIGTH / 2);
            Positions[InfoTypes.Lifes] = new Vector2(GameConstants.SQUARE_WIDTH * 6, 0);
            FontPositions[InfoTypes.Lifes] = new Vector2(GameConstants.SQUARE_WIDTH * 7.5f, GameConstants.SQUARE_HEIGTH / 2);
            Positions[InfoTypes.Time] = new Vector2(GameConstants.SQUARE_WIDTH * 9, 0);
            FontPositions[InfoTypes.Time] = new Vector2(GameConstants.SQUARE_WIDTH * 11, GameConstants.SQUARE_HEIGTH / 2);

            FontPositions[InfoTypes.Level] = new Vector2(GameConstants.SQUARE_WIDTH * 13, GameConstants.SQUARE_HEIGTH / 2);
        }
        private static GamePanel instance;
        public static GamePanel GetInstance()
        {
            if (instance == null)
            {
                return instance = new GamePanel();
            }
            else
            {
                return instance;
            }
        }
    }
}
