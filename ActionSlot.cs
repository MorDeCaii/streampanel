using System.Windows.Forms;

namespace Streampanel
{
    public class ActionSlot : PictureBox
    {
        public SlotType Type { get; set; }

        public ActionSlot()
        {
            Type = SlotType.None;
        }
    }
}
