using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;

namespace Streampanel
{
    partial class Form1 : Form
    {
        //List<Control> baseControls = new List<Control>();
        List<Control> otherControls = new List<Control>();
        bool isVisible = false;

        const int SETTINGS_MARGIN = 20;
        const int LABEL_POS_X = 121;
        const int TEXT_POS_X = 221;
        const int LABEL_WIDTH = 90;
        const int TEXT_WIDTH = 164;
        public ActionSlot curSlot;
        public Panel bottomPanel;

        SPButton deleteButton;
        Label titleLabel;
        TextBox title;

        private void InitSettings()
        {
            bottomPanel = new Panel();
            bottomPanel.Location = new Point(260, 409);
            bottomPanel.Size = new Size(438, 185);
            bottomPanel.BackColor = Color.FromArgb(41, 41, 41);
            bottomPanel.Click += new EventHandler(bottomPanel_MouseClick);
            this.Controls.Add(bottomPanel);

            curSlot = new ActionSlot();
            curSlot.Location = new Point(46, 20);
            curSlot.Size = new Size(70, 70);
            curSlot.BackColor = Color.FromArgb(30,30,30);
            curSlot.MouseClick += new MouseEventHandler(curSlot_MouseClick);
            bottomPanel.Controls.Add(curSlot);

            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add("&Set Image", new EventHandler(menu_SetImage_Click));
            menu.MenuItems.Add("&Reset to Default", new EventHandler(menu_Reset_Click));
            menu.Popup += new EventHandler(menu_Popup);
            curSlot.ContextMenu = menu;

            deleteButton = new SPButton();
            deleteButton.Location = new Point(51, 130);
            deleteButton.Size = new Size(61, 26);
            deleteButton.FlatStyle = FlatStyle.Flat;
            deleteButton.FlatAppearance.BorderSize = 0;
            deleteButton.FlatAppearance.BorderColor = Color.FromArgb(41,41,41);
            deleteButton.BackgroundImage = Properties.Resources.deleteButton;
            deleteButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            deleteButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            deleteButton.BackColor = Color.Transparent;
            deleteButton.Cursor = Cursors.Hand;
            deleteButton.MouseEnter += new EventHandler(deleteButton_Enter);
            deleteButton.MouseLeave += new EventHandler(deleteButton_Leave);
            deleteButton.MouseClick += new MouseEventHandler(deleteButton_Click);
            bottomPanel.Controls.Add(deleteButton);

            titleLabel = new Label();
            titleLabel.AutoSize = false;
            titleLabel.Location = new Point(LABEL_POS_X, 21);
            titleLabel.Size = new Size(LABEL_WIDTH, 24);
            titleLabel.TextAlign = ContentAlignment.MiddleRight;
            titleLabel.Text = "Title:";
            titleLabel.Font = new Font("Segoe UI", 10f,  FontStyle.Regular);
            titleLabel.ForeColor = Color.FromArgb(120, 120, 120);
            bottomPanel.Controls.Add(titleLabel);

            title = new TextBox();
            title.Location = new Point(TEXT_POS_X, 26);
            title.Width = TEXT_WIDTH;
            title.Font = new Font("Segoe UI", 10f, FontStyle.Regular);
            title.ForeColor = Color.FromArgb(120, 120, 120);
            title.BackColor = Color.FromArgb(30, 30, 30);
            title.BorderStyle = BorderStyle.None;
            title.TextChanged += new EventHandler(title_TextChanged);
            bottomPanel.Controls.Add(title);

            HideSettings();
        }

        private void ShowSlotSettings(ActionSlot actionSlot)
        {
            SlotPage page = Storage.GetSlotPage();
            Slot slot = null;
            int id = 1;
            if (page != null)
            {
                id = GetSlotId(actionSlot);
                slot = page.Slots.Find(s => s.Id == id);
            }

            if (slot == null) return;

            switch (slot.Type)
            {
                case SlotType.None:
                    break;
                case SlotType.Folder:
                    break;
                case SlotType.FolderBack:
                    break;
                case SlotType.OpenApp:
                    break;
                case SlotType.Website:
                    break;
                case SlotType.Hotkey:
                    break;
            }

            curSlot.BackgroundImage = actionSlot.BackgroundImage;
            title.Text = Storage.GetSetting(id, "Title");

            bottomPanel.Visible = true;

            isVisible = true;
        }

        private void HideSettings()
        {
            bottomPanel.Visible = false;
            CurSlotDraw = false;

            curSlot.BackgroundImage = null;
            isVisible = false;
        }

        private void ShowFolderSettings(Slot slot)
        {
            
        }

        private void ShowWebsiteSettings(Slot slot)
        {

        }

        #region Bottom Panel

        private void bottomPanel_MouseClick(object sender, EventArgs e)
        {
            CurSlotDraw = false;
            Invalidate(true);
        }

        #endregion

        #region Current Slot

        private void menu_Popup(object sender, EventArgs e)
        {
            CurSlotDraw = true;
            Invalidate(true);
        }

        private void curSlot_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CurSlotDraw = true;
                Invalidate(true);
            }
        }
        private void menu_SetImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "Image Files (*.bmp;*.png;*.jpg)|*.bmp;*.png;*.jpg|BMP Files (*.bmp)|*.bmp|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Image image = Image.FromFile(dlg.FileName, true);
                    Bitmap slot_image = new Bitmap(70, 70);

                    float scale = Math.Min((float)slot_image.Width / image.Width, (float)slot_image.Height / image.Height);

                    var scaleWidth = (int)(image.Width * scale);
                    var scaleHeight = (int)(image.Height * scale);

                    Graphics g = Graphics.FromImage(slot_image);
                    g.CompositingMode = CompositingMode.SourceOver;
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(0, 0, 0)))
                    {
                        g.FillRectangle(brush, 0, 0, slot_image.Width, slot_image.Height);
                        g.DrawImage(image, ((int)slot_image.Width - scaleWidth) / 2, ((int)slot_image.Height - scaleHeight) / 2, scaleWidth, scaleHeight);
                        image.Dispose();
                    }

                    curSlot.BackgroundImage = slot_image;
                    borderedSlot.BackgroundImage = curSlot.BackgroundImage;

                    //  TODO: Manage slot state (for now state == 0)
                    SaveSlotImage(GetSlotId(borderedSlot), false);
                    SaveButtonImage(GetSlotId(borderedSlot));
                }
            }
        }
        private void menu_Reset_Click(object sender, EventArgs e)
        {

        }

        private void SaveSlotImage(int id, bool state = false)
        {
            string filename = "img" + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + Guid.NewGuid().ToString() + "_";

            if (Slots[id - 1] == null || Slots[id - 1].BackgroundImage == null || Storage.GetSlotById(id) == null) return;

            try
            {
                if (!Directory.Exists(Path.Combine(Storage.ImageDir, Storage.CurProfileName)))
                    Directory.CreateDirectory(Path.Combine(Storage.ImageDir, Storage.CurProfileName));

                if (!state)
                {
                    string img = Storage.GetSlotById(id).Image_0;
                    if (File.Exists(img)) File.Delete(img);
                    else if (img != "")
                    {
                        Storage.GetSlotById(id).Image_0 = "";
                    }
                    img = Storage.GetSlotById(id).Image_1;
                    if (!File.Exists(img)) Storage.GetSlotById(id).Image_1 = "";
                }
                else
                {
                    string img = Storage.GetSlotById(id).Image_1;
                    if (File.Exists(img)) File.Delete(img);
                    else if (img != "")
                    {
                        Storage.GetSlotById(id).Image_1 = "";
                    }
                }
                Storage.SetImage(id, Path.Combine(Storage.ImageDir, Storage.CurProfileName) + "\\" + filename + Convert.ToInt32(state).ToString() + ".png", state);

                Bitmap slotImage = Slots[id - 1].BackgroundImage as Bitmap;

                string save = Path.Combine(Storage.ImageDir, Storage.CurProfileName) + "\\" + filename + Convert.ToInt32(state).ToString() + ".png";
                if (File.Exists(save)) File.Delete(save);

                slotImage.Save(save);

                if (!state && Storage.GetSlotById(id).Image_1 == "")
                {
                    Bitmap slotImageDarken = ChangeColorBrightness(slotImage, 0.7f, 0.7f, 0.6f);
                    Storage.SetImage(id, Path.Combine(Storage.ImageDir, Storage.CurProfileName) + "\\" + filename + Convert.ToInt32(!state).ToString() + ".png", !state);
                    save = Path.Combine(Storage.ImageDir, Storage.CurProfileName) + "\\" + filename + Convert.ToInt32(!state).ToString() + ".png";
                    slotImageDarken.Save(save);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }
        }

        private void SwapSlotImages(int fromId, int toId)
        {
            if (Slots[fromId - 1] == null || Slots[fromId - 1].BackgroundImage == null || 
                Storage.GetSlotById(fromId) == null || Slots[toId - 1] == null || fromId == toId) return;


            string filename = Storage.CurPathString();
            if (filename != "") filename += "_";
            string from_0 = Storage.ImageDir + "\\" + filename + fromId.ToString() + "-0.png";
            string from_1 = Storage.ImageDir + "\\" + filename + fromId.ToString() + "-1.png";
            string to_0 = Storage.ImageDir + "\\" + filename + toId.ToString() + "-0.png";
            string to_1 = Storage.ImageDir + "\\" + filename + toId.ToString() + "-1.png";

            if (!File.Exists(from_0))
            {
                Bitmap slotImage = Slots[fromId - 1].BackgroundImage as Bitmap;
                slotImage.Save(from_0);
            }
            /*if (!File.Exists(from_1))
            {
                Bitmap slotImage = Slots[fromId - 1].BackgroundImage as Bitmap;
                slotImage.Save(from_1);
            }*/


            if (Storage.GetSlotById(toId) == null)
            {
                if (File.Exists(to_0)) File.Delete(to_0);
                //if (File.Exists(to_1)) File.Delete(to_1);

                File.Move(from_0, to_0);
                //File.Move(from_1, to_1);

                Storage.SetImage(fromId, "", false);
                //Storage.SetImage(fromId, "", true);
                Storage.SetImage(toId, to_0, false);
                //Storage.SetImage(toId, to_1, true);
            }
            else
            {
                if (!File.Exists(to_0))
                {
                    Bitmap slotImage = Slots[toId - 1].BackgroundImage as Bitmap;
                    slotImage.Save(to_0);
                }
                /*if (!File.Exists(to_1))
                {
                    Bitmap slotImage = Slots[toId - 1].BackgroundImage as Bitmap;
                    slotImage.Save(to_1);
                }*/

                File.Move(to_0, Storage.ImageDir + "\\tmp.png");
                File.Move(from_0, to_0);
                File.Move(Storage.ImageDir + "\\tmp.png", from_0);

                /*File.Move(to_1, Storage.ImageDir + "\\tmp.png");
                File.Move(from_1, to_1);
                File.Move(Storage.ImageDir + "\\tmp.png", from_1);*/

                Storage.SetImage(fromId, from_0, false);
                //Storage.SetImage(fromId, from_1, true);
                Storage.SetImage(toId, to_0, false);
                //Storage.SetImage(toId, to_1, true);
            }
        }

        private void SaveButtonImage(int id)
        {
            if (Slots[id - 1] == null || Storage.GetSlotById(id) == null) return;

            string filename_0 = Storage.GetSlotById(id).Image_0;
            string filename_1 = Storage.GetSlotById(id).Image_1;
            string baseName = Storage.CurPathString();
            if (baseName != "") baseName += ".";
            string saveName_0 = baseName + id.ToString() + "_0.png";
            string saveName_1 = baseName + id.ToString() + "_1.png";

            try
            {
                Bitmap slotImage_0 = Bitmap.FromFile(filename_0) as Bitmap;
                Graphics g = Graphics.FromImage(slotImage_0);
                g.InterpolationMode = InterpolationMode.HighQualityBilinear;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                SlotLabel label = Slots[id - 1].Controls.OfType<SlotLabel>().First();
                if (label != null)
                {
                    Bitmap label_bmp = new Bitmap(label.Width, label.Height);
                    label.DrawToBitmap(label_bmp, new Rectangle(0, 0, label.Width, label.Height));
                    g.CompositingMode = CompositingMode.SourceOver;
                    g.DrawImage(label_bmp, new Rectangle(0, 0, label.Width, label.Height));
                }

                string save = Storage.ButtonsDir + "\\" + saveName_0;
                if (File.Exists(save)) File.Delete(save);
                slotImage_0.Save(save);

                g.Flush();

                Bitmap slotImage_1 = new Bitmap(70,70);
                g = Graphics.FromImage(slotImage_1);
                g.InterpolationMode = InterpolationMode.HighQualityBilinear;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                ActionSlot sl = new ActionSlot();
                sl = Slots[id - 1].Clone();
                sl.BackgroundImage = Image.FromFile(filename_1);
                //label = sl.Controls.OfType<SlotLabel>().First();
                SlotLabel l = label.Clone();
                l.Parent = sl;
                foreach(Control c in sl.Controls)
                {
                    Console.WriteLine("!");
                }
                //label = sl.Controls.OfType<SlotLabel>().First();
                //sl.DrawToBitmap(slotImage_1, new Rectangle(0, 0, sl.Width, sl.Height));
                //g.DrawImage(slotImage_1, new Rectangle(0, 0, slotImage_1.Width, slotImage_1.Height));
                if (l != null)
                {
                    Bitmap label_bmp = new Bitmap(label.Width, label.Height);
                    l.DrawToBitmap(label_bmp, new Rectangle(0, 0, label.Width, label.Height));
                    g.CompositingMode = CompositingMode.SourceOver;
                    g.DrawImage(label_bmp, new Rectangle(0, 0, label.Width, label.Height));
                }

                save = Storage.ButtonsDir + "\\" + saveName_1;
                if (File.Exists(save)) File.Delete(save);
                slotImage_1.Save(save);
            }
            catch(Exception err)
            {
                Console.WriteLine(err);
            }
        }

        void TransparetBackground(Control C)
        {
            C.Visible = false;

            C.Refresh();
            Application.DoEvents();

            Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);
            int titleHeight = screenRectangle.Top - this.Top;
            int Right = screenRectangle.Left - this.Left;

            Bitmap bmp = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(bmp, new Rectangle(0, 0, this.Width, this.Height));
            Bitmap bmpImage = new Bitmap(bmp);
            bmp = bmpImage.Clone(new Rectangle(C.Location.X + Right, C.Location.Y + titleHeight, C.Width, C.Height), bmpImage.PixelFormat);
            C.BackgroundImage = bmp;

            C.Visible = true;
        }
        #endregion

        #region Delete Button

        private void deleteButton_Enter(object sender, EventArgs e)
        {
            deleteButton.BackgroundImage = Properties.Resources.deleteButton_hover;
        }
        private void deleteButton_Leave(object sender, EventArgs e)
        {
            deleteButton.BackgroundImage = Properties.Resources.deleteButton;
        }
        private void deleteButton_Click(object sender, MouseEventArgs e)
        {
            int id = GetSlotId(borderedSlot);
            borderedSlot.BackgroundImage.Dispose();
            Storage.CurProfile.DeleteSlot(Storage.CurPathString(), id);
            Storage.SaveData();
            HideSettings();
            SyncSlots();
            Invalidate(true);
        }

        #endregion

        #region Title

        private void title_TextChanged(object sender, EventArgs e)
        {
            int id = GetSlotId(borderedSlot);
            if (id > Labels.Count && id-1 >= 0) return;

            Labels[id-1].Text = ((TextBox)sender).Text;
            Storage.SetSetting(id, "Title", ((TextBox)sender).Text);
        }

        #endregion

        public Bitmap ChangeColorBrightness(Bitmap originalImage, float brightness, float contrast, float gamma)
        {
            Bitmap adjustedImage = new Bitmap(originalImage.Width, originalImage.Height);

            float adjustedBrightness = brightness - 1.0f;
            // create matrix that will brighten and contrast the image
            float[][] ptsArray ={
            new float[] {contrast, 0, 0, 0, 0}, // scale red
            new float[] {0, contrast, 0, 0, 0}, // scale green
            new float[] {0, 0, contrast, 0, 0}, // scale blue
            new float[] {0, 0, 0, 1.0f, 0}, // don't scale alpha
            new float[] {adjustedBrightness, adjustedBrightness, adjustedBrightness, 0, 1}};

            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.ClearColorMatrix();
            imageAttributes.SetColorMatrix(new ColorMatrix(ptsArray), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            imageAttributes.SetGamma(gamma, ColorAdjustType.Bitmap);
            Graphics g = Graphics.FromImage(adjustedImage);
            g.DrawImage(originalImage, new Rectangle(0, 0, adjustedImage.Width, adjustedImage.Height)
                , 0, 0, originalImage.Width, originalImage.Height,
                GraphicsUnit.Pixel, imageAttributes);

            return adjustedImage;
        }
    }

    public class SPButton : Button
    {
        public override void NotifyDefault(bool value)
        {
            base.NotifyDefault(false);
        }
    }
}
