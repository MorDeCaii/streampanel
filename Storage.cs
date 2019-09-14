using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Linq;

namespace Streampanel
{
    public static class Storage
    {
        public static List<Profile> Profiles { get; set; }
        public static List<int> CurPath { get; set; }
        public static Profile CurProfile { get; set; }
        public static string CurProfileName { get; set; }

        private static Config config;
        private static Profile defaultProfile;

        private static string AppDir;
        private static string ProfileDir;
        private static string ConfigFile;
        private static string DefaultProfile;
        public static string ImageDir;
        public static string ButtonsDir;

        static Storage()
        {
            AppDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Streampanel");
            ProfileDir = Path.Combine(AppDir, "profiles");
            ConfigFile = Path.Combine(AppDir, "conf.spcfg");
            ImageDir = Path.Combine(AppDir, "img");
            ButtonsDir = Path.Combine(ImageDir, "buttons");
            DefaultProfile = Path.Combine(ProfileDir, "Default.spprofile");
            CurProfileName = "Default";
            CurPath = new List<int>();
            Profiles = new List<Profile>();
            config = new Config();
            defaultProfile = new Profile("Default");

            if (!Directory.Exists(AppDir)) Directory.CreateDirectory(AppDir);
            if (!Directory.Exists(ProfileDir)) Directory.CreateDirectory(ProfileDir);
            if (!Directory.Exists(ImageDir)) Directory.CreateDirectory(ImageDir);
            if (!Directory.Exists(ButtonsDir)) Directory.CreateDirectory(ButtonsDir);
            if (!File.Exists(ConfigFile))
            {
                File.WriteAllText(ConfigFile, "");
            }
            if (!File.Exists(DefaultProfile))
            {
                File.WriteAllText(DefaultProfile, "");
            }

            string json;
            JObject jsobj;
            JSchemaGenerator generator;
            JSchema schema;
            try
            {
                json = File.ReadAllText(ConfigFile);
                jsobj = JObject.Parse(json);
                generator = new JSchemaGenerator();
                schema = generator.Generate(typeof(Config));
                if (!jsobj.IsValid(schema))
                {
                    File.WriteAllText(ConfigFile, JsonConvert.SerializeObject(config));
                }
            }
            catch (Exception)
            {
                File.WriteAllText(ConfigFile, JsonConvert.SerializeObject(config));
            }

            try
            {
                json = File.ReadAllText(DefaultProfile);
                jsobj = JObject.Parse(json);
                generator = new JSchemaGenerator();
                schema = generator.Generate(typeof(Profile));
                if (!jsobj.IsValid(schema))
                {
                    File.WriteAllText(DefaultProfile, JsonConvert.SerializeObject(defaultProfile));
                }
            }
            catch (Exception)
            {
                File.WriteAllText(DefaultProfile, JsonConvert.SerializeObject(defaultProfile));
            }

            LoadData();
        }

        public static void Initialize()
        {
            //Call this to create instance of Storage manually
        }

        public static void SaveData()
        {
            try
            {
                config.CurProfileName = CurProfileName;
                File.WriteAllText(ConfigFile, JsonConvert.SerializeObject(config));

                foreach (Profile profile in Profiles)
                {
                    File.WriteAllText(Path.Combine(ProfileDir, profile.Name + ".spprofile"), JsonConvert.SerializeObject(profile));
                }
            }
            catch
            {
                MessageBox.Show("Data save error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void LoadData()
        {
            string json;
            JObject jsobj;
            JSchemaGenerator generator;
            JSchema schema;

            json = File.ReadAllText(ConfigFile);
            Config config = new Config();
            config = JsonConvert.DeserializeObject<Config>(json);
            CurProfileName = config.CurProfileName;

            //  Load profiles
            List<string> files = Directory.GetFiles(ProfileDir, "*.spprofile").ToList<string>();
            foreach (string s in files)
            {
                try
                {
                    json = File.ReadAllText(s);
                    jsobj = JObject.Parse(json);
                    generator = new JSchemaGenerator();
                    schema = generator.Generate(typeof(Profile));
                    if (jsobj.IsValid(schema))
                    {
                        Profile newProfile = new Profile(Path.GetFileNameWithoutExtension(s));
                        newProfile = JsonConvert.DeserializeObject<Profile>(json);
                        Console.WriteLine(json);
                        Console.WriteLine("\n");
                        Profiles.Add(newProfile);
                    }
                }
                catch (Exception) {}
            }

            //  Load last opened profile
            Profile prof = Profiles.Find(p => p.Name == CurProfileName);
            if (prof != null)
            {
                CurProfile = prof;
            }
            else
            {
                config.CurProfileName = "Default";
                CurProfileName = "Default";
                if (Profiles.Find(p => p.Name == "Default") != null)
                    CurProfile = Profiles.Find(p => p.Name == "Default");
                else
                {
                    MessageBox.Show("Can't find any profile.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                }
            }
        }

        public static void Log()
        {
            string json;
            JObject jsobj;
            JSchemaGenerator generator;
            JSchema schema;

            json = File.ReadAllText(ConfigFile);
            Config config = new Config();
            config = JsonConvert.DeserializeObject<Config>(json);
            CurProfileName = config.CurProfileName;

            List<string> files = Directory.GetFiles(ProfileDir, "*.spprofile").ToList<string>();
            foreach (string s in files)
            {
                try
                {
                    if (Path.GetFileName(s) == CurProfileName+".spprofile")
                    {
                        json = File.ReadAllText(s);
                        jsobj = JObject.Parse(json);
                        generator = new JSchemaGenerator();
                        schema = generator.Generate(typeof(Profile));
                        if (jsobj.IsValid(schema))
                        {
                            Profile newProfile = new Profile(Path.GetFileNameWithoutExtension(s));
                            newProfile = JsonConvert.DeserializeObject<Profile>(json);
                            Console.WriteLine(json);
                            Console.WriteLine("\n");
                        }
                    }
                }
                catch (Exception) { }
            }
        }

        public static string CurPathString()
        {
            if (CurPath.Count == 0) return "";
            return String.Join(".", CurPath.ToArray());
        }

        public static SlotPage GetSlotPage()
        {
            SlotPage page = CurProfile.SlotPages.Find(p => p.Path == CurPathString());

            return page;
        }

        public static Slot GetSlotById(int id)
        {
            SlotPage page = CurProfile.SlotPages.Find(p => p.Path == CurPathString());
            if (page != null)
            {
                Slot slot = page.Slots.Find(s => s.Id == id);
                return slot;
            }
            return null;
        }

        public static void SetSetting(int id, string settingName, string value)
        {
            SlotPage page = CurProfile.SlotPages.Find(p => p.Path == CurPathString());
            if(page != null)
            {
                Slot slot = page.Slots.Find(s => s.Id == id);
                if(slot != null)
                {
                    slot.Settings[settingName] = value;
                    SaveData();
                }
            }
        }
        public static string GetSetting(int id, string settingName)
        {
            try
            {
                SlotPage page = CurProfile.SlotPages.Find(p => p.Path == CurPathString());
                if (page != null)
                {
                    Slot slot = page.Slots.Find(s => s.Id == id);
                    if (slot != null)
                    {
                        if (slot.Settings[settingName] != null) return slot.Settings[settingName];
                    }
                }
            }
            catch(Exception err)
            {
                Console.WriteLine(err);
            }

            return null;
        }

        public static void SetImage(int id, string path, bool state = false)
        {
            SlotPage page = CurProfile.SlotPages.Find(p => p.Path == CurPathString());
            if (page != null)
            {
                Slot slot = page.Slots.Find(s => s.Id == id);
                if (slot != null)
                {
                    if (!state)
                        slot.Image_0 = path;
                    else
                        slot.Image_1 = path;
                    SaveData();
                }
            }
        }
    }

    public class Config
    {
        [JsonProperty("CurProfileName", Required = Required.Always)]
        public string CurProfileName;

        public Config()
        {
            CurProfileName = "Default";
        }
    }
}
