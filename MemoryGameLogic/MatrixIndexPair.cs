using System;

namespace MemoryGameLogic
{
    public struct MatrixIndexPair
    {
        // MEMBER VARIABLES
        private readonly MatrixIndex? r_FirstIndex;
        private readonly MatrixIndex? r_SecondIndex;

        // CTOR
        public MatrixIndexPair(MatrixIndex? i_FirstIndex, MatrixIndex? i_SecondIndex)
        {
            this.r_FirstIndex = i_FirstIndex;
            this.r_SecondIndex = i_SecondIndex;
        }

        // PROPERTIES
        public MatrixIndex? FirstIndex
        {
            get { return this.r_FirstIndex; }
        }

        public MatrixIndex? SecondIndex
        {
            get { return this.r_SecondIndex; }
        }
    }
}