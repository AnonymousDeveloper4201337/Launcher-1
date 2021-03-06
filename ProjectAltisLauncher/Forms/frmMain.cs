﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectAltis.Core;
using ProjectAltis.Forms.ContentPacks;
using Timer = System.Timers.Timer;

namespace ProjectAltis.Forms
{
    public partial class FrmMain : Form
    {
        #region Fields
        private readonly string _currentDir;
        #endregion
        #region Main Form Events
        public FrmMain()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            _currentDir = Directory.GetCurrentDirectory() + @"\";
            if (!IsWriteable())
            {
                MessageBox.Show(@"It appears you do not have permission to write the the current directory: " + _currentDir + @"
" +
                                @"The launcher may not work correctly without permissions. 
" +
                                @"Try running the launcher with administrator rights or installing in a different location.");
            }

            versionLabel.Text = "Launcher v" + typeof(Program).Assembly.GetName().Version.ToString();
            ValidatePrefJson();
            webBrowser1.Navigate("https://projectaltis.com/launcher", null, null, "User-Agent: Altis Launcher\r\n");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
#if DEBUG
            MessageBox.Show(@"This is a debug build, do not put this into production",
                @"Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif
            LoadUserSettings();
            ActiveControl = string.IsNullOrEmpty(txtUser.Text) ? txtUser : txtPass;
            Button_MouseLeave(btnPlay, EventArgs.Empty);
            webBrowser1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, webBrowser1.Width, webBrowser1.Height, 20, 20));
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            ActiveControl = null;
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            ActiveControl = null;
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            new RedistCheck(this).CheckRedistHandler();
        }
        #endregion
        #region Borderless Form Code

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // X-coordinate of upper-left corner
            int nTopRect, // Y-coordinate of upper-left corner
            int nRightRect, // X-coordinate of lower-right corner
            int nBottomRect, // Y-coordinate of lower-right corner
            int nWidthEllipse, // Height of ellipse
            int nHeightEllipse // Width of ellipse
        );

        private Point mouseDownPoint = Point.Empty;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownPoint = new Point(e.X, e.Y);
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDownPoint = Point.Empty;
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDownPoint.IsEmpty)
            {
                return;
            }
            if (sender is Form)
            {
                Form form = sender as Form;
                form.Location = new Point(form.Location.X + (e.X - mouseDownPoint.X),
                                form.Location.Y + (e.Y - mouseDownPoint.Y));
            }
        }

        #endregion
        #region Button Behaviors
        #region Exit Button
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        #endregion
        #region Minimize Button
        private void BtnMin_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            WindowState = FormWindowState.Minimized;
            ActiveControl = null;
        }
        #endregion
        #region Play Button
        private void BtnPlay_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            if (string.IsNullOrEmpty(txtUser.Text))
            {
                Log.TryOpenUrl("https://projectaltis.com/register");
                return;
            }
            btnPlay.Enabled = false;

            ErrorReporter.Instance.Username = txtUser.Text;
            SaveCredentials();// Save credentials if necessary
            RunUpdater();
            ActiveControl = null;
        }


        #endregion
        #region Site Button
        private void BtnOfficialSite_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            Process.Start("https://www.projectaltis.com/");
            ActiveControl = null;
        }
        #endregion
        #region Discord Button
        private void BtnDiscord_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            Log.TryOpenUrl("https://discord.me/ttprojectaltis");
            ActiveControl = null;
        }
        #endregion
        #region Content Packs
        private void BtnContentPacks_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            btnContentPacks.BackgroundImage = Properties.Resources.contentpacks_d;
            FrmContentPacks contentPack = new FrmContentPacks();
            contentPack.ShowDialog(this);
            ActiveControl = null;
        }
        #endregion
        #region Change Theme
        private void BtnChangeBg_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            FrmBackgroundChoices bg = new FrmBackgroundChoices();
            bg.ShowDialog();
            if (!Properties.Settings.Default.WantsRandomBg)
            {
                BackgroundImage.Dispose();
                BackgroundImage = Background.ReturnBackground(Properties.Settings.Default.Background);
            }

            ActiveControl = null;
        }
        #endregion
        #region Options Button
        private void BtnOptions_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            FrmOptions op = new FrmOptions();
            op.ShowDialog();
            // Apply user settings
            if (Properties.Settings.Default.WantsCursor) // Cursor
            {
                MemoryStream cursorMemoryStream = new MemoryStream(Properties.Resources.toonmono);
                Cursor = new Cursor(cursorMemoryStream);
            }
            else
            {
                Cursor = Cursors.Default;
            }
            ActiveControl = null;
        }
        #endregion
        #region Credits
        private void BtnCredits_Click(object sender, EventArgs e)
        {
            Audio.PlaySoundFile("sndclick");
            FrmCredits cred = new FrmCredits();
            cred.ShowDialog();
            ActiveControl = null;
        }
        #endregion
        #region Main Button Events
        private void Button_MouseEnter(object sender, EventArgs e)
        {
            // Take the name of the button
            Button btnSender = (Button)sender;
            string btnName = btnSender.Name;
            btnName = btnName.Replace("btn", "").ToLower();
            if (string.IsNullOrEmpty(txtUser.Text) && btnName == "play")
            {
                btnName = "create";
            }
            btnSender.BackgroundImage = Background.ImageChooser(btnName, "MouseEnter");
        }
        private void Button_MouseLeave(object sender, EventArgs e)
        {
            // Take the name of the button
            Button btnSender = (Button)sender;
            string btnName = btnSender.Name;
            btnName = btnName.Replace("btn", "").ToLower();
            if (string.IsNullOrEmpty(txtUser.Text) && btnName == "play")
            {
                btnName = "create";
            }
            btnSender.BackgroundImage = Background.ImageChooser(btnName, "MouseLeave");
        }
        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            // Take the name of the button
            Button btnSender = (Button)sender;
            string btnName = btnSender.Name;
            btnName = btnName.Replace("btn", "").ToLower();
            if (string.IsNullOrEmpty(txtUser.Text) && btnName == "play")
            {
                btnName = "create";
            }
            btnSender.BackgroundImage = Background.ImageChooser(btnName, "MouseDown");
        }
        private void Button_MouseUp(object sender, MouseEventArgs e)
        {
            // Take the name of the button
            Button btnSender = (Button)sender;
            string btnName = btnSender.Name;
            btnName = btnName.Replace("btn", "").ToLower();
            if (string.IsNullOrEmpty(txtUser.Text) && btnName == "play")
            {
                btnName = "create";
            }
            btnSender.BackgroundImage = Background.ImageChooser(btnName, "MouseUp");
        }
        #endregion
        #region Play on Enter
        private void TxtPassAndUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (btnPlay.Enabled)
                {
                    btnPlay.PerformClick();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        #endregion
        #endregion
        #region Web Browser / News
        // Place any web browser events inside here
        private void WebBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (e.Url.ToString().Contains("https://projectaltis.com/launcher"))
            {
                return;
            }
            e.Cancel = true;
            Log.TryOpenUrl(e.Url.ToString());
        }

        #endregion

        #region Directory Things

        /// <summary>
        /// Determines whether the current directory can be written to.
        /// </summary>
        /// <returns><c>true</c> if the directory is writeable; otherwise, <c>false</c>.</returns>
        private bool IsWriteable()
        {
            try
            {
                Log.Info("Checking if directory is writeable.");
                if (!File.Exists(_currentDir + "writeTest"))
                {
                    using (File.Create(_currentDir + "writeTest"))
                    {
                    }
                    File.Delete(_currentDir + "writeTest");
                }
                Log.Info("Directory is writeable.");
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Exception thrown, not writeable");
                Log.Error(ex);
                return false;
            }
        }

        private void ValidatePrefJson()
        {
            if(!File.Exists("preferences.json"))
            {
                // Will be automatically created by the game
                return;
            }
            string prefFile = File.ReadAllText("preferences.json");
            try
            {
                JObject contents = JObject.Parse(prefFile);
            }
            catch (JsonReaderException ex)
            {
                File.Delete("preferences.json");
                Log.Info("DELETED Preferences.json due to it being corrupted");
            }
            catch(Exception ex)
            {
                Log.Error(ex);
                throw;
            }

        }
        #endregion

        private void OnFilesUpdated()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    OnFilesUpdated();
                });
            }
            else
            {
                Log.Info("||||||||||");
                Log.Info("Files have been verified. Starting game!");
                Log.Info("||||||||||");

                lblNowDownloading.Text = "Have fun!";
                // The progress bar visibility should be invisible because
                // game files are no longer being updated
                pbDownload.Visible = false; 
                Thread playThread = new Thread(() =>
                {
                    Play.LaunchGame(txtUser.Text, txtPass.Text, this);
                });

                playThread.Start();
                btnPlay.Enabled = true;
            }
        }

        private void RunUpdater()
        {
            FileUpdater fileUpdater = new FileUpdater(this);
            fileUpdater.FilesUpdated += OnFilesUpdated;
            Thread updaterThread = new Thread(fileUpdater.RunUpdater);
            try
            {
                updaterThread.Start();
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("Unable to start the updating process. It appears your computer is out of memory.");
            }
            catch (ThreadStateException)
            {
                MessageBox.Show("The updater thread could not be started. Try and restarting the launcher.");
            }
        }

        private void LoadUserSettings()
        {
            try
            {
                txtUser.Text = Properties.Settings.Default.Username;
                if (Properties.Settings.Default.WantsPassword)
                {
                    txtPass.Text = UwpHelper.GetPassword();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            // Read user settings
            if (Properties.Settings.Default.WantsCursor) // Old Toontown Cursors
            {
                MemoryStream cursorMemoryStream = new MemoryStream(Properties.Resources.toonmono);
                Cursor = new Cursor(cursorMemoryStream);
            }
            // Load last saved user background choice
            BackgroundImage.Dispose();
            if (Properties.Settings.Default.WantsRandomBg)
            {
                BackgroundImage = Background.ReturnRandomBackground();
            }
            else
            {
                BackgroundImage = Background.ReturnBackground(Properties.Settings.Default.Background);
            }
        }

        public void SaveCredentials()
        {
            Properties.Settings.Default.Username = txtUser.Text;
            Properties.Settings.Default.Save();
            if (Properties.Settings.Default.WantsPassword)
            {
                Log.Info("Trying to save password securely...");
                try
                {
                    UwpHelper.SetCredentials(txtUser.Text, txtPass.Text);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    Log.Error("Don't want to break after trying to save pass so continuing");
                }
            }
        }

        private void TxtUser_TextChanged(object sender, EventArgs e)
        {
            Button_MouseLeave(btnPlay, EventArgs.Empty);
        }
    }
}