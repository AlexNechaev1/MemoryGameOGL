using myOpenGL.Enums;
using myOpenGL.Structs;
using System;
using System.Collections.Generic;
using MemoryGameLogic;
using myOpenGL.Forms;

namespace myOpenGL.Classes
{
    public class SecretBoxMatrix
    {
        #region CLASS MEMBERS
        private int m_UserTurnCounter = 0;
        private FormGameBoard m_MainForm;
        private int m_NumberOfRowsAndColumns;
        private List<Color> m_ColorsList;
        private List<List<SecretBox>> m_SecretBoxesMatrix;
        private List<MatrixIndexPair> m_MatrixIndexPairList;
        private int m_CurrentSecretBoxXCoordinate = 0;
        private int m_CurrentSecretBoxYCoordinate = 0;
        public SecretBox CurrentSecretBoxPointer { get; private set; }
        public bool DrawSelectedSecretBoxArrowFlag { get; set; }
        #endregion

        public SecretBoxMatrix(int i_NumberOfRowsAndColumns, FormGameBoard i_MainForm)
        {
            this.initializeColorsList();
            this.m_MainForm = i_MainForm;
            this.m_NumberOfRowsAndColumns = i_NumberOfRowsAndColumns;
            this.fillSecretBoxesMatrix();
            this.CurrentSecretBoxPointer = this.m_SecretBoxesMatrix[0][0];
            this.DrawSelectedSecretBoxArrowFlag = true;
        }

        #region initialize, fill and set methods
        public void ResetSecretBoxMatrix()
        {
            this.m_UserTurnCounter = -1;//test
            this.CurrentSecretBoxPointer = this.m_SecretBoxesMatrix[0][0];
            this.resetAllDiscoveredSecretBoxes();
            this.m_CurrentSecretBoxXCoordinate = 0;
            this.m_CurrentSecretBoxYCoordinate = 0;
            this.DrawSelectedSecretBoxArrowFlag = true;
        }

        private void resetAllDiscoveredSecretBoxes()
        {
            foreach (var secretBoxesList in this.m_SecretBoxesMatrix)
            {
                foreach (var secretBox in secretBoxesList)
                {
                    secretBox.ResetSecretBoxParams();
                }
            }
        }

        private void fillSecretBoxesMatrix()
        {
            float heightValue = 0;
            checkIfNumberOfRowsAndColumnsIsValid(this.m_NumberOfRowsAndColumns);
            this.m_SecretBoxesMatrix = new List<List<SecretBox>>(this.m_NumberOfRowsAndColumns);
            for (int i = 0; i < this.m_NumberOfRowsAndColumns; i++)
            {
                this.m_SecretBoxesMatrix.Add(new List<SecretBox>(this.m_NumberOfRowsAndColumns));
                fillCurrentSecretBoxesList(this.m_SecretBoxesMatrix[i], heightValue);
                heightValue += 3.0f;
            }
        }

        private void fillCurrentSecretBoxesList(List<SecretBox> i_SecretBoxListToFill, float i_HeightOffsetValue)
        {
            SecretBox secretBoxPointer = null;

            for (int i = 0; i < this.m_NumberOfRowsAndColumns; i++)
            {
                secretBoxPointer = new SecretBox(new Point3D(0.0f + i * 3, 0.0f, 0.0f + i_HeightOffsetValue));
                secretBoxPointer.CheckIfPlayerStepsAreCorrectAction = this.m_MainForm.CheckIfPlayerStepsAreCorrect;
                i_SecretBoxListToFill.Add(secretBoxPointer);
            }
        }

        public void ColorHiddenObjectsInSecretBoxesMatrix(GameLogicComponent i_GameLogicComponent)
        {
            int colorsListCounter = 0;
            MatrixIndex firstMatrixIndex, secondMatrixIndex;
            this.m_MatrixIndexPairList = i_GameLogicComponent.MatrixIndexPairList;

            foreach (MatrixIndexPair matrixIndexPair in this.m_MatrixIndexPairList)
            {
                firstMatrixIndex = matrixIndexPair.FirstIndex.Value;
                secondMatrixIndex = matrixIndexPair.SecondIndex.Value;
                this.m_SecretBoxesMatrix[firstMatrixIndex.MatrixRowIndex][firstMatrixIndex.MatrixColumnIndex].HiddenObjectColor = this.m_ColorsList[colorsListCounter];
                this.m_SecretBoxesMatrix[secondMatrixIndex.MatrixRowIndex][secondMatrixIndex.MatrixColumnIndex].HiddenObjectColor = this.m_ColorsList[colorsListCounter];
                colorsListCounter++;
            }
        }

        private void initializeColorsList()
        {
            this.m_ColorsList = new List<Color>();
            this.m_ColorsList.Add(new Color(1, 0, 0));
            this.m_ColorsList.Add(new Color(0, 1, 0));
            this.m_ColorsList.Add(new Color(1, 0.5f, 1));
            this.m_ColorsList.Add(new Color(0.5f, 1, 1));
            this.m_ColorsList.Add(new Color(1, 0.3f, 1));
            this.m_ColorsList.Add(new Color(0, 0.5f, 0.8f));
            this.m_ColorsList.Add(new Color(1, 1, 1));
            this.m_ColorsList.Add(new Color(0.5f, 0, 0));
        }

        public void SetXAndYValuesAsCurrentPlayerStep(Player i_CurrentPlayer, ePlayerStepsStates i_PlayerStepsState)
        {
            if (i_PlayerStepsState == ePlayerStepsStates.FirstPlayerStep)
            {
                i_CurrentPlayer.FirstStep = new PlayerStep(this.m_CurrentSecretBoxXCoordinate, this.m_CurrentSecretBoxYCoordinate);
            }
            else
            {
                i_CurrentPlayer.SecondStep = new PlayerStep(this.m_CurrentSecretBoxXCoordinate, this.m_CurrentSecretBoxYCoordinate);
            }
        }
        #endregion

        #region draw methods
        public void DrawSecretBoxMatrix(bool i_DrawShadowFlag = false)
        {
            foreach (List<SecretBox> secretBoxList in this.m_SecretBoxesMatrix)
            {
                foreach (SecretBox secretBox in secretBoxList)
                {
                    secretBox.drawSecretBoxWithItsContent(i_DrawShadowFlag);
                }
            }
        }
        #endregion

        #region select and forget methods
        public void SelectCurrentSecretBoxByRandomPlayerStep(PlayerStep i_MachineRandomPlayerStep)
        {
            this.m_CurrentSecretBoxXCoordinate = i_MachineRandomPlayerStep.RowIndex;
            this.m_CurrentSecretBoxYCoordinate = i_MachineRandomPlayerStep.ColumnIndex;
            this.CurrentSecretBoxPointer = this.m_SecretBoxesMatrix[m_CurrentSecretBoxXCoordinate][m_CurrentSecretBoxYCoordinate];
        }

        public void SelectTheCurrentSecretBox()
        {
            if (!this.CurrentSecretBoxPointer.IsBoxOpen) // box is closed
            {
                this.CurrentSecretBoxPointer.SelectThisSecretBox();
                this.CurrentSecretBoxPointer.SecretBoxDrawState = eSecretBoxDrawState.OpenSecretBox;
            }

            this.CountUserTurnCounter();
        }

        public void ForgetSecretBoxByGivenPlayerStep(PlayerStep i_PlayerStep)
        {
            int xValue = i_PlayerStep.RowIndex;
            int yValue = i_PlayerStep.ColumnIndex;

            this.m_SecretBoxesMatrix[xValue][yValue].SecretBoxDrawState = eSecretBoxDrawState.CloseSecretBox;
            this.m_SecretBoxesMatrix[xValue][yValue].ForgetThisSecretBox();
        }
        #endregion

        #region move secret box arrow
        public void MoveSelectedSecretBoxArrow(ePossibleMoveInSecretBoxMatrix? i_PossibleMoveInSecretBoxMatrix)
        {
            switch (i_PossibleMoveInSecretBoxMatrix)
            {
                case ePossibleMoveInSecretBoxMatrix.MoveUpInSecretBoxMatrix:
                    this.moveUpInSecretBoxMatrix();
                    break;
                case ePossibleMoveInSecretBoxMatrix.MoveDownInSecretBoxMatrix:
                    this.moveDownInSecretBoxMatrix();
                    break;
                case ePossibleMoveInSecretBoxMatrix.MoveRightInSecretBoxMatrix:
                    this.moveRightInSecretBoxMatrix();
                    break;
                case ePossibleMoveInSecretBoxMatrix.MoveLeftInSecretBoxMatrix:
                    this.moveLeftInSecretBoxMatrix();
                    break;
            }

            this.CurrentSecretBoxPointer = this.m_SecretBoxesMatrix[m_CurrentSecretBoxXCoordinate][m_CurrentSecretBoxYCoordinate];
        }

        public void CountUserTurnCounter()
        {
            this.m_UserTurnCounter++;
            DrawSelectedSecretBoxArrowFlag = this.m_UserTurnCounter != 2;

            if (!DrawSelectedSecretBoxArrowFlag)
            {
                this.m_UserTurnCounter = 0;
            }
        }
        #endregion

        #region checking methods
        public bool CheckIfSecretBoxIsNotShown(GameLogicComponent i_GameLogicComponent)
        {
            bool result = i_GameLogicComponent.CheckIfCardIsNotShown(this.m_CurrentSecretBoxXCoordinate, this.m_CurrentSecretBoxYCoordinate);

            return result;
        }

        private void checkIfNumberOfRowsAndColumnsIsValid(int i_NumberOfRowsAndColumnsToCheck)
        {
            if (i_NumberOfRowsAndColumnsToCheck % 2 != 0)
            {
                throw new Exception("You cant create a game board with odd number of cards!");
            }
        }
        #endregion

        #region Movement methods
        private void moveUpInSecretBoxMatrix()
        {
            bool isNewBoxFound = false;

            for (int yIndex = this.m_CurrentSecretBoxYCoordinate - 1; yIndex >= 0; yIndex--)
            {
                if (!this.m_SecretBoxesMatrix[this.m_CurrentSecretBoxXCoordinate][yIndex].IsSecretBoxRevealed)
                {
                    this.m_CurrentSecretBoxYCoordinate = yIndex;
                    isNewBoxFound = true;
                    break;
                }
            }

            if (!isNewBoxFound)
            {
                for (int xIndex = this.m_CurrentSecretBoxXCoordinate + 1; xIndex < this.m_NumberOfRowsAndColumns; xIndex++)
                {
                    for (int yIndex = this.m_CurrentSecretBoxYCoordinate - 1; yIndex >= 0; yIndex--)
                    {
                        if (!this.m_SecretBoxesMatrix[xIndex][yIndex].IsSecretBoxRevealed)
                        {
                            this.m_CurrentSecretBoxYCoordinate = yIndex;
                            this.m_CurrentSecretBoxXCoordinate = xIndex;
                            isNewBoxFound = true;
                            break;
                        }
                    }

                    if (isNewBoxFound)
                    {
                        break;
                    }
                }

                if (!isNewBoxFound)
                {
                    for (int xIndex = this.m_CurrentSecretBoxXCoordinate - 1; xIndex >= 0; xIndex--)
                    {
                        for (int yIndex = this.m_CurrentSecretBoxYCoordinate - 1; yIndex >= 0; yIndex--)
                        {
                            if (!this.m_SecretBoxesMatrix[xIndex][yIndex].IsSecretBoxRevealed)
                            {
                                this.m_CurrentSecretBoxYCoordinate = yIndex;
                                this.m_CurrentSecretBoxXCoordinate = xIndex;
                                isNewBoxFound = true;
                                break;
                            }
                        }

                        if (isNewBoxFound)
                        {
                            break;
                        }
                    }
                }
            }
        }

        private void moveDownInSecretBoxMatrix()
        {
            bool isNewBoxFound = false;

            for (int yIndex = this.m_CurrentSecretBoxYCoordinate + 1; yIndex < this.m_NumberOfRowsAndColumns; yIndex++)
            {
                if (!this.m_SecretBoxesMatrix[this.m_CurrentSecretBoxXCoordinate][yIndex].IsSecretBoxRevealed)
                {
                    this.m_CurrentSecretBoxYCoordinate = yIndex;
                    isNewBoxFound = true;
                    break;
                }
            }

            if (!isNewBoxFound)
            {
                for (int xIndex = this.m_CurrentSecretBoxXCoordinate - 1; xIndex >= 0; xIndex--)
                {
                    for (int yIndex = this.m_CurrentSecretBoxYCoordinate + 1; yIndex < this.m_NumberOfRowsAndColumns; yIndex++)
                    {
                        if (!this.m_SecretBoxesMatrix[xIndex][yIndex].IsSecretBoxRevealed)
                        {
                            this.m_CurrentSecretBoxYCoordinate = yIndex;
                            this.m_CurrentSecretBoxXCoordinate = xIndex;
                            isNewBoxFound = true;
                            break;
                        }
                    }

                    if (isNewBoxFound)
                    {
                        break;
                    }
                }

                if (!isNewBoxFound)
                {
                    for (int xIndex = this.m_CurrentSecretBoxXCoordinate + 1; xIndex < this.m_NumberOfRowsAndColumns; xIndex++)
                    {
                        for (int yIndex = this.m_CurrentSecretBoxYCoordinate + 1; yIndex < this.m_NumberOfRowsAndColumns; yIndex++)
                        {
                            if (!this.m_SecretBoxesMatrix[xIndex][yIndex].IsSecretBoxRevealed)
                            {
                                this.m_CurrentSecretBoxYCoordinate = yIndex;
                                this.m_CurrentSecretBoxXCoordinate = xIndex;
                                isNewBoxFound = true;
                                break;
                            }
                        }

                        if (isNewBoxFound)
                        {
                            break;
                        }
                    }
                }
            }
        }

        private void moveLeftInSecretBoxMatrix()
        {
            bool isNewBoxFound = false;

            for (int xIndex = this.m_CurrentSecretBoxXCoordinate + 1; xIndex < this.m_NumberOfRowsAndColumns; xIndex++)
            {
                if (!this.m_SecretBoxesMatrix[xIndex][this.m_CurrentSecretBoxYCoordinate].IsSecretBoxRevealed)
                {
                    this.m_CurrentSecretBoxXCoordinate = xIndex;
                    isNewBoxFound = true;
                    break;
                }
            }

            if (!isNewBoxFound)
            {
                for (int yIndex = this.m_CurrentSecretBoxYCoordinate + 1; yIndex < this.m_NumberOfRowsAndColumns; yIndex++)
                {
                    for (int xIndex = this.m_CurrentSecretBoxXCoordinate + 1; xIndex < this.m_NumberOfRowsAndColumns; xIndex++)
                    {
                        if (!this.m_SecretBoxesMatrix[xIndex][this.m_CurrentSecretBoxYCoordinate].IsSecretBoxRevealed)
                        {
                            this.m_CurrentSecretBoxXCoordinate = xIndex;
                            isNewBoxFound = true;
                            break;
                        }
                    }

                    if (isNewBoxFound)
                    {
                        break;
                    }
                }

                if (!isNewBoxFound)
                {
                    for (int yIndex = this.m_CurrentSecretBoxYCoordinate - 1; yIndex >=0; yIndex--)
                    {
                        for (int xIndex = this.m_CurrentSecretBoxXCoordinate + 1; xIndex < this.m_NumberOfRowsAndColumns; xIndex++)
                        {
                            if (!this.m_SecretBoxesMatrix[xIndex][this.m_CurrentSecretBoxYCoordinate].IsSecretBoxRevealed)
                            {
                                this.m_CurrentSecretBoxXCoordinate = xIndex;
                                isNewBoxFound = true;
                                break;
                            }
                        }

                        if (isNewBoxFound)
                        {
                            break;
                        }
                    }
                }
            }
        }

        private void moveRightInSecretBoxMatrix()
        {
            bool isNewBoxFound = false;

            for (int xIndex = this.m_CurrentSecretBoxXCoordinate - 1; xIndex >= 0; xIndex--)
            {
                if (!this.m_SecretBoxesMatrix[xIndex][this.m_CurrentSecretBoxYCoordinate].IsSecretBoxRevealed)
                {
                    this.m_CurrentSecretBoxXCoordinate = xIndex;
                    isNewBoxFound = true;
                    break;
                }
            }

            if (!isNewBoxFound)
            {
                for (int yIndex = this.m_CurrentSecretBoxYCoordinate - 1; yIndex >= 0; yIndex--)
                {
                    for (int xIndex = this.m_CurrentSecretBoxXCoordinate - 1; xIndex >= 0; xIndex--)
                    {
                        if (!this.m_SecretBoxesMatrix[xIndex][this.m_CurrentSecretBoxYCoordinate].IsSecretBoxRevealed)
                        {
                            this.m_CurrentSecretBoxXCoordinate = xIndex;
                            isNewBoxFound = true;
                            break;
                        }
                    }

                    if (isNewBoxFound)
                    {
                        break;
                    }
                }

                if (!isNewBoxFound)
                {
                    for (int yIndex = this.m_CurrentSecretBoxYCoordinate + 1; yIndex < this.m_NumberOfRowsAndColumns; yIndex++)
                    {
                        for (int xIndex = this.m_CurrentSecretBoxXCoordinate - 1; xIndex >= 0; xIndex--)
                        {
                            if (!this.m_SecretBoxesMatrix[xIndex][this.m_CurrentSecretBoxYCoordinate].IsSecretBoxRevealed)
                            {
                                this.m_CurrentSecretBoxXCoordinate = xIndex;
                                isNewBoxFound = true;
                                break;
                            }
                        }

                        if (isNewBoxFound)
                        {
                            break;
                        }
                    }
                }
            }
        }

        public void setPlayerStepRevealed(PlayerStep i_PlayerStep)
        {
            this.m_SecretBoxesMatrix[i_PlayerStep.RowIndex][i_PlayerStep.ColumnIndex].IsSecretBoxRevealed = true;
        }
        #endregion
    }
}