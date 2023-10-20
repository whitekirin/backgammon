
namespace 五子棋_註冊_登錄
{
    partial class player
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(player));
            this.panel1 = new System.Windows.Forms.Panel();
            this.time_txt = new System.Windows.Forms.Label();
            this.peace1 = new System.Windows.Forms.Button();
            this.time1 = new System.Windows.Forms.Label();
            this.show_name1 = new System.Windows.Forms.Label();
            this.ply_lab = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.time2_txt = new System.Windows.Forms.Label();
            this.peace2 = new System.Windows.Forms.Button();
            this.show_name2 = new System.Windows.Forms.Label();
            this.time2 = new System.Windows.Forms.Label();
            this.ply_lab2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.time_txt);
            this.panel1.Controls.Add(this.peace1);
            this.panel1.Controls.Add(this.time1);
            this.panel1.Controls.Add(this.show_name1);
            this.panel1.Controls.Add(this.ply_lab);
            this.panel1.Location = new System.Drawing.Point(14, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(225, 242);
            this.panel1.TabIndex = 0;
            // 
            // time_txt
            // 
            this.time_txt.AutoSize = true;
            this.time_txt.Font = new System.Drawing.Font("標楷體", 10.2F);
            this.time_txt.Location = new System.Drawing.Point(92, 85);
            this.time_txt.Name = "time_txt";
            this.time_txt.Size = new System.Drawing.Size(43, 21);
            this.time_txt.TabIndex = 4;
            this.time_txt.Text = "123";
            // 
            // peace1
            // 
            this.peace1.Location = new System.Drawing.Point(58, 193);
            this.peace1.Name = "peace1";
            this.peace1.Size = new System.Drawing.Size(84, 28);
            this.peace1.TabIndex = 3;
            this.peace1.Text = "求和";
            this.peace1.UseVisualStyleBackColor = true;
            this.peace1.Click += new System.EventHandler(this.peace1_Click);
            // 
            // time1
            // 
            this.time1.AutoSize = true;
            this.time1.Font = new System.Drawing.Font("標楷體", 10.2F);
            this.time1.Location = new System.Drawing.Point(24, 85);
            this.time1.Name = "time1";
            this.time1.Size = new System.Drawing.Size(65, 21);
            this.time1.TabIndex = 2;
            this.time1.Text = "倒數:";
            // 
            // show_name1
            // 
            this.show_name1.AutoSize = true;
            this.show_name1.Location = new System.Drawing.Point(107, 30);
            this.show_name1.Name = "show_name1";
            this.show_name1.Size = new System.Drawing.Size(50, 18);
            this.show_name1.TabIndex = 1;
            this.show_name1.Text = "label2";
            // 
            // ply_lab
            // 
            this.ply_lab.AutoSize = true;
            this.ply_lab.Font = new System.Drawing.Font("標楷體", 12F);
            this.ply_lab.Location = new System.Drawing.Point(17, 24);
            this.ply_lab.Name = "ply_lab";
            this.ply_lab.Size = new System.Drawing.Size(94, 24);
            this.ply_lab.TabIndex = 0;
            this.ply_lab.Text = "玩家一:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.time2_txt);
            this.panel2.Controls.Add(this.peace2);
            this.panel2.Controls.Add(this.show_name2);
            this.panel2.Controls.Add(this.time2);
            this.panel2.Controls.Add(this.ply_lab2);
            this.panel2.Location = new System.Drawing.Point(767, 14);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(225, 242);
            this.panel2.TabIndex = 1;
            // 
            // time2_txt
            // 
            this.time2_txt.AutoSize = true;
            this.time2_txt.Font = new System.Drawing.Font("標楷體", 10.2F);
            this.time2_txt.Location = new System.Drawing.Point(121, 85);
            this.time2_txt.Name = "time2_txt";
            this.time2_txt.Size = new System.Drawing.Size(43, 21);
            this.time2_txt.TabIndex = 5;
            this.time2_txt.Text = "123";
            // 
            // peace2
            // 
            this.peace2.Location = new System.Drawing.Point(82, 193);
            this.peace2.Name = "peace2";
            this.peace2.Size = new System.Drawing.Size(84, 28);
            this.peace2.TabIndex = 4;
            this.peace2.Text = "求和";
            this.peace2.UseVisualStyleBackColor = true;
            this.peace2.Click += new System.EventHandler(this.peace2_Click);
            // 
            // show_name2
            // 
            this.show_name2.AutoSize = true;
            this.show_name2.Location = new System.Drawing.Point(122, 29);
            this.show_name2.Name = "show_name2";
            this.show_name2.Size = new System.Drawing.Size(50, 18);
            this.show_name2.TabIndex = 4;
            this.show_name2.Text = "label6";
            // 
            // time2
            // 
            this.time2.AutoSize = true;
            this.time2.Font = new System.Drawing.Font("標楷體", 10.2F);
            this.time2.Location = new System.Drawing.Point(27, 85);
            this.time2.Name = "time2";
            this.time2.Size = new System.Drawing.Size(65, 21);
            this.time2.TabIndex = 3;
            this.time2.Text = "倒數:";
            // 
            // ply_lab2
            // 
            this.ply_lab2.AutoSize = true;
            this.ply_lab2.Font = new System.Drawing.Font("標楷體", 12F);
            this.ply_lab2.Location = new System.Drawing.Point(26, 24);
            this.ply_lab2.Name = "ply_lab2";
            this.ply_lab2.Size = new System.Drawing.Size(94, 24);
            this.ply_lab2.TabIndex = 2;
            this.ply_lab2.Text = "玩家二:";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.Location = new System.Drawing.Point(72, 292);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 45);
            this.button1.TabIndex = 11;
            this.button1.Text = "儲存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.ForeColor = System.Drawing.Color.Transparent;
            this.panel3.Location = new System.Drawing.Point(271, 14);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(360, 384);
            this.panel3.TabIndex = 12;
            this.panel3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel3_MouseDown);
            this.panel3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel3_MouseMove);
            // 
            // player
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1050, 588);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "player";
            this.Text = "player";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label show_name1;
        private System.Windows.Forms.Label ply_lab;
        private System.Windows.Forms.Label time1;
        private System.Windows.Forms.Button peace1;
        private System.Windows.Forms.Button peace2;
        private System.Windows.Forms.Label show_name2;
        private System.Windows.Forms.Label time2;
        private System.Windows.Forms.Label ply_lab2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label time_txt;
        private System.Windows.Forms.Label time2_txt;
        private System.Windows.Forms.Button button1;
        protected internal System.Windows.Forms.Panel panel3;
    }
}