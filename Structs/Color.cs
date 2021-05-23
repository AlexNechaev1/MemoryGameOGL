namespace myOpenGL.Structs
{
    public struct Color
    {
        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }

        public Color(float i_R, float i_G, float i_B)
        {
            this.R = i_R;
            this.G = i_G;
            this.B = i_B;
        }
    }
}