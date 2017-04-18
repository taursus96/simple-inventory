

namespace SimpleInventory
{
    public class Position : IPosition
    {
        protected int x;
        protected int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }
    }
}