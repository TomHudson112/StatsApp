namespace StatsAppUI {
    partial class MainForm {
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
            this.evaluateDiscreteTableButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // evaluateDiscreteTableButton
            // 
            this.evaluateDiscreteTableButton.Location = new System.Drawing.Point(390, 452);
            this.evaluateDiscreteTableButton.Name = "evaluateDiscreteTableButton";
            this.evaluateDiscreteTableButton.Size = new System.Drawing.Size(75, 23);
            this.evaluateDiscreteTableButton.TabIndex = 0;
            this.evaluateDiscreteTableButton.Text = "Evaluate";
            this.evaluateDiscreteTableButton.UseVisualStyleBackColor = true;
            this.evaluateDiscreteTableButton.Click += new System.EventHandler(this.evaluateDiscreteTableButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.evaluateDiscreteTableButton);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stats App";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button evaluateDiscreteTableButton;
    }
}

