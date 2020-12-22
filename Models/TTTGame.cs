using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ph_UserEnv.Models
{
    public class TTTGame
    {
        public static bool MarkBoard(int row, int col, string mark,string [,] Board)
        {
            if (Board[row, col] == "")
            {
                Board[row, col] = mark;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool CheckForWinner(string[,] Board)
        {
            for (int i = 0; i < 3; i++)
            {
                if (Board[i, 0] == Board[i, 1] && Board[i, 0] == Board[i, 2] && (Board[i, 0] == "X" || Board[i, 0] == "O"))
                {
                    return true;
                }
                if (Board[0, i] == Board[1, i] && Board[0, i] == Board[2, i] && (Board[0, i] == "X" || Board[0, i] == "O"))
                {
                    return true;
                }
                if (Board[1, 1] == Board[0, 0] && Board[1, 1] == Board[2, 2] && (Board[1, 1] == "X" || Board[1, 1] == "O"))
                {
                    return true;
                }
                if (Board[1, 1] == Board[0, 2] && Board[1, 1] == Board[2, 0] && (Board[1, 1] == "X" || Board[1, 1] == "O"))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
