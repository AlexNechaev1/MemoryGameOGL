namespace myOpenGL.Structs
{
    public struct Point3D
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Point3D(float i_X, float i_Y, float i_Z)
        {
            this.X = i_X;
            this.Y = i_Y;
            this.Z = i_Z;
        }
    }
}