namespace MemoryGameLogic
{
    public struct MatrixIndex
    {
        // MEMBER VARIABLES
        private readonly int r_MatrixRowIndex;
        private readonly int r_MatrixColumnIndex;

        // CTOR
        public MatrixIndex(int i_RowIndex, int i_ColumnIndex)
        {
            this.r_MatrixRowIndex = i_RowIndex;
            this.r_MatrixColumnIndex = i_ColumnIndex;
        }

        // PROPERTIES
        public int MatrixRowIndex
        {
            get { return this.r_MatrixRowIndex; }
        }

        public int MatrixColumnIndex
        {
            get { return this.r_MatrixColumnIndex; }
        }
    }
}