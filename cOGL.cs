using System;
using System.Collections.Generic;
using System.Windows.Forms;

//2
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

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


        void DrawFigures()
        {

            int j, jj, ii;
            int CONTROL_POINTS_CURVE_NUMBER = 32; // 30
            double[] ctrl_pts_curve_coordinates = new double[CONTROL_POINTS_CURVE_NUMBER * 3];
            const int CONTROL_POINTS_SURF_NUMBER = 16;
            double[,,] ctrl_pts_surf_coordinates = new double[4, 4, 3];
            double[] ctrl_pts_surf_coordinatesOneDimension = new double[4 * 4 * 3];
            double pi = 4.0f * (double)Math.Atan(1.0);
            int how_much_internal_curve_pts = 100;
            int how_much_internal_ptsU = 50;
            int how_much_internal_ptsV = 50;
            double uStep = 1.0f / how_much_internal_ptsU;
            double vStep = 1.0f / how_much_internal_ptsV;
            double u = 0.0;
            double v = 0.0;
            int[] iii = new int[1];


            switch (intRadioOption)
            {
                case 1:
                    GL.glGetIntegerv(GL.GL_MAX_EVAL_ORDER, iii); //neccessary. I check here the MAX ctrl point number: ii=30
                    CONTROL_POINTS_CURVE_NUMBER = iii[0];
                    for (j = 0; j < CONTROL_POINTS_CURVE_NUMBER; j++)
                    {
                        // one full circle with
                        // CONTROL_POINTS_CURVE_NUMBER
                        // points on it
                        ctrl_pts_curve_coordinates[3 * j] = (2 * Math.Cos(j * (2 * pi / CONTROL_POINTS_CURVE_NUMBER)));
                        ctrl_pts_curve_coordinates[3 * j + 1] = (2 * Math.Sin(j * (2 * pi / CONTROL_POINTS_CURVE_NUMBER)));
                        ctrl_pts_curve_coordinates[3 * j + 2] = (8.0 * (((double)j) / CONTROL_POINTS_CURVE_NUMBER - 0.5));
                    }

                    GL.glDisable(GL.GL_TEXTURE_2D);
                    GL.glDisable(GL.GL_LIGHTING);
                    GL.glColor3f(1.0f, 1.0f, 1.0f);

                    //double x,y,z vertex coord
                    //domain of u param in glEvalCoord1f()
                    //0.0--1.0
                    //3 coord for each point-we are in GL_MAP1_VERTEX_3 mode

                    GL.glMap1d(GL.GL_MAP1_VERTEX_3, 0.0, 1.0, 3, CONTROL_POINTS_CURVE_NUMBER, ctrl_pts_curve_coordinates);
                    GL.glEnable(GL.GL_MAP1_VERTEX_3);

                    u = 0.0;
                    GL.glBegin(GL.GL_LINE_STRIP);
                    while (u <= 1.0)
                    {                    //evaluate enabled one-dimensional map 
                        GL.glEvalCoord1d(u);//for u value from (0,1)
                                            //domain

                        u += 1.0 / (how_much_internal_curve_pts * CONTROL_POINTS_CURVE_NUMBER);
                    }
                    GL.glEnd();

                    GL.glEnable(GL.GL_COLOR_MATERIAL);
                    GL.glEnable(GL.GL_LIGHT0);
                    GL.glEnable(GL.GL_LIGHTING);

                    GL.glColor3f(1.0f, 0.0f, 0.0f);
                    for (j = 0; j < CONTROL_POINTS_CURVE_NUMBER; j++)
                    {
                        GL.glTranslated(ctrl_pts_curve_coordinates[3 * j],
                                     ctrl_pts_curve_coordinates[3 * j + 1],
                                     ctrl_pts_curve_coordinates[3 * j + 2]);
                        GLU.gluSphere(obj, 0.1, 16, 16);
                        GL.glTranslated(-ctrl_pts_curve_coordinates[3 * j],
                                     -ctrl_pts_curve_coordinates[3 * j + 1],
                                     -ctrl_pts_curve_coordinates[3 * j + 2]);
                    }
                    break;

                case 2:
                    GL.glGetIntegerv(GL.GL_MAX_EVAL_ORDER, iii); //not neccessary. I check here the MAX ctrl point number: ii=30
                    ii = iii[0];
                    int ijk = 0;
                    for (j = 0; j < 4; j++)
                        for (jj = 0; jj < 4; jj++)
                        {
                            ctrl_pts_surf_coordinates[j, jj, 0] = (double)(2.0f * (j - 1.5f));
                            ctrl_pts_surf_coordinates[j, jj, 1] = (double)(2.0f * (jj - 1.5f));
                            ctrl_pts_surf_coordinates[j, jj, 2] = (double)((j - 1.5f) * (jj - 1.5f));
                            if (j == 2 && jj == 2)
                                ctrl_pts_surf_coordinates[j, jj, 2] = (double)((j - 1.5f) * (jj - 1.5f) + hh);
                            ctrl_pts_surf_coordinatesOneDimension[ijk++] = ctrl_pts_surf_coordinates[j, jj, 0];
                            ctrl_pts_surf_coordinatesOneDimension[ijk++] = ctrl_pts_surf_coordinates[j, jj, 1];
                            ctrl_pts_surf_coordinatesOneDimension[ijk++] = ctrl_pts_surf_coordinates[j, jj, 2];
                        }

                    GL.glEnable(GL.GL_COLOR_MATERIAL);
                    GL.glEnable(GL.GL_LIGHT0);
                    GL.glEnable(GL.GL_LIGHTING);
                    GL.glColor3f(0.5f, 1.0f, 1.0f);

                    //double x,y,z vertex coord
                    //domain of u param in glEvalCoord2f()
                    //0.0--1.0 u parameter
                    //3 coord for each point-we are in GL_MAP2_VERTEX_3 mode
                    //4 ctrl pnts by u param
                    //domain of u param in glEvalCoord1f()
                    //0.0--1.0 v parameter
                    //12=3 coord for each of 4 pts by v param
                    //4 ctrl pts for v param
                    GL.glMap2d(GL.GL_MAP2_VERTEX_3, 0.0, 1.0, 3, 4, 0.0, 1.0, 3 * 4, 4, ctrl_pts_surf_coordinatesOneDimension);
                    GL.glEnable(GL.GL_MAP2_VERTEX_3);

                    GL.glDisable(GL.GL_AUTO_NORMAL);
                    GL.glDisable(GL.GL_NORMALIZE);
                    GL.glMap2d(GL.GL_MAP2_NORMAL, 0.0, 1.0, 3, 4, 0.0, 1.0, 3 * 4, 4, ctrl_pts_surf_coordinatesOneDimension);
                    GL.glEnable(GL.GL_MAP2_NORMAL);

                    if (bChkTexture)
                    {
                        GL.glEnable(GL.GL_TEXTURE_2D);
                        GL.glColor3f(1.0f, 1.0f, 1.0f); // try other combinations
                        GL.glBindTexture(GL.GL_TEXTURE_2D, Textures[0]);
                        GL.glMap2d(GL.GL_MAP2_TEXTURE_COORD_2, 0.0, 1.0, 3, 4, 0.0, 1.0, 3 * 4, 4, ctrl_pts_surf_coordinatesOneDimension);
                        GL.glEnable(GL.GL_MAP2_TEXTURE_COORD_2);
                    }
                    else
                        GL.glDisable(GL.GL_TEXTURE_2D);


                    //must be refore glBegin(GL_QUADS); else no influence
                    GL.glPolygonMode(GL.GL_FRONT_AND_BACK, GL.GL_FILL);//GL_POINT, GL_LINE, GL_FILL
                    GL.glBegin(GL.GL_QUADS);
                    while (v <= 1.0f)
                    {
                        u = 0.0f;
                        while (u <= 1.0f)
                        {
                            //evaluate enabled two-dimensional map 
                            GL.glEvalCoord2d(u, v);
                            GL.glEvalCoord2d(u, v + vStep);
                            GL.glEvalCoord2d(u + uStep, v + vStep);
                            GL.glEvalCoord2d(u + uStep, v);
                            u += uStep;
                        }
                        v += vStep;
                    }
                    GL.glEnd();
                    GL.glEvalCoord2d(0.1, 0.2);
                    double[] coord = new double[4];
                    double[] norm = new double[4];
                    //GL.glVertex3d
                    //GL.glGetDoublev(GL.GL_VE, norm);
                    GL.glGetDoublev(GL.GL_CURRENT_NORMAL, norm);


                    GL.glColor3f(1.0f, 0.0f, 0.0f);
                    for (j = 0; j < 4; j++)
                        for (jj = 0; jj < 4; jj++)
                        {
                            GL.glTranslated(ctrl_pts_surf_coordinates[j, jj, 0],
                                         ctrl_pts_surf_coordinates[j, jj, 1],
                                         ctrl_pts_surf_coordinates[j, jj, 2]);
                            GLU.gluSphere(obj, 0.1, 16, 16);
                            GL.glTranslated(-ctrl_pts_surf_coordinates[j, jj, 0],
                                         -ctrl_pts_surf_coordinates[j, jj, 1],
                                         -ctrl_pts_surf_coordinates[j, jj, 2]);
                        }
                    //anyway
                    GL.glDisable(GL.GL_TEXTURE_2D);
                    break;
            }






        }

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

            DrawAxes();

            DrawFigures();




            GL.glFlush();

            WGL.wglSwapBuffers(m_uint_DC);

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
        public uint[] Textures = new uint[2];

        void GenerateTextures()
        {
            GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA);
            GL.glGenTextures(6, Textures);
            string[] imagesName = { "Flower.bmp", "image.bmp", };
            for (int i = 0; i < 2; i++)
            {
                Bitmap image = new Bitmap(imagesName[i]);
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

    }

}