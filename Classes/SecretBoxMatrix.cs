using myOpenGL.Enums;
using myOpenGL.Structs;
using myOpenGL;
using System;
using System.Collections.Generic;

namespace myOpenGL.Classes
{
    public class SecretBoxMatrix
    {
        // CLASS MEMBERS
        private int m_NumberOfRowsAndColumns;
        private List<List<SecretBox>> m_SecretBoxesMatrix;
        private int m_CurrentSecretBoxXCoordinate = 0;
        private int m_CurrentSecretBoxYCoordinate = 0;

        // CTOR
        public SecretBoxMatrix(int i_NumberOfRowsAndColumns)
        {
            this.m_NumberOfRowsAndColumns = i_NumberOfRowsAndColumns;
            this.fillSecretBoxesMatrix();
        }

        // PUBLIC METHODS
        public void DrawSecretBoxMatrix()
        {
            this.drawSecretBoxesMatrix();
        }

        public void PerformAMoveInSecretBoxMatrix(ePossibleMoveInSecretBoxMatrix? i_PossibleMoveInSecretBoxMatrix)
        {
            this.m_SecretBoxesMatrix[this.m_CurrentSecretBoxXCoordinate][this.m_CurrentSecretBoxYCoordinate].ForgetThisSecretBox();

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

            this.m_SecretBoxesMatrix[this.m_CurrentSecretBoxXCoordinate][this.m_CurrentSecretBoxYCoordinate].SelectThisSecretBox();
        }

        // PRIVATE METHODS
        private void fillSecretBoxesMatrix()
        {
            float heightValue = 0;
            checkIfNumberOfRowsAndColumnsIsValid(this.m_NumberOfRowsAndColumns);
            this.m_SecretBoxesMatrix = new List<List<SecretBox>>(this.m_NumberOfRowsAndColumns);

            for (int i = 0; i < this.m_NumberOfRowsAndColumns; i++)
            {
                this.m_SecretBoxesMatrix.Add(new List<SecretBox>(this.m_NumberOfRowsAndColumns));
                fillCurrentSecretBoxesList(this.m_SecretBoxesMatrix[i], heightValue);
                heightValue += 2.0f;
            }
        }

        private void checkIfNumberOfRowsAndColumnsIsValid(int i_NumberOfRowsAndColumnsToCheck)
        {
            if (i_NumberOfRowsAndColumnsToCheck % 2 != 0)
            {
                throw new Exception("You cant create a game board with odd number of cards!");
            }
        }

        private void fillCurrentSecretBoxesList(List<SecretBox> i_SecretBoxListToFill, float i_HeightOffsetValue)
        {
            for (int i = 0; i < this.m_NumberOfRowsAndColumns; i++)
            {

                i_SecretBoxListToFill.Add(new SecretBox(new Point3D(0.0f + i * 2, 0.0f, 0.0f + i_HeightOffsetValue)));

            }
        }

        private void drawSecretBoxesMatrix()
        {
            OpenGL.GL.glColor3f(1, 1, 1);
            // test           
            for (int i = 0; i < this.m_NumberOfRowsAndColumns; i++)
            {
                for (int n = 0; n < this.m_NumberOfRowsAndColumns; n++)
                {

                    m_SecretBoxesMatrix[i][n].DrawSecretBox();
                    if (i == m_CurrentSecretBoxXCoordinate && n == m_CurrentSecretBoxYCoordinate)
                    {
                        //should i do something???
                    }
                }
            }

            /*foreach (List<SecretBox> secretBoxList in this.m_SecretBoxesMatrix)
            {
                foreach (SecretBox secretBox in secretBoxList)
                {
                    secretBox.DrawSecretBox();
                }
            }*/
        }

        #region Movement methods
        private void moveUpInSecretBoxMatrix()
        {
            if (this.m_CurrentSecretBoxXCoordinate - 1 >= 0)
            {
                this.m_CurrentSecretBoxXCoordinate--;
            }
        }

        private void moveDownInSecretBoxMatrix()
        {
            if (this.m_CurrentSecretBoxXCoordinate + 1 < this.m_NumberOfRowsAndColumns)
            {
                this.m_CurrentSecretBoxXCoordinate++;
            }
        }

        private void moveLeftInSecretBoxMatrix()
        {
            if (this.m_CurrentSecretBoxYCoordinate - 1 >= 0)
            {
                this.m_CurrentSecretBoxYCoordinate--;
            }
        }

        private void moveRightInSecretBoxMatrix()
        {
            if (this.m_CurrentSecretBoxYCoordinate + 1 < this.m_NumberOfRowsAndColumns)
            {
                this.m_CurrentSecretBoxYCoordinate++;
            }
        }
        #endregion
    }
}