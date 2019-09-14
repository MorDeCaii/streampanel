using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Streampanel
{
    public struct IconInfo
    {
        public bool fIcon;
        public int xHotspot;
        public int yHotspot;
        public IntPtr hbmMask;
        public IntPtr hbmColor;
    }

    public class CursorUtil
    {
        public static int cursorSize = 18; 

        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconIndirect(ref IconInfo icon);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr handle);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);

        // Based on the article and comments here:
        // http://www.switchonthecode.com/tutorials/csharp-tutorial-how-to-use-custom-cursors
        // Note that the returned Cursor must be disposed of after use, or you'll leak memory!
        public static Cursor CreateCursor(Bitmap bm, int xHotspot, int yHotspot)
        {
            IntPtr cursorPtr;
            IntPtr ptr = bm.GetHicon();
            IconInfo tmp = new IconInfo();
            GetIconInfo(ptr, ref tmp);
            tmp.xHotspot = xHotspot;
            tmp.yHotspot = yHotspot;
            tmp.fIcon = false;
            cursorPtr = CreateIconIndirect(ref tmp);

            if (tmp.hbmColor != IntPtr.Zero) DeleteObject(tmp.hbmColor);
            if (tmp.hbmMask != IntPtr.Zero) DeleteObject(tmp.hbmMask);
            if (ptr != IntPtr.Zero) DestroyIcon(ptr);

            return new Cursor(cursorPtr);
        }

        public static Bitmap AsBitmap(Control c)
        {
            Bitmap bm = new Bitmap(c.Width, c.Height);
            c.DrawToBitmap(bm, new Rectangle(0, 0, c.Width, c.Height));
            return bm;
        }

        public static Bitmap AsBitmapWithCursor(Control c)
        {
            Bitmap bmp = AsBitmap(c);
            Bitmap largeBmp = new Bitmap(cursorSize * 2 + bmp.Width, cursorSize * 2 + bmp.Height);
            Graphics g = Graphics.FromImage(largeBmp);
            g.CompositingMode = CompositingMode.SourceOver;
            g.DrawImage(bmp, new Point(cursorSize, cursorSize));
            Bitmap cur = new Bitmap(Properties.Resources.hand_drag, new Size(cursorSize, cursorSize));
            int x = c.PointToClient(Cursor.Position).X;
            int y = c.PointToClient(Cursor.Position).Y;
            g.DrawImage(cur, new Point(x+cursorSize/2, y+cursorSize));
            return largeBmp;
        }
    }
}
