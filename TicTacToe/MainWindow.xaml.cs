using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        private MarkType[] nResults;
        private bool nPlayer1Turn;
        private bool nGameEnded;
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }
        #endregion

        private void NewGame()
        {
            // Create a new blank array of free cells
            nResults = new MarkType[9];

            // Setting all cells to be free
            for (var i = 0; i < nResults.Length; i++)
                nResults[i] = MarkType.Free;

            // Make sure P1 starts the game
            nPlayer1Turn = true;
            // Iterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                // Change background, foreground and content to defualt values
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            // Making sure game hasnt finished
            nGameEnded = false;






        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (nGameEnded)
            {
                NewGame();
                return;
            }

            //  Cast the sender toa  button
            var button = (Button)sender;

            // Find the buttons positions in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            // dont do anything if the cell already has a value in it
            if (nResults[index] != MarkType.Free)
                return;

            //Set the cell value based on player
            nResults[index] = nPlayer1Turn ? MarkType.Cross : MarkType.Nought;
            // set button text to the result
            button.Content = nPlayer1Turn ? "X" : "O";
            //changes noughts to green
            if (!nPlayer1Turn)
                button.Foreground = Brushes.Red;

            // Toggle the platers turns
            nPlayer1Turn ^= true;
            //check for a winner
            CheckForWinner();
        }

        private void CheckForWinner()
        {
            // Check for horizontal wins
            var same = (nResults[0] & nResults[1] & nResults[2] == nResults[0]);

            if (nResults[0] != MarkType.Free && same)
            {
                nGameEnded = true;
            }
        }
    }
}