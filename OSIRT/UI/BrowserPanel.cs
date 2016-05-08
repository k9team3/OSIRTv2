﻿using System;
using System.Drawing;
using System.Windows.Forms;
using OSIRT.UI.Attachment;
using OSIRT.UI.AuditLog;
using OSIRT.UI.CaseNotes;
using OSIRT.VideoCapture;
using OSIRT.UI.Options;

namespace OSIRT.UI
{

    public partial class BrowserPanel : UserControl
    {

        public event EventHandler CaseClosing;


        public BrowserPanel()
        {
            InitializeComponent();
            uiTabbedBrowserControl.SetAddressBar(uiURLComboBox);
        }


        private void BrowserPanel_Load(object sender, EventArgs e)
        {
            ConfigureUi();
            AddNewTab();
            uiAuditLogToolStripButton.Alignment = ToolStripItemAlignment.Right;
            uiTabbedBrowserControl.ScreenshotComplete += UiTabbedBrowserControl_ScreenshotComplete;
            OsirtVideoCapture.VideoCaptureComplete += osirtVideoCapture_VideoCaptureComplete;
        }

        private void UiTabbedBrowserControl_ScreenshotComplete(object sender, EventArgs e)
        {
            uiScreenshotButton.Enabled = true;
        }

        private void ConfigureUi()
        {
            Dock = DockStyle.Fill;
            uiBrowserToolStrip.ImageScalingSize = new Size(32, 32);
            uiURLComboBox.KeyDown += UiURLComboBox_KeyDown;

        }

        private void UiURLComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                uiTabbedBrowserControl.Navigate(uiURLComboBox.Text);
                e.SuppressKeyPress = true; //stops "ding" sound when enter is pushed
            }
        }

        private void uiAddTabButton_Click(object sender, EventArgs e)
        {
            //AddNewTab();
        }


        private void uiScreenshotButton_Click(object sender, EventArgs e)
        {
            uiScreenshotButton.Enabled = false;
            uiTabbedBrowserControl.FullPageScreenshot();
            //uiScreenshotButton.Enabled = true;
        }

        private void AddNewTab()
        {
            //uiTabbedBrowserControl.NewTab(UserSettings.Load().Homepage, uiURLComboBox);
        }

        private void uiBrowserMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void uiCaseNotesButton_Click(object sender, EventArgs e)
        {
            CaseNotesForm caseNotes = new CaseNotesForm();
            caseNotes.Show();
        }

        private void auditLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //using(AuditLogForm audit = new AuditLogForm())
            //{
            //    audit.ShowDialog();
            //}
        }

        private void uiAttachmentToolStripButton_Click(object sender, EventArgs e)
        {
            using(AttachmentForm attachment = new AttachmentForm())
            {
                attachment.ShowDialog();
            }
        }

        private void uiLBackButton_Click(object sender, EventArgs e)
        {
            //TODO: check if can back
            uiTabbedBrowserControl.CurrentTab.Browser.GoBack();
        }

        private void uiForwardButton_Click(object sender, EventArgs e)
        {
            //TODO: check if can go forward
            uiTabbedBrowserControl.CurrentTab.Browser.GoForward();
        }

        private void uiRefreshButton_Click(object sender, EventArgs e)
        {
            uiTabbedBrowserControl.CurrentTab.Browser.Refresh();
        }

        private void closeCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CaseClosing?.Invoke(this, e);
        }


        private void uiVideoCaptureButton_Click(object sender, EventArgs e)
        {

            uiTabbedBrowserControl.CurrentTab.Browser.ShowMouseTrail = true;

            if (!OsirtVideoCapture.IsRecording())
                OsirtVideoCapture.StartCapture(Width, Height, uiVideoCaptureButton, (uint)Handle);
            else
                OsirtVideoCapture.StopCapture();
        }

        private void osirtVideoCapture_VideoCaptureComplete(object sender, EventArgs e)
        {
            uiTabbedBrowserControl.CurrentTab.Browser.ShowMouseTrail = false;
            VideoCaptureCompleteEventArgs ev = (VideoCaptureCompleteEventArgs)e;

            using (VideoPreviewer vidPreviewer = new VideoPreviewer(Enums.Actions.Video))
            {
                vidPreviewer.ShowDialog();
            }
        }

        private void uiAuditLogToolStripButton_Click(object sender, EventArgs e)
        {
            using (AuditLogForm audit = new AuditLogForm())
            {
                audit.ShowDialog();
            }
        }

        private void uiOptionsToolStripButton_Click(object sender, EventArgs e)
        {
            using (OptionsForm options = new OptionsForm())
            {
                options.ShowDialog();
            }
        }
    }
}
