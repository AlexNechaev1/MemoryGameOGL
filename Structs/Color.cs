namespace myOpenGL.Structs
{
    public struct Color
    {
        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }

        public Color(float i_X, float i_Y, float i_B)
        {
            this.R = i_X;
            this.G = i_Y;
            this.B = i_B;
        }
    }
}