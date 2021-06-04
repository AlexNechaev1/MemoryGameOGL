using myOpenGL.Enums;
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
            this.CenterToScreen();
            InitializeComponent();
            this.m_GameMode = eGameModes.FullScreenMode;
        }

        #region Events
        private void gameModeBtn_Click(object sender, System.EventArgs e)
        {
            this.switchGameModeAndButtonText();
        }

        private void buttonStart_Click(object sender, System.EventArgs e)
        {
            this.startNewGame();
        }
        #endregion

        #region Events Methods
        private void switchGameModeAndButtonText()
        {
            if (this.m_GameMode == eGameModes.FullScreenMode)
            {
                this.m_GameMode = eGameModes.ControlsShowMode;
                this.gameModeBtn.Text = "Controls Show Mode";
            }
            else
            {
                this.m_GameMode = eGameModes.FullScreenMode;
                this.gameModeBtn.Text = "Full Screen Mode";
            }
        }

        private bool checkPlayerOneName()
        {
            return Regex.IsMatch(this.textBoxFirstPlayerName.Text, @"^[a-zA-Z]+$");
        }

        private void startNewGame()
        {
            if (this.checkPlayerOneName())
            {
                this.m_PlayerOneName = this.textBoxFirstPlayerName.Text;
                this.m_FormGameBoardInstance = new FormGameBoard(this.m_PlayerOneName, this.m_GameMode);

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