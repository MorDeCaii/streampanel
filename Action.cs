using System.Windows.Forms;

namespace Streampanel
{
    public class Action : Button
    {
        public SlotType Type { get; set; }

        public Action(SlotType type)
        {
            Type = type;
        }
    }
}
