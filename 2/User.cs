using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2
{
    class User
    {
        public string Name { get; private set; }
        public int ProfileImage { get; set; }

        public uint GamesPlayed { get; set; }
        public uint GamesWon { get; set; }

        public string CurrentWord { get; set; }
        public string Attempts { get; set; }

        public User(string name)
        {
            Name = name;
            ProfileImage = 0;
            GamesPlayed = 0;
            GamesWon = 0;
            CurrentWord = "";
            Attempts = "";
        }

        public User(string name, int profileImage, uint gamesPlayed, uint gamesWon, string currentWord, string attempts)
        {
            Name = name;
            ProfileImage = profileImage;
            GamesPlayed = gamesPlayed;
            GamesWon = gamesWon;
            CurrentWord = currentWord;
            Attempts = attempts;
        }

        public override string ToString()
        {
            return
                $"{Name}\n" +
                $"{ProfileImage}\n" +
                $"{GamesPlayed}\n" +
                $"{GamesWon}\n" +
                $"{CurrentWord}\n" +
                $"{Attempts}\n";
        }
    }
}
