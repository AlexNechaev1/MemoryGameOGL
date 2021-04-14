using myOpenGL.Structs;
using OpenGL;

namespace myOpenGL.Classes
{
    public class SecretBox
    {
        private Point3D m_MiddlePoint;

        public SecretBox(Point3D i_MiddlePoint)
        {
            this.m_MiddlePoint = i_MiddlePoint;
        }

        public void DrawSecretBox()
        {
            drawCubeAndPasteTexture(0.5f);
        }

        private void drawCubeAndPasteTexture(float i_Height)
        {
            GL.glBegin(GL.GL_QUADS);

            //Back side
            GL.glTexCoord2f(0.5f, 0.0f);
            GL.glVertex3f(0.0f, 0.0f + i_Height, 0.0f);

            GL.glTexCoord2f(0.0f, 0.0f);
            GL.glVertex3f(1.0f, 0.0f + i_Height, 0.0f);

            GL.glTexCoord2f(0.0f, 1.0f);
            GL.glVertex3f(1.0f, 1.0f + i_Height, 0.0f);

            GL.glTexCoord2f(0.5f, 1.0f);
            GL.glVertex3f(0.0f, 1.0f + i_Height, 0.0f);

            //Left side
            GL.glTexCoord2f(0.0f, 0.0f);
            GL.glVertex3f(0.0f, 0.0f + i_Height, 0.0f);

            GL.glTexCoord2f(0.5f, 0.0f);
            GL.glVertex3f(0.0f, 0.0f + i_Height, 1.0f);

            GL.glTexCoord2f(0.5f, 1.0f);
            GL.glVertex3f(0.0f, 1.0f + i_Height, 1.0f);

            GL.glTexCoord2f(0.0f, 1.0f);
            GL.glVertex3f(0.0f, 1.0f + i_Height, 0.0f);

            //Right side
            GL.glTexCoord2f(0.5f, 0.0f);
            GL.glVertex3f(1.0f, 0.0f + i_Height, 0.0f);

            GL.glTexCoord2f(0.0f, 0.0f);
            GL.glVertex3f(1.0f, 0.0f + i_Height, 1.0f);

            GL.glTexCoord2f(0.0f, 1.0f);
            GL.glVertex3f(1.0f, 1.0f + i_Height, 1.0f);

            GL.glTexCoord2f(0.5f, 1.0f);
            GL.glVertex3f(1.0f, 1.0f + i_Height, 0.0f);

            //FrontSide
            GL.glTexCoord2f(0.0f, 0.0f);
            GL.glVertex3f(0.0f, 0.0f + i_Height, 1.0f);

            GL.glTexCoord2f(0.5f, 0.0f);
            GL.glVertex3f(1.0f, 0.0f + i_Height, 1.0f);

            GL.glTexCoord2f(0.5f, 1.0f);
            GL.glVertex3f(1.0f, 1.0f + i_Height, 1.0f);

            GL.glTexCoord2f(0.0f, 1.0f);
            GL.glVertex3f(0.0f, 1.0f + i_Height, 1.0f);

            //upper case
            GL.glTexCoord2f(0.5f, 0.0f);
            GL.glVertex3f(0.0f, 1.0f + i_Height, 0.0f);

            GL.glTexCoord2f(0.5f, 1.0f);
            GL.glVertex3f(1.0f, 1.0f + i_Height, 0.0f);

            GL.glTexCoord2f(1.0f, 1.0f);
            GL.glVertex3f(1.0f, 1.0f + i_Height, 1.0f);

            GL.glTexCoord2f(1.0f, 0.0f);
            GL.glVertex3f(0.0f, 1.0f + i_Height, 1.0f);

            //bottom case
            GL.glTexCoord2f(0.5f, 0.0f);
            GL.glVertex3f(0.0f, 0.0f + i_Height, 0.0f);

            GL.glTexCoord2f(0.5f, 1.0f);
            GL.glVertex3f(1.0f, 0.0f + i_Height, 0.0f);

            GL.glTexCoord2f(1.0f, 1.0f);
            GL.glVertex3f(1.0f, 0.0f + i_Height, 1.0f);

            GL.glTexCoord2f(1.0f, 0.0f);
            GL.glVertex3f(0.0f, 0.0f + i_Height, 1.0f);

            GL.glEnd();
        }
    }
}