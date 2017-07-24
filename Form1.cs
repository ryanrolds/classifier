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

        public Form1() {
            InitializeComponent();

            loadImages(".\\");
        }

        private void loadImages(String dir) {
            imageDir = dir;

            IEnumerable<string> files = Directory.EnumerateFiles(dir, "*.ppm");

            // Show some detials in status bar
            statusStrip1.Invalidate();
            statusStrip1.Refresh();

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

                viewItem.ImageKey = filename;
            }

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
    }
}
