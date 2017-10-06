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
            IEnumerable<string> files = Directory.EnumerateFiles(dir, "*.ppm");            
            imageCount = files.Count();
            if (imageCount == 0) {
                return;
            }

            imageDir = dir;
            completedCount = 0;
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

            analyzeImagesToolStripMenuItem.Enabled = true;
            mergeImagesToolStripMenuItem.Enabled = true;
            compareImagesToolStripMenuItem.Enabled = true;

            updateCompletionStatus();
            moveListToNext();
        }

        private object getClassTag(PixelMap pix) {
            return pix.Header.GetComment("Class");
        }

        private void updateImageDir(object sender, EventArgs e) {
            FolderBrowserDialog diag = new FolderBrowserDialog();
            diag.RootFolder = Environment.SpecialFolder.MyComputer;
            diag.SelectedPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

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

            pix.Header.SetComment("Class", classification);
            pix.Header.SetComment("Hostname", System.Environment.MachineName.ToString());
            pix.Header.SetComment("Date", DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff"));

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

        private void oAnalyzeImagesMenuClick(object sender, EventArgs e) {
            if (imageDir != null) {
                analyzeImages(imageDir);
            }
        }

        private void analyzeImages(String analyzeDir) {
            // Get list of incoming files
            IEnumerable<string> files = Directory.EnumerateFiles(analyzeDir, "*.ppm");

            // Setup variables to hold analyais
            // Average time between classifications
            int totalImages = files.Count();           
            String prevImageDate = null; // Nonnullable DateTime is BS
            List<double> diffs = new List<double>();

            int numBad = 0;
            int numForward = 0;
            int numSlightLeft = 0;
            int numLeft = 0;
            int numSlightRight = 0;
            int numRight = 0;
            int numBackwards = 0;

            // Iterate sorted files (ensures dates are in order)
           
            IOrderedEnumerable<string> sortedFiles = files.OrderBy(s => (new PixelMap(s)).Header.GetComment("Date"));
            foreach (String file in sortedFiles) {
                // Load existing file's metadata
                PixelMap pix = new PixelMap(file);

                // Update analytics
                String classification = pix.Header.GetComment("Class");
                if (classification != null) {
                    switch(classification) {
                        case "SL":
                            numSlightLeft++;
                            break;
                        case "L":
                            numLeft++;
                            break;
                        case "SR":
                            numSlightRight++;
                            break;
                        case "R":
                            numRight++;                        
                            break;
                        case "F":
                            numForward++;
                            break;
                        case "B":
                            numBackwards++;
                            break;
                        case "Bad":
                            numBad++;
                            break;
                    }
                }

                // Calculate averge time spent classifying the frames                         
                String dateStr = pix.Header.GetComment("Date");
                if (dateStr != null) {
                    DateTime date;
                    if (dateStr.Contains("/")) {
                        date = DateTime.Parse(dateStr);
                    } else {
                        date = DateTime.ParseExact(dateStr, "yyyyMMdd HH:mm:ss.fff", null);
                    }

                    // Check if we have a previous frame
                    if (prevImageDate != null) {
                        DateTime prevDate;
                        if (prevImageDate.Contains("/")) {
                            prevDate = DateTime.Parse(prevImageDate);
                        } else {
                            prevDate = DateTime.ParseExact(prevImageDate, "yyyyMMdd HH:mm:ss.fff", null);
                        }

                        
                        // Add time since last to sum
                        TimeSpan diff = date - prevDate;
                        diffs.Add(diff.TotalMilliseconds);
                    }

                    // Update previous frame
                    prevImageDate = dateStr;
                }
            }

            diffs.Sort();
            double medianTime = diffs[diffs.Count() / 2];

            double sumTimeMS = diffs.Sum();
            double averageTime = diffs.Average();            
            double minTime = diffs.Max();

            // Output analysis in dialog
            String analysis = "Time:\n" +
                "Median: " + medianTime + "\n" +
                "Average: " + averageTime + "\n" +
                "Total images: " + totalImages + "\n" +
                "Bad: " + numBad + "\n" +
                "Total L: " + numLeft + "\n" +
                "Total SL: " + numSlightLeft + "\n" +
                "Total F: " + numForward + "\n" +
                "Total SR: " + numSlightRight + "\n" +
                "Total R: " + numRight + "\n" +
                "Total B: " + numBackwards;
            MessageBox.Show(analysis, "Analysis", MessageBoxButtons.OK);
        }

        private void onCompareImagesMenuClick(object sender, EventArgs e) {
            FolderBrowserDialog diag = new FolderBrowserDialog();
            diag.RootFolder = Environment.SpecialFolder.MyComputer;
            diag.SelectedPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

            DialogResult result = diag.ShowDialog();
            if (result == DialogResult.OK) {
                compareImages(diag.SelectedPath);
            }
        }

        private void compareImages(String compareDir) {
            // Get list of incoming files

            // Setup variables to hold analyais

            // Iterate files
            // Load existing file's metadata
            // Load incoming files metadadta
            // Compare metadata

            // Update analytics
            
            // Output analtycs in dialog        
        }

        private void onMergeImagesMenuClick(object sender, EventArgs e) {
            FolderBrowserDialog diag = new FolderBrowserDialog();
            diag.RootFolder = Environment.SpecialFolder.MyComputer;
            diag.SelectedPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

            DialogResult result = diag.ShowDialog();
            if (result == DialogResult.OK) {
                mergeImages(diag.SelectedPath);
            }
        }

        private void mergeImages(String incomingDir) {
            if (imageDir == null) {
                return;
            }

            IEnumerable<string> files = Directory.EnumerateFiles(incomingDir, "*.ppm");            
            foreach (String incomingFile in files) {
                PixelMap incoming = new PixelMap(incomingFile);

                String incomingClass = incoming.Header.GetComment("Class");
                double incomingScore = Convert.ToDouble(incoming.Header.GetComment("Score", "2.0")); // 2.0 == null
                int incomingScoreCount = Convert.ToInt32(incoming.Header.GetComment("ScoreCount", "1"));
                double incomingConfidence = Convert.ToDouble(incoming.Header.GetComment("ScoreConf", "1.0"));

                if (incomingScore == 2.0) { // 2.0 == null                    
                    incomingScore = getScoreFromImageClassification(incomingClass);
                }

                String filename = Path.GetFileName(incomingFile);
                String targetFile = imageDir + '\\' + filename;                
       
                // No target file, save incoming as target
                if (File.Exists(targetFile) == false) {
                    // Set metadata and save in current dest dir
                    incoming.Header.SetComment("Score", Convert.ToString(incomingScore));
                    incoming.Header.SetComment("ScoreCount", incoming.Header.GetComment("ScoreCount", "1"));
                    incoming.Header.SetComment("ScoreError", incoming.Header.GetComment("ScoreConf", "1.0"));

                    incoming.Save(targetFile);
                    continue;
                }
                                
                // Load target file's metadata                
                PixelMap target = new PixelMap(targetFile);              

                // Update analytics
                String classification = target.Header.GetComment("Class");
                if (classification != null) {
                    // Merge classifiction
                    String targetClass = target.Header.GetComment("Class");
                    double targetScore = Convert.ToDouble(target.Header.GetComment("Score", Convert.ToString(getScoreFromImageClassification(targetClass))));
                    int targetScoreCount = Convert.ToInt32(target.Header.GetComment("ScoreCount", "1"));
                    double targetConfidence = Convert.ToDouble(target.Header.GetComment("ScoreConf", "1.0"));

                    double updatedScore = (targetScore + incomingScore) / 2;

                    // Bad images should stay bad
                    if (targetClass == "Bad" || incomingClass == "Bad") {
                        updatedScore = getScoreFromImageClassification("Bad");
                    }

                    String updatedClassification = getClassificationFromImageScore(updatedScore);
                    double incomingDistance = 1 - Math.Abs(targetScore - incomingScore);
                    double updatedConfidence = (targetConfidence + incomingDistance) / 2;                                    
                    
                    // Set merged metadata and sve in current est dir
                    target.Header.SetComment("Score", Convert.ToString(updatedScore));
                    target.Header.SetComment("ScoreCount", Convert.ToString(targetScoreCount + 1));
                    target.Header.SetComment("ScoreConf", Convert.ToString(updatedConfidence));
                    
                    target.Header.SetComment("Class", updatedClassification);
                } else {
                    throw new Exception("Unclassified image");
                }

                // Save updated target
                target.Save(targetFile);
            }
        }

        private void onCreateTxtMenuClick(object sender, EventArgs e) {
            createTxtFiles();
        }

        private void createTxtFiles() {
            if (imageDir == null) {
                return;
            }

            FileStream stream = new FileStream(imageDir + "/classes.txt", FileMode.Create);

            IEnumerable<string> files = Directory.EnumerateFiles(imageDir, "*.ppm");
            foreach (String incomingFile in files) {                
                PixelMap incoming = new PixelMap(incomingFile);
                String imageClass = incoming.Header.GetComment("Class");
                String record = incomingFile + " " + imageClassToNumber(imageClass) + "\n";
                byte[] bytes = Encoding.ASCII.GetBytes(record);
                stream.Write(bytes, 0, bytes.Length);
            }

            stream.Close();
        }

        private int imageClassToNumber(String classification) {
            switch (classification) {
                case "B":
                    return 0;
                case "BR":
                    return 1;
                case "R":
                    return 2;
                case "SR":
                    return 3;
                case "F":
                    return 4;
                case "SL":
                    return 5;
                case "L":
                    return 6;
                case "BL":
                    return 7;
                case "Bad":
                    return 8;
                default:
                    throw new Exception("Invalid class:" + classification);
            }
        }

        private double getScoreFromImageClassification(String classification) {
            switch(classification) {
                case "B":
                    return -1.0;
                case "BR":
                    return 0.75;
                case "R":
                    return 0.45;
                case "SR":
                    return 0.30;
                case "F":
                    return 0.0;
                case "SL":
                    return -0.30;
                case "L":
                    return -0.45;
                case "BL":
                    return -0.75;
                case "Bad":
                    return -2.0;
                default:
                    throw new Exception("Invalid class:" + classification);
            }
        }

        private string getClassificationFromImageScore(double score) {
            if (score > 0.75) {
                return "B";
            } else if (score > 0.55) {
                return "BL";
            } else if (score > 0.35) {
                return "R";
            } else if (score > 0.15) {
                return "SR";
            } else if (score > -0.15) {
                return "F";
            } else if (score > -0.35) {
                return "SL";
            } else if (score > -0.55) {
                return "L";            
            } else if (score > -0.75) {
                return "BR";
            } else if (score > -1.1) {
                return "Bad";
            } else {
                return "B";
            }

            throw new Exception("Should not happen");
        }
    }
}
