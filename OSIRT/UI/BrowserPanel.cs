﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using OSIRT.Helpers;
using OSIRT.UI.Attachment;
using OSIRT.UI.CaseNotes;

namespace OSIRT.UI
{

    public partial class BrowserPanel : UserControl
    {

        public BrowserPanel()
        {
            InitializeComponent();
        }


        private void BrowserPanel_Load(object sender, EventArgs e)
        {
            ConfigureUI();
            AddNewTab();
        }

        private void ConfigureUI()
        {
            this.Dock = DockStyle.Fill;
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
            AddNewTab();
        }


        private void uiScreenshotButton_Click(object sender, EventArgs e)
        {
            uiTabbedBrowserControl.GetFullPageScreenshot();
        }

        private void AddNewTab()
        {
            uiTabbedBrowserControl.NewTab("http://bbc.co.uk", uiURLComboBox);
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
            using(AuditLogForm audit = new AuditLogForm())
            {
                audit.ShowDialog();
            }
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
    }
}
