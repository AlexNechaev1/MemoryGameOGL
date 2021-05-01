namespace MemoryGameLogic
{
    public struct MemoryCard
    {
        // MEMBER VARIABLES
        private char m_Symbol;
        private bool m_IsShown;

        // CTOR
        public MemoryCard(char i_Symbol, bool i_IsShown)
        {
            this.m_Symbol = i_Symbol;
            this.m_IsShown = i_IsShown;
        }

        // PROPERTIES
        public char Symbol
        {
            get { return this.m_Symbol; }
            set { this.m_Symbol = value; }
        }

        public bool IsShown
        {
            get { return this.m_IsShown; }
            set { this.m_IsShown = value; }
        }
    }
}