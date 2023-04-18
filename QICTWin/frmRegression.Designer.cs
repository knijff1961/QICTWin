namespace QICTWin
{
    partial class frmRegression
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtRegression = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtRegression
            // 
            this.txtRegression.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRegression.Location = new System.Drawing.Point(0, 0);
            this.txtRegression.Multiline = true;
            this.txtRegression.Name = "txtRegression";
            this.txtRegression.Size = new System.Drawing.Size(443, 341);
            this.txtRegression.TabIndex = 0;
            this.txtRegression.Text = "Param0,Param1,Param2,Param3\r\na1,c2,g3,j4\r\nb2,c2,h3,k4\r\na1,d2,i3,k4\r\nb2,e2,i3,j4\r\n" +
    "b2,f2,g3,k4\r\na1,d2,h3,j4\r\na1,e2,g3,k4\r\na1,f2,h3,j4\r\na1,c2,i3,j4\r\nb2,d2,g3,j4\r\na1" +
    ",e2,h3,j4\r\na1,f2,i3,j4";
            // 
            // frmRegression
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 341);
            this.Controls.Add(this.txtRegression);
            this.Name = "frmRegression";
            this.Text = "frmRegression";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtRegression;
    }
}