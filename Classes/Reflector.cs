using OpenGL;

namespace myOpenGL.Classes
{
    public class Reflector
    {
        // PUBLIC METHODS
        public void ReflectBeforeSecretBoxMatrixDrawing()
        {
            GL.glEnable(GL.GL_BLEND);
            GL.glEnable(GL.GL_STENCIL_TEST);
            GL.glStencilOp(GL.GL_REPLACE, GL.GL_REPLACE, GL.GL_REPLACE);
            GL.glStencilFunc(GL.GL_ALWAYS, 1, 0xFFFFFFFF); // draw floor always
            GL.glColorMask((byte)GL.GL_FALSE, (byte)GL.GL_FALSE, (byte)GL.GL_FALSE, (byte)GL.GL_FALSE);
            GL.glDisable(GL.GL_DEPTH_TEST);

            this.drawFloor();

            // restore regular settings
            GL.glColorMask((byte)GL.GL_TRUE, (byte)GL.GL_TRUE, (byte)GL.GL_TRUE, (byte)GL.GL_TRUE);
            GL.glEnable(GL.GL_DEPTH_TEST);

            // reflection is drawn only where STENCIL buffer value equal to 1
            GL.glStencilFunc(GL.GL_EQUAL, 1, 0xFFFFFFFF);
            GL.glStencilOp(GL.GL_KEEP, GL.GL_KEEP, GL.GL_KEEP);

            // draw reflected scene
            GL.glPushMatrix();
            GL.glScalef(1, -1, 1); //swap on Y axis
        }

        public void ReflectAfterSecretBoxMatrixDrawing()
        {
            GL.glPopMatrix();
            GL.glDisable(GL.GL_TEXTURE_2D);
            GL.glDisable(GL.GL_STENCIL_TEST);
            GL.glDepthMask((byte)GL.GL_FALSE);
            this.drawFloor();
            GL.glDepthMask((byte)GL.GL_TRUE);
            GL.glColor3f(1.0f, 1.0f, 1.0f);
            GL.glDisable(GL.GL_BLEND);
        }

        // PRIVATE METHODS
        private void drawFloor()
        {
            GL.glBegin(GL.GL_QUADS);

            //!!! for blended REFLECTION 
            GL.glColor4d(0, 0, 1, 0.5);
            GL.glVertex3d(-1, 0, -1);
            GL.glVertex3d(11, 0, -1);
            GL.glVertex3d(11, 0, 11);
            GL.glVertex3d(-1, 0, 11);
            GL.glEnd();
        }
    }
}