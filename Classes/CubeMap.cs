using System;
using myOpenGL.Structs;
using myOpenGL.Properties;
using System.Drawing;
using OpenGL;
using System.IO;

namespace myOpenGL.Classes
{
    public class CubeMap
    {
        public uint[] Textures = new uint[6];
        public Point3D TranslatePoint = new Point3D(30, 10, 5);
        private myOpenGL.Structs.Color m_CubeMapColor = new myOpenGL.Structs.Color(1, 0.5f, 1);
        private const int k_ScalingValue = 100;

        public CubeMap()
        {
            loadTexture();
        }

        public void DrawCubeMap()
        {
            GL.glColor3f(1, 1, 1);
            GL.glPushMatrix();

            GL.glTranslatef(TranslatePoint.X, TranslatePoint.Y, TranslatePoint.Z);
            GL.glScalef(k_ScalingValue, k_ScalingValue, k_ScalingValue);

            this.preformCubeMapDrawing();

            GL.glPopMatrix();
        }

        private void loadTexture()
        {
            GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA);
            GL.glGenTextures(6, Textures);

            Bitmap[] images = { Resources.secondTextureFromLeft, Resources.forthTextureFromLeft,
                                Resources.firstTextureFromLeft, Resources.thirdTextureFromLeft,
                                Resources.topTexture, Resources.bottomTexture};


            for (int i = 0; i < 6; i++)
            {
                Bitmap image = images[i];
                
                image.RotateFlip(RotateFlipType.RotateNoneFlipY); //Y axis in Windows is directed downwards, while in OpenGL-upwards
                System.Drawing.Imaging.BitmapData bitmapdata;
                Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

                bitmapdata = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                GL.glBindTexture(GL.GL_TEXTURE_2D, Textures[i]);
                //2D for XYZ
                GL.glTexImage2D(GL.GL_TEXTURE_2D, 0, (int)GL.GL_RGB8, image.Width, image.Height,
                                                              0, GL.GL_BGR_EXT, GL.GL_UNSIGNED_byte, bitmapdata.Scan0);
                GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, (int)GL.GL_LINEAR);
                GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, (int)GL.GL_LINEAR);

                image.UnlockBits(bitmapdata);
                image.Dispose();
            }
        }

        private void preformCubeMapDrawing()
        {
            GL.glPushMatrix();

            GL.glEnable(GL.GL_TEXTURE_2D);

            drawTopCase();
            drawFrontCase();
            drawLeftCase();
            drawBackCase();
            drawRightCase();
            drawBottomCase();

            GL.glDisable(GL.GL_TEXTURE_2D);

            GL.glPopMatrix();
        }
        private void drawTopCase()
        {
            GL.glBindTexture(GL.GL_TEXTURE_2D, Textures[4]);
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-0.5f, 0.5f, 0.5f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(0.5f, 0.5f, 0.5f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(0.5f, 0.5f, -0.5f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-0.5f, 0.5f, -0.5f);
            GL.glEnd();
        }

        private void drawFrontCase()
        {
            GL.glBindTexture(GL.GL_TEXTURE_2D, Textures[0]);
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-0.5f, -0.5f, 0.5f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(0.5f, -0.5f, 0.5f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(0.5f, 0.5f, 0.5f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-0.5f, 0.5f, 0.5f);
            GL.glEnd();
        }

        private void drawRightCase()
        {
            GL.glBindTexture(GL.GL_TEXTURE_2D, Textures[3]);
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(0.5f, -0.5f, 0.5f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(0.5f, -0.5f, -0.5f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(0.5f, 0.5f, -0.5f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(0.5f, 0.5f, 0.5f);
            GL.glEnd();
        }

        private void drawBackCase()
        {
            GL.glBindTexture(GL.GL_TEXTURE_2D, Textures[1]);
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(0.5f, -0.5f, -0.5f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(-0.5f, -0.5f, -0.5f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(-0.5f, 0.5f, -0.5f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(0.5f, 0.5f, -0.5f);
            GL.glEnd();
        }

        private void drawLeftCase()
        {
            GL.glBindTexture(GL.GL_TEXTURE_2D, Textures[2]);
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-0.5f, -0.5f, -0.5f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(-0.5f, -0.5f, 0.5f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(-0.5f, 0.5f, 0.5f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-0.5f, 0.5f, -0.5f);
            GL.glEnd();
        }

        private void drawBottomCase()
        {
            GL.glBindTexture(GL.GL_TEXTURE_2D, Textures[5]);
            GL.glBegin(GL.GL_QUADS);
            GL.glTexCoord2f(0.0f, 0.0f); GL.glVertex3f(-0.5f, -0.5f, -0.5f);
            GL.glTexCoord2f(1.0f, 0.0f); GL.glVertex3f(0.5f, -0.5f, -0.5f);
            GL.glTexCoord2f(1.0f, 1.0f); GL.glVertex3f(0.5f, -0.5f, 0.5f);
            GL.glTexCoord2f(0.0f, 1.0f); GL.glVertex3f(-0.5f, -0.5f, 0.5f);
            GL.glEnd();
        }


    }
}