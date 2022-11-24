using System.IO;
using System.Xml.Serialization;

namespace RegionEditor
{
    public class RegEdOptions
    {
        public string ClientPath = "";
        public bool AlwaysOnTop = false;
        public bool DrawStatics = true;
        public bool XRayVision = false;

        public RegEdOptions()
        {
        }

        public static void Save(RegEdOptions Options)
        {
            string FileName = Path.Combine(Directory.GetCurrentDirectory(), "RegEdOptions.xml");

            XmlSerializer serializer = new XmlSerializer(typeof(RegEdOptions));
            FileStream theStream = new FileStream(FileName, FileMode.Create);
            serializer.Serialize(theStream, Options);
            theStream.Close();
        }

        public static RegEdOptions Load()
        {
            RegEdOptions Options = new RegEdOptions();

            // Look for filename
            System.Reflection.Assembly theExe = Options.GetType().Assembly;

            string file = theExe.Location;

            string FileName = Path.Combine(Path.GetDirectoryName(file), "RegEdOptions.xml");

            if (!File.Exists(FileName))
                return Options;

            // File exists
            XmlSerializer serializer = new XmlSerializer(typeof(RegEdOptions));
            FileStream theStream = new FileStream(FileName, FileMode.Open);
            Options = (RegEdOptions)serializer.Deserialize(theStream);
            theStream.Close();

            return Options;
        }
    }
}