using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using iMobileDevice;
using iMobileDevice.iDevice;
using iMobileDevice.iDeviceActivation;
using Newtonsoft.Json;

namespace Streampanel
{
    public partial class Form1 : Form
    {
        //Constants
        public const int SIDEBAR_WIDTH = 210;
        public const int ACTION_HEIGHT = 30;
        public Color ACTION_FONT_COLOR = Color.FromArgb(0xBF,0xBF,0xBF);
        public Color ACTION_COLOR = Color.FromArgb(0x1E, 0x1E, 0x1E);
        public Color ACTION_TINT_COLOR = Color.FromArgb(0x25, 0x25, 0x25);

        //Bitmaps
        Bitmap USB_OFF = Properties.Resources.usb_off;
        Bitmap USB_ON = Properties.Resources.usb_on;
        Bitmap BUTTON_CLOSE = Properties.Resources.CloseButton;
        Bitmap BUTTON_CLOSE_HOVER = Properties.Resources.CloseButton_hover;

        Bitmap ACTION_STREAM = Properties.Resources.action_stream;

        //Fields for main window drag
        private bool drag = false;
        private Point startPoint = new Point(0, 0);

        //Draw managing
        List<ActionSlot> Slots = new List<ActionSlot>();
        List<Label> Labels = new List<Label>();
        ActionSlot borderedSlot = null;
        bool CurSlotDraw = false;

        //Drag&Drop managing
        private bool isDrag = false;
        private bool slotMouseDown = false;
        private ActionSlot startDragSlot = new ActionSlot();
        private Button dragObject = new Button();
        private Cursor dragCursor;
        private Point lastMoveSlot;

        public Form1()
        {
            try
            {
                Storage.Initialize();
            }
            catch
            {
                MessageBox.Show("Storage initialization error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            InitializeComponent();
            graphicalOverlay1.Owner = this;
            this.DoubleBuffered = true;

            NativeLibraries.Load();
            ConnectionHandlerUSB.Init(timerLookForConnection, timerConnection, labelStatus);

            pictureUsbStatus.Image = ((System.Drawing.Image)(Properties.Resources.usb_off));

            notifyIcon1.Icon = ((System.Drawing.Icon)(Properties.Resources.TrayIcon));
            notifyIcon1.Visible = true;

            this.AllowDrop = true;

            CreateAccordion();

            InitSettings();

            //Setting up the panel
            Slots.Add(slot_1);
            Slots.Add(slot_2);
            Slots.Add(slot_3);
            Slots.Add(slot_4);
            Slots.Add(slot_5);
            Slots.Add(slot_6);
            Slots.Add(slot_7);
            Slots.Add(slot_8);
            Slots.Add(slot_9);
            Slots.Add(slot_10);
            Slots.Add(slot_11);
            Slots.Add(slot_12);
            Slots.Add(slot_13);
            Slots.Add(slot_14);
            Slots.Add(slot_15);

            foreach (ActionSlot slot in Slots)
            {
                slot.AllowDrop = true;
                slot.DragEnter += new DragEventHandler(slot_DragEnter);
                slot.DragLeave += new EventHandler(slot_DragLeave);
                slot.DragDrop += new DragEventHandler(slot_DragDrop);
                slot.MouseDown += new MouseEventHandler(slot_MouseDown);
                slot.MouseUp += new MouseEventHandler(slot_MouseUp);
                slot.MouseMove += new MouseEventHandler(slot_MouseMove);
                slot.MouseDoubleClick += new MouseEventHandler(slot_MouseDoubleClick);
            }

            InitLabels();
            SyncSlots();
        }

        private void CreateAccordion()
        {
            Accordion accordion = new Accordion();
            accordion.Size = new Size(SIDEBAR_WIDTH, 700);
            accordion.BackColor = Color.Transparent;
            accordion.Top = 30;
            
            //Test actions

            Expander expander1 = new Expander();
            expander1.BorderStyle = BorderStyle.FixedSingle;
            ExpanderHelper.CreateLabelHeader(expander1, "Test actions", SystemColors.ActiveBorder);

            List<Action> buttons1 = new List<Action>();
            buttons1.Add(CreateActionButton("Folder", ACTION_STREAM, SlotType.Folder));
            buttons1.Add(CreateActionButton("Launch app", ACTION_STREAM, SlotType.OpenApp));
            buttons1.Add(CreateActionButton("Website", ACTION_STREAM, SlotType.Website));

            CreateContentPanel(expander1, buttons1);
            accordion.Add(expander1);


            Expander expander2 = new Expander();
            expander2.BorderStyle = BorderStyle.FixedSingle;
            ExpanderHelper.CreateLabelHeader(expander2, "OBS", SystemColors.ActiveBorder);
            CreateContentLabel(expander2, "Test2", 120);
            accordion.Add(expander2);

            Expander expander3 = new Expander();
            expander3.BorderStyle = BorderStyle.FixedSingle;
            ExpanderHelper.CreateLabelHeader(expander3, "XSplit", SystemColors.ActiveBorder);
            CreateContentLabel(expander3, "Test3", 60);
            accordion.Add(expander3);

            this.Controls.Add(accordion);
            accordion.BringToFront();
        }

        private void CreateContentLabel(Expander expander, string text, int height)
        {
            Label labelContent = new Label();
            labelContent.Text = text;
            labelContent.Size = new System.Drawing.Size(expander.Width, height);
            expander.Content = labelContent;
        }

        private void CreateContentPanel(Expander expander, List<Action> buttons)
        {
            Panel panel = new Panel();
            panel.Width = SIDEBAR_WIDTH;

            int h = 0;
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Left = 0;
                buttons[i].Top = i * ACTION_HEIGHT;
                buttons[i].Height = ACTION_HEIGHT;
                buttons[i].Width = SIDEBAR_WIDTH;

                panel.Controls.Add(buttons[i]);

                h += ACTION_HEIGHT;
            }
            panel.Height = h;

            expander.Content = panel;
        }
        private Action CreateActionButton(string text, Bitmap image, SlotType type)
        {
            Action button = new Action(type);
            button.Text = "   " + text;
            button.BackColor = ACTION_COLOR;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.BorderColor = ACTION_COLOR;
            button.ForeColor = ACTION_FONT_COLOR;
            button.FlatAppearance.MouseDownBackColor = ACTION_TINT_COLOR;
            button.FlatAppearance.MouseOverBackColor = ACTION_TINT_COLOR;
            button.Font = new Font("System", 9f, FontStyle.Regular);
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Padding = new Padding(20, 0, 0, 0);
            button.Cursor = Cursors.Hand;
            button.Image = (Image)(new Bitmap(image, new Size(20, 20)));
            button.ImageAlign = ContentAlignment.MiddleLeft;
            button.TextImageRelation = TextImageRelation.ImageBeforeText;
            button.MouseDown += new MouseEventHandler(action_MouseDown);

            return button;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            ClipSlot(e, slot_1);
            ClipSlot(e, slot_2);
            ClipSlot(e, slot_3);
            ClipSlot(e, slot_4);
            ClipSlot(e, slot_5);
            ClipSlot(e, slot_6);
            ClipSlot(e, slot_7);
            ClipSlot(e, slot_8);
            ClipSlot(e, slot_9);
            ClipSlot(e, slot_10);
            ClipSlot(e, slot_11);
            ClipSlot(e, slot_12);
            ClipSlot(e, slot_13);
            ClipSlot(e, slot_14);
            ClipSlot(e, slot_15);

            ClipSlot(e, curSlot);
        }

        private void graphicalOverlay1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (PictureBox slot in Slots)
            {
                if (borderedSlot == slot)
                {
                    GraphicsPath path = RoundedRectangle.Create(slot.Location.X - 1, slot.Location.Y - 1, slot.Width + 1, slot.Height + 1, 8);
                    Pen pen = new Pen(Color.FromArgb(0xf2, 0x9d, 0x48), 2);
                    e.Graphics.DrawPath(pen, path);
                    pen.Dispose();
                }
                else
                {
                    GraphicsPath path = RoundedRectangle.Create(slot.Location.X, slot.Location.Y, slot.Width, slot.Height, 8);
                    Pen pen = new Pen(Color.FromArgb(50, 50, 50), 2);
                    e.Graphics.DrawPath(pen, path);
                    pen.Dispose();
                }
            }

            if (isVisible)
            {
                if (CurSlotDraw)
                {
                    GraphicsPath _path = RoundedRectangle.Create(bottomPanel.Location.X + curSlot.Location.X, bottomPanel.Location.Y + curSlot.Location.Y, curSlot.Width, curSlot.Height, 8);
                    Pen _pen = new Pen(Color.FromArgb(0xf2, 0x9d, 0x48), 2);
                    e.Graphics.DrawPath(_pen, _path);
                    _pen.Dispose();
                }
                else
                {
                    GraphicsPath _path = RoundedRectangle.Create(bottomPanel.Location.X + curSlot.Location.X, bottomPanel.Location.Y + curSlot.Location.Y, curSlot.Width, curSlot.Height, 8);
                    Pen _pen = new Pen(Color.FromArgb(50, 50, 50), 2);
                    e.Graphics.DrawPath(_pen, _path);
                    _pen.Dispose();
                }
            }
        }

        private void ClipSlot(PaintEventArgs e, PictureBox slot)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            GraphicsPath path = RoundedRectangle.Create(0, 0, slot.Width, slot.Height, 8);
            slot.Region = new Region(path);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                borderedSlot = null;
                this.Hide();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        #region Timers

        private void timerConnection_Tick(object sender, EventArgs e)
        {
            if (ConnectionHandlerUSB.connection == null)
                pictureUsbStatus.Image = USB_OFF;
            else
            {
                pictureUsbStatus.Image = USB_ON;

                byte[] cmd = { 1 };
                ConnectionHandlerUSB.SendBytes(cmd);
            }
        }

        private void timerLookForConnection_Tick(object sender, EventArgs e)
        {
            if (ConnectionHandlerUSB.connection == null)
            {
                labelStatus.Text = "Connect mobile device and open app";
                List<string> ids = ConnectionHandlerUSB.GetIDs();
                foreach (string id in ids)
                {
                    Console.WriteLine(id);
                    ConnectionHandlerUSB.Connect(id);
                }
                if (ids.Count > 0) Console.WriteLine("\n");
            }
            else
            {
                ConnectionHandlerUSB.CheckConnection();
            }
        }

        #endregion

        #region Title Bar Events

        private void closeButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.Image = BUTTON_CLOSE;
        }
        private void closeButton_MouseEnter(object sender, EventArgs e)
        {
            closeButton.Image = BUTTON_CLOSE_HOVER;
        }
        private void titleBar_MouseDown(object sender, MouseEventArgs e)
        {
            this.startPoint = e.Location;
            this.drag = true;
        }
        private void titleBar_MouseUp(object sender, MouseEventArgs e)
        {
            this.drag = false;
        }
        private void titleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.drag)
            {
                Point p1 = new Point(e.X, e.Y);
                Point p2 = this.PointToScreen(p1);
                Point p3 = new Point(p2.X - this.startPoint.X,
                                     p2.Y - this.startPoint.Y);
                this.Location = p3;
            }
        }

        #endregion

        #region Slots Management

        //  Syncronize slots with current storage state
        private void SyncSlots()
        {
            SlotPage page = Storage.CurProfile.GetPageByPath(Storage.CurPathString());
            foreach (ActionSlot slot in Slots)
            {
                int id = Int32.Parse(Regex.Match(slot.Name, @"\d+").Value);
                SlotLabel label = slot.Controls.OfType<SlotLabel>().First();
                Slot found = Storage.GetSlotById(id);
                if (found != null)
                {
                    slot.Type = found.Type;
                    label.Text = Storage.GetSetting(id, "Title");
                    ((Control)slot).AllowDrop = true;

                    //TODO: Manage image changing
                    if (!File.Exists(found.Image_0))
                    {
                        switch (found.Type)
                        {
                            case SlotType.None:
                                slot.BackgroundImage = null;
                                break;
                            case SlotType.Folder:
                                slot.BackgroundImage = Properties.Resources.slot_folder;
                                break;
                            case SlotType.FolderBack:
                                slot.BackgroundImage = Properties.Resources.slot_folder_back;
                                break;
                            case SlotType.OpenApp:
                                slot.BackgroundImage = Properties.Resources.slot_launch;
                                break;
                            case SlotType.Website:
                                slot.BackgroundImage = Properties.Resources.slot_website;
                                break;
                            case SlotType.Hotkey:
                                break;
                        }
                    }
                    else
                    {
                        Image img = Bitmap.FromFile(found.Image_0);
                        Bitmap bgImg = new Bitmap(img);
                        img.Dispose();
                        slot.BackgroundImage = bgImg;
                    }
                }
                else if (Storage.CurPathString() != "" && slot == Slots[0])
                {
                    slot.Type = SlotType.FolderBack;
                    slot.BackgroundImage = Properties.Resources.slot_folder_back;
                    label.Text = "";
                    ((Control)slot).AllowDrop = false;
                }
                else
                {
                    slot.Type = SlotType.None;
                    label.Text = "";
                    slot.BackgroundImage = null;
                }
            }
        }

        private int GetSlotId(ActionSlot slot)
        {
            int id = Int32.Parse(Regex.Match(slot.Name, @"\d+").Value);
            return id;
        }

        private void slot_MouseUp(object sender, MouseEventArgs e)
        {
            slotMouseDown = false;
            isDrag = false;
        }

        private void slot_MouseDown(object sender, MouseEventArgs e)
        {
            borderedSlot = (ActionSlot)sender;

            if (((ActionSlot)sender).Type != SlotType.FolderBack)
            {
                slotMouseDown = true;

                if (((ActionSlot)sender).Type != SlotType.None)
                {
                    ShowSlotSettings((ActionSlot)sender);
                }
                else
                {
                    HideSettings();
                }
            }
            else
            {
                HideSettings();
            }

            Invalidate(true);
        }

        private void slot_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (((ActionSlot)sender).Type == SlotType.Folder)
            {
                int id = GetSlotId((ActionSlot)sender);
                Storage.CurPath.Add(id);

                HideSettings();

                borderedSlot = null;
                Invalidate(true);
            }
            else if(((ActionSlot)sender).Type == SlotType.FolderBack)
            {
                Storage.CurPath.Remove(Storage.CurPath.Last());

                borderedSlot = null;
                Invalidate(true);
            }

            if (Storage.CurPathString() == "") ((Control)Slots[0]).AllowDrop = true;

            SyncSlots();
        }

        #endregion

        #region Drag&Drop

        private void action_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrag = true;
                dragObject = ((Button)sender);
                HideSettings();
                borderedSlot = null;
                Invalidate(true);

                Point ptStart = this.PointToScreen(new Point(e.X, e.Y));
                int relX = ptStart.X - this.Location.X;
                int relY = ptStart.Y - this.Location.Y;
                Bitmap bm = CursorUtil.AsBitmapWithCursor((Button)sender);
                if(dragCursor != null) dragCursor.Dispose();
                dragCursor = CursorUtil.CreateCursor((Bitmap)bm, relX+CursorUtil.cursorSize, relY+CursorUtil.cursorSize);
                
                this.DoDragDrop(((Action)sender).Type, DragDropEffects.All);
                Cursor.Current = dragCursor;
            }
            else
            {
                isDrag = false;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            isDrag = false;
            slotMouseDown = false;

            if (e.Button == MouseButtons.Left && isDrag)
            {
                foreach (ActionSlot slot in Slots)
                {
                    Point pt = slot.PointToClient(Cursor.Position);
                    Rectangle rc = slot.ClientRectangle;
                    if (rc.Contains(pt))
                    {
                        borderedSlot = slot;
                    }
                }
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            slotMouseDown = false;
            isDrag = false;
        }

        private void slot_MouseMove(object sender, MouseEventArgs e)
        {
            var p = Cursor.Position;
            if (lastMoveSlot != p)
            {
                lastMoveSlot = p;

                if (slotMouseDown && ((ActionSlot)sender).Type != SlotType.None && ((ActionSlot)sender).Type != SlotType.FolderBack)
                {
                    isDrag = true;
                    borderedSlot = null;
                    Invalidate(true);

                    Point ptStart = this.PointToScreen(new Point(e.X, e.Y));
                    int relX = ptStart.X - this.Location.X;
                    int relY = ptStart.Y - this.Location.Y;
                    Bitmap bm = CursorUtil.AsBitmapWithCursor((ActionSlot)sender);
                    if (dragCursor != null) dragCursor.Dispose();
                    dragCursor = CursorUtil.CreateCursor((Bitmap)bm, relX + CursorUtil.cursorSize, relY + CursorUtil.cursorSize);

                    startDragSlot = ((ActionSlot)sender);

                    int id = GetSlotId((ActionSlot)sender);
                    Slot draggedSlot = Storage.CurProfile.GetPageByPath(Storage.CurPathString()).Slots.Find(s => s.Id == id);
                    this.DoDragDrop((Slot)draggedSlot, DragDropEffects.All);
                    Cursor.Current = dragCursor;
                }
            }
        }

        private void slot_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(SlotType))) e.Effect = DragDropEffects.Copy;
            if (e.Data.GetDataPresent(typeof(Slot))) e.Effect = DragDropEffects.Copy;

            borderedSlot = (ActionSlot)sender;
            Invalidate(true);
        }

        private void slot_DragLeave(object sender, EventArgs e)
        {
            borderedSlot = null;
            Invalidate(true);
        }

        private void slot_DragDrop(object sender, DragEventArgs e)
        {
            isDrag = false;
            slotMouseDown = false;
            borderedSlot = (ActionSlot)sender;

            if (((ActionSlot)sender).Type != SlotType.FolderBack)
            {
                if (e.Data.GetDataPresent(typeof(SlotType)))
                {
                    ((ActionSlot)sender).Type = (SlotType)e.Data.GetData(typeof(SlotType));

                    int id = GetSlotId((ActionSlot)sender);
                    Storage.CurProfile.AddNewSlot(Storage.CurPathString(), id, ((ActionSlot)sender).Type);

                    //Storage.SaveData();
                    //SyncSlots();

                    //SaveSlotImage(id, false);
                    // TODO: Save second state default image
                }
                else if (e.Data.GetDataPresent(typeof(Slot)))
                {
                    int fromId = GetSlotId(startDragSlot);
                    int toId = GetSlotId((ActionSlot)sender);

                    SlotLabel label_from = startDragSlot.Controls.OfType<SlotLabel>().First();
                    SlotLabel label_to = ((ActionSlot)sender).Controls.OfType<SlotLabel>().First();
                    string tmp = label_from.Text;
                    label_from.Text = label_to.Text;
                    label_to.Text = tmp;

                    Storage.CurProfile.SwapSlots(Storage.CurPathString(), fromId, toId);
                    SaveButtonImage(fromId);

                    //Storage.SaveData();
                    //SyncSlots();


                    //if (Storage.GetSlotById(toId) != null) SaveSlotImage(toId, false);
                    //if (Storage.GetSlotById(fromId) != null) SaveSlotImage(fromId, false);

                    // TODO: Save second state default image
                }

                Storage.SaveData();
                SyncSlots();

                ShowSlotSettings((ActionSlot)sender);
                Invalidate(true);
            }
        }

        private void Form1_DragOver(object sender, DragEventArgs e)
        {
            if (dragCursor != null)
                Cursor.Current = dragCursor;
        }

        private void Form1_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            e.UseDefaultCursors = false;
            if (dragCursor != null)
                Cursor.Current = dragCursor;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            HideSettings();
            borderedSlot = null;
            Invalidate(true);
        }

        #endregion

        #region Labels

        private void InitLabels()
        {
            for(int i = 0; i < Slots.Count; i++)
            {
                SlotLabel _label = new SlotLabel();
                this.Controls.Add(_label);
                _label.AutoSize = false;
                _label.Size = new Size(70, 70);
                _label.Text = Storage.GetSetting(i+1, "Title");
                _label.ForeColor = Color.FromArgb(160, 160, 160);
                _label.BackColor = Color.Transparent;
                _label.TextAlign = ContentAlignment.BottomCenter;
                Labels.Add(_label);
            }

            for(int i = 0; i < Slots.Count; i++)
            {
                var pos = this.PointToScreen(Labels[i].Location);
                pos = Slots[i].PointToClient(pos);
                Labels[i].Parent = Slots[i];
                Labels[i].Location = new Point(0,0);
                Labels[i].BackColor = Color.Transparent;
                Labels[i].BringToFront();
            }
        }

        #endregion

        private void SyncWithDevice()
        {
            //  TODO:
            //  - Save slots as images with file names as <path>.<id> ('1.5.6', '1', '2.4'...)
            //  - Send images to device
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Storage.Log();
        }
    }
}
