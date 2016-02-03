﻿using ImageMagick;
using Jacksonsoft;
using mshtml;
using OSIRT.Browser;
using OSIRT.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OSIRT
{
    public partial class Form1 : Form
    {
      

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        
           // uiBrowserTabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            //uiBrowserTabControl.DrawItem += uiBrowserTabControl_DrawItem;
            //uiBrowserTabControl.MouseDown += uiBrowserTabControl_MouseDown;

            //uiBrowserTabControl.SizeMode = TabSizeMode.Fixed;
       
        }

        void uiBrowserTabControl_MouseDown(object sender, MouseEventArgs e)
        {
            //for (int i = 0; i < this.uiBrowserTabControl.TabPages.Count; i++)
            //{
            //    Rectangle r = uiBrowserTabControl.GetTabRect(i);
            //    //Getting the position of the "x" mark.
            //    Rectangle closeButton = new Rectangle(r.Right - 15, r.Top + 4, 9, 7);
            //    if (closeButton.Contains(e.Location))
            //    {
            //        if (MessageBox.Show("Would you like to Close this Tab?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //        {
            //            this.uiBrowserTabControl.TabPages.RemoveAt(i);
            //            break;
            //        }
            //    }
            //}
        }

        void uiBrowserTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
           // e.Graphics.DrawString("x", e.Font, Brushes.Red, e.Bounds.Right - 15, e.Bounds.Top + 4);
           //// e.Graphics.DrawString(this.uiBrowserTabControl.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + 12, e.Bounds.Top + 4);
           // e.DrawFocusRectangle();
        }

     

        
        private void button2_Click(object sender, EventArgs e)
        {
            //using (Bitmap fullpage = browser.GetFullpageScreenshot())
            //{
            //    fullpage.Save(@"D:/fullpageEX.png");
            //}
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            uiTabbedBrowserControl.NewTab("http://google.co.uk");
        }

        private void uiURLtoolStripComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                uiTabbedBrowserControl.Navigate(uiURLtoolStripComboBox.Text);
            }
        }

        private void fullpageScreenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string location = uiTabbedBrowserControl.GetFullPageScreenshot();
            
            //using (Bitmap fullpage = uiTabbedBrowserControl.GetFullPageScreenshot())
           // {
               // fullpage.Save(@"D:/fullpageEX_TAB.png");
            //}
        }
    }
}
