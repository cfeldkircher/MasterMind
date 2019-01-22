using System;
using System.Linq;
using System.Text;

namespace mMind
{
    class Program
    {
        
        static void Main(string[] args)
        {
            #region Properties
            int guessCount = 10;
            int totalDigits = 4;
            int startRange = 1;
            int endRange = 6;
            bool playGame = true;
            int answer = CreateRandomInt(totalDigits, startRange, endRange);
            StringBuilder correctAnswer = new StringBuilder();
            for (int i = 1; i <= totalDigits; i++)
            {
                correctAnswer.Append("+");
            }
            #endregion

            //Console.WriteLine(answer);

            Console.WriteLine("Would you like to play the game? (y/n)");
            if (Console.ReadLine().ToUpper() == "N")
            {
                playGame = false;
            }

            //Set Rules
            Console.WriteLine("Please guess a number " + totalDigits + " digits in length, with each digit ranging from " + startRange + " to " + endRange + ".");
            Console.WriteLine("A minus(-) sign will be printed for every digit that is correct but in the wrong position, and a plus(+) sign will be printed for every digit that is both correct and in the correct position.");
            Console.WriteLine("All plus signs will print first, all minus signs second, and nothing for incorrect digits.");
           
            while (playGame == true)
            {
                Console.WriteLine("You will have 10 attempts to guess the number correctly.");
                Console.WriteLine("Please enter your first guess.");
                int guessed = guessCount;
                while (guessed >= 1)
                {
                    char[] arrGuess = Console.ReadLine().ToArray();
                    if (arrGuess.Length != 4)
                    {
                        Console.WriteLine("Please enter a four digit number.");
                    }
                    else
                    {
                        string strGuess = Guess(arrGuess, answer, totalDigits, startRange, endRange);
                        if (strGuess == correctAnswer.ToString())
                        {
                            Console.WriteLine("Correct! Nice Job!.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine(strGuess);
                            guessed = guessed - 1;
                            Console.WriteLine(guessed + " more guesses.");
                        }
                    }
                }

                Console.WriteLine("The correct answer was " + answer + ". Would you like to play another game? (y/n)");
                if (Console.ReadLine().ToUpper() == "N")
                {
                    playGame = false;
                }
                answer = CreateRandomInt(totalDigits, startRange, endRange);
                //Console.WriteLine(answer);
                guessed = guessCount;
            }
        }

        #region Methods
        static int CreateRandomInt(int totalDigits, int startRange, int endRange)
        {
            StringBuilder sb = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i <= totalDigits - 1; i++)
            {
                sb.Append(random.Next(startRange, endRange).ToString());
            }
            return Convert.ToInt32(sb.ToString());
        }
        static string Guess(char[] guess, int answer, int digits, int startRange, int endRange)
        {
            char[] answerArray = answer.ToString().ToArray();
            int[] rankArray = new int[digits];
            char[] resultArray = new char[digits];
            StringBuilder sb = new StringBuilder();
            
            for (int i = 0; i < digits; i++)
            {
                if(Convert.ToInt32(guess[i].ToString()) > endRange || Convert.ToInt32(guess[i].ToString()) < startRange)
                {
                    Console.WriteLine("Digit in postion " + (i + 1) + " is out of range (1-6)");
                    rankArray[i] = 3;
                    resultArray[i] = ' ';
                }
                else if (DigitExists(Convert.ToChar(guess[i]), answerArray) && CorrectPosition(i, guess, answerArray))
                {
                    rankArray[i] = 1;
                    resultArray[i] = '+';
                }
                else if (DigitExists(Convert.ToChar(guess[i]), answerArray) && !CorrectPosition(i, guess, answerArray))
                {
                    rankArray[i] = 2;
                    resultArray[i] = '-';
                }
                else 
                {
                    rankArray[i] = 3;
                    resultArray[i] = ' ';
                }
            }

            Array.Sort(rankArray, resultArray);
            foreach(char res in resultArray)
            {
                sb.Append(res.ToString());
            }
            return sb.ToString();
        }

        static bool CorrectPosition(int position, char[] guess, char[]answer)
        {
            if (guess[position] == answer[position])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static bool DigitExists(char guess, char[] answer)
        {
            bool result = false;
            foreach(Char c in answer)
            {
                if(guess == c)
                {
                    result =  true;
                    break;
                }
            }
            return result;
        }
        #endregion
    }
}
