using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Classifier {
    public partial class Form1 : Form {
        String imageDir = null;
        String imageFile = null;
        Boolean toldDone = false;
        int imageCount = 0;
        int completedCount = 0;

        public Form1() {
            InitializeComponent();

            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(this.buttonSL, "Slight left (Q) - Move left to avoid obstruction");
            toolTip1.SetToolTip(this.buttonF, "Forward (W) - No obstructions for at least a few feet");
            toolTip1.SetToolTip(this.buttonSR, "Slight right (E) - Move right to avoid obstruction");
            toolTip1.SetToolTip(this.buttonL, "Full left - Move left to avoid nearby obstruction");
            toolTip1.SetToolTip(this.buttonB, "Backwards - Completely blocked, move backwards");
            toolTip1.SetToolTip(this.buttonR, "Full right - Move right to avoid nearby obstruction");
            toolTip1.SetToolTip(this.buttonDelete, "Bad image - Incomplete, too dark, or unclassifiable");

            // Speed up classification by allowing users to press buttons
            this.KeyDown += new KeyEventHandler(this.onKeyDown);

            loadImages(".\\");
        }

        private void onKeyDown(object sender, KeyEventArgs e) {
            Control[] target = null;
            switch(e.KeyCode) {
                case Keys.Q:
                    target = this.Controls.Find("buttonSL", true);
                    break;
                case Keys.W:
                    target = this.Controls.Find("buttonF", true);
                    break;
                case Keys.E:
                    target = this.Controls.Find("buttonSR", true);
                    break;
                case Keys.A:
                    target = this.Controls.Find("buttonL", true);
                    break;
                case Keys.S:
                    target = this.Controls.Find("buttonB", true);
                    break;
                case Keys.D:
                    target = this.Controls.Find("buttonR", true);
                    break;
                case Keys.Delete:
                    target = this.Controls.Find("buttonDelete", true);
                    break;
                case Keys.R:
                    moveListToRandom();
                    return;
                case Keys.N:
                    moveListToNext();
                    return;                    
            }

            if (target != null && target.Length == 1) {
                ((Button)target[0]).PerformClick();
            }            
        }

        private void loadImages(String dir) {
            imageDir = dir;
            completedCount = 0;

            IEnumerable<string> files = Directory.EnumerateFiles(dir, "*.ppm");

            imageCount = files.Count();

            ImageList images = new ImageList();
            images.ImageSize = new Size(64, 64);

            listView1.Clear(); // Clear existing items
            listView1.LargeImageList = images;
            
            foreach (String file in files) {
                String filename = Path.GetFileName(file);
                PixelMap pix = new PixelMap(file);

                Image img = Image.FromHbitmap(pix.BitMap.GetHbitmap());
                images.Images.Add(filename, img);
                ListViewItem viewItem = listView1.Items.Add(filename);

                viewItem.Tag = getClassTag(pix);

                if (viewItem.Tag != null) {
                    completedCount++;
                }

                viewItem.ImageKey = filename;
            }

            updateCompletionStatus();
            moveListToNext();
        }

        private object getClassTag(PixelMap pix) {
            PixelMap.PixelMapHeader header = pix.Header;

            String tag = null;
            foreach(String comment in header.Comments) {
                if (comment.IndexOf("Class: ") != -1) {
                    tag = comment.Substring(8);
                    break;                        
                }
            }

            return tag;
        }

        private void updateImageDir(object sender, EventArgs e) {
            FolderBrowserDialog diag = new FolderBrowserDialog();
            DialogResult result = diag.ShowDialog();

            if (result == DialogResult.OK) {
                loadImages(diag.SelectedPath);
            }
        }

        private void itemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
            imageFile = e.Item.ImageKey;
            label1.Text = imageDir + "\\" + imageFile;

            pictureBox1.Image = new PixelMap(imageDir + "\\" + imageFile).BitMap;

            setSelectedAction(e.Item.Tag);
        }

        private void setSelectedAction(object action) {
            foreach (Control control in this.Controls) {
                if (control.GetType() == typeof(Button)) {
                    if (control.Text == (String)action) {
                        control.BackColor = Color.Tomato;
                    } else {
                        control.BackColor = Button.DefaultBackColor;
                    }
                }
            }
        }

        private void imageButtonClicked(object sender, EventArgs e) {
            if (imageFile == null || imageDir == null) {
                return;
            }

            String classification = ((Button)sender).Text;           

            // Write action to image comments
            int index = listView1.SelectedIndices[0];

            if (listView1.Items[index].Tag == null) {
                completedCount++;
            }

            listView1.Items[index].Tag = classification; // Tag list view item for searching

            String fromFile = imageDir + "\\" + listView1.Items[index].ImageKey;
            PixelMap pix = new PixelMap(fromFile);

            List<String> comments = new List<string>();
            comments.Add("Class: " + classification);
            comments.Add("Hostname: " + System.Environment.MachineName.ToString());
            comments.Add("Date: " + DateTime.Now.ToUniversalTime().ToString());

            PixelMap.PixelMapHeader header = pix.Header;
            header.Comments = comments;
            pix.Header = header;

            String toFile = imageDir + "\\" + listView1.Items[index].ImageKey;
            pix.Save(toFile);

            
            updateCompletionStatus();
            moveListToNext();
        }

        private void moveListToNext() {
            if (listView1.Items.Count == 0) {
                return;
            }

            int i = 0;
            for (; i < listView1.Items.Count; i++) {
                if (listView1.Items[i].Tag == null) {
                    break;
                }
            }

            // Check we didn't find anything
            if (i == listView1.Items.Count) {
                if (toldDone != true) {
                    toldDone = true;
                    MessageBox.Show("Thank you. Please zip up the extracted directory and send it back.", "Done!", MessageBoxButtons.OK);
                }
                return;
            }

            listView1.Items[i].Selected = true;
            listView1.EnsureVisible(i);
        }

        private void moveListToRandom() {
            if (listView1.Items.Count == 0) {
                return;
            }

            Random rando = new Random();
            int i = rando.Next(listView1.Items.Count);

            listView1.Items[i].Selected = true;
            listView1.EnsureVisible(i);
        }

        private void updateCompletionStatus() {
            if (imageCount > 0) {
                float completionPercentage = (float)completedCount / imageCount * 100;
                toolStripStatusLabel1.Text = completedCount + " / " + imageCount +
                    " (" + Math.Round(completionPercentage, 1) + "%)";
            } else {
                toolStripStatusLabel1.Text = "No *.ppm images found";
            }

            // Show some detials in status bar
            statusStrip1.Invalidate();
            statusStrip1.Refresh();
        }
    }
}
