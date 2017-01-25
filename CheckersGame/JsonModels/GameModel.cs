using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using CheckersGame.Models;

namespace CheckersGame.JsonModels
{
    [Serializable]
    public class GameModel
    {
        private const string SAVE_FILE = "game.json";
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public List<CellModel> Cells { get; set; }

        public GameModel()
        { }
        
        public GameModel(Game game)
        {
            Player1 = game.Player1;
            Player2 = game.Player2;
            Cells = GetCurrentCells();
        }

        private List<CellModel> GetCurrentCells()
        {
            var cells = GameManager.GetAllCells();
            List<CellModel> result = new List<CellModel>();

            foreach (var cell in cells)
            {
                result.Add(new CellModel(cell.CellPosition, cell.Owner, cell.Disabled));
            }

            return result;
        }

        public bool SerializeToFile()
        {
            bool success = false;

            try
            {
                using (StreamWriter file = File.CreateText(SAVE_FILE))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, this);
                    success = true;
                }
            }
            catch (Exception)
            { }

            return success && File.Exists(SAVE_FILE);  
        }

        public bool DeserializeFromFile()
        {
            GameModel gameModel;
            bool success = false;

            try
            {
                using (StreamReader file = File.OpenText(SAVE_FILE))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    gameModel = (GameModel)serializer.Deserialize(file, typeof(GameModel));
                    success = true;
                }

                Player1 = gameModel.Player1;
                Player2 = gameModel.Player2;
                Cells = gameModel.Cells;
            }
            catch (Exception)
            { }

            return success;     
        }
    }
}
