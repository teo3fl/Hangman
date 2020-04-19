using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace _2
{
    class MenuUtils
    {
        public static Dictionary<string, User> Users { get; set; }
        private static string UserFilePath { get; set; }

        public static List<BitmapImage> Images { get; private set; }
        private static string ImagesFilePath { get; set; }
        public static int CurrentImageIndex { get; set; }

        public static bool Initialized { get; private set; }


        // initialize

        public static void Initialize()
        {
            //users
            UserFilePath = "..\\..\\Resources\\users.txt";
            InitializeUsers();

            //profile pics
            ImagesFilePath = "..\\..\\Resources\\Images\\Profile";
            InitializeImages();
            CurrentImageIndex = 0;

            Initialized = true;
        }

        private static void InitializeUsers()
        {
            Users = new Dictionary<string, User>();
            ReadUsers();
        }

        private static void InitializeImages()
        {
            Images = new List<BitmapImage>();

            string[] imageNames = { "abi", "dani_mocanu", "florin_salam", "sandu_ciorba", "satra_benz" };

            foreach (string name in imageNames)
            {
                Images.Add(new BitmapImage(new Uri(ImagesFilePath + $"\\{name}.jpg", UriKind.Relative)));
            }
        }


        // users

        private static void ReadUsers()
        {
            string[] lines = System.IO.File.ReadAllLines(UserFilePath);

            if (lines.Length > 0)
            {
                uint i = 0;
                while (i < lines.Length)
                {
                    string name = lines[i++];
                    int profilePicture = Int32.Parse(lines[i++]);
                    uint gamesPlayed = UInt32.Parse(lines[i++]);
                    uint gamesWon = UInt32.Parse(lines[i++]);
                    string currentWord = lines[i++];
                    string attempts = lines[i++];

                    Users[name] = (new User(name, profilePicture, gamesPlayed, gamesWon, currentWord, attempts));
                }

            }
        }

        public static void SaveUsers()
        {
            if (Users.Count > 0)
            {
                string userData = "";
                foreach (KeyValuePair<string, User> user in Users)
                {
                    userData += user.Value.ToString();
                }

                System.IO.File.WriteAllText(UserFilePath, userData);
            }
            else
            {
                System.IO.File.WriteAllText(UserFilePath, "");
            }
        }

        public static void AddUser(string name)
        {
            if (!Users.ContainsKey(name) && name!="")
                Users[name] = new User(name);
        }

        public static void DeleteUser(string name)
        {
            Users.Remove(name);
        }


        // images

        public static BitmapImage GetNextImage()
        {
            CurrentImageIndex = ++CurrentImageIndex % Images.Count;
            return Images[CurrentImageIndex];
        }

        public static BitmapImage GetPreviousImage()
        {
            CurrentImageIndex = (CurrentImageIndex + Images.Count - 1) % Images.Count;
            return Images[CurrentImageIndex];
        }

        public static BitmapImage GetUserImage(string name)
        {
            CurrentImageIndex = Users[name].ProfileImage;
            return Images[CurrentImageIndex];
        }

        public static void UpdateUserImage(string name)
        {
            Users[name].ProfileImage = CurrentImageIndex;
        }
    }
}
