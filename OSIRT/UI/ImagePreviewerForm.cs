﻿using Cyotek.Windows.Forms;
using ImageMagick;
using OSIRT.Helpers;
using OSIRT.Properties;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OSIRT.UI
{
    public partial class ImagePreviewerForm : Form
    {

        private ImageBox imageBox;
        private Image image;
        private string imagePath;
        private ScreenshotDetails details;


        public ImagePreviewerForm()
        {
            InitializeComponent();
        }

        public ImagePreviewerForm(string imagePath, ScreenshotDetails details) : this()
        {
            this.imagePath = imagePath;
            this.details = details;
        }

        public ImagePreviewerForm(Image image) : this()
        {
            this.image = image;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
           

            uiURLTextBox.Text = details.URL;
            //uiHashTextBox.Text = details.Hash;
            uiDateAndTimeTextBox.Text = details.Date + " " + details.Time;


            //TODO: very large images cause this to just die.
  

            int width, height;
            using(MagickImage image = new MagickImage(imagePath))
            {
                width = image.Width;
                height = image.Height;
            }

            if(height < 10000)
            {
                //TODO: Display a reduced sized image?
                imageBox = new ImageBox();
                imageBox.Dock = DockStyle.Fill;
                uiSplitContainer.Panel2.Controls.Add(imageBox);
                LoadImage(Image.FromFile(imagePath));
            }
            else //too big, display a panel with a label and link to open in the system's default application
            {
                CannotOpenImagePanel cantOpen = new CannotOpenImagePanel();
                cantOpen.Dock = DockStyle.Fill;
                uiSplitContainer.Panel2.Controls.Add(cantOpen);
            }
        }

        private void LoadImage(Image image)
        {
            imageBox.Image = image;
            imageBox.ZoomToFit();
            //imageBox.Refresh();
        }


        private void uiCalcHashLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HashService hashService = HashServiceFactory.Create(Settings.Default.Hash);
            string hash = "";
            //TODO: Check this is the right image!
            string tempImgPath = Path.Combine(Constants.CacheLocation, "temp.png");
            using (FileStream fileStream = File.OpenRead(tempImgPath))
            {
                hash = hashService.ToHex(hashService.ComputeHash(fileStream));
            }

            uiHashTextBox.Text = hash;
        }

        private void ImagePreviewerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
       
            imageBox?.Image.Dispose();
            //imageBox.Dispose();
        }

     

        private void ImagePreviewerForm_Load(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (MagickImage image = new MagickImage(imagePath))
            {
                using (MagickImage watermark = new MagickImage(@"C:\Users\Joe\Documents\ccculogo.gif"))
                {
                    image.Composite(watermark, Gravity.Southeast, CompositeOperator.Over);
                    watermark.Evaluate(Channels.Alpha, EvaluateOperator.Divide, 1);
                }
                image.Format = MagickFormat.Pdf;
                image.Write(@"C:\Users\Joe\Documents\testimage.pdf");
            }
        }

     
    }
}
