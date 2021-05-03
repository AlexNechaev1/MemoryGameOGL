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
        private Point3D m_TranslatePoint;

        // CTOR
        public SecretBox(Point3D i_BottomLeftPoint)
        {
            this.m_TranslatePoint = i_BottomLeftPoint;
        }

        // PUBLIC METHODS
        public void DrawSecretBox()
        {
            GL.glPushMatrix();
            calculateAddValue();
    
            GL.glTranslatef(this.m_TranslatePoint.X, this.m_TranslatePoint.Y, this.m_TranslatePoint.Z);
            GL.glTranslatef(0.0f, this.m_CurrentElevationValue, 0);
            preformSecretBoxDrawing();
        
            GL.glTranslatef(0.0f, 1, 0);
            GL.glRotatef(-180f, 1, 0, 0);
            GL.glTranslatef(0.0f, -1, 0);

            drawUpperCase();


            GL.glPopMatrix();
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
            GL.glVertex3f(0, 0, 0);

            GL.glTexCoord2f(0.0f, 0.0f);
            GL.glVertex3f(1, 0, 0);

            GL.glTexCoord2f(0.0f, 1.0f);
            GL.glVertex3f(1, 1, 0);

            GL.glTexCoord2f(0.5f, 1.0f);
            GL.glVertex3f(0, 1, 0);

            //Left side
            GL.glTexCoord2f(0.0f, 0.0f);
            GL.glVertex3f(0, 0, 0);

            GL.glTexCoord2f(0.5f, 0.0f);
            GL.glVertex3f(0, 0, 1);

            GL.glTexCoord2f(0.5f, 1.0f);
            GL.glVertex3f(0, 1, 1);

            GL.glTexCoord2f(0.0f, 1.0f);
            GL.glVertex3f(0, 1, 0);

            //Right side
            GL.glTexCoord2f(0.5f, 0.0f);
            GL.glVertex3f(1, 0, 0);

            GL.glTexCoord2f(0.0f, 0.0f);
            GL.glVertex3f(1, 0, 1);

            GL.glTexCoord2f(0.0f, 1.0f);
            GL.glVertex3f(1, 1, 1);

            GL.glTexCoord2f(0.5f, 1.0f);
            GL.glVertex3f(1, 1, 0);

            //FrontSide
            GL.glTexCoord2f(0.0f, 0.0f);
            GL.glVertex3f(0, 0, 1);

            GL.glTexCoord2f(0.5f, 0.0f);
            GL.glVertex3f(1, 0, 1);

            GL.glTexCoord2f(0.5f, 1.0f);
            GL.glVertex3f( 1, 1,  1);

            GL.glTexCoord2f(0.0f, 1.0f);
            GL.glVertex3f(0, 1, 1);

            //bottom case
            GL.glTexCoord2f(0.5f, 0.0f);
            GL.glVertex3f(0, 0, 0);

            GL.glTexCoord2f(0.5f, 1.0f);
            GL.glVertex3f(1, 0, 0);

            GL.glTexCoord2f(1.0f, 1.0f);
            GL.glVertex3f(1, 0, 1);

            GL.glTexCoord2f(1.0f, 0.0f);
            GL.glVertex3f(0, 0, 1);

            //upper case
            //drawUpperCase();

            GL.glEnd();
        }

        private void drawUpperCase()
        {
            GL.glBegin(GL.GL_QUADS);

            GL.glTexCoord2f(0.5f, 0.0f);
            GL.glVertex3f(0, 1, 0);

            GL.glTexCoord2f(0.5f, 1.0f);
            GL.glVertex3f(1, 1, 0);

            GL.glTexCoord2f(1.0f, 1.0f);
            GL.glVertex3f(1, 1, 1);

            GL.glTexCoord2f(1.0f, 0.0f);
            GL.glVertex3f(0, 1, 1);

            GL.glEnd();
        }
    }
}