namespace GuideBoard
{
    partial class MainWindow
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.IpAddress = new System.Windows.Forms.TextBox();
            this.PortValue = new System.Windows.Forms.TextBox();
            this.getConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.revMessage = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // IpAddress
            // 
            this.IpAddress.Location = new System.Drawing.Point(32, 13);
            this.IpAddress.Name = "IpAddress";
            this.IpAddress.Size = new System.Drawing.Size(116, 21);
            this.IpAddress.TabIndex = 0;
            this.IpAddress.Text = "192.168.1.101";

            // 
            // PortValue
            // 
            this.PortValue.Location = new System.Drawing.Point(189, 13);
            this.PortValue.Name = "PortValue";
            this.PortValue.Size = new System.Drawing.Size(110, 21);
            this.PortValue.TabIndex = 1;
            this.PortValue.Text = "6800";
            // 
            // getConnect
            // 
            this.getConnect.Location = new System.Drawing.Point(320, 11);
            this.getConnect.Name = "getConnect";
            this.getConnect.Size = new System.Drawing.Size(75, 23);
            this.getConnect.TabIndex = 2;
            this.getConnect.Text = "建立连接";
            this.getConnect.UseVisualStyleBackColor = true;
            this.getConnect.Click += new System.EventHandler(this.getConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(154, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Port:";
            // 
            // revMessage
            // 
            this.revMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.revMessage.Location = new System.Drawing.Point(8, 43);
            this.revMessage.Name = "revMessage";
            this.revMessage.Size = new System.Drawing.Size(387, 299);
            this.revMessage.TabIndex = 5;
            this.revMessage.Text = "";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 354);
            this.Controls.Add(this.revMessage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.getConnect);
            this.Controls.Add(this.PortValue);
            this.Controls.Add(this.IpAddress);
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox IpAddress;
        private System.Windows.Forms.TextBox PortValue;
        private System.Windows.Forms.Button getConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox revMessage;
    }
}

