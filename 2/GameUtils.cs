using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace _2
{
    class GameUtils
    {

        // game
        public static Game Game { get; set; }
        private const uint MAX_SCORE = 5;

        // words
        public enum Category
        {
            Cars,
            Mountains,
            Movies,
            Rivers,
            States
        }
        private static string WordsFilesPath { get; set; }
        private static List<string> Cars { get; set; }
        private static List<string> Mountains { get; set; }
        private static List<string> Movies { get; set; }
        private static List<string> Rivers { get; set; }
        private static List<string> States { get; set; }
        public static List<Category> SelectedCategories { get; private set; }

        // user
        public static User User { get; set; }
        public static uint Score { get; set; }

        // images
        private static List<BitmapImage> StageImages { get; set; }
        private static string ImagesPath { get; set; }

        public static bool Initialized { get; private set; }



        // initialize

        public static void Initialize()
        {
            //profile pics
            ImagesPath = "..\\..\\Resources\\Images\\GameStages";
            InitializeImages();

            // words
            WordsFilesPath = "..\\..\\Resources\\Words";
            InitializeWords();

            SelectedCategories = new List<Category>();

            Initialized = true;
        }

        private static void InitializeImages()
        {
            StageImages = new List<BitmapImage>();

            int maxStages = 7;

            for (int i = 0; i < maxStages; ++i)
            {
                StageImages.Add(new BitmapImage(new Uri(ImagesPath + $"\\stage{i}.png", UriKind.Relative)));
            }
        }

        private static void InitializeWords()
        {
            Cars = new List<string>();
            Mountains = new List<string>();
            Movies = new List<string>();
            Rivers = new List<string>();
            States = new List<string>();

            ReadList(Cars, WordsFilesPath + "\\cars.txt");
            ReadList(Mountains, WordsFilesPath + "\\mountains.txt");
            ReadList(Movies, WordsFilesPath + "\\movies.txt");
            ReadList(Rivers, WordsFilesPath + "\\rivers.txt");
            ReadList(States, WordsFilesPath + "\\states.txt");
        }

        private static void InitializeUser(User user)
        {
            User = user;
            Score = 0;
        }

        // words

        private static void ReadList(List<string> list, string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);

            foreach (string line in lines)
                list.Add(line.ToUpper());
        }

        public static void AddCategory(string category)
        {
            Category c = (Category)Enum.Parse(typeof(Category), category, true);

            if (!SelectedCategories.Contains(c))
                SelectedCategories.Add(c);
        }

        public static void RemoveCategory(string category)
        {
            Category c = (Category)Enum.Parse(typeof(Category), category, true);

            if (SelectedCategories.Contains(c))
                SelectedCategories.Remove(c);
        }

        public static void AddAllCategories()
        {
            foreach (Category c in Enum.GetValues(typeof(Category)))
            {
                if (!SelectedCategories.Contains(c))
                    SelectedCategories.Add(c);
            }
        }

        private static string GenerateWord()
        {
            Random rand = new Random();

            switch (SelectedCategories[rand.Next(0, SelectedCategories.Count)])
            {
                case Category.Cars:
                    {
                        return Cars[rand.Next(0, Cars.Count)];
                    }
                case Category.Mountains:
                    {
                        return Mountains[rand.Next(0, Mountains.Count)];
                    }
                case Category.Movies:
                    {
                        return Movies[rand.Next(0, Movies.Count)];
                    }
                case Category.Rivers:
                    {
                        return Rivers[rand.Next(0, Rivers.Count)];
                    }
                case Category.States:
                    {
                        return States[rand.Next(0, States.Count)];
                    }
            }

            return "ERROR";
        }


        // game

        public static void NewGame()
        {
            Game = new Game(GenerateWord());
        }

        public static bool OpenGame()
        {
            if (User.CurrentWord != "")
            {
                Game = new Game(User.CurrentWord, User.Attempts);
                return true;
            }
            return false;
        }

        public static void SaveGame()
        {
            if (Game != null)
            {
                User.CurrentWord = Game.Word;
                User.Attempts = Game.Attempts;
            }
        }

        public static BitmapImage GetStageImage()
        {
            return StageImages[Game.FailedAttempts];
        }

        public static Game.GameState GetGameState()
        {
            return Game.GetGameState();
        }

        public static void EndGame()
        {
            if (Game != null)
            {
                switch (Game.GetGameState())
                {
                    case Game.GameState.Won:
                        {
                            if (++Score == MAX_SCORE)
                            {
                                User.GamesWon++;
                                User.GamesPlayed++;
                                Score = 0;
                            }
                            break;
                        }
                    case Game.GameState.Ongoing:
                        {
                            User.GamesPlayed++;
                            break;
                        }

                    case Game.GameState.Lost:
                        {
                            Score = 0;
                            User.GamesPlayed++;
                            break;
                        }
                }

                Game = null;
            }
        }

    }
}
