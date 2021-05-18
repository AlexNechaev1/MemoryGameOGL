using myOpenGL.Structs;
using OpenGL;

namespace myOpenGL.Classes
{
    public class SecretBox
    {
        // CLASS MEMBERS
        private float m_MaxElevationValue = 1;
        private float m_MinElevationValue = 0;
        private float m_ElevationDeltaValue = 0.01f;
        private float m_CurrentElevationValue = 0;
        private bool m_AddToCurrentElevationValueFlag = true;
        private bool m_IsSelectedSecretBox = false;
        private bool m_HasReachedMaxHeight = false;
        private Point3D m_TranslatePoint;

        // CTOR
        public SecretBox(Point3D i_TranslatePoint)
        {
            this.m_TranslatePoint = i_TranslatePoint;
        }

        // PUBLIC METHODS
        public void DrawSecretBox()
        {
            OpenGL.GL.glColor3f(1, 1, 1);

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

        public void SelectThisSecretBox()
        {
            this.m_IsSelectedSecretBox = true;
            this.m_MaxElevationValue = 2.5f;
            this.m_ElevationDeltaValue = 0.02f;
        }

        public void ForgetThisSecretBox()
        {
            this.m_IsSelectedSecretBox = false;
            this.m_MaxElevationValue = 1;
            this.m_ElevationDeltaValue = 0.01f;
        }

        // PRIVATE METHODS
        private void calculateAddValue()
        {
            if (this.m_AddToCurrentElevationValueFlag)
            {
                this.m_CurrentElevationValue += m_ElevationDeltaValue;
                if (this.m_CurrentElevationValue > m_MaxElevationValue)
                {
                    this.m_AddToCurrentElevationValueFlag = false;
                    if (this.m_IsSelectedSecretBox)
                    {
                        this.m_HasReachedMaxHeight = true;
                    }
                }
            }
            else
            {
                if (!(this.m_IsSelectedSecretBox && this.m_HasReachedMaxHeight))
                {
                    this.m_CurrentElevationValue -= m_ElevationDeltaValue;
                    if (this.m_CurrentElevationValue < m_MinElevationValue)
                    {
                        this.m_AddToCurrentElevationValueFlag = true;
                    }
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