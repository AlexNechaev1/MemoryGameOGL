using myOpenGL.Enums;
using myOpenGL.Structs;
using System;
using System.Collections.Generic;
using OpenGL;
using MemoryGameLogic;

namespace myOpenGL.Classes
{
    public class SecretBoxMatrix
    {
        // CLASS MEMBERS
        private int m_NumberOfRowsAndColumns;
        private List<Color> m_ColorsList;
        private List<List<SecretBox>> m_SecretBoxesMatrix;
        private List<MatrixIndexPair> m_MatrixIndexPairList;
        private int m_CurrentSecretBoxXCoordinate = 0;
        private int m_CurrentSecretBoxYCoordinate = 0;
        private GLUquadric m_GLUquadricObject;
        private SecretBox m_CurrentSecretBoxPointer;
        private bool m_DrawSelectedSecretBoxArrow = true;

        // CTOR
        public SecretBoxMatrix(int i_NumberOfRowsAndColumns)
        {
            this.initializeColorsList();
            this.m_NumberOfRowsAndColumns = i_NumberOfRowsAndColumns;
            this.fillSecretBoxesMatrix();
            this.m_GLUquadricObject = GLU.gluNewQuadric();
            this.m_CurrentSecretBoxPointer = this.m_SecretBoxesMatrix[0][0];
        }

        // DTOR
        ~SecretBoxMatrix()
        {
            GLU.gluDeleteQuadric(this.m_GLUquadricObject);
        }

        // PUBLIC METHODS
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

        public void SetXAndYValuesAsCurrentPlayerStep(Player i_CurrentPlayer, bool i_FirstStepFlag)
        {
            if (i_FirstStepFlag)
            {
                i_CurrentPlayer.FirstStep = new PlayerStep(this.m_CurrentSecretBoxXCoordinate, this.m_CurrentSecretBoxYCoordinate);
            }
            else
            {
                i_CurrentPlayer.SecondStep = new PlayerStep(this.m_CurrentSecretBoxXCoordinate, this.m_CurrentSecretBoxYCoordinate);
            }
        }

        public void DrawSelectedSecretBoxArrow()
        {
            if (this.m_DrawSelectedSecretBoxArrow)
            {
                Point3D currentPoint = this.m_CurrentSecretBoxPointer.TranslatePoint;
                GL.glPushMatrix();

                GL.glColor3f(1, 0, 0);
                GL.glTranslatef(currentPoint.X + 0.5f, currentPoint.Y + 2, currentPoint.Z + 0.5f);
                GL.glRotatef(-90, 1, 0, 0);
                GLU.gluCylinder(this.m_GLUquadricObject, 0.0, 0.5, 1.5, 16, 16);
                GL.glTranslatef(-1 * (currentPoint.X + 0.5f), -1 * (currentPoint.Y + 3), -1 * (currentPoint.Z + 0.5f));

                GL.glPopMatrix();
            }
        }

        public void DrawSecretBoxMatrix()
        {
            this.drawSecretBoxesMatrix();
        }

        public void SelectTheCurrentSecretBox()
        {
            this.m_CurrentSecretBoxPointer.SelectThisSecretBox();
            this.moveSelectedSecretBoxArrowToTheNextSecretBox();
        }

        public void MoveSelectedSecretBoxArrow(ePossibleMoveInSecretBoxMatrix? i_PossibleMoveInSecretBoxMatrix)
        {
            //this.m_CurrentSecretBoxPointer.ForgetThisSecretBox();

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

            this.m_CurrentSecretBoxPointer = this.m_SecretBoxesMatrix[m_CurrentSecretBoxXCoordinate][m_CurrentSecretBoxYCoordinate];
        }

        // PRIVATE METHODS
        private void initializeColorsList()
        {
            this.m_ColorsList = new List<Color>();

            // alex, please pick better colors
            // note, that for some reason there are some colors that dont work
            // like blue (0,0,1) that will show as black
            // and white (1,1,1) that will show as yellow
            this.m_ColorsList.Add(new Color(1, 0, 0));//1
            this.m_ColorsList.Add(new Color(0, 1, 0));//2
            this.m_ColorsList.Add(new Color(0, 1, 1));//3
            this.m_ColorsList.Add(new Color(1, 0.5f, 0));//4
            this.m_ColorsList.Add(new Color(0.5f, 0, 1));//5
            this.m_ColorsList.Add(new Color(1, 0, 1));//6
            this.m_ColorsList.Add(new Color(0.5f, 0, 0.5f));//7
            this.m_ColorsList.Add(new Color(0.7f, 0 ,0.3f));//8
        }

        private void moveSelectedSecretBoxArrowToTheNextSecretBox()
        {
            SecretBox secretBoxPointer = null;
            bool isSuitabeSecretBoxFound = false;

            for (int i = 0; i < this.m_NumberOfRowsAndColumns; i++)
            {
                for (int n = 0; n < this.m_NumberOfRowsAndColumns; n++)
                {
                    secretBoxPointer = this.m_SecretBoxesMatrix[i][n];
                    if (!secretBoxPointer.IsSelectedSecretBox && secretBoxPointer.IsSecretBoxVisible)
                    {
                        this.m_CurrentSecretBoxPointer = secretBoxPointer;
                        this.m_CurrentSecretBoxXCoordinate = i;
                        this.m_CurrentSecretBoxYCoordinate = n;
                        isSuitabeSecretBoxFound = true;
                        break;
                    }
                }

                if (isSuitabeSecretBoxFound)
                {
                    break;
                }
            }

            this.m_DrawSelectedSecretBoxArrow = isSuitabeSecretBoxFound;
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
            GL.glColor3f(1, 1, 1);
          
            foreach (List<SecretBox> secretBoxList in this.m_SecretBoxesMatrix)
            {
                foreach (SecretBox secretBox in secretBoxList)
                {
                    secretBox.DrawSecretBoxWithItsContent();
                }
            }
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