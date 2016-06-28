using System;

namespace PacManGame
{
    class Game
    {
        private Board mBoard;
        private Coordinate mPacMansPosition;
        private Coordinate mMonsterPosition1;
        private Coordinate mMonsterPosition2;
        private Coordinate mMonsterPosition3;
        private bool mGameOver;
        private bool mWon;

        public Game(Board pBoard)
        {
            mBoard = pBoard;
            mPacMansPosition = new Coordinate(1, 1);
            //add monsters
            mMonsterPosition1 = new Coordinate(8, 8);
            mMonsterPosition2 = new Coordinate(10, 10);
            mMonsterPosition3 = new Coordinate(12, 12);


        }

        public enum move
        {
            LEFT,
            RIGHT,
            UP,
            DOWN
        }

        public void Move(move pMove)
        {
            //legal move
            Coordinate newPosition;
            switch (pMove)
            {
                case move.LEFT:
                    newPosition = new Coordinate(mPacMansPosition.x - 1, mPacMansPosition.y);
                    break;
                case move.RIGHT:
                    newPosition = new Coordinate(mPacMansPosition.x + 1, mPacMansPosition.y);
                    break;
                case move.UP:
                    newPosition = new Coordinate(mPacMansPosition.x, mPacMansPosition.y - 1);
                    break;
                default:
                    newPosition = new Coordinate(mPacMansPosition.x, mPacMansPosition.y + 1);
                    break;
            }
            if (mBoard.GetState(newPosition) == BoardStates.WALL)
            {
                // dont move him
            }
            else if (mBoard.GetState(newPosition) == BoardStates.TUNNEL)
            {
                //find the other end of the tunnel


                newPosition = new Coordinate(15, 15);
                mPacMansPosition = newPosition;
                mBoard.Eat(newPosition);
            }
            else
            {
                mPacMansPosition = newPosition;
                mBoard.Eat(newPosition);
            }

            if (mPacMansPosition.toString().Equals(mMonsterPosition1.toString()))
            {
                mGameOver = true;
                mWon = false;
            }

            //check to see if anymore pills are on the board
            if (mBoard.AreAllPillsEaten())
            {
                mGameOver = true;
                mWon = true;
            }

            mMonsterPosition1 = MoveMonster(mMonsterPosition1);
            mMonsterPosition2 = MoveMonster(mMonsterPosition2);
            mMonsterPosition3 = MoveMonster(mMonsterPosition3);

            //check all of the monster positions
            if (mPacMansPosition.toString().Equals(mMonsterPosition1.toString()) || mPacMansPosition.toString().Equals(mMonsterPosition2.toString()) || mPacMansPosition.toString().Equals(mMonsterPosition3.toString()))
            {
                mGameOver = true;
                mWon = false;
            }

            //win/loose
        }



        private Coordinate MoveMonster(Coordinate mPosition)
        {
            Coordinate newMonsterPosition;
            Array values = Enum.GetValues(typeof(move));
            Random random = new Random();
            move monsterMove = (move)values.GetValue(random.Next(values.Length));
            switch (monsterMove)
            {
                case move.LEFT:
                    newMonsterPosition = new Coordinate(mPosition.x - 1, mPosition.y);

                    break;
                case move.RIGHT:
                    newMonsterPosition = new Coordinate(mPosition.x + 1, mPosition.y);

                    break;
                case move.UP:
                    newMonsterPosition = new Coordinate(mPosition.x, mPosition.y - 1);

                    break;
                default:
                    newMonsterPosition = new Coordinate(mPosition.x, mPosition.y + 1);

                    break;
            }
            if (mBoard.GetState(newMonsterPosition) == BoardStates.WALL)
            {
                MoveMonster(newMonsterPosition);
            }

            return newMonsterPosition;

        }

        public string present()
        {
            if (mGameOver)
            {
                if (mWon)
                {
                    return mBoard.ToString(mPacMansPosition, mMonsterPosition1, mMonsterPosition2, mMonsterPosition3) + "GAME OVER YOU WON";
                }
                return mBoard.ToString(mPacMansPosition, mMonsterPosition1, mMonsterPosition2, mMonsterPosition3, false) + "GAME OVER YOU LOST!!!!!!";
            }
            return mBoard.ToString(mPacMansPosition, mMonsterPosition1, mMonsterPosition2, mMonsterPosition3);
        }

        internal bool GameOver
        {
            get { return mGameOver; }
        }
    }
}
