using OpenGL;

namespace myOpenGL.Classes
{
    public class Axis3D
    {
        // CLASS MEMBERS
        private float[] m_FloatArr;

        // CTOR
        public Axis3D(float[] i_FloatArr = null)
        {
            this.m_FloatArr = i_FloatArr;
        }

        // PUBLIC METHODS
        public void DrawAxis3D()
        {
            if (this.m_FloatArr != null)
            {
                GL.glLightfv(GL.GL_LIGHT0, GL.GL_POSITION, this.m_FloatArr);
                GL.glDisable(GL.GL_LIGHTING);
                GL.glEnable(GL.GL_LINE_STIPPLE);
                GL.glLineStipple(1, 0xFF00); // dotted
            }

            preformAxis3DDrawing();
            if (this.m_FloatArr != null)
            {
                GL.glDisable(GL.GL_LINE_STIPPLE);
            }
        }

        // PRIVATE METHODS
        private void preformAxis3DDrawing()
        {
            GL.glBegin(GL.GL_LINES);
            //x  RED
            GL.glColor3f(1.0f, 0.0f, 0.0f);
            GL.glVertex3f(-3.0f, 0.0f, 0.0f);
            GL.glVertex3f(3.0f, 0.0f, 0.0f);
            //y  GREEN 
            GL.glColor3f(0.0f, 1.0f, 0.0f);
            GL.glVertex3f(0.0f, -3.0f, 0.0f);
            GL.glVertex3f(0.0f, 3.0f, 0.0f);
            //z  BLUE
            GL.glColor3f(0.0f, 0.0f, 1.0f);
            GL.glVertex3f(0.0f, 0.0f, -3.0f);
            GL.glVertex3f(0.0f, 0.0f, 3.0f);
            GL.glEnd();
        }
    }
}