using CheckersGame.Models;
using CheckersGame.JsonModels;
using System.Windows;

namespace CheckersGame
{
    public partial class MainWindow : Window
    {
        public MainWindow(string player1, string player2)
        {
            InitializeComponent();
            GameManager.InitializeGame(this, player1, player2);
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Game game = GameManager.Game;

            if (!game.GameOver)
            {
                GameModel model = new GameModel(game);
                string msg = model.SerializeToFile() ? "Successfully saved" : "Error occured while saving";
                MessageBox.Show(msg);            
            }       
        }

        private void LoadMenuItem_Click(object sender, RoutedEventArgs e)
        {
            GameModel model = new GameModel();
            model.DeserializeFromFile();
            GameManager.Game.LoadNewGame(model);
        }

        private void RestartItem_Click(object sender, RoutedEventArgs e)
        {
            GameManager.Game.RestartGame();
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
