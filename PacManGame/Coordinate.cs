namespace PacManGame
{
    public class Coordinate
    {
        public int x;
        public int y;

        public Coordinate(int pX, int pY)
        {
            x = pX;
            y = pY;
        }

        public string toString()
        {
            return string.Format("{0}{1}", x.ToString("D2"), y.ToString("D2"));
        }

    }
}