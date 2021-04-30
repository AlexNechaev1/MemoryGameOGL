namespace MemoryGameLogic
{
    public struct GameBoardDimensions
    {
        // MEMBER VARIABLES
        private readonly int r_Height;
        private readonly int r_Width;

        // CTOR
        public GameBoardDimensions(int i_Height, int i_Width)
        {
            this.r_Height = i_Height;
            this.r_Width = i_Width;
        }

        // PROPERTIES
        public int Height
        {
            get { return this.r_Height; }
        }

        public int Width
        {
            get { return this.r_Width; }
        }

        // TOSTRING METHOD
        public override string ToString()
        {
            string stringToReturn = string.Format("{0} x {1}", this.r_Height, this.r_Width);

            return stringToReturn;
        }
    }
}