namespace WebPageYouTubeLinkPlayer
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.WMP = new AxWMPLib.AxWindowsMediaPlayer();
            this.KURL = new System.Windows.Forms.TextBox();
            this.PlaysList = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.StatusView = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.WMP)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1664, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(241, 45);
            this.button1.TabIndex = 1;
            this.button1.Text = "取得YouTube連結";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // WMP
            // 
            this.WMP.Enabled = true;
            this.WMP.Location = new System.Drawing.Point(351, 54);
            this.WMP.Name = "WMP";
            this.WMP.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("WMP.OcxState")));
            this.WMP.Size = new System.Drawing.Size(524, 440);
            this.WMP.TabIndex = 2;
            this.WMP.Enter += new System.EventHandler(this.WMP_Enter);
            // 
            // KURL
            // 
            this.KURL.Location = new System.Drawing.Point(779, 12);
            this.KURL.Name = "KURL";
            this.KURL.Size = new System.Drawing.Size(879, 36);
            this.KURL.TabIndex = 3;
            // 
            // PlaysList
            // 
            this.PlaysList.FormattingEnabled = true;
            this.PlaysList.ItemHeight = 24;
            this.PlaysList.Location = new System.Drawing.Point(17, 46);
            this.PlaysList.Name = "PlaysList";
            this.PlaysList.Size = new System.Drawing.Size(716, 868);
            this.PlaysList.TabIndex = 4;
            this.PlaysList.SelectedIndexChanged += new System.EventHandler(this.PlaysList_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(17, 927);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 39);
            this.button2.TabIndex = 5;
            this.button2.Text = "<-";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(99, 927);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 39);
            this.button3.TabIndex = 6;
            this.button3.Text = "->";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // StatusView
            // 
            this.StatusView.AutoSize = true;
            this.StatusView.Location = new System.Drawing.Point(775, 66);
            this.StatusView.Name = "StatusView";
            this.StatusView.Size = new System.Drawing.Size(113, 24);
            this.StatusView.TabIndex = 7;
            this.StatusView.Text = "StatusView";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(633, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 24);
            this.label1.TabIndex = 8;
            this.label1.Text = "網站連結：";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(894, 66);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(300, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1911, 1159);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StatusView);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.PlaysList);
            this.Controls.Add(this.KURL);
            this.Controls.Add(this.WMP);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "網頁中的YouTube連結自動播放器";
            ((System.ComponentModel.ISupportInitialize)(this.WMP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private AxWMPLib.AxWindowsMediaPlayer WMP;
        private System.Windows.Forms.TextBox KURL;
        private System.Windows.Forms.ListBox PlaysList;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label StatusView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

