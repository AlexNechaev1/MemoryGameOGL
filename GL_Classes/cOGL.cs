using System;
using System.Windows.Forms;
using System.Drawing;
using myOpenGL.Properties;
using myOpenGL.Classes;
using myOpenGL.Structs;
using System.Collections.Generic;
using myOpenGL;

namespace OpenGL
{
    class cOGL
    {
        #region Original Class Members
        public double hh = 0;
        Control m_ControlInstance;
        private int m_WidthValue;
        int m_HeightValue;

        public float[] ScrollValue = new float[10];
        public float zShift = 0.0f;
        public float yShift = 0.0f;
        public float xShift = 0.0f;
        public float zAngle = 0.0f;
        public float yAngle = 0.0f;
        public float xAngle = 0.0f;
        public int intOptionC = 0;
        double[] AccumulatedRotationsTraslations = new double[16];

        uint m_uint_HWND = 0;
        uint m_uint_DC = 0;
        uint m_uint_RC = 0;
        #endregion

        #region Original Class Properties
        public uint HWND
        {
            get { return m_uint_HWND; }
        }

        public uint DC
        {
            get { return m_uint_DC; }
        }

        public uint RC
        {
            get { return m_uint_RC; }
        }
        #endregion

        #region Original GL Methods
        protected virtual void InitializeGL()
        {
            m_uint_HWND = (uint)m_ControlInstance.Handle.ToInt32();
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
            m_WidthValue = m_ControlInstance.Width;
            m_HeightValue = m_ControlInstance.Height;
            GL.glViewport(0, 0, m_WidthValue, m_HeightValue);
            Draw();
        }

        protected virtual void initRenderingGL()
        {
            if (m_uint_DC == 0 || m_uint_RC == 0)
                return;
            if (this.m_WidthValue == 0 || this.m_HeightValue == 0)
                return;
            GL.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            GL.glEnable(GL.GL_DEPTH_TEST);
            GL.glDepthFunc(GL.GL_LEQUAL);

            GL.glViewport(0, 0, this.m_WidthValue, this.m_HeightValue);
            GL.glMatrixMode(GL.GL_PROJECTION);
            GL.glLoadIdentity();

            //nice 3D
            GLU.gluPerspective(45.0, 1.0, 0.4, 100.0);


            GL.glMatrixMode(GL.GL_MODELVIEW);
            GL.glLoadIdentity();

            //save the current MODELVIEW Matrix (now it is Identity)
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, AccumulatedRotationsTraslations);

            //! TEXTURE 1a 
            generateTextures();
            //! TEXTURE 1b 
        }

        private void generateTextures()
        {
            GL.glEnable(GL.GL_TEXTURE_2D);

            m_TextureUIntArray = new uint[1];		// storage for texture

            Bitmap image = new Bitmap(Resources.cubeTexture);
            image.RotateFlip(RotateFlipType.RotateNoneFlipY); //Y axis in Windows is directed downwards, while in OpenGL-upwards
            System.Drawing.Imaging.BitmapData bitmapdata;
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

            bitmapdata = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            GL.glGenTextures(1, m_TextureUIntArray);
            GL.glBindTexture(GL.GL_TEXTURE_2D, m_TextureUIntArray[0]);
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
        #endregion

        private List<SecretBox> m_SecretBoxesList = new List<SecretBox>();
        private Form1 m_Form1Instance;
        private uint[] m_TextureUIntArray;
        private Axis3D m_StaticAxis3D;
        private Axis3D m_DynamicAxis3D;

        // CTOR
        public cOGL(Control i_ControlInstance, Form1 i_Form1Instance)
        {
            this.m_Form1Instance = i_Form1Instance;
            m_ControlInstance = i_ControlInstance;
            m_WidthValue = m_ControlInstance.Width;
            m_HeightValue = m_ControlInstance.Height;
            InitializeGL();

            this.m_StaticAxis3D = new Axis3D(new float[] { 10, 10, 10, 1 });
            this.m_DynamicAxis3D = new Axis3D();
            this.fillSecretBoxesList(5);
        }

        // DTOR
        ~cOGL()
        {
            WGL.wglDeleteContext(m_uint_RC);
        }

        // PUBLIC METHODS
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

            this.m_StaticAxis3D.DrawAxis3D();

            //save current ModelView Matrix values
            //in ModelVievMatrixBeforeSpecificTransforms array
            //ModelView Matrix ========>>>>>> ModelVievMatrixBeforeSpecificTransforms
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, ModelVievMatrixBeforeSpecificTransforms);
            //ModelView Matrix was saved, so
            GL.glLoadIdentity(); // make it identity matrix

            //make transformation in accordance to KeyCode
            float delta;
            if ((intOptionC != 0) && (this.m_Form1Instance.NumericUpDownValueChanged))
            {
                m_Form1Instance.NumericUpDownValueChanged = false;
                delta = 5.0f * Math.Abs(intOptionC) / intOptionC; // signed 5
                Console.WriteLine("delta=" + delta);
                Console.WriteLine("intOptionC=" + intOptionC);

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
            GL.glBindTexture(GL.GL_TEXTURE_2D, m_TextureUIntArray[0]);

            this.m_DynamicAxis3D.DrawAxis3D();
            GL.glColor3f(1.0f, 1.0f, 1.0f);

            //test
            drawSecretBoxesList();
            //test

            GL.glFlush();
            WGL.wglSwapBuffers(m_uint_DC);
            GL.glDisable(GL.GL_TEXTURE_2D);
        }

        // PRIVATE METHODS
        private void fillSecretBoxesList(int i_AmountOfSecretBoxesToCreate)
        {
            for (int i = 0; i < i_AmountOfSecretBoxesToCreate; i++)
            {
                this.m_SecretBoxesList.Add(new SecretBox(new Point3D(0.0f + i * 1.5f, 0.0f, 0.5f)));
            }
        }

        private void drawSecretBoxesList()
        {
            foreach (SecretBox secretBox in this.m_SecretBoxesList)
            {
                secretBox.DrawSecretBox();
            }
        }
    }
}