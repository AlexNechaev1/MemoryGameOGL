using myOpenGL.Structs;
using OpenGL;
using System;

namespace myOpenGL.Classes
{
    public class SecretBoxArrow
    {
        private SecretBoxMatrix m_SecretBoxMatrixInstance;
        private GLUquadric m_GLUquadricObject;
        
        public SecretBoxArrow(SecretBoxMatrix i_SecretBoxMatrix)
        {
            this.m_GLUquadricObject = GLU.gluNewQuadric();
            this.m_SecretBoxMatrixInstance = i_SecretBoxMatrix;
        }

        ~SecretBoxArrow()
        {
            GLU.gluDeleteQuadric(this.m_GLUquadricObject);
        }

        public void DrawSelectedSecretBoxArrow()
        {
            this.performSecretBoxArrowDrawing();
        }

        private void performSecretBoxArrowDrawing()
        {
            if (this.m_SecretBoxMatrixInstance.DrawSelectedSecretBoxArrowFlag)
            {
                Point3D currentPoint = this.m_SecretBoxMatrixInstance.CurrentSecretBoxPointer.TranslatePoint;
                Console.WriteLine(currentPoint.ToString());
                GL.glPushMatrix();

                GL.glColor3f(1, 0, 0);
                GL.glTranslatef(currentPoint.X + 0.5f, currentPoint.Y + 3.5f, currentPoint.Z + 0.5f);
                GL.glRotatef(-90, 1, 0, 0);
                GLU.gluCylinder(this.m_GLUquadricObject, 0.0, 0.5, 1.5, 16, 16);
                GL.glTranslatef(-1 * (currentPoint.X + 0.5f), -1 * (currentPoint.Y + 3.5f), -1 * (currentPoint.Z + 0.5f));

                GL.glPopMatrix();
            }
        }
    }
}
