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
        private const int k_ComputerThinkingTimerInterval = 5000;
        private GameBoardDimensions m_CurrentGameBoardDimensions;
        private GameLogicComponent m_GameLogicComponent;
        private Player m_PlayerOne;
        private Player m_PlayerTwo;
        private Player m_CurrentPlayerPointer;
        private int m_FirstPlayerStepsCounter = 0;
        private int m_SecondPlayerStepsCounter = 0;
        private ePlayerStepsStates m_PlayerStepsState;
        private Timer m_ComputerThinkingTimer;

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

            this.m_ComputerThinkingTimer = new Timer();
            this.m_ComputerThinkingTimer.Interval = k_ComputerThinkingTimerInterval;
            this.m_ComputerThinkingTimer.Tick += m_ComputerThinkingTimer_Tick;
            this.m_PlayerStepsState = ePlayerStepsStates.FirstPlayerStep;
            this.m_CurrentGameBoardDimensions = new GameBoardDimensions(4, 4);
            this.m_PlayerOne = new Player("Player one", true, Color.FromArgb(0, 192, 0));
            this.m_PlayerTwo = new Player("Computer", false, Color.FromArgb(148, 0, 211));
            this.m_CurrentPlayerPointer = this.m_PlayerOne;
            this.m_GameLogicComponent = new GameLogicComponent(this.m_CurrentGameBoardDimensions, this.m_PlayerOne, this.m_PlayerTwo);
            this.cGL.SecretBoxMatrixInstance.ColorHiddenObjectsInSecretBoxesMatrix(this.m_GameLogicComponent);
        }

        private void m_ComputerThinkingTimer_Tick(object sender, EventArgs e)
        {
            this.preformComputerTurn();
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

        #region Events
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

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.preformATurn();
            }
        }
        #endregion

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

        private void switchValueForPlayerStepsState()
        {
            if (this.m_PlayerStepsState == ePlayerStepsStates.FirstPlayerStep)
            {
                this.m_PlayerStepsState = ePlayerStepsStates.SecondPlayerStep;
            }
            else
            {
                this.m_PlayerStepsState = ePlayerStepsStates.FirstPlayerStep;
            }
        }

        private void preformATurn()
        {
            if (this.cGL.SecretBoxMatrixInstance.CheckIfSecretBoxIsNotShown(this.m_GameLogicComponent))
            {
                if (this.m_CurrentPlayerPointer == this.m_PlayerOne)
                {
                    this.cGL.SecretBoxMatrixInstance.SetXAndYValuesAsCurrentPlayerStep(this.m_CurrentPlayerPointer, this.m_PlayerStepsState);
                }

                this.switchCardStatus();// switch card visibility step by step
                this.switchValueForPlayerStepsState();
                this.cGL.SecretBoxMatrixInstance.SelectTheCurrentSecretBox();
            }
        }

        private void preformComputerTurn()
        {
            bool restartTimerFlag = false;
            this.m_ComputerThinkingTimer.Stop();
            this.m_SecondPlayerStepsCounter++;

            if (this.m_SecondPlayerStepsCounter == 1)
            {
                this.m_CurrentPlayerPointer.FirstStep = this.m_GameLogicComponent.GetRandomMachineStep();
                this.cGL.SecretBoxMatrixInstance.SelectCurrentSecretBoxByRandomPlayerStep(this.m_CurrentPlayerPointer.FirstStep);
                restartTimerFlag = !this.m_GameLogicComponent.CheckIfGameIsFinished();
            }
            else
            {
                this.m_SecondPlayerStepsCounter = 0;
                this.m_CurrentPlayerPointer.SecondStep = this.m_GameLogicComponent.GetMachineSecondStep(this.m_CurrentPlayerPointer.FirstStep);
                this.cGL.SecretBoxMatrixInstance.SelectCurrentSecretBoxByRandomPlayerStep(this.m_CurrentPlayerPointer.SecondStep);
            }

            this.preformATurn();
            if (restartTimerFlag)
            {
                this.m_ComputerThinkingTimer.Start();
            }
        }

        private void switchCardStatus()
        {
            PlayerStep? currentPlayerStep = null;

            if (this.m_PlayerStepsState == ePlayerStepsStates.FirstPlayerStep)
            {
                currentPlayerStep = this.m_CurrentPlayerPointer.FirstStep;
            }
            else
            {
                currentPlayerStep = this.m_CurrentPlayerPointer.SecondStep;
            }

            this.m_GameLogicComponent.SwitchCardStatus(currentPlayerStep.Value.RowIndex, currentPlayerStep.Value.ColumnIndex);
        }

        #region End game methods
        private bool checkIfGameFinished()
        {
            bool isGameFinished = this.m_GameLogicComponent.CheckIfGameIsFinished();

            if (isGameFinished)
            {
                string messageToShow = getWinnerMessage();
                messageToShow = string.Format("{0}{1}{2}", messageToShow, Environment.NewLine, "Would you like to play another game?");
                DialogResult dialogResult = MessageBox.Show(messageToShow, "Game Over", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    /*this.m_GameLogicComponent.ResetGameSettings();
                    this.m_GameLogicComponent.CreateAndFillMemoryBoard();
                    setMatchingPicturesInIndexPictureBoxesMatrix();
                    resetGameBoard();
                    setTextInLables();*/
                    this.Dispose();
                    this.Close();
                }
                else if (dialogResult == DialogResult.No)
                {
                    this.Dispose();
                    this.Close();
                }
            }

            return isGameFinished;
        }

        private string getWinnerMessage()
        {
            string stringToReturn = string.Empty;
            Player winningPlayerPointer = null;

            this.m_GameLogicComponent.DecideWinningPlayer(ref winningPlayerPointer);
            if (winningPlayerPointer == null)
            {
                stringToReturn = "We have a tie here!";
            }
            else
            {
                stringToReturn = string.Format("{0}, you are the winner!", winningPlayerPointer.PlayerName);
            }

            return stringToReturn;
        }
        #endregion

        public void CheckIfPlayerStepsAreCorrect()
        {
            if (this.m_CurrentPlayerPointer == this.m_PlayerOne)
            {
                checkIfHumanPlayerStepsAreCorrect();
            }
            else
            {
                checkIfComputerPlayerStepsAreCorrect();
            }

            if (this.checkIfGameFinished())
            {
                // reset the game???
            }
        }

        private void checkIfHumanPlayerStepsAreCorrect()
        {
            this.m_FirstPlayerStepsCounter++;
            if (this.m_FirstPlayerStepsCounter == 2)
            {
                this.m_FirstPlayerStepsCounter = 0;
                if (this.m_GameLogicComponent.CheckCardMatch(this.m_CurrentPlayerPointer.FirstStep, this.m_CurrentPlayerPointer.SecondStep))
                {
                    this.m_GameLogicComponent.AddPoint();
                    Console.Beep();
                }
                else
                {
                    this.prepareForOtherPlayerMoves();
                    this.m_ComputerThinkingTimer.Start();
                }
            }
        }

        private void checkIfComputerPlayerStepsAreCorrect()
        {
            if (this.m_SecondPlayerStepsCounter == 0)
            {
                if (this.m_GameLogicComponent.CheckCardMatch(this.m_CurrentPlayerPointer.FirstStep, this.m_CurrentPlayerPointer.SecondStep))
                {
                    this.m_GameLogicComponent.AddPoint();
                    Console.Beep();
                    this.m_ComputerThinkingTimer.Start();
                }
                else
                {
                    this.prepareForOtherPlayerMoves();
                }
            }
        }

        private void prepareForOtherPlayerMoves()
        {
            bool drawSelectSecretBoxArrowFlag;

            this.m_GameLogicComponent.SaveMachineStepInList(this.m_GameLogicComponent.CurrentPlayerPointer.FirstStep);
            this.m_GameLogicComponent.SaveMachineStepInList(this.m_GameLogicComponent.CurrentPlayerPointer.SecondStep);
            this.cGL.SecretBoxMatrixInstance.ForgetSecretBoxByGivenPlayerStep(this.m_CurrentPlayerPointer.FirstStep);
            this.cGL.SecretBoxMatrixInstance.ForgetSecretBoxByGivenPlayerStep(this.m_CurrentPlayerPointer.SecondStep);
            this.m_GameLogicComponent.SwitchCardStatus(this.m_CurrentPlayerPointer.FirstStep);// switch both cards visibility at once
            this.m_GameLogicComponent.SwitchCardStatus(this.m_CurrentPlayerPointer.SecondStep);// switch both cards visibility at once
            this.m_GameLogicComponent.SwitchTurn();
            this.m_CurrentPlayerPointer = this.m_GameLogicComponent.CurrentPlayerPointer;
            drawSelectSecretBoxArrowFlag = this.m_CurrentPlayerPointer == this.m_PlayerOne;
            this.cGL.SecretBoxArrowInstance.DrawSecretBoxArrowFlag = drawSelectSecretBoxArrowFlag;
        }
    }
}