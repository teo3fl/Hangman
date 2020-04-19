using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _2
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private List<TextBlock> MistakeBoxes { get; set; }
        private Dictionary<char, Button> LetterButtons { get; set; }
        private List<CheckBox> CategoryButtons { get; set; }

        public GameWindow()
        {
            InitializeComponent();

            if (!GameUtils.Initialized)
                GameUtils.Initialize();

            grid_game.Visibility = Visibility.Collapsed;
            grid_statistics.Visibility = Visibility.Collapsed;

            list_statistics.ItemsSource = MenuUtils.Users;

            InitializeLists();
        }

        // initialize

        private void InitializeLists()
        {
            MistakeBoxes = new List<TextBlock>();

            MistakeBoxes.Add(txt_mistake1);
            MistakeBoxes.Add(txt_mistake2);
            MistakeBoxes.Add(txt_mistake3);
            MistakeBoxes.Add(txt_mistake4);
            MistakeBoxes.Add(txt_mistake5);
            MistakeBoxes.Add(txt_mistake6);

            LetterButtons = new Dictionary<char, Button>();

            LetterButtons['A'] = btn_a;
            LetterButtons['B'] = btn_b;
            LetterButtons['C'] = btn_c;
            LetterButtons['D'] = btn_d;
            LetterButtons['E'] = btn_e;
            LetterButtons['F'] = btn_f;
            LetterButtons['G'] = btn_g;
            LetterButtons['H'] = btn_h;
            LetterButtons['I'] = btn_i;
            LetterButtons['J'] = btn_j;
            LetterButtons['K'] = btn_k;
            LetterButtons['L'] = btn_l;
            LetterButtons['M'] = btn_m;
            LetterButtons['N'] = btn_n;
            LetterButtons['O'] = btn_o;
            LetterButtons['P'] = btn_p;
            LetterButtons['Q'] = btn_q;
            LetterButtons['R'] = btn_r;
            LetterButtons['S'] = btn_s;
            LetterButtons['T'] = btn_t;
            LetterButtons['U'] = btn_u;
            LetterButtons['V'] = btn_v;
            LetterButtons['X'] = btn_x;
            LetterButtons['Y'] = btn_y;
            LetterButtons['Z'] = btn_z;
            LetterButtons['W'] = btn_w;

            CategoryButtons = new List<CheckBox>();

            CategoryButtons.Add(checkBox_cars);
            CategoryButtons.Add(checkBox_mountains);
            CategoryButtons.Add(checkBox_movies);
            CategoryButtons.Add(checkBox_rivers);
            CategoryButtons.Add(checkBox_states);
        }

        private void InitializeGameScreen()
        {
            LoadUserData();
            ResetLetterButtons();
            ResetMistakeBoxes();
            UpdateScreen();
        }

        // menu file

        private void new_game_Click(object sender, RoutedEventArgs e)
        {
            if (GameUtils.SelectedCategories.Count > 0)
            {
                ShowGameGrid();

                GameUtils.NewGame();

                InitializeGameScreen();
            }
            else
            {
                MessageBox.Show("No category was selected.", "Game", MessageBoxButton.OK);
            }
        }

        private void open_game_Click(object sender, RoutedEventArgs e)
        {

            if (GameUtils.OpenGame())
            {
                ShowGameGrid();
                InitializeGameScreen();
            }
            else
            {
                MessageBox.Show("This player has no saved game.", "Game", MessageBoxButton.OK);
            }
        }

        private void save_game_Click(object sender, RoutedEventArgs e)
        {
            GameUtils.SaveGame();
        }

        private void statistics_Click(object sender, RoutedEventArgs e)
        {
            ShowStatisticsGrid();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            GameUtils.EndGame();

            Window window = new MainWindow();
            window.Show();

            Close();
        }

        // menu categories

        private void all_categories_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;

            if(checkbox.IsChecked==true)
            {
                GameUtils.AddAllCategories();
                foreach(CheckBox c in CategoryButtons)
                {
                    c.IsChecked = true;
                }
            }
        }

        private void category_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;

            if (checkbox.IsChecked == true)
            {
                GameUtils.AddCategory(checkbox.Content.ToString());
            }
            else
            {
                GameUtils.RemoveCategory(checkbox.Content.ToString());
                if (checkBox_all_categories.IsChecked == true)
                    checkBox_all_categories.IsChecked = false;
            }
        }

        // menu help

        private void menu_help_Click(object sender, RoutedEventArgs e)
        {
            string message =
                "Student: ProfirescuTeodora\n" +
                "Specializare: INFO\n" +
                "Grupa: 283";

            string caption = "About";

            MessageBox.Show(message, caption, MessageBoxButton.OK);
        }


        // game grid

        private void Letter_Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            GameUtils.Game.TryLetter(button.Content.ToString()[0]);

            UpdateScreen();

            CheckGameState();

            button.IsEnabled = false;
        }


        // game

        private void UpdateScreen()
        {
            img_stage.Source = GameUtils.GetStageImage();
            UpdateMistakeBoxes();
            txt_displayed_word.Text = GameUtils.Game.GetDisplayedText();
        }

        private void ResetMistakeBoxes()
        {
            foreach (TextBlock box in MistakeBoxes)
                box.Text = "";
        }

        private void UpdateMistakeBoxes()
        {
            int failedAttempts = GameUtils.Game.FailedAttempts;

            if (failedAttempts>0)
            {
                for (int i = 0; i < failedAttempts; ++i)
                    MistakeBoxes[i].Text = "X";
            }
        }

        private void LoadUserData()
        {
            img_user.Source = MenuUtils.Images[GameUtils.User.ProfileImage];
            txt_user_data.Text = $"Player: {GameUtils.User.Name}\nScore: {GameUtils.Score}";
        }

        private void ResetLetterButtons()
        {
            string attempts = GameUtils.Game.Attempts;
            if (attempts == "")
            {
                foreach (KeyValuePair<char, Button> button in LetterButtons)
                {
                    button.Value.IsEnabled = true;
                }
            }
            else
            {
                foreach (KeyValuePair<char, Button> button in LetterButtons)
                {
                    if (attempts.Contains(button.Key))
                        button.Value.IsEnabled = false;
                    else
                        button.Value.IsEnabled = true;

                }
            }
        }

        private void DisableAllLetterButtons()
        {
            foreach (KeyValuePair<char, Button> button in LetterButtons)
            {
                button.Value.IsEnabled = false;
            }
        }
       
        private void CheckGameState()
        {
            switch (GameUtils.GetGameState())
            {
                case Game.GameState.Won:
                    {
                        MessageBox.Show("You won.", "Game state", MessageBoxButton.OK);
                        DisableAllLetterButtons();
                        GameUtils.EndGame();
                        break;
                    }
                case Game.GameState.Lost:
                    {
                        MessageBox.Show($"You lost.\nWord: {GameUtils.Game.Word}.", "Game state", MessageBoxButton.OK);
                        DisableAllLetterButtons();
                        GameUtils.EndGame();
                        break;
                    }
            }
        }

        private void ShowGameGrid()
        {
            if (grid_statistics.IsVisible)
                grid_statistics.Visibility = Visibility.Collapsed;

            if (!grid_game.IsVisible)
                grid_game.Visibility = Visibility.Visible;
        }
        
        
        // statistics

        private void ShowStatisticsGrid()
        {
            if (grid_game.IsVisible)
                grid_game.Visibility = Visibility.Collapsed;

            if (!grid_statistics.IsVisible)
                grid_statistics.Visibility = Visibility.Visible;
        }
    }
}
