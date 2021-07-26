
namespace MyJoy
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ComboBox_Target = new System.Windows.Forms.ComboBox();
            this.label_linkState = new System.Windows.Forms.Label();
            this.label_ip = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ExistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ComboBox_Target
            // 
            this.ComboBox_Target.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Target.FormattingEnabled = true;
            this.ComboBox_Target.Location = new System.Drawing.Point(12, 12);
            this.ComboBox_Target.Name = "ComboBox_Target";
            this.ComboBox_Target.Size = new System.Drawing.Size(158, 26);
            this.ComboBox_Target.TabIndex = 1;
            this.ComboBox_Target.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Target_SelectedIndexChanged);
            // 
            // label_linkState
            // 
            this.label_linkState.AutoSize = true;
            this.label_linkState.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label_linkState.Font = new System.Drawing.Font("宋体", 10F);
            this.label_linkState.Location = new System.Drawing.Point(12, 52);
            this.label_linkState.Name = "label_linkState";
            this.label_linkState.Size = new System.Drawing.Size(159, 20);
            this.label_linkState.TabIndex = 4;
            this.label_linkState.Text = "链接状态:未连接";
            // 
            // label_ip
            // 
            this.label_ip.AutoSize = true;
            this.label_ip.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label_ip.Location = new System.Drawing.Point(252, 15);
            this.label_ip.Name = "label_ip";
            this.label_ip.Size = new System.Drawing.Size(62, 18);
            this.label_ip.TabIndex = 5;
            this.label_ip.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(197, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "端口：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(215, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "ip：";
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(255, 52);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(100, 28);
            this.textBox_port.TabIndex = 8;
            this.textBox_port.Text = "40425";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "MyJoy";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExistToolStripMenuItem,
            this.ShowToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(117, 64);
            // 
            // ExistToolStripMenuItem
            // 
            this.ExistToolStripMenuItem.Name = "ExistToolStripMenuItem";
            this.ExistToolStripMenuItem.Size = new System.Drawing.Size(116, 30);
            this.ExistToolStripMenuItem.Text = "退出";
            this.ExistToolStripMenuItem.Click += new System.EventHandler(this.ExistToolStripMenuItem_Click);
            // 
            // ShowToolStripMenuItem
            // 
            this.ShowToolStripMenuItem.Name = "ShowToolStripMenuItem";
            this.ShowToolStripMenuItem.Size = new System.Drawing.Size(116, 30);
            this.ShowToolStripMenuItem.Text = "窗口";
            this.ShowToolStripMenuItem.Click += new System.EventHandler(this.ShowToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(380, 91);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_ip);
            this.Controls.Add(this.label_linkState);
            this.Controls.Add(this.ComboBox_Target);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "MyJoy";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox ComboBox_Target;
        private System.Windows.Forms.Label label_linkState;
        private System.Windows.Forms.Label label_ip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ExistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowToolStripMenuItem;
    }
}

