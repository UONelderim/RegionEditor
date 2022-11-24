using System.IO;
using System.Xml.Serialization;

namespace RegionEditor
{
    public class RegEdOptions
    {
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(RegEdOptions));
        
        public string ClientPath = "";
        public bool AlwaysOnTop = false;
        public bool DrawStatics = true;
        public bool XRayVision = false;
        
        public static void Save(RegEdOptions Options)
        {
            string FileName = Path.Combine(Directory.GetCurrentDirectory(), "RegEdOptions.xml");

            using (FileStream theStream = new FileStream(FileName, FileMode.Create))
            {
                Serializer.Serialize(theStream, Options);
            }
        }

        public static RegEdOptions Load()
        {
            RegEdOptions Options = new RegEdOptions();
            
            string FileName = Path.Combine(Directory.GetCurrentDirectory(), "RegEdOptions.xml");

            if (!File.Exists(FileName))
                return Options;

            using (FileStream theStream = new FileStream(FileName, FileMode.Open))
            {
                Options = (RegEdOptions)Serializer.Deserialize(theStream);
            }

            return Options;
        }
    }
}