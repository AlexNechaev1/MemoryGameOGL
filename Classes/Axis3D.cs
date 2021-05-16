using OpenGL;

namespace myOpenGL.Classes
{
    public class Axis3D
    {
        // CLASS MEMBERS
        private float[] m_FloatArr;
        private int m_AxisLength = 10;

        // CTOR
        public Axis3D(float[] i_FloatArr = null, int i_AxisLength = 10)
        {
            this.m_FloatArr = i_FloatArr;
            this.m_AxisLength = i_AxisLength;
        }

        // PUBLIC METHODS
        public void DrawAxis3D()
        {
            if (this.m_FloatArr != null)
            {
                GL.glLightfv(GL.GL_LIGHT0, GL.GL_POSITION, this.m_FloatArr);
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
            GL.glVertex3f(-1*m_AxisLength, 0.0f, 0.0f);
            GL.glVertex3f(m_AxisLength, 0.0f, 0.0f);
            //y  GREEN 
            GL.glColor3f(0.0f, 1.0f, 0.0f);
            GL.glVertex3f(0.0f, -1 * m_AxisLength, 0.0f);
            GL.glVertex3f(0.0f, m_AxisLength, 0.0f);
            //z  BLUE
            GL.glColor3f(0.0f, 0.0f, 1.0f);
            GL.glVertex3f(0.0f, 0.0f, -1 * m_AxisLength);
            GL.glVertex3f(0.0f, 0.0f, m_AxisLength);
            GL.glEnd();
        }
    }
}