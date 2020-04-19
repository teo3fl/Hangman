using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2
{
    class Game
    {
        public enum GameState
        {
            Won,
            Ongoing,
            Lost
        }

        private const uint MAX_FAILED_ATTEMPTS = 6;

        public string Word { get; set; }

        public string Attempts { get; set; }

        public int FailedAttempts { get; private set; }


        public Game(string word)
        {
            Word = word;
            Attempts = "";
            FailedAttempts = 0;
        }

        public Game(string word, string attempts)
        {
            Word = word;
            Attempts = attempts;
            FailedAttempts = 0;

            foreach (char c in Attempts)
                if (!Word.Contains(c))
                    FailedAttempts++;
        }

        public void TryLetter (char c)
        {
            Attempts += c;

            if (!Word.Contains(c))
                FailedAttempts++;
        }

        public string GetDisplayedText()
        {
            string text = "";

            foreach(char c in Word)
            {
                if(Char.IsLetter(c))
                {
                    if(Attempts.Contains(c))
                    {
                        text += c + " ";
                    }
                    else
                    {
                        text += "_ ";
                    }
                }
                else
                {
                    text += c + " ";
                }
            }

            return text;
        }

        public GameState GetGameState()
        {
            if (CheckForWin())
                return GameState.Won;

            if (FailedAttempts == MAX_FAILED_ATTEMPTS)
                return GameState.Lost;

            return GameState.Ongoing;
        }

        private bool CheckForWin()
        {
            foreach(char c in Word)
            {
                if (Char.IsLetter(c) && !Attempts.Contains(c))
                    return false;
            }

            return true;
        }
    }
}
