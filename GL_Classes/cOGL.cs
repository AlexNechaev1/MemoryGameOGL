using System;
using System.Windows.Forms;
using System.Drawing;
using myOpenGL.Properties;
using myOpenGL.Classes;
using myOpenGL.Forms;

namespace OpenGL
{
    public class cOGL
    {
        #region Original Class Members
        public double hh = 0;
        Control m_ControlInstance;
        private int m_WidthValue;
        int m_HeightValue;

        private float[] m_LightFloatArr = new float[] { 35, -0.3f, 5, 1 };
        public float[] ScrollValue = new float[10];
        public float zShift = 0.0f;
        public float yShift = 0.0f;
        public float xShift = 0.0f;
        public float zAngle = 0.0f;
        public float yAngle = 0.0f;
        public float xAngle = 0.0f;
        public int intOptionC = 0;
        double[] AccumulatedRotationsTraslations = new double[16];
        private float[,] m_FloorPointsMatrix = new float[3, 3];

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

            // for Stencil support 
            pfd.cStencilBits = 32;

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
            //GLU.gluPerspective(45, ((double)m_WidthValue) / m_HeightValue, 1.0, 1000.0);
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

            GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA);

            this.controlLightSettings();
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

        public SecretBoxMatrix SecretBoxMatrixInstance { get; private set; }
        public SecretBoxArrow SecretBoxArrowInstance { get; set; }
        private FormGameBoard m_FormGameBoardInstance;
        private uint[] m_TextureUIntArray;
        private bool m_DrawAxisFlag;
        private Axis3D m_StaticAxis3D;
        private Axis3D m_DynamicAxis3D;
        private Reflector m_Reflector;

        private CubeMap m_CubeMapInstance;
        private GLUquadric m_GLUquadricObject;

        // CTOR
        public cOGL(Control i_ControlInstance, FormGameBoard i_FormGameBoardInstance, bool i_DrawAxisFlag)
        {
            this.m_FormGameBoardInstance = i_FormGameBoardInstance;
            m_ControlInstance = i_ControlInstance;
            m_WidthValue = m_ControlInstance.Width;
            m_HeightValue = m_ControlInstance.Height;
            this.SecretBoxMatrixInstance = new SecretBoxMatrix(4, this.m_FormGameBoardInstance);
            InitializeGL();

            this.m_DrawAxisFlag = i_DrawAxisFlag;
            if (this.m_DrawAxisFlag)
            {
                this.m_StaticAxis3D = new Axis3D(new float[] { 10, 10, 10, 1 });
                this.m_DynamicAxis3D = new Axis3D();
            }

            this.m_Reflector = new Reflector();
            this.SecretBoxArrowInstance = new SecretBoxArrow(this.SecretBoxMatrixInstance);

            this.m_CubeMapInstance = new CubeMap();
            this.defineFloorPointsMatrix();

            this.m_GLUquadricObject = GLU.gluNewQuadric();
        }

        // DTOR
        ~cOGL()
        {
            WGL.wglDeleteContext(m_uint_RC);
            GLU.gluDeleteQuadric(this.m_GLUquadricObject);
        }

        // PUBLIC METHODS
        public void Draw()
        {
            if (m_uint_DC == 0 || m_uint_RC == 0)
                return;

            //Color, Depth and Stencil buffers have been added
            GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT | GL.GL_STENCIL_BUFFER_BIT);

            //FULL and COMPLICATED
            //GL.glViewport(0, m_HeightValue / 2, m_WidthValue, m_HeightValue / 2);					
            
            GL.glLoadIdentity();

            // not trivial
            double[] ModelVievMatrixBeforeSpecificTransforms = new double[16];
            double[] CurrentRotationTraslation = new double[16];

            GLU.gluLookAt(30, 10, 5, 1, 5, 6, 0, 1, 0);

            if (this.m_DrawAxisFlag)
            {
                this.m_StaticAxis3D.DrawAxis3D();
            }

            //save current ModelView Matrix values
            //in ModelVievMatrixBeforeSpecificTransforms array
            //ModelView Matrix ========>>>>>> ModelVievMatrixBeforeSpecificTransforms
            GL.glGetDoublev(GL.GL_MODELVIEW_MATRIX, ModelVievMatrixBeforeSpecificTransforms);
            //ModelView Matrix was saved, so
            GL.glLoadIdentity(); // make it identity matrix

            //make transformation in accordance to KeyCode
            float delta;
            if ((intOptionC != 0) && this.m_FormGameBoardInstance.NumericUpDownValueChanged)
            {
                m_FormGameBoardInstance.NumericUpDownValueChanged = false;
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

            GL.glLightfv(GL.GL_LIGHT0, GL.GL_POSITION, this.m_LightFloatArr);
            GL.glColor3f(1, 0, 0);
            GL.glTranslatef(m_LightFloatArr[0], m_LightFloatArr[1], m_LightFloatArr[2]);
            GLUT.gluSphere(this.m_GLUquadricObject, 0.5, 20, 20);
            GL.glTranslatef(-m_LightFloatArr[0], -m_LightFloatArr[1], -m_LightFloatArr[2]);

            this.m_CubeMapInstance.DrawCubeMap();

            GL.glEnable(GL.GL_TEXTURE_2D);
            GL.glBindTexture(GL.GL_TEXTURE_2D, m_TextureUIntArray[0]);

            this.drawShadowWall();
            if (this.m_DrawAxisFlag)
            {
                this.m_DynamicAxis3D.DrawAxis3D();
            }

            // draw real objects
            this.SecretBoxMatrixInstance.DrawSecretBoxMatrix();
            this.SecretBoxArrowInstance.DrawSelectedSecretBoxArrow();

            GL.glPushMatrix();

            GL.glDisable(GL.GL_LIGHTING);

            GL.glTranslatef(0, 0.1f, 0);
            this.MakeShadowMatrix(this.m_FloorPointsMatrix);
            GL.glMultMatrixf(cubeXform);
            GL.glTranslatef(0, -0.1f, 0);

            // draw shadow objects
            this.SecretBoxMatrixInstance.DrawSecretBoxMatrix(true);
            this.SecretBoxArrowInstance.DrawSelectedSecretBoxArrow(true);

            GL.glPopMatrix();

            GL.glEnable(GL.GL_LIGHTING);

            this.m_Reflector.ReflectBeforeSecretBoxMatrixDrawing();

            // draw real objects reflection
            this.SecretBoxMatrixInstance.DrawSecretBoxMatrix();
            this.SecretBoxArrowInstance.DrawSelectedSecretBoxArrow();

            this.m_Reflector.ReflectAfterSecretBoxMatrixDrawing();

            GL.glFlush();
            WGL.wglSwapBuffers(m_uint_DC);
        }

        private void drawShadowWall()
        { 
            GL.glColor3f(1,0,0);
            GL.glBegin(GL.GL_QUADS);

            GL.glVertex3d(-5.01f, 0, -7);
            GL.glVertex3d(-5.01f, 0, 17);
            GL.glVertex3d(-5.01f, 7, 17);
            GL.glVertex3d(-5.01f, 7, -7);

            GL.glEnd();
        }

        #region Light and shadow functions
        private void defineFloorPointsMatrix()
        {
            this.m_FloorPointsMatrix[0, 0] = -5;
            this.m_FloorPointsMatrix[0, 1] = 0;//this.m_CubeMapInstance.TranslatePoint.Y;
            this.m_FloorPointsMatrix[0, 2] = 0;

            this.m_FloorPointsMatrix[1, 0] = -5;
            this.m_FloorPointsMatrix[1, 1] = 1;// this.m_CubeMapInstance.TranslatePoint.Y;
            this.m_FloorPointsMatrix[1, 2] = 0;

            this.m_FloorPointsMatrix[2, 0] = -5;
            this.m_FloorPointsMatrix[2, 1] = 0;// this.m_CubeMapInstance.TranslatePoint.Y;
            this.m_FloorPointsMatrix[2, 2] = 1;
        }

        private void controlLightSettings()
        {
            GL.glEnable(GL.GL_LIGHTING);
            GL.glEnable(GL.GL_LIGHT0);
            GL.glEnable(GL.GL_COLOR_MATERIAL);

            //GL.glLightfv(GL.GL_LIGHT0, GL.GL_POSITION, this.m_LightFloatArr);

            GL.glLightfv(GL.GL_LIGHT0, GL.GL_AMBIENT, new float[] { 0.2f, 0.2f, 0.2f, 1 });
            GL.glLightfv(GL.GL_LIGHT0, GL.GL_DIFFUSE, new float[] { 1f, 1f, 1f, 1 }); // change on runtime
            GL.glLightfv(GL.GL_LIGHT0, GL.GL_SPECULAR, new float[] { 1, 1, 1, 1 });
        }

        float[] planeCoeff = { 1, 1, 1, 1 };
        float[,] ground = new float[3, 3];//{ { 1, 1, -0.5 }, { 0, 1, -0.5 }, { 1, 0, -0.5 } };
        float[,] wall = new float[3, 3];//{ { -15, 3, 0 }, { 15, 3, 0 }, { 15, 3, 15 } };

        float[] cubeXform = new float[16];

        private void MakeShadowMatrix(float[,] points) // points = floor arr
        {
            float[] planeCoeff = new float[4];
            float dot;

            // Find the plane equation coefficients
            // Find the first three coefficients the same way we
            // find a normal.
            calcNormal(points, planeCoeff);

            // Find the last coefficient by back substitutions
            planeCoeff[3] = -(
                (planeCoeff[0] * points[2, 0]) + (planeCoeff[1] * points[2, 1]) +
                (planeCoeff[2] * points[2, 2]));


            // Dot product of plane and light position
            dot = planeCoeff[0] * m_LightFloatArr[0] +
                    planeCoeff[1] * m_LightFloatArr[1] +
                    planeCoeff[2] * m_LightFloatArr[2] +
                    planeCoeff[3];

            // Now do the projection
            // First column
            cubeXform[0] = dot - m_LightFloatArr[0] * planeCoeff[0];
            cubeXform[4] = 0.0f - m_LightFloatArr[0] * planeCoeff[1];
            cubeXform[8] = 0.0f - m_LightFloatArr[0] * planeCoeff[2];
            cubeXform[12] = 0.0f - m_LightFloatArr[0] * planeCoeff[3];

            // Second column
            cubeXform[1] = 0.0f - m_LightFloatArr[1] * planeCoeff[0];
            cubeXform[5] = dot - m_LightFloatArr[1] * planeCoeff[1];
            cubeXform[9] = 0.0f - m_LightFloatArr[1] * planeCoeff[2];
            cubeXform[13] = 0.0f - m_LightFloatArr[1] * planeCoeff[3];

            // Third Column
            cubeXform[2] = 0.0f - m_LightFloatArr[2] * planeCoeff[0];
            cubeXform[6] = 0.0f - m_LightFloatArr[2] * planeCoeff[1];
            cubeXform[10] = dot - m_LightFloatArr[2] * planeCoeff[2];
            cubeXform[14] = 0.0f - m_LightFloatArr[2] * planeCoeff[3];

            // Fourth Column
            cubeXform[3] = 0.0f - m_LightFloatArr[3] * planeCoeff[0];
            cubeXform[7] = 0.0f - m_LightFloatArr[3] * planeCoeff[1];
            cubeXform[11] = 0.0f - m_LightFloatArr[3] * planeCoeff[2];
            cubeXform[15] = dot - m_LightFloatArr[3] * planeCoeff[3];
        }

        const int x = 0;
        const int y = 1;
        const int z = 2;

        private void calcNormal(float[,] v, float[] outp)
        {
            float[] v1 = new float[3];
            float[] v2 = new float[3];

            // Calculate two vectors from the three points
            v1[x] = v[0, x] - v[1, x];
            v1[y] = v[0, y] - v[1, y];
            v1[z] = v[0, z] - v[1, z];

            v2[x] = v[1, x] - v[2, x];
            v2[y] = v[1, y] - v[2, y];
            v2[z] = v[1, z] - v[2, z];

            // Take the cross product of the two vectors to get
            // the normal vector which will be stored in out
            outp[x] = v1[y] * v2[z] - v1[z] * v2[y];
            outp[y] = v1[z] * v2[x] - v1[x] * v2[z];
            outp[z] = v1[x] * v2[y] - v1[y] * v2[x];

            // Normalize the vector (shorten length to one)
            ReduceToUnit(outp);
        }

        private void ReduceToUnit(float[] vector)
        {
            float length;

            // Calculate the length of the vector		
            length = (float)Math.Sqrt((vector[0] * vector[0]) +
                                (vector[1] * vector[1]) +
                                (vector[2] * vector[2]));

            // Keep the program from blowing up by providing an exceptable
            // value for vectors that may calculated too close to zero.
            if (length == 0.0f)
                length = 1.0f;

            // Dividing each element by the length will result in a
            // unit normal vector.
            vector[0] /= length;
            vector[1] /= length;
            vector[2] /= length;
        }
        #endregion
    }
}