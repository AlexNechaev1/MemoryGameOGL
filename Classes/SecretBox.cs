using myOpenGL.Enums;
using myOpenGL.Structs;
using OpenGL;
using System;

namespace myOpenGL.Classes
{
    public class SecretBox
    {
        #region CLASS MEMBERS
        private float m_MaxElevationValue = 1;
        private float m_MinElevationValue = 0;
        private float m_ElevationDeltaValue = 0.01f;
        private float m_CurrentElevationValue = 0;
        private const float k_BoxSize = 1;
        private bool m_AddToCurrentElevationValueFlag = true;
        private bool m_HasReachedMaxHeight = false;

        public Action CheckIfPlayerStepsAreCorrectAction { get; set; }

        #region Box state
        public eSecretBoxDrawState SecretBoxDrawState { get; set; }
        public bool IsBoxOpen { get; private set; }
        #endregion

        #region Case Angels
        private float m_TopCaseAngle = 0;
        private float m_RightCaseAngle = 0;
        private float m_LeftCaseAngle = 0;
        private float m_FrontCaseAngle = 0;
        private float m_BackCaseAngle = 0;
        #endregion

        #region Base Angle
        private float m_CurrentAngle = 0.0f;
        private float m_AngleDelta = 1.0f;
        private float m_BoxRotateAngle = 0.0f;
        #endregion

        #endregion

        public bool IsSecretBoxVisible { get; private set; }
        public bool IsSelectedSecretBox { get; private set; }
        public Point3D TranslatePoint { get; private set; }

        public Color HiddenObjectColor { get; set; }
        private GLUquadric m_GLUquadricObject;
       
        // CTOR
        public SecretBox(Point3D i_TranslatePoint)
        {
            this.HiddenObjectColor = new Color(1, 1, 1);
            this.TranslatePoint = i_TranslatePoint;
            this.IsSecretBoxVisible = true;
            this.IsSelectedSecretBox = false;
            this.m_GLUquadricObject = GLU.gluNewQuadric();

            this.SecretBoxDrawState = eSecretBoxDrawState.None;
            this.IsBoxOpen = false;
        }

        ~SecretBox()
        {
            GLU.gluDeleteQuadric(this.m_GLUquadricObject);
        }

        // PUBLIC METHODS
        #region Drawing methods
        private void drawSecretBox()
        {
            GL.glColor3f(1, 1, 1);

            GL.glPushMatrix();
            calculateAddValue();
            openBoxRotateAngle();
            closeBoxRotateAngle();

            spinBox();

            GL.glTranslatef(this.TranslatePoint.X, this.TranslatePoint.Y, this.TranslatePoint.Z);
            GL.glTranslatef(0.0f, this.m_CurrentElevationValue, 0);

            this.preformSecretBoxDrawing();

            GL.glPopMatrix();
        }

        private void drawHiddenObject()
        {
            GL.glPushMatrix();

            GL.glColor3f(this.HiddenObjectColor.R, this.HiddenObjectColor.G, this.HiddenObjectColor.B);
            GL.glTranslatef(this.TranslatePoint.X + 0.5f, this.TranslatePoint.Y + 0.5f, this.TranslatePoint.Z + 0.5f);
            GL.glTranslatef(0.0f, this.m_CurrentElevationValue, 0);
            this.preformHiddenObjectDrawing();

            GL.glPopMatrix();
        }

        private void preformSecretBoxDrawing()
        {
            GL.glPushMatrix();

            GL.glTranslatef(0.5f, 0.0f, 0.5f);
            GL.glRotatef(m_BoxRotateAngle, 0, 1, 0);
            GL.glTranslatef(-0.5f, 0.0f, -0.5f);

            drawBackCase();
            drawLeftCase();
            drawRightCase();
            drawFrontCase();
            drawBottomCase();
            drawTopCase();

            GL.glPopMatrix();
        }

        private void preformHiddenObjectDrawing()
        {
            GL.glPushMatrix();
            GLU.gluSphere(this.m_GLUquadricObject, 0.4, 10, 10);
            GL.glPopMatrix();
        }
        #endregion

        public void openBoxRotateAngle()
        {
            if (this.SecretBoxDrawState == eSecretBoxDrawState.OpenSecretBox) 
            {
                if (this.m_CurrentAngle < 90)
                {
                    this.m_CurrentAngle += this.m_AngleDelta*(2.2f);
                    changeBoxState();
                }
                else
                {
                    this.SecretBoxDrawState = eSecretBoxDrawState.None;
                    this.IsBoxOpen = true;
                }
                
            }
        }

        public void closeBoxRotateAngle()
        {
            if (this.SecretBoxDrawState == eSecretBoxDrawState.CloseSecretBox)
            {
                if(this.m_CurrentAngle > 0)
                {
                    this.m_CurrentAngle -= this.m_AngleDelta * (2.2f);
                    changeBoxState();
                }
                else
                {
                    this.SecretBoxDrawState = eSecretBoxDrawState.None;
                    this.IsBoxOpen = false;
                }
            }
        }

        public void drawSecretBoxWithItsContent()
        {
            this.drawSecretBox();
            this.drawHiddenObject();
        }

        public void SelectThisSecretBox()
        {
            this.IsSelectedSecretBox = true;
            this.m_AddToCurrentElevationValueFlag = true;
            this.m_MaxElevationValue = 2.5f;
            this.m_ElevationDeltaValue = 0.05f;
        }

        public void ForgetThisSecretBox()
        {
            this.IsSelectedSecretBox = false;
            this.m_HasReachedMaxHeight = false;
            this.m_MaxElevationValue = 1;
            this.m_ElevationDeltaValue = 0.01f;

            closeBoxRotateAngle();
        }

        public void spinBox()
        {
            m_BoxRotateAngle = m_BoxRotateAngle + 5;
            m_BoxRotateAngle = m_BoxRotateAngle % 360;
        }

        // PRIVATE METHODS
        private void Update(int i_CaseSide, float i_Angle)
        {
            switch (i_CaseSide)
            {
                case 1: //Top case
                    m_TopCaseAngle = i_Angle;
                    break;
                case 2: //Right case
                    m_RightCaseAngle = i_Angle;
                    break;
                case 3: //Left case
                    m_LeftCaseAngle = i_Angle;
                    break;
                case 4: //Front case
                    m_FrontCaseAngle = i_Angle;
                    break;
                case 5: //Back Case
                    m_BackCaseAngle = i_Angle;
                    break;
            }

        }

        private void calculateAddValue()
        {
            if (this.m_AddToCurrentElevationValueFlag)
            {
                this.m_CurrentElevationValue += m_ElevationDeltaValue;
                if (this.m_CurrentElevationValue > m_MaxElevationValue)
                {
                    this.m_AddToCurrentElevationValueFlag = false;
                    if (this.IsSelectedSecretBox)
                    {
                        this.m_HasReachedMaxHeight = true;
                        Console.WriteLine(this.TranslatePoint.ToString());
                        CheckIfPlayerStepsAreCorrectAction.Invoke();
                    }
                }
            }
            else
            {
                if (!(this.IsSelectedSecretBox && this.m_HasReachedMaxHeight))
                {
                    this.m_CurrentElevationValue -= m_ElevationDeltaValue;
                    if (this.m_CurrentElevationValue < m_MinElevationValue)
                    {
                        this.m_AddToCurrentElevationValueFlag = true;
                    }
                }
            }
        }

        public void changeBoxState()
        {
            //OPENS TOP CASE
            this.Update(1, (-3) * m_CurrentAngle);
            //OPENS RIGHT CASE
            this.Update(2, (-1) * m_CurrentAngle);
            //OPENS LEFT CASE
            this.Update(3, m_CurrentAngle);
            //OPENS FRONT CASE
            this.Update(4, m_CurrentAngle);
            //OPENS BACK CASE
            this.Update(5, (-1)*m_CurrentAngle);
        }
      

        #region SecretBox drawing methods
        private void drawBackCase()
        {
            GL.glPushMatrix();

            GL.glRotatef(m_BackCaseAngle+1, 1, 0, 0);

            GL.glBegin(GL.GL_QUADS);

            GL.glTexCoord2f(0.5f, 0.0f);
            GL.glVertex3f(0, 0, 0);

            GL.glTexCoord2f(0.0f, 0.0f);
            GL.glVertex3f(1, 0, 0);

            GL.glTexCoord2f(0.0f, 1.0f);
            GL.glVertex3f(1, 1, 0);

            GL.glTexCoord2f(0.5f, 1.0f);
            GL.glVertex3f(0, 1, 0);

            GL.glEnd();

            GL.glPopMatrix();
        }

        private void drawLeftCase()
        {
            GL.glPushMatrix();

            GL.glRotatef(m_LeftCaseAngle, 0, 0, 1);

            GL.glBegin(GL.GL_QUADS);

            GL.glTexCoord2f(0.0f, 0.0f);
            GL.glVertex3f(0, 0, 0);

            GL.glTexCoord2f(0.5f, 0.0f);
            GL.glVertex3f(0, 0, 1);

            GL.glTexCoord2f(0.5f, 1.0f);
            GL.glVertex3f(0, 1, 1);

            GL.glTexCoord2f(0.0f, 1.0f);
            GL.glVertex3f(0, 1, 0);

            GL.glEnd();

            GL.glPopMatrix();
        }

        private void drawRightCase()
        {
            GL.glPushMatrix();

            GL.glTranslatef(1.0f, 0.0f, 0.0f);
            GL.glRotatef(m_RightCaseAngle, 0, 0, 1);
            GL.glTranslatef(-1.0f, 0.0f, 0.0f);

            GL.glBegin(GL.GL_QUADS);

            GL.glTexCoord2f(0.5f, 0.0f);
            GL.glVertex3f(1, 0, 0);

            GL.glTexCoord2f(0.0f, 0.0f);
            GL.glVertex3f(1, 0, 1);

            GL.glTexCoord2f(0.0f, 1.0f);
            GL.glVertex3f(1, 1, 1);

            GL.glTexCoord2f(0.5f, 1.0f);
            GL.glVertex3f(1, 1, 0);


            GL.glEnd();

            GL.glPopMatrix();
        }

        private void drawFrontCase()
        {
            GL.glPushMatrix();

            GL.glTranslatef(0.0f, 0.0f, 1.0f);
            GL.glRotatef(m_FrontCaseAngle, 1, 0, 0);
            GL.glTranslatef(0.0f, 0.0f, -1.0f);

            GL.glBegin(GL.GL_QUADS);

            GL.glTexCoord2f(0.0f, 0.0f);
            GL.glVertex3f(0, 0, 1);

            GL.glTexCoord2f(0.5f, 0.0f);
            GL.glVertex3f(1, 0, 1);

            GL.glTexCoord2f(0.5f, 1.0f);
            GL.glVertex3f(1, 1, 1);

            GL.glTexCoord2f(0.0f, 1.0f);
            GL.glVertex3f(0, 1, 1);

            GL.glEnd();

            GL.glPopMatrix();
        }

        private void drawBottomCase()
        {
            GL.glBegin(GL.GL_QUADS);

            GL.glTexCoord2f(0.5f, 0.0f);
            GL.glVertex3f(0, 0, 0);

            GL.glTexCoord2f(0.5f, 1.0f);
            GL.glVertex3f(1, 0, 0);

            GL.glTexCoord2f(1.0f, 1.0f);
            GL.glVertex3f(1, 0, 1);

            GL.glTexCoord2f(1.0f, 0.0f);
            GL.glVertex3f(0, 0, 1);

            GL.glEnd();
        }

        private void drawTopCase()
        {
            GL.glPushMatrix();

            GL.glRotatef(m_BackCaseAngle, 1, 0, 0);

            GL.glTranslatef(0.0f, 1f, 0);
            GL.glRotatef(m_TopCaseAngle, 1, 0, 0);
            GL.glTranslatef(0.0f, -1, 0);

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

            GL.glPopMatrix();
        }
        #endregion
    }
}