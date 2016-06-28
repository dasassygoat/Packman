using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PacManGame
{
    class Board
    {

        private Dictionary<string, BoardStates> map;

        public Board()
        {
            map = new Dictionary<string, BoardStates>();
            ConvertToMap("####################");
            ConvertToMap("# *****************#");
            ConvertToMap("####**##T#********##");
            ConvertToMap("#********######****#");
            ConvertToMap("#*#****#*##********#");
            ConvertToMap("#*#****#*##**#****##");
            ConvertToMap("#******************#");
            ConvertToMap("#*##**###*****#****#");
            ConvertToMap("#******************#");
            ConvertToMap("#******************#");
            ConvertToMap("#******************#");
            ConvertToMap("#******************#");
            ConvertToMap("#*##**##*#********##");
            ConvertToMap("#********######****#");
            ConvertToMap("#*#****#*#*********#");
            ConvertToMap("#*#*##*#*##**#*T**##");
            ConvertToMap("#******************#");
            ConvertToMap("#*##**#######*######");
            ConvertToMap("#******************#");
            ConvertToMap("####################");
        }

        private int _lineCount = 0;
        private void ConvertToMap(string pMapString)
        {
            for (int i = 0; i < pMapString.Length; i++)
            {
                Coordinate coordinate = new Coordinate(i, _lineCount);
                BoardStates state;
                if (pMapString[i].Equals('#'))
                {
                    state = BoardStates.WALL;
                }
                else if (pMapString[i].Equals('*'))
                {
                    state = BoardStates.PILL;
                }
                else if (pMapString[i].Equals('T'))
                {
                    state = BoardStates.TUNNEL;
                }
                else
                {
                    state = BoardStates.EMPTY;
                }
                map.Add(coordinate.toString(), state);
            }

            _lineCount++;
        }

        public string ToString(Coordinate pPacMan, Coordinate pMonster1, Coordinate pMonster2, Coordinate pMonster3, bool pPacLives = true)
        {
            string[] board = new string[20];
            for (int i = 0; i < 20; i++)
            {
                board[i] = "";
            }

            //P = Pac-Man
            //M = Monster
            //# = Wall
            //* = Pill
            //T = Tunnel
            foreach (KeyValuePair<string, BoardStates> boardStatese in map)
            {
                string character = "";
                if (!pPacLives)
                {
                    character = "X";
                }
                else if (boardStatese.Key.Equals(pPacMan.toString()))
                {
                    character = "P";
                }
                else if (boardStatese.Key.Equals(pMonster1.toString()))
                {
                    character = "M";
                }
                //added for second monster
                else if (boardStatese.Key.Equals(pMonster2.toString()))
                {
                    character = "M";
                }
                //added the third monster
                else if (boardStatese.Key.Equals(pMonster3.toString()))
                {
                    character = "M";
                }
                else if (boardStatese.Value == BoardStates.WALL)
                {
                    character = "#";
                }
                else if (boardStatese.Value == BoardStates.TUNNEL)
                {
                    character = "T";
                }
                else if (boardStatese.Value == BoardStates.PILL)
                {
                    character = "*";
                }
                else
                {
                    character = " ";
                }


                board[Convert.ToInt32(boardStatese.Key.Substring(2, 2))] = board[Convert.ToInt32(boardStatese.Key.Substring(2, 2))].Insert(Convert.ToInt32(boardStatese.Key.Substring(0, 2)), character);
            }
            StringBuilder test = new StringBuilder();
            foreach (string s in board)
            {
                test.AppendLine(s);
            }
            return test.ToString();
        }

        public BoardStates GetState(Coordinate pNewPosition)
        {
            return map[pNewPosition.toString()];
        }

        public void Eat(Coordinate pNewPosition)
        {
            map[pNewPosition.toString()] = BoardStates.EMPTY;
        }

        //checks to see if any pills are still on the map
        public bool AreAllPillsEaten()
        {
            return map.All(i => i.Value != BoardStates.PILL);
        }
    }



}
