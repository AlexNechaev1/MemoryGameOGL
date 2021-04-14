using System;
using System.Windows.Forms;
using System.Drawing;
using myOpenGL.Properties;
using myOpenGL.Classes;
using myOpenGL.Structs;

namespace OpenGL
{
    class cOGL
    {
        public double hh = 0;
        Control p;
        int Width;
        int Height;
        GLUquadric obj;

        public cOGL(Control pb)
        {
            p = pb;
            Width = p.Width;
            Height = p.Height;
            InitializeGL();
            obj = GLU.gluNewQuadric(); //do not forget to call gluDeleteQuadric at the end
        }

        ~cOGL()
        {
            GLU.gluDeleteQuadric(obj); //the MUST in case of previous call to gluNewQuadric
            WGL.wglDeleteContext(m_uint_RC);
        }

        uint m_uint_HWND = 0;

        public uint HWND
        {
            get { return m_uint_HWND; }
        }

        uint m_uint_DC = 0;

        public uint DC
        {
            get { return m_uint_DC; }
        }
        uint m_uint_RC = 0;

        public uint RC
        {
            get { return m_uint_RC; }
        }


        void DrawOldAxes()
        {
            //for this time
            //Lights positioning is here!!!
            float[] pos = new float[4];
            pos[0] = 10; pos[1] = 10; pos[2] = 10; pos[3] = 1;
            GL.glLightfv(GL.GL_LIGHT0, GL.GL_POSITION, pos);
            GL.glDisable(GL.GL_LIGHTING);

            //INITIAL axes
            GL.glEnable(GL.GL_LINE_STIPPLE);
            GL.glLineStipple(1, 0xFF00);  //  dotted   
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
            GL.glDisable(GL.GL_LINE_STIPPLE);
        }
        void DrawAxes()
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

        public bool bChkTexture = false;
        public int intRadioOption = 1;

        public float[] ScrollValue = new float[10];
        public float zShift = 0.0f;
        public float yShift = 0.0f;
        public float xShift = 0.0f;
        public float zAngle = 0.0f;
        public float yAngle = 0.0f;
        public float xAngle = 0.0f;
        public int intOptionC = 0;
        double[] AccumulatedRotationsTraslations = new double[16];

        public void Draw()
        {
            if (m_uint_DC == 0 || m_uint_RC == 0)
                return;

            GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT);


            //FULL and COMPLICATED
            // GL.glViewport(0,Height/2,Width,Height/2);						

            GL.glLoadIdentity();

            // not trivial
            double[] ModelVievMatrixBeforeSpecificTransforms = new double[16];
            double[] CurrentRotationTraslation = new double[16];

            GLU.gluLookAt(ScrollValue[0], ScrollValue[1], ScrollValue[2],
                       ScrollValue[3], ScrollValue[4], ScrollValue[5],
                       ScrollValue[6], ScrollValue[7], ScrollValue[8]);
            GL.glTranslatef(0.0f, 0.0f, -4.0f);

            DrawOldAxes();

            //save current ModelView Matrix values
            //in ModelVievMatrixBeforeSpecificTransforms array
            //ModelView Matrix ========>>>>>> ModelVievMatrixBeforeSpecificTransforms
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, ModelVievMatrixBeforeSpecificTransforms);
            //ModelView Matrix was saved, so
            GL.glLoadIdentity(); // make it identity matrix

            //make transformation in accordance to KeyCode
            float delta;
            if (intOptionC != 0)
            {
                delta = 5.0f * Math.Abs(intOptionC) / intOptionC; // signed 5

                switch (Math.Abs(intOptionC))
                {
                    case 1:
                        GL.glRotatef(delta, 1, 0, 0);
                        break;
                    case 2:
                        GL.glRotatef(delta, 0, 1, 0);
                        break;
                    case 3:
                        GL.glRotatef(delta, 0, 0, 1);
                        break;
                    case 4:
                        GL.glTranslatef(delta / 20, 0, 0);
                        break;
                    case 5:
                        GL.glTranslatef(0, delta / 20, 0);
                        break;
                    case 6:
                        GL.glTranslatef(0, 0, delta / 20);
                        break;
                }
            }
            //as result - the ModelView Matrix now is pure representation
            //of KeyCode transform and only it !!!

            //save current ModelView Matrix values
            //in CurrentRotationTraslation array
            //ModelView Matrix =======>>>>>>> CurrentRotationTraslation
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, CurrentRotationTraslation);

            //The GL.glLoadMatrix function replaces the current matrix with
            //the one specified in its argument.
            //The current matrix is the
            //projection matrix, modelview matrix, or texture matrix,
            //determined by the current matrix mode (now is ModelView mode)
            GL.glLoadMatrixd(AccumulatedRotationsTraslations); //Global Matrix

            //The GL.glMultMatrix function multiplies the current matrix by
            //the one specified in its argument.
            //That is, if M is the current matrix and T is the matrix passed to
            //GL.glMultMatrix, then M is replaced with M  T
            GL.glMultMatrixd(CurrentRotationTraslation);

            //save the matrix product in AccumulatedRotationsTraslations
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, AccumulatedRotationsTraslations);

            //replace ModelViev Matrix with stored ModelVievMatrixBeforeSpecificTransforms
            GL.glLoadMatrixd(ModelVievMatrixBeforeSpecificTransforms);
            //multiply it by KeyCode defined AccumulatedRotationsTraslations matrix
            GL.glMultMatrixd(AccumulatedRotationsTraslations);

            GL.glEnable(GL.GL_TEXTURE_2D);
            GL.glBindTexture(GL.GL_TEXTURE_2D, m_Texture[0]);
           
            DrawAxes();
            GL.glColor3f(1.0f, 1.0f, 1.0f);

            SecretBox secretBox = new SecretBox(new Point3D(0, 0, 0));
            secretBox.DrawSecretBox();
            //drawCubeAndPasteTexture(0.5f);

            GL.glFlush();
            WGL.wglSwapBuffers(m_uint_DC);
            GL.glDisable(GL.GL_TEXTURE_2D);
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

        protected virtual void InitializeGL()
        {
            m_uint_HWND = (uint)p.Handle.ToInt32();
            m_uint_DC = WGL.GetDC(m_uint_HWND);

            // Not doing the following WGL.wglSwapBuffers() on the DC will
            // result in a failure to subsequently create the RC.
            WGL.wglSwapBuffers(m_uint_DC);

            WGL.PIXELFORMATDESCRIPTOR pfd = new WGL.PIXELFORMATDESCRIPTOR();
            WGL.ZeroPixelDescriptor(ref pfd);
            pfd.nVersion = 1;
            pfd.dwFlags = (WGL.PFD_DRAW_TO_WINDOW | WGL.PFD_SUPPORT_OPENGL | WGL.PFD_DOUBLEBUFFER);
            pfd.iPixelType = (byte)(WGL.PFD_TYPE_RGBA);
            pfd.cColorBits = 32;
            pfd.cDepthBits = 32;
            pfd.iLayerType = (byte)(WGL.PFD_MAIN_PLANE);

            int pixelFormatIndex = 0;
            pixelFormatIndex = WGL.ChoosePixelFormat(m_uint_DC, ref pfd);
            if (pixelFormatIndex == 0)
            {
                MessageBox.Show("Unable to retrieve pixel format");
                return;
            }

            if (WGL.SetPixelFormat(m_uint_DC, pixelFormatIndex, ref pfd) == 0)
            {
                MessageBox.Show("Unable to set pixel format");
                return;
            }
            //Create rendering context
            m_uint_RC = WGL.wglCreateContext(m_uint_DC);
            if (m_uint_RC == 0)
            {
                MessageBox.Show("Unable to get rendering context");
                return;
            }
            if (WGL.wglMakeCurrent(m_uint_DC, m_uint_RC) == 0)
            {
                MessageBox.Show("Unable to make rendering context current");
                return;
            }


            initRenderingGL();
        }

        public void OnResize()
        {
            Width = p.Width;
            Height = p.Height;
            GL.glViewport(0, 0, Width, Height);
            Draw();
        }

        protected virtual void initRenderingGL()
        {
            if (m_uint_DC == 0 || m_uint_RC == 0)
                return;
            if (this.Width == 0 || this.Height == 0)
                return;
            GL.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            GL.glEnable(GL.GL_DEPTH_TEST);
            GL.glDepthFunc(GL.GL_LEQUAL);

            GL.glViewport(0, 0, this.Width, this.Height);
            GL.glMatrixMode(GL.GL_PROJECTION);
            GL.glLoadIdentity();

            //nice 3D
            GLU.gluPerspective(45.0, 1.0, 0.4, 100.0);


            GL.glMatrixMode(GL.GL_MODELVIEW);
            GL.glLoadIdentity();

            //save the current MODELVIEW Matrix (now it is Identity)
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, AccumulatedRotationsTraslations);

            //! TEXTURE 1a 
            GenerateTextures();
            //! TEXTURE 1b 
        }


        //! TEXTURE b
        private uint[] m_Texture;

        void GenerateTextures()
        {
            GL.glEnable(GL.GL_TEXTURE_2D);

            m_Texture = new uint[1];		// storage for texture

            Bitmap image = new Bitmap(Resources.cubeTexture);
            image.RotateFlip(RotateFlipType.RotateNoneFlipY); //Y axis in Windows is directed downwards, while in OpenGL-upwards
            System.Drawing.Imaging.BitmapData bitmapdata;
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

            bitmapdata = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            GL.glGenTextures(1, m_Texture);
            GL.glBindTexture(GL.GL_TEXTURE_2D, m_Texture[0]);
            //  VN-in order to use System.Drawing.Imaging.BitmapData Scan0 I've added overloaded version to
            //  OpenGL.cs
            //  [DllImport(GL_DLL, EntryPoint = "glTexImage2D")]
            //  public static extern void glTexImage2D(uint target, int level, int internalformat, int width, int height, int border, uint format, uint type, IntPtr pixels);
            GL.glTexImage2D(GL.GL_TEXTURE_2D, 0, (int)GL.GL_RGB8, image.Width, image.Height,
                0, GL.GL_BGR_EXT, GL.GL_UNSIGNED_byte, bitmapdata.Scan0);

            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, (int)GL.GL_LINEAR);		// Linear Filtering
            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, (int)GL.GL_LINEAR);		// Linear Filtering

            image.UnlockBits(bitmapdata);
            image.Dispose();
        }
    }
}