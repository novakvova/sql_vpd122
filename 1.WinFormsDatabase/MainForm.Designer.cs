namespace _1.WinFormsDatabase
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGenerateTabels = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGenerateTabels
            // 
            this.btnGenerateTabels.Location = new System.Drawing.Point(616, 23);
            this.btnGenerateTabels.Name = "btnGenerateTabels";
            this.btnGenerateTabels.Size = new System.Drawing.Size(155, 62);
            this.btnGenerateTabels.TabIndex = 0;
            this.btnGenerateTabels.Text = "Генерація таблиць";
            this.btnGenerateTabels.UseVisualStyleBackColor = true;
            this.btnGenerateTabels.Click += new System.EventHandler(this.btnGenerateTabels_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnGenerateTabels);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnGenerateTabels;
    }
}