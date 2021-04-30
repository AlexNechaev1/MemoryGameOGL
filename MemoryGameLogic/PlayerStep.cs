namespace MemoryGameLogic
{
    public struct PlayerStep
    {
        // MEMBER VARIABLES
        private int m_RowIndex;
        private int m_ColumnIndex;

        // CTOR
        public PlayerStep(int i_RowIndex, int i_ColumnIndex)
        {
            this.m_RowIndex = i_RowIndex;
            this.m_ColumnIndex = i_ColumnIndex;
        }

        // PROPERTIES
        public int RowIndex
        {
            get { return this.m_RowIndex; }
            set { this.m_RowIndex = value; }
        }

        public int ColumnIndex
        {
            get { return this.m_ColumnIndex; }
            set { this.m_ColumnIndex = value; }
        }
    }
}