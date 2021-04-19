using myOpenGL.Structs;
using OpenGL;

namespace myOpenGL.Classes
{
    public class SecretBox
    {
        // CLASS MEMBERS
        private const float k_MaxElevationValue = 1;
        private const float k_MinElevationValue = 0;
        private const float k_ElevationDeltaValue = 0.01f;
        private float m_CurrentElevationValue = 0;
        private bool m_AddToCurrentElevationValueFlag = true;
        private Point3D m_BottomLeftPoint;

        // CTOR
        public SecretBox(Point3D i_BottomLeftPoint)
        {
            this.m_BottomLeftPoint = i_BottomLeftPoint;
        }

        // PUBLIC METHODS
        public void DrawSecretBox()
        {
            calculateAddValue();

            GL.glTranslatef(0.0f, this.m_CurrentElevationValue, 0);
            preformSecretBoxDrawing();
            GL.glTranslatef(0.0f, -1*this.m_CurrentElevationValue, 0);
        }

        // PRIVATE METHODS
        private void calculateAddValue()
        {
            if (this.m_AddToCurrentElevationValueFlag)
            {
                this.m_CurrentElevationValue += k_ElevationDeltaValue;
                if (this.m_CurrentElevationValue > k_MaxElevationValue)
                {
                    this.m_AddToCurrentElevationValueFlag = false;
                }
            }
            else
            {
                this.m_CurrentElevationValue -= k_ElevationDeltaValue;
                if (this.m_CurrentElevationValue < k_MinElevationValue)
                {
                    this.m_AddToCurrentElevationValueFlag = true;
                }
            }
        }

        private void preformSecretBoxDrawing()
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