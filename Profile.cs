using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace Streampanel
{
    public class Profile
    {
        public string Name { get; set; }
        public List<SlotPage> SlotPages { get; set; }

        public Profile(string name)
        {
            Name = name;
            SlotPage firstPage = new SlotPage("");
            SlotPages = new List<SlotPage>();
            SlotPages.Add(firstPage);
        }

        public SlotPage GetPageByPath(string path)
        {
            SlotPage page = SlotPages.Find(sp => sp.Path == path);
            if(page != null) return page;
            page = new SlotPage(path);
            SlotPages.Add(page);
            return page;
        }

        public void AddNewSlot(string path, int id, SlotType type)
        {
            SlotPage page = GetPageByPath(path);
            if (page != null)
            {
                Slot slot = page.Slots.Find(s => s.Id == id);
                if (slot == null)
                {
                    slot = new Slot(id, type);
                    page.Slots.Add(slot);
                }
                else
                {
                    //Here ask for change slot
                    slot.Type = type;
                    //TODO: Manage slot settings
                }
            }
        }
        public bool SwapSlots(string path, int from, int to)
        {
            Slot fromSlot = GetPageByPath(path).Slots.Find(s => s.Id == from);
            if (fromSlot == null || from == to ) return false;
            Slot toSlot = GetPageByPath(path).Slots.Find(s => s.Id == to);

            try
            {
                string curPath = Storage.CurPathString();
                if (curPath != "") curPath += ".";
                List<SlotPage> fromPages = SlotPages.FindAll(sp => sp.Path.StartsWith(curPath + from.ToString()));
                List<SlotPage> toPages = SlotPages.FindAll(sp => sp.Path.StartsWith(curPath + to.ToString()));

                if (toSlot != null)
                {
                    if (toSlot.Type == SlotType.Folder)
                    {

                        foreach (SlotPage page in toPages)
                        {
                            var regex = new Regex(Regex.Escape(curPath + to.ToString()));
                            page.Path = regex.Replace(page.Path, curPath + from.ToString(), 1);
                        }
                    }
                    toSlot.Id = from;
                }

                if (fromSlot.Type == SlotType.Folder)
                {
                    foreach (SlotPage page in fromPages)
                    {
                        var regex = new Regex(Regex.Escape(curPath + from.ToString()));
                        page.Path = regex.Replace(page.Path, curPath + to.ToString(), 1);
                    }
                }
                fromSlot.Id = to;
                return true;
            }
            catch(Exception err)
            {
                Console.WriteLine(err);
                return false;
            }
        }

        public void DeleteSlot(string path, int id)
        {
            SlotPage page = GetPageByPath(path);
            if (page != null)
            {
                Slot slot = page.Slots.Find(s => s.Id == id);
                if (slot != null)
                {
                    //Delete slot images
                    try
                    {
                        if (File.Exists(slot.Image_0))
                        {
                            File.Delete(slot.Image_0);
                        }
                        if (File.Exists(slot.Image_1))
                        {
                            File.Delete(slot.Image_1);
                        }
                    }
                    catch(Exception err)
                    {
                        Console.WriteLine(err);
                    }

                    if (slot.Type == SlotType.Folder)
                    {
                        string deletePath = path;
                        if (path != "") deletePath += ".";
                        DeletePage(deletePath + id.ToString());
                        page.Slots.Remove(slot);
                    }
                    else
                    {
                        page.Slots.Remove(slot);
                    }
                }
            }
        }

        public void DeletePage(string path)
        {
            List<SlotPage> pages = SlotPages.FindAll(sp => sp.Path.StartsWith(path));
            foreach(SlotPage page in pages)
            {
                //  Delete inner slot images
                foreach(Slot slot in page.Slots)
                {
                    if (File.Exists(slot.Image_0))
                    {
                        File.Delete(slot.Image_0);
                    }
                    if (File.Exists(slot.Image_1))
                    {
                        File.Delete(slot.Image_1);
                    }
                }
                SlotPages.Remove(page);
            }
        }
    }

    public class Slot
    {
        public int Id { get; set; }
        public SlotType Type { get; set; }
        public string Image_0 { get; set; }
        public string Image_1 { get; set; }
        public bool State { get; set; }
        public Dictionary<string, string> Settings { get; set; }

        public Slot(int id, SlotType type)
        {
            Id = id;
            Type = type;
            State = false;
            Image_0 = "";
            Image_1 = "";

            Settings = new Dictionary<string, string>();
            Settings["Title"] = "";

            switch (type)
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
        }
    }

    public class SlotPage
    {
        public string Path { get; set; }
        public List<Slot> Slots { get; set; }

        public SlotPage(string path)
        {
            Path = path;
            Slots = new List<Slot>();
        }
    }

    public enum SlotType
    {
        None = 0,
        Folder = 1,
        FolderBack = 2,
        OpenApp = 3,
        Website = 4,
        Hotkey = 5
    }
}
