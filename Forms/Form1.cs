using System;
using System.Drawing;
using System.Windows.Forms;
using MemoryGameLogic;
using myOpenGL.Enums;
using OpenGL;

namespace myOpenGL
{
    public partial class Form1 : Form
    {
        cOGL cGL;
        private GameBoardDimensions m_CurrentGameBoardDimensions;
        private GameLogicComponent m_GameLogicComponent;
        private Player m_PlayerOne;
        private Player m_PlayerTwo;
        private Player m_CurrentPlayerPointer;
        private int m_PlayerStepsCounter = 0;

        public Form1()
        {
            #region Original CTOR code
            InitializeComponent();
            cGL = new cOGL(panel1, this);
            //apply the bars values as cGL.ScrollValue[..] properties 
            //!!!
            hScrollBarScroll(hScrollBar1, null);
            hScrollBarScroll(hScrollBar2, null);
            hScrollBarScroll(hScrollBar3, null);
            hScrollBarScroll(hScrollBar4, null);
            hScrollBarScroll(hScrollBar5, null);
            hScrollBarScroll(hScrollBar6, null);
            hScrollBarScroll(hScrollBar7, null);
            hScrollBarScroll(hScrollBar8, null);
            hScrollBarScroll(hScrollBar9, null);
            #endregion

            this.m_CurrentGameBoardDimensions = new GameBoardDimensions(4, 4);
            this.m_PlayerOne = new Player("Player one", true, Color.FromArgb(0, 192, 0));
            this.m_PlayerTwo = new Player("Computer", false, Color.FromArgb(148, 0, 211));
            this.m_CurrentPlayerPointer = this.m_PlayerOne;
            this.m_GameLogicComponent = new GameLogicComponent(this.m_CurrentGameBoardDimensions, this.m_PlayerOne, this.m_PlayerTwo);
            this.cGL.SecretBoxMatrixInstance.ColorHiddenObjectsInSecretBoxesMatrix(this.m_GameLogicComponent);
        }

        #region Original methods
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            cGL.Draw();
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            cGL.OnResize();
        }

        private void hScrollBarScroll(object sender, ScrollEventArgs e)
        {
            cGL.intOptionC = 0;
            HScrollBar hb = (HScrollBar)sender;
            int n = int.Parse(hb.Name.Substring(hb.Name.Length - 1));
            cGL.ScrollValue[n - 1] = (hb.Value - 100) / 10.0f;
            if (e != null)
            {
                cGL.Draw();
            }
        }

        public float[] oldPos = new float[7];

        public bool NumericUpDownValueChanged { get; set; }

        private void numericUpDownValueChanged(object sender, EventArgs e)
        {
            NumericUpDownValueChanged = true;
            NumericUpDown nUD = (NumericUpDown)sender;
            int i = int.Parse(nUD.Name.Substring(nUD.Name.Length - 1));
            int pos = (int)nUD.Value;
            switch (i)
            {
                case 1:
                    if (pos > oldPos[i - 1])
                    {
                        cGL.xShift += 0.25f;
                        cGL.intOptionC = 4;
                    }
                    else
                    {
                        cGL.xShift -= 0.25f;
                        cGL.intOptionC = -4;
                    }
                    break;
                case 2:
                    if (pos > oldPos[i - 1])
                    {
                        cGL.yShift += 0.25f;
                        cGL.intOptionC = 5;
                    }
                    else
                    {
                        cGL.yShift -= 0.25f;
                        cGL.intOptionC = -5;
                    }
                    break;
                case 3:
                    if (pos > oldPos[i - 1])
                    {
                        cGL.zShift += 0.25f;
                        cGL.intOptionC = 6;
                    }
                    else
                    {
                        cGL.zShift -= 0.25f;
                        cGL.intOptionC = -6;
                    }
                    break;
                case 4:
                    if (pos > oldPos[i - 1])
                    {
                        cGL.xAngle += 5;
                        cGL.intOptionC = 1;
                    }
                    else
                    {
                        cGL.xAngle -= 5;
                        cGL.intOptionC = -1;
                    }
                    break;
                case 5:
                    if (pos > oldPos[i - 1])
                    {
                        cGL.yAngle += 5;
                        cGL.intOptionC = 2;
                    }
                    else
                    {
                        cGL.yAngle -= 5;
                        cGL.intOptionC = -2;
                    }
                    break;
                case 6:
                    if (pos > oldPos[i - 1])
                    {
                        cGL.zAngle += 5;
                        cGL.intOptionC = 3;
                    }
                    else
                    {
                        cGL.zAngle -= 5;
                        cGL.intOptionC = -3;
                    }
                    break;
            }
            cGL.Draw();
            oldPos[i - 1] = pos;

        }

        private void hScrollBar10_Scroll(object sender, ScrollEventArgs e)
        {
            HScrollBar hb = (HScrollBar)sender;
            cGL.hh = hb.Value / 5.0;
            cGL.Draw();
        }

        private void secretBoxElevationTimer_Tick(object sender, EventArgs e)
        {
            cGL.Draw();
        }
        #endregion

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char pressedKey = e.KeyChar;
            e.Handled = true;

            if (this.checkIfPressedKeyIsMovementKey(pressedKey))
            {
                ePossibleMoveInSecretBoxMatrix? possibleMoveInSecretBoxMatrix = null;
                switch (pressedKey)
                {
                    case 'w':
                    case 'W':
                        possibleMoveInSecretBoxMatrix = ePossibleMoveInSecretBoxMatrix.MoveUpInSecretBoxMatrix;
                        break;
                    case 'a':
                    case 'A':
                        possibleMoveInSecretBoxMatrix = ePossibleMoveInSecretBoxMatrix.MoveLeftInSecretBoxMatrix;
                        break;
                    case 's':
                    case 'S':
                        possibleMoveInSecretBoxMatrix = ePossibleMoveInSecretBoxMatrix.MoveDownInSecretBoxMatrix;
                        break;
                    case 'd':
                    case 'D':
                        possibleMoveInSecretBoxMatrix = ePossibleMoveInSecretBoxMatrix.MoveRightInSecretBoxMatrix;
                        break;
                }

                cGL.SecretBoxMatrixInstance.MoveSelectedSecretBoxArrow(possibleMoveInSecretBoxMatrix);
            }
        }

        private bool checkIfPressedKeyIsMovementKey(char i_KeyToCheck)
        {
            bool result = false;
            i_KeyToCheck = Char.ToLower(i_KeyToCheck);

            result = i_KeyToCheck == 's';
            result |= i_KeyToCheck == 'a';
            result |= i_KeyToCheck == 'd';
            result |= i_KeyToCheck == 'w';

            return result;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.preformATurn();
            }
        }

        private void preformATurn()
        {
            this.m_PlayerStepsCounter++;
            this.cGL.SecretBoxMatrixInstance.SelectTheCurrentSecretBox();
            if (this.m_PlayerStepsCounter == 1)
            {
                this.cGL.SecretBoxMatrixInstance.SetXAndYValuesAsCurrentPlayerStep(this.m_CurrentPlayerPointer, true);
            }
            else
            {
                this.m_PlayerStepsCounter = 0;
                this.cGL.SecretBoxMatrixInstance.SetXAndYValuesAsCurrentPlayerStep(this.m_CurrentPlayerPointer, false);
            }
        }
    }
}