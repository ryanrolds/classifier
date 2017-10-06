namespace Classifier {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analyzeImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compareImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createTrainvalFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSL = new System.Windows.Forms.Button();
            this.buttonF = new System.Windows.Forms.Button();
            this.buttonSR = new System.Windows.Forms.Button();
            this.buttonR = new System.Windows.Forms.Button();
            this.buttonL = new System.Windows.Forms.Button();
            this.buttonB = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.versionLabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(584, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadImagesToolStripMenuItem,
            this.analyzeImagesToolStripMenuItem,
            this.compareImagesToolStripMenuItem,
            this.mergeImagesToolStripMenuItem,
            this.createTrainvalFileToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadImagesToolStripMenuItem
            // 
            this.loadImagesToolStripMenuItem.Name = "loadImagesToolStripMenuItem";
            this.loadImagesToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.loadImagesToolStripMenuItem.Text = "Load Images";
            this.loadImagesToolStripMenuItem.Click += new System.EventHandler(this.updateImageDir);
            // 
            // analyzeImagesToolStripMenuItem
            // 
            this.analyzeImagesToolStripMenuItem.Enabled = false;
            this.analyzeImagesToolStripMenuItem.Name = "analyzeImagesToolStripMenuItem";
            this.analyzeImagesToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.analyzeImagesToolStripMenuItem.Text = "Analyze Images";
            this.analyzeImagesToolStripMenuItem.Click += new System.EventHandler(this.oAnalyzeImagesMenuClick);
            // 
            // compareImagesToolStripMenuItem
            // 
            this.compareImagesToolStripMenuItem.Enabled = false;
            this.compareImagesToolStripMenuItem.Name = "compareImagesToolStripMenuItem";
            this.compareImagesToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.compareImagesToolStripMenuItem.Text = "Compare Images";
            this.compareImagesToolStripMenuItem.Click += new System.EventHandler(this.onCompareImagesMenuClick);
            // 
            // mergeImagesToolStripMenuItem
            // 
            this.mergeImagesToolStripMenuItem.Enabled = false;
            this.mergeImagesToolStripMenuItem.Name = "mergeImagesToolStripMenuItem";
            this.mergeImagesToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.mergeImagesToolStripMenuItem.Text = "Merge Images";
            this.mergeImagesToolStripMenuItem.Click += new System.EventHandler(this.onMergeImagesMenuClick);
            // 
            // createTrainvalFileToolStripMenuItem
            // 
            this.createTrainvalFileToolStripMenuItem.Name = "createTrainvalFileToolStripMenuItem";
            this.createTrainvalFileToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.createTrainvalFileToolStripMenuItem.Text = "Create train/val file";
            this.createTrainvalFileToolStripMenuItem.Click += new System.EventHandler(this.onCreateTxtMenuClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 489);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(584, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "Ready";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel1.Text = "Ready";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(0, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(584, 323);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // listView1
            // 
            this.listView1.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.AutoArrange = false;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 356);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.ShowGroups = false;
            this.listView1.Size = new System.Drawing.Size(584, 130);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.itemSelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "label1";
            // 
            // buttonSL
            // 
            this.buttonSL.Location = new System.Drawing.Point(12, 58);
            this.buttonSL.Name = "buttonSL";
            this.buttonSL.Size = new System.Drawing.Size(30, 30);
            this.buttonSL.TabIndex = 5;
            this.buttonSL.Text = "SL";
            this.buttonSL.UseVisualStyleBackColor = true;
            this.buttonSL.Click += new System.EventHandler(this.imageButtonClicked);
            // 
            // buttonF
            // 
            this.buttonF.Location = new System.Drawing.Point(48, 58);
            this.buttonF.Name = "buttonF";
            this.buttonF.Size = new System.Drawing.Size(30, 30);
            this.buttonF.TabIndex = 6;
            this.buttonF.Text = "F";
            this.buttonF.UseVisualStyleBackColor = true;
            this.buttonF.Click += new System.EventHandler(this.imageButtonClicked);
            // 
            // buttonSR
            // 
            this.buttonSR.Location = new System.Drawing.Point(84, 58);
            this.buttonSR.Name = "buttonSR";
            this.buttonSR.Size = new System.Drawing.Size(30, 30);
            this.buttonSR.TabIndex = 7;
            this.buttonSR.Text = "SR";
            this.buttonSR.UseVisualStyleBackColor = true;
            this.buttonSR.Click += new System.EventHandler(this.imageButtonClicked);
            // 
            // buttonR
            // 
            this.buttonR.Location = new System.Drawing.Point(84, 94);
            this.buttonR.Name = "buttonR";
            this.buttonR.Size = new System.Drawing.Size(30, 30);
            this.buttonR.TabIndex = 10;
            this.buttonR.Text = "R";
            this.buttonR.UseVisualStyleBackColor = true;
            this.buttonR.Click += new System.EventHandler(this.imageButtonClicked);
            // 
            // buttonL
            // 
            this.buttonL.Location = new System.Drawing.Point(12, 94);
            this.buttonL.Name = "buttonL";
            this.buttonL.Size = new System.Drawing.Size(30, 30);
            this.buttonL.TabIndex = 8;
            this.buttonL.Text = "L";
            this.buttonL.UseVisualStyleBackColor = true;
            this.buttonL.Click += new System.EventHandler(this.imageButtonClicked);
            // 
            // buttonB
            // 
            this.buttonB.Location = new System.Drawing.Point(48, 130);
            this.buttonB.Name = "buttonB";
            this.buttonB.Size = new System.Drawing.Size(30, 30);
            this.buttonB.TabIndex = 12;
            this.buttonB.Text = "B";
            this.buttonB.UseVisualStyleBackColor = true;
            this.buttonB.Click += new System.EventHandler(this.imageButtonClicked);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.Location = new System.Drawing.Point(497, 35);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 14;
            this.buttonDelete.Text = "Bad";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.imageButtonClicked);
            // 
            // versionLabel
            // 
            this.versionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(535, 337);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(37, 13);
            this.versionLabel.TabIndex = 15;
            this.versionLabel.Text = "v0.0.3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 511);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonB);
            this.Controls.Add(this.buttonR);
            this.Controls.Add(this.buttonL);
            this.Controls.Add(this.buttonSR);
            this.Controls.Add(this.buttonF);
            this.Controls.Add(this.buttonSL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(600, 550);
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadImagesToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSL;
        private System.Windows.Forms.Button buttonF;
        private System.Windows.Forms.Button buttonSR;
        private System.Windows.Forms.Button buttonR;
        private System.Windows.Forms.Button buttonL;
        private System.Windows.Forms.Button buttonB;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Label versionLabel;

        private System.Windows.Forms.ToolStripMenuItem mergeImagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compareImagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem analyzeImagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createTrainvalFileToolStripMenuItem;
    }
}

