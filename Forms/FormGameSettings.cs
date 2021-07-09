using myOpenGL.Enums;
using myOpenGL.Properties;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace myOpenGL.Forms
{
    public partial class FormGameSettings : Form
    {
        private string m_PlayerOneName;
        private eGameModes m_GameMode;
        private FormGameBoard m_FormGameBoardInstance;

        public FormGameSettings()
        {
            this.Icon = Resources.questionMarkIcon;
            this.CenterToScreen();
            InitializeComponent();
            this.m_GameMode = eGameModes.GameMode;
        }

        #region Events
        private void gameModeBtn_Click(object sender, System.EventArgs e)
        {
            this.m_GameMode = eGameModes.GameMode;
            this.startNewGame();
        }

        private void sceneModeBtn_Click(object sender, System.EventArgs e)
        {
            this.m_GameMode = eGameModes.SceneMode;
            this.startNewGame();
        }
        #endregion

        #region Events Methods
        private bool checkPlayerOneName()
        {
            return Regex.IsMatch(this.textBoxFirstPlayerName.Text, @"^[a-zA-Z]+$");
        }

        private void startNewGame()
        {
            if (this.checkPlayerOneName())
            {
                this.m_PlayerOneName = this.textBoxFirstPlayerName.Text;
                bool drawAxisFlag = this.m_GameMode == eGameModes.SceneMode ? true : false;
                this.m_FormGameBoardInstance = new FormGameBoard(this.m_PlayerOneName, this.m_GameMode, drawAxisFlag);

                this.Hide();
                this.m_FormGameBoardInstance.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("The given name is invalid.\nPlease enter a name that consists only from letters",
                    "Player One Name error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.textBoxFirstPlayerName.Text = "";
            }
        }
        #endregion
    }
}