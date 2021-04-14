using myOpenGL.Structs;
using OpenGL;

namespace myOpenGL.Classes
{
    public class SecretBox
    {
        private Point3D m_BottomLeftPoint;

        public SecretBox(Point3D i_MiddlePoint)
        {
            this.m_BottomLeftPoint = i_MiddlePoint;
        }

        public void DrawSecretBox()
        {
            GL.glBegin(GL.GL_QUADS);

            //Back side
            GL.glTexCoord2f(0.5f, 0.0f);
            GL.glVertex3f(this.m_BottomLeftPoint.X, this.m_BottomLeftPoint.Y, this.m_BottomLeftPoint.Z);

            GL.glTexCoord2f(0.0f, 0.0f);
            GL.glVertex3f(this.m_BottomLeftPoint.X + 1, this.m_BottomLeftPoint.Y, this.m_BottomLeftPoint.Z);

            GL.glTexCoord2f(0.0f, 1.0f);
            GL.glVertex3f(this.m_BottomLeftPoint.X + 1, this.m_BottomLeftPoint.Y + 1, this.m_BottomLeftPoint.Z);

            GL.glTexCoord2f(0.5f, 1.0f);
            GL.glVertex3f(this.m_BottomLeftPoint.X, this.m_BottomLeftPoint.Y + 1, this.m_BottomLeftPoint.Z);

            //Left side
            GL.glTexCoord2f(0.0f, 0.0f);
            GL.glVertex3f(this.m_BottomLeftPoint.X, this.m_BottomLeftPoint.Y, this.m_BottomLeftPoint.Z);

            GL.glTexCoord2f(0.5f, 0.0f);
            GL.glVertex3f(this.m_BottomLeftPoint.X, this.m_BottomLeftPoint.Y, this.m_BottomLeftPoint.Z + 1);

            GL.glTexCoord2f(0.5f, 1.0f);
            GL.glVertex3f(this.m_BottomLeftPoint.X, this.m_BottomLeftPoint.Y + 1, this.m_BottomLeftPoint.Z + 1);

            GL.glTexCoord2f(0.0f, 1.0f);
            GL.glVertex3f(this.m_BottomLeftPoint.X, this.m_BottomLeftPoint.Y + 1, this.m_BottomLeftPoint.Z);

            //Right side
            GL.glTexCoord2f(0.5f, 0.0f);
            GL.glVertex3f(this.m_BottomLeftPoint.X + 1, this.m_BottomLeftPoint.Y, this.m_BottomLeftPoint.Z);

            GL.glTexCoord2f(0.0f, 0.0f);
            GL.glVertex3f(this.m_BottomLeftPoint.X + 1, this.m_BottomLeftPoint.Y, this.m_BottomLeftPoint.Z + 1);

            GL.glTexCoord2f(0.0f, 1.0f);
            GL.glVertex3f(this.m_BottomLeftPoint.X + 1, this.m_BottomLeftPoint.Y + 1, this.m_BottomLeftPoint.Z + 1);

            GL.glTexCoord2f(0.5f, 1.0f);
            GL.glVertex3f(this.m_BottomLeftPoint.X + 1, this.m_BottomLeftPoint.Y + 1, this.m_BottomLeftPoint.Z);

            //FrontSide
            GL.glTexCoord2f(0.0f, 0.0f);
            GL.glVertex3f(this.m_BottomLeftPoint.X, this.m_BottomLeftPoint.Y, this.m_BottomLeftPoint.Z + 1);

            GL.glTexCoord2f(0.5f, 0.0f);
            GL.glVertex3f(this.m_BottomLeftPoint.X + 1, this.m_BottomLeftPoint.Y, this.m_BottomLeftPoint.Z + 1);

            GL.glTexCoord2f(0.5f, 1.0f);
            GL.glVertex3f(this.m_BottomLeftPoint.X + 1, this.m_BottomLeftPoint.Y + 1, this.m_BottomLeftPoint.Z + 1);

            GL.glTexCoord2f(0.0f, 1.0f);
            GL.glVertex3f(this.m_BottomLeftPoint.X, this.m_BottomLeftPoint.Y + 1, this.m_BottomLeftPoint.Z + 1);
                                  
            //bottom case
            GL.glTexCoord2f(0.5f, 0.0f);
            GL.glVertex3f(this.m_BottomLeftPoint.X, this.m_BottomLeftPoint.Y, this.m_BottomLeftPoint.Z);

            GL.glTexCoord2f(0.5f, 1.0f);
            GL.glVertex3f(this.m_BottomLeftPoint.X + 1, this.m_BottomLeftPoint.Y, this.m_BottomLeftPoint.Z);

            GL.glTexCoord2f(1.0f, 1.0f);
            GL.glVertex3f(this.m_BottomLeftPoint.X + 1, this.m_BottomLeftPoint.Y, this.m_BottomLeftPoint.Z + 1);

            GL.glTexCoord2f(1.0f, 0.0f);
            GL.glVertex3f(this.m_BottomLeftPoint.X, this.m_BottomLeftPoint.Y, this.m_BottomLeftPoint.Z + 1);

            //upper case
            GL.glTexCoord2f(0.5f, 0.0f);
            GL.glVertex3f(this.m_BottomLeftPoint.X, this.m_BottomLeftPoint.Y + 1, this.m_BottomLeftPoint.Z);

            GL.glTexCoord2f(0.5f, 1.0f);
            GL.glVertex3f(this.m_BottomLeftPoint.X + 1, this.m_BottomLeftPoint.Y + 1, this.m_BottomLeftPoint.Z);

            GL.glTexCoord2f(1.0f, 1.0f);
            GL.glVertex3f(this.m_BottomLeftPoint.X + 1, this.m_BottomLeftPoint.Y + 1, this.m_BottomLeftPoint.Z + 1);

            GL.glTexCoord2f(1.0f, 0.0f);
            GL.glVertex3f(this.m_BottomLeftPoint.X, this.m_BottomLeftPoint.Y + 1, this.m_BottomLeftPoint.Z + 1);

            GL.glEnd();
        }
    }
}