using myOpenGL.Enums;
using myOpenGL.Structs;
using myOpenGL;
using System;
using System.Collections.Generic;
using OpenGL;

namespace myOpenGL.Classes
{
    public class SecretBoxMatrix
    {
        // CLASS MEMBERS
        private int m_NumberOfRowsAndColumns;
        private List<List<SecretBox>> m_SecretBoxesMatrix;
        private int m_CurrentSecretBoxXCoordinate = 0;
        private int m_CurrentSecretBoxYCoordinate = 0;
        private GLUquadric m_GLUquadricObject;

        // CTOR
        public SecretBoxMatrix(int i_NumberOfRowsAndColumns)
        {
            this.m_NumberOfRowsAndColumns = i_NumberOfRowsAndColumns;
            this.fillSecretBoxesMatrix();
            this.m_GLUquadricObject = GLU.gluNewQuadric();
        }

        // DTOR
        ~SecretBoxMatrix()
        {
            GLU.gluDeleteQuadric(this.m_GLUquadricObject);
        }

        // PUBLIC METHODS
        public void DrawSelectedSecretBoxArrow()
        {
            Point3D currentPoint = this.m_SecretBoxesMatrix[this.m_CurrentSecretBoxXCoordinate][this.m_CurrentSecretBoxYCoordinate].TranslatePoint;

            GL.glPushMatrix();

            GL.glColor3f(1.0f, 0.0f, 0.0f);
            GL.glTranslatef(currentPoint.X + 0.5f, currentPoint.Y + 2, currentPoint.Z + 0.5f);
            GL.glRotatef(-90, 1, 0, 0);
            GLU.gluCylinder(this.m_GLUquadricObject, 0.0, 0.5, 1.5, 16, 16);
            GL.glTranslatef(-1 * (currentPoint.X + 0.5f), -1 * (currentPoint.Y + 3), -1 * (currentPoint.Z + 0.5f));

            GL.glPopMatrix();
        }

        public void DrawSecretBoxMatrix()
        {
            this.drawSecretBoxesMatrix();
        }

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