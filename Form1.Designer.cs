namespace Streampanel
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
            this.components = new System.ComponentModel.Container();
            this.closeButton = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.pictureUsbStatus = new System.Windows.Forms.PictureBox();
            this.timerConnection = new System.Windows.Forms.Timer(this.components);
            this.timerLookForConnection = new System.Windows.Forms.Timer(this.components);
            this.titleBar = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureSettings = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.separatorTop = new System.Windows.Forms.PictureBox();
            this.separatorBottom = new System.Windows.Forms.PictureBox();
            this.slot_5 = new Streampanel.ActionSlot();
            this.slot_4 = new Streampanel.ActionSlot();
            this.slot_3 = new Streampanel.ActionSlot();
            this.slot_2 = new Streampanel.ActionSlot();
            this.slot_10 = new Streampanel.ActionSlot();
            this.slot_9 = new Streampanel.ActionSlot();
            this.slot_8 = new Streampanel.ActionSlot();
            this.slot_7 = new Streampanel.ActionSlot();
            this.slot_15 = new Streampanel.ActionSlot();
            this.slot_14 = new Streampanel.ActionSlot();
            this.slot_13 = new Streampanel.ActionSlot();
            this.slot_12 = new Streampanel.ActionSlot();
            this.slot_11 = new Streampanel.ActionSlot();
            this.slot_6 = new Streampanel.ActionSlot();
            this.slot_1 = new Streampanel.ActionSlot();
            this.graphicalOverlay1 = new Streampanel.GraphicalOverlay(this.components);
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureUsbStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.titleBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_1)).BeginInit();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.closeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.closeButton.BackgroundImage = global::Streampanel.Properties.Resources.CloseButton;
            this.closeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.closeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(72)))), ((int)(((byte)(72)))));
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Location = new System.Drawing.Point(710, 0);
            this.closeButton.Margin = new System.Windows.Forms.Padding(0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(40, 30);
            this.closeButton.TabIndex = 0;
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            this.closeButton.MouseEnter += new System.EventHandler(this.closeButton_MouseEnter);
            this.closeButton.MouseLeave += new System.EventHandler(this.closeButton_MouseLeave);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // pictureUsbStatus
            // 
            this.pictureUsbStatus.BackgroundImage = global::Streampanel.Properties.Resources.usb_off;
            this.pictureUsbStatus.Location = new System.Drawing.Point(304, 45);
            this.pictureUsbStatus.Name = "pictureUsbStatus";
            this.pictureUsbStatus.Size = new System.Drawing.Size(26, 26);
            this.pictureUsbStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureUsbStatus.TabIndex = 3;
            this.pictureUsbStatus.TabStop = false;
            // 
            // timerConnection
            // 
            this.timerConnection.Interval = 10;
            this.timerConnection.Tick += new System.EventHandler(this.timerConnection_Tick);
            // 
            // timerLookForConnection
            // 
            this.timerLookForConnection.Interval = 1000;
            this.timerLookForConnection.Tick += new System.EventHandler(this.timerLookForConnection_Tick);
            // 
            // titleBar
            // 
            this.titleBar.BackgroundImage = global::Streampanel.Properties.Resources.TitleBar;
            this.titleBar.Location = new System.Drawing.Point(0, 0);
            this.titleBar.Name = "titleBar";
            this.titleBar.Size = new System.Drawing.Size(750, 30);
            this.titleBar.TabIndex = 4;
            this.titleBar.TabStop = false;
            this.titleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.titleBar_MouseDown);
            this.titleBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.titleBar_MouseMove);
            this.titleBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.titleBar_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pictureBox1.Location = new System.Drawing.Point(0, 30);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(210, 570);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // pictureSettings
            // 
            this.pictureSettings.BackgroundImage = global::Streampanel.Properties.Resources.settings;
            this.pictureSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureSettings.Location = new System.Drawing.Point(260, 45);
            this.pictureSettings.Name = "pictureSettings";
            this.pictureSettings.Size = new System.Drawing.Size(26, 26);
            this.pictureSettings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureSettings.TabIndex = 6;
            this.pictureSettings.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::Streampanel.Properties.Resources.wifi_off;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(338, 45);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(26, 26);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.BackColor = System.Drawing.Color.Transparent;
            this.labelStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelStatus.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelStatus.ForeColor = System.Drawing.Color.DimGray;
            this.labelStatus.Location = new System.Drawing.Point(380, 52);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelStatus.Size = new System.Drawing.Size(123, 13);
            this.labelStatus.TabIndex = 8;
            this.labelStatus.Text = "Connect mobile device";
            // 
            // separatorTop
            // 
            this.separatorTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.separatorTop.Location = new System.Drawing.Point(260, 86);
            this.separatorTop.Name = "separatorTop";
            this.separatorTop.Size = new System.Drawing.Size(438, 1);
            this.separatorTop.TabIndex = 14;
            this.separatorTop.TabStop = false;
            // 
            // separatorBottom
            // 
            this.separatorBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.separatorBottom.Location = new System.Drawing.Point(260, 408);
            this.separatorBottom.Name = "separatorBottom";
            this.separatorBottom.Size = new System.Drawing.Size(438, 1);
            this.separatorBottom.TabIndex = 14;
            this.separatorBottom.TabStop = false;
            // 
            // slot_5
            // 
            this.slot_5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.slot_5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.slot_5.ErrorImage = null;
            this.slot_5.InitialImage = null;
            this.slot_5.Location = new System.Drawing.Point(628, 120);
            this.slot_5.Margin = new System.Windows.Forms.Padding(0);
            this.slot_5.Name = "slot_5";
            this.slot_5.Size = new System.Drawing.Size(70, 70);
            this.slot_5.TabIndex = 12;
            this.slot_5.TabStop = false;
            this.slot_5.Type = Streampanel.SlotType.None;
            // 
            // slot_4
            // 
            this.slot_4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.slot_4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.slot_4.ErrorImage = null;
            this.slot_4.InitialImage = null;
            this.slot_4.Location = new System.Drawing.Point(536, 120);
            this.slot_4.Margin = new System.Windows.Forms.Padding(0);
            this.slot_4.Name = "slot_4";
            this.slot_4.Size = new System.Drawing.Size(70, 70);
            this.slot_4.TabIndex = 12;
            this.slot_4.TabStop = false;
            this.slot_4.Type = Streampanel.SlotType.None;
            // 
            // slot_3
            // 
            this.slot_3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.slot_3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.slot_3.ErrorImage = null;
            this.slot_3.InitialImage = null;
            this.slot_3.Location = new System.Drawing.Point(444, 120);
            this.slot_3.Margin = new System.Windows.Forms.Padding(0);
            this.slot_3.Name = "slot_3";
            this.slot_3.Size = new System.Drawing.Size(70, 70);
            this.slot_3.TabIndex = 12;
            this.slot_3.TabStop = false;
            this.slot_3.Type = Streampanel.SlotType.None;
            // 
            // slot_2
            // 
            this.slot_2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.slot_2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.slot_2.ErrorImage = null;
            this.slot_2.InitialImage = null;
            this.slot_2.Location = new System.Drawing.Point(352, 120);
            this.slot_2.Margin = new System.Windows.Forms.Padding(0);
            this.slot_2.Name = "slot_2";
            this.slot_2.Size = new System.Drawing.Size(70, 70);
            this.slot_2.TabIndex = 12;
            this.slot_2.TabStop = false;
            this.slot_2.Type = Streampanel.SlotType.None;
            // 
            // slot_10
            // 
            this.slot_10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.slot_10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.slot_10.ErrorImage = null;
            this.slot_10.InitialImage = null;
            this.slot_10.Location = new System.Drawing.Point(628, 212);
            this.slot_10.Margin = new System.Windows.Forms.Padding(0);
            this.slot_10.Name = "slot_10";
            this.slot_10.Size = new System.Drawing.Size(70, 70);
            this.slot_10.TabIndex = 12;
            this.slot_10.TabStop = false;
            this.slot_10.Type = Streampanel.SlotType.None;
            // 
            // slot_9
            // 
            this.slot_9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.slot_9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.slot_9.ErrorImage = null;
            this.slot_9.InitialImage = null;
            this.slot_9.Location = new System.Drawing.Point(536, 212);
            this.slot_9.Margin = new System.Windows.Forms.Padding(0);
            this.slot_9.Name = "slot_9";
            this.slot_9.Size = new System.Drawing.Size(70, 70);
            this.slot_9.TabIndex = 12;
            this.slot_9.TabStop = false;
            this.slot_9.Type = Streampanel.SlotType.None;
            // 
            // slot_8
            // 
            this.slot_8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.slot_8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.slot_8.ErrorImage = null;
            this.slot_8.InitialImage = null;
            this.slot_8.Location = new System.Drawing.Point(444, 212);
            this.slot_8.Margin = new System.Windows.Forms.Padding(0);
            this.slot_8.Name = "slot_8";
            this.slot_8.Size = new System.Drawing.Size(70, 70);
            this.slot_8.TabIndex = 12;
            this.slot_8.TabStop = false;
            this.slot_8.Type = Streampanel.SlotType.None;
            // 
            // slot_7
            // 
            this.slot_7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.slot_7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.slot_7.ErrorImage = null;
            this.slot_7.InitialImage = null;
            this.slot_7.Location = new System.Drawing.Point(352, 212);
            this.slot_7.Margin = new System.Windows.Forms.Padding(0);
            this.slot_7.Name = "slot_7";
            this.slot_7.Size = new System.Drawing.Size(70, 70);
            this.slot_7.TabIndex = 12;
            this.slot_7.TabStop = false;
            this.slot_7.Type = Streampanel.SlotType.None;
            // 
            // slot_15
            // 
            this.slot_15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.slot_15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.slot_15.ErrorImage = null;
            this.slot_15.InitialImage = null;
            this.slot_15.Location = new System.Drawing.Point(628, 304);
            this.slot_15.Margin = new System.Windows.Forms.Padding(0);
            this.slot_15.Name = "slot_15";
            this.slot_15.Size = new System.Drawing.Size(70, 70);
            this.slot_15.TabIndex = 12;
            this.slot_15.TabStop = false;
            this.slot_15.Type = Streampanel.SlotType.None;
            // 
            // slot_14
            // 
            this.slot_14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.slot_14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.slot_14.ErrorImage = null;
            this.slot_14.InitialImage = null;
            this.slot_14.Location = new System.Drawing.Point(536, 304);
            this.slot_14.Margin = new System.Windows.Forms.Padding(0);
            this.slot_14.Name = "slot_14";
            this.slot_14.Size = new System.Drawing.Size(70, 70);
            this.slot_14.TabIndex = 12;
            this.slot_14.TabStop = false;
            this.slot_14.Type = Streampanel.SlotType.None;
            // 
            // slot_13
            // 
            this.slot_13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.slot_13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.slot_13.ErrorImage = null;
            this.slot_13.InitialImage = null;
            this.slot_13.Location = new System.Drawing.Point(444, 304);
            this.slot_13.Margin = new System.Windows.Forms.Padding(0);
            this.slot_13.Name = "slot_13";
            this.slot_13.Size = new System.Drawing.Size(70, 70);
            this.slot_13.TabIndex = 12;
            this.slot_13.TabStop = false;
            this.slot_13.Type = Streampanel.SlotType.None;
            // 
            // slot_12
            // 
            this.slot_12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.slot_12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.slot_12.ErrorImage = null;
            this.slot_12.InitialImage = null;
            this.slot_12.Location = new System.Drawing.Point(352, 304);
            this.slot_12.Margin = new System.Windows.Forms.Padding(0);
            this.slot_12.Name = "slot_12";
            this.slot_12.Size = new System.Drawing.Size(70, 70);
            this.slot_12.TabIndex = 12;
            this.slot_12.TabStop = false;
            this.slot_12.Type = Streampanel.SlotType.None;
            // 
            // slot_11
            // 
            this.slot_11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.slot_11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.slot_11.ErrorImage = null;
            this.slot_11.InitialImage = null;
            this.slot_11.Location = new System.Drawing.Point(260, 304);
            this.slot_11.Margin = new System.Windows.Forms.Padding(0);
            this.slot_11.Name = "slot_11";
            this.slot_11.Size = new System.Drawing.Size(70, 70);
            this.slot_11.TabIndex = 12;
            this.slot_11.TabStop = false;
            this.slot_11.Type = Streampanel.SlotType.None;
            // 
            // slot_6
            // 
            this.slot_6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.slot_6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.slot_6.ErrorImage = null;
            this.slot_6.InitialImage = null;
            this.slot_6.Location = new System.Drawing.Point(260, 212);
            this.slot_6.Margin = new System.Windows.Forms.Padding(0);
            this.slot_6.Name = "slot_6";
            this.slot_6.Size = new System.Drawing.Size(70, 70);
            this.slot_6.TabIndex = 12;
            this.slot_6.TabStop = false;
            this.slot_6.Type = Streampanel.SlotType.None;
            // 
            // slot_1
            // 
            this.slot_1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.slot_1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.slot_1.ErrorImage = null;
            this.slot_1.InitialImage = null;
            this.slot_1.Location = new System.Drawing.Point(260, 120);
            this.slot_1.Margin = new System.Windows.Forms.Padding(0);
            this.slot_1.Name = "slot_1";
            this.slot_1.Size = new System.Drawing.Size(70, 70);
            this.slot_1.TabIndex = 12;
            this.slot_1.TabStop = false;
            this.slot_1.Type = Streampanel.SlotType.None;
            // 
            // graphicalOverlay1
            // 
            this.graphicalOverlay1.Paint += new System.EventHandler<System.Windows.Forms.PaintEventArgs>(this.graphicalOverlay1_Paint);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(549, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(750, 600);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.separatorBottom);
            this.Controls.Add(this.separatorTop);
            this.Controls.Add(this.slot_5);
            this.Controls.Add(this.slot_4);
            this.Controls.Add(this.slot_3);
            this.Controls.Add(this.slot_2);
            this.Controls.Add(this.slot_10);
            this.Controls.Add(this.slot_9);
            this.Controls.Add(this.slot_8);
            this.Controls.Add(this.slot_7);
            this.Controls.Add(this.slot_15);
            this.Controls.Add(this.slot_14);
            this.Controls.Add(this.slot_13);
            this.Controls.Add(this.slot_12);
            this.Controls.Add(this.slot_11);
            this.Controls.Add(this.slot_6);
            this.Controls.Add(this.slot_1);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureSettings);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.titleBar);
            this.Controls.Add(this.pictureUsbStatus);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Streampanel";
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.Form1_DragOver);
            this.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.Form1_GiveFeedback);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureUsbStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.titleBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slot_1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.PictureBox pictureUsbStatus;
        private System.Windows.Forms.Timer timerConnection;
        private System.Windows.Forms.Timer timerLookForConnection;
        private System.Windows.Forms.PictureBox titleBar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureSettings;
        private System.Windows.Forms.PictureBox pictureBox2;
        public System.Windows.Forms.Label labelStatus;
        private Streampanel.GraphicalOverlay graphicalOverlay1;
        internal ActionSlot slot_1;
        internal ActionSlot slot_2;
        internal ActionSlot slot_3;
        internal ActionSlot slot_4;
        internal ActionSlot slot_5;
        internal ActionSlot slot_6;
        internal ActionSlot slot_7;
        internal ActionSlot slot_8;
        internal ActionSlot slot_9;
        internal ActionSlot slot_10;
        internal ActionSlot slot_11;
        internal ActionSlot slot_12;
        internal ActionSlot slot_13;
        internal ActionSlot slot_14;
        internal ActionSlot slot_15;
        private System.Windows.Forms.PictureBox separatorTop;
        private System.Windows.Forms.PictureBox separatorBottom;
        private System.Windows.Forms.Button button1;
    }
}

