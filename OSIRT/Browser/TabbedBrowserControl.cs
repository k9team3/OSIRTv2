﻿using System;
using System.Windows.Forms;
using System.IO;
using OSIRT.UI;
using OSIRT.Helpers;
using OSIRT.Loggers;
using System.Diagnostics;
using OSIRT.Properties;

namespace OSIRT.Browser
{


    public partial class TabbedBrowserControl : UserControl
    {

        private ToolStripComboBox addressBar;

        public TabbedBrowserControl()
        {
            InitializeComponent();
            //uiBrowserTabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            //uiBrowserTabControl.DrawItem += UiBrowserTabControl_DrawItem;
        }
  
        private void UiBrowserTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {

           
        }

        public BrowserTab CurrentTab //active tab
        {
            get
            {
                BrowserTab page = null;
                if (uiBrowserTabControl.SelectedTab != null)
                {
                    page = uiBrowserTabControl.SelectedTab as BrowserTab;
                }
                 return page;
             }
        }

        private ExtendedBrowser CurrentBrowser
        {
            get
            {
                return CurrentTab.Browser;
            }
        }

        private BrowserTab CreateTab()
        {
            BrowserTab tab = new BrowserTab();
            uiBrowserTabControl.TabPages.Add(tab);
            uiBrowserTabControl.SelectedTab = tab;
            //TODO: Unsubscribe from this event once tab has closed?
            AddBrowserEvents();
            return tab;
        }

        private void AddBrowserEvents()
        {
            CurrentBrowser.StatusTextChanged += Browser_StatusTextChanged;
            CurrentBrowser.Navigated += CurrentBrowser_Navigated;
            CurrentBrowser.Screenshot_Completed += Screenshot_Completed;
        }

        private void CurrentBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            CurrentTab.CurrentURL = CurrentBrowser.Url.AbsoluteUri;
            addressBar.Text = CurrentTab.CurrentURL;
        }

        private void Screenshot_Completed(object sender, ScreenshotCompletedEventArgs e)
        {
    
            //Log: Perhaps move this into the ImagePreviewer?
            //TODO: Get Hash from user settings (fixed as sha1, for now)
            //TODO: Have the wait window display while hashing
            

            ScreenshotDetails details = new ScreenshotDetails(CurrentBrowser.URL);

            //Debug.WriteLine("Algo: {0}. Hash: {1} ", Settings.Default.Hash, hash);
            string tempImgPath = Path.Combine(Constants.CacheLocation, "temp.png");
            ImagePreviewerForm previewForm = new ImagePreviewerForm(tempImgPath, details);
            DialogResult res =  previewForm.ShowDialog();

         
            if (res != DialogResult.OK)
                return;


            Logger.Log(new WebpageActionsLog(CurrentBrowser.URL, Constants.Actions.Screenshot, "GET HASH", "temp.png", "This is an example note"));

            //TODO: Delete image cache
        }

        void Browser_StatusTextChanged(object sender, EventArgs e)
        {
            uiStatusLabel.Text = CurrentBrowser.StatusText;
        }

        public void GetFullPageScreenshot()
        {
            if (CurrentTab == null)
                throw new NullReferenceException("No tabs to screenshot"); //TODO: Handle this better

            CurrentBrowser.GenerateFullpageScreenshot(); 
        }

        public void NewTab(string url, ToolStripComboBox urlBar)
        {
            addressBar =  urlBar;
            CreateTab();
            Navigate(url);
        }

        public void Navigate(string url)
        {
            if (CurrentBrowser != null)
            {
                CurrentTab.Browser.Navigate(url);
            }
        }

        private void uiBrowserTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {

            if(CurrentTab.Browser != null)
                addressBar.Text = CurrentTab.CurrentURL;
        }


    }
}
