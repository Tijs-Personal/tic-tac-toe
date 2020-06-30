using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace temp
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> playingField = new Dictionary<string, string>();
            playingField.Add("A1", null);
            playingField.Add("B1", null);
            playingField.Add("C1", null);
            playingField.Add("A2", null);
            playingField.Add("B2", null);
            playingField.Add("C2", null);
            playingField.Add("A3", null);
            playingField.Add("B3", null);
            playingField.Add("C3", null);

            Random rnd = new Random();
            int start = rnd.Next(2);

            string player1;
            string player2;
            string player1Mark;
            string player2Mark;

            bool play = true;

            while (play == true)
            {
                if (start == 1)
                {
                    Console.WriteLine("player 1 starts!");
                    player1 = "player 1";
                    player1Mark = "X";
                    player2 = "player 2";
                    player2Mark = "O";
                }
                else
                {
                    Console.WriteLine("player 2 starts!");
                    player1 = "player 2";
                    player1Mark = "O";
                    player2 = "player 1";
                    player2Mark = "X";
                }

                Display(playingField);

                string result = "";
                bool gameEnd = false;

                while (gameEnd == false)
                {
                    playingField = ChooseField(playingField, player1, player1Mark);
                    Display(playingField);

                    result = CheckEnd(playingField);
                    if(result != "")
                    {
                        break;
                    }

                    playingField = ChooseField(playingField, player2, player2Mark);
                    Display(playingField);
                    
                    result = CheckEnd(playingField);
                    if (result != "")
                    {
                        break;
                    }
                }

                switch (result)
                {
                    case "player1":
                        Console.WriteLine($"{player1} wins!");
                        break;

                    case "player2":
                        Console.WriteLine($"{player2} wins!");
                        break;

                    case "tie":
                        Console.WriteLine("Game ended in a tie!");
                        break;
                }

                string yesOrNo;
                do
                {
                    Console.Write("Do you want to play again? (Y/N)");
                    yesOrNo = Convert.ToString(Console.ReadLine());
                    if (yesOrNo == "Y")
                    {
                        play = true;
                    }
                    else if (yesOrNo == "N")
                    {
                        play = false;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input!");
                        yesOrNo = null;
                    }
                } while (yesOrNo == null);
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        static Dictionary<string, string> ChooseField(Dictionary<string, string> playingField, string player, string playerMark)
        {
            bool playerchose = false;
            while (playerchose == false)
            {
                Console.Write($"{player}, choose a field: ");
                string field = Convert.ToString(Console.ReadLine());
                Console.WriteLine();

                if (field == "A1" || field == "A2" || field == "A3" || field == "B1" || field == "B2" || field == "B3" || field == "C1" || field == "C2" || field == "C3")
                {
                    Console.WriteLine($"{player} chose field {field}!\n");
                    if (playingField[field] == null)
                    {
                        playingField[field] = playerMark;

                        playerchose = true;
                    }
                    else
                    {
                        Console.WriteLine("This field is already occupied!");
                    }
                }
                else
                {
                    Console.WriteLine($"{field} is not a valid field!");
                }
            }
            return playingField;
        }

        static string CheckEnd(Dictionary<string, string> playingField)
        {
            if (CheckWin(playingField, "X") == true)
            {
                return "player1";
            }
            else if (CheckWin(playingField, "O") == true)
            {
                return "player2";
            }
            else if (CheckTie(playingField) == true)
            {
                return "tie";
            }
            return "";
        }

        static bool CheckTie(Dictionary<string, string> playingField)
        {
            foreach(KeyValuePair<string, string> entry in playingField)
            {
                if( entry.Value == null)
                {
                    return false;
                }
            }

            return true;
        }

        static bool CheckWin(Dictionary<string, string> playingField, string mark)
        {
            if
                (
                playingField["A1"] == mark && playingField["B1"] == mark && playingField["C1"] == mark ||
                playingField["A2"] == mark && playingField["B2"] == mark && playingField["C2"] == mark ||
                playingField["A3"] == mark && playingField["B3"] == mark && playingField["C3"] == mark ||
                playingField["A1"] == mark && playingField["A2"] == mark && playingField["A3"] == mark ||
                playingField["B1"] == mark && playingField["B2"] == mark && playingField["B3"] == mark ||
                playingField["C1"] == mark && playingField["C2"] == mark && playingField["C3"] == mark ||
                playingField["A1"] == mark && playingField["B2"] == mark && playingField["C3"] == mark ||
                playingField["A3"] == mark && playingField["B2"] == mark && playingField["C1"] == mark
                )
            {
                return true;
            }

            return false;
        }

        static void Display(Dictionary<string, string> playingField)
        {
            string A1 = playingField["A1"];
            string B1 = playingField["B1"];
            string C1 = playingField["C1"];
            string A2 = playingField["A2"];
            string B2 = playingField["B2"];
            string C2 = playingField["C2"];
            string A3 = playingField["A3"];
            string B3 = playingField["B3"];
            string C3 = playingField["C3"];

            string disp =
                $"\t\tA\t\tB\t\tC\n" +
                $"\t|---------------|---------------|---------------|\n" +
                $"    1\t|\t{A1}\t|\t{B1}\t|\t{C1}\t|\n" +
                $"\t|---------------|---------------|---------------|\tPlayer 1 has mark 'X'\n" +
                $"    2\t|\t{A2}\t|\t{B2}\t|\t{C2}\t|\n" +
                $"\t|---------------|---------------|---------------|\tPlayer 2 has mark 'O'\n" +
                $"    3\t|\t{A3}\t|\t{B3}\t|\t{C3}\t|\n" +
                $"\t|---------------|---------------|---------------|";

            Console.WriteLine(disp + "\n");
        }
    }
}
