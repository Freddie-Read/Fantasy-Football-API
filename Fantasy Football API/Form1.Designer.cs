namespace Fantasy_Football_API
{
    partial class Form1
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
            this.callAPIBtn = new System.Windows.Forms.Button();
            this.displayTxt = new System.Windows.Forms.TextBox();
            this.sortBtn = new System.Windows.Forms.Button();
            this.sortTxt = new System.Windows.Forms.TextBox();
            this.sortFirstBtn = new System.Windows.Forms.Button();
            this.suggestionTxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // callAPIBtn
            // 
            this.callAPIBtn.Location = new System.Drawing.Point(12, 12);
            this.callAPIBtn.Name = "callAPIBtn";
            this.callAPIBtn.Size = new System.Drawing.Size(287, 86);
            this.callAPIBtn.TabIndex = 0;
            this.callAPIBtn.Text = "API Call";
            this.callAPIBtn.UseVisualStyleBackColor = true;
            this.callAPIBtn.Click += new System.EventHandler(this.callAPIBtn_Click);
            // 
            // displayTxt
            // 
            this.displayTxt.Location = new System.Drawing.Point(305, 12);
            this.displayTxt.Multiline = true;
            this.displayTxt.Name = "displayTxt";
            this.displayTxt.ReadOnly = true;
            this.displayTxt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.displayTxt.Size = new System.Drawing.Size(720, 764);
            this.displayTxt.TabIndex = 1;
            // 
            // sortBtn
            // 
            this.sortBtn.Location = new System.Drawing.Point(12, 130);
            this.sortBtn.Name = "sortBtn";
            this.sortBtn.Size = new System.Drawing.Size(287, 86);
            this.sortBtn.TabIndex = 2;
            this.sortBtn.Text = "Sort";
            this.sortBtn.UseVisualStyleBackColor = true;
            this.sortBtn.Click += new System.EventHandler(this.sortBtn_Click);
            // 
            // sortTxt
            // 
            this.sortTxt.Location = new System.Drawing.Point(12, 104);
            this.sortTxt.Name = "sortTxt";
            this.sortTxt.Size = new System.Drawing.Size(287, 20);
            this.sortTxt.TabIndex = 3;
            // 
            // sortFirstBtn
            // 
            this.sortFirstBtn.Location = new System.Drawing.Point(12, 222);
            this.sortFirstBtn.Name = "sortFirstBtn";
            this.sortFirstBtn.Size = new System.Drawing.Size(287, 86);
            this.sortFirstBtn.TabIndex = 4;
            this.sortFirstBtn.Text = "Sort First Team";
            this.sortFirstBtn.UseVisualStyleBackColor = true;
            this.sortFirstBtn.Click += new System.EventHandler(this.sortFirstBtn_Click);
            // 
            // suggestionTxt
            // 
            this.suggestionTxt.Location = new System.Drawing.Point(12, 314);
            this.suggestionTxt.Multiline = true;
            this.suggestionTxt.Name = "suggestionTxt";
            this.suggestionTxt.ReadOnly = true;
            this.suggestionTxt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.suggestionTxt.Size = new System.Drawing.Size(287, 462);
            this.suggestionTxt.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1223, 788);
            this.Controls.Add(this.suggestionTxt);
            this.Controls.Add(this.sortFirstBtn);
            this.Controls.Add(this.sortTxt);
            this.Controls.Add(this.sortBtn);
            this.Controls.Add(this.displayTxt);
            this.Controls.Add(this.callAPIBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button callAPIBtn;
        private System.Windows.Forms.TextBox displayTxt;
        private System.Windows.Forms.Button sortBtn;
        private System.Windows.Forms.TextBox sortTxt;
        private System.Windows.Forms.Button sortFirstBtn;
        private System.Windows.Forms.TextBox suggestionTxt;
    }
}

