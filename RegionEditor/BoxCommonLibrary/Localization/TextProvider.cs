// Issue 10 - Update the code to Net Framework 3.5 - http://code.google.com/p/pandorasbox3/issues/detail?id=10 - Smjert

/*
 * 27.04.2010
 * Updated by Tarion
 * - TextProvider now sperated in TheBox.Common.Localization.TextProvider
 * - TheBox.Localization.LocalizationHelper for localization logic added
 * */

using System;
using System.Collections.Generic;
using System.Xml;

namespace TheBox.Common.Localization
{
    [Serializable]
    public class TextProvider
    {
        private Dictionary<string, Dictionary<string, string>> m_Sections;
        private string m_Language;
        
        public string this[string description]
        {
            get
            {
                string[] locate = description.Split(new char[] { '.' });

                if (locate.Length != 2)
                {
                    return null;
                }

                Dictionary<string, string> loc;
                m_Sections.TryGetValue(locate[0], out loc);

                if (loc == null)
                    return null;

                string s;
                loc.TryGetValue(locate[1], out s);
                return s;
            }
            set
            {
                string[] locate = description.Split(new char[] { '.' });

                if (locate.Length != 2)
                {
                    throw new Exception("Bad descriptor when adding a new entry to text provider");
                }

                Add(value, locate[0], locate[1]);
            }
        }
        
        public string Language
        {
            get => m_Language;
            set => m_Language = value;
        }
        
        public Dictionary<string, Dictionary<string, string>> Data
        {
            get => m_Sections;
            set => m_Sections = value;
        }
        
        public TextProvider()
        {
            m_Sections = new Dictionary<string, Dictionary<string, string>>();
        }
        
        public void DeleteSection(string name)
        {
            m_Sections.Remove(name);
        }
        
        public void RemoveItem(string section, string item)
        {
            Dictionary<string, string> hash;
            m_Sections.TryGetValue(section, out hash);

            if (hash != null)
            {
                hash.Remove(item);
            }
        }
        
        public void RemoveItem(string definition)
        {
            string[] loc = definition.Split(new char[] { '.' });

            if (loc.Length != 2)
                return;

            RemoveItem(loc[0], loc[1]);
        }



        private void Add(string text, string category, string definition)
        {
            Dictionary<string, string> loc = null;


            if (m_Sections.ContainsKey(category))
            {
                loc = m_Sections[category];
            }
            else
            {
                loc = new Dictionary<string, string>();

                m_Sections.Add(category, loc);
            }
            loc[definition] = text;
        }

        #region serialization
        
        public void Serialize(string filename)
        {
            XmlDocument dom = new XmlDocument();

            XmlNode decl = dom.CreateXmlDeclaration("1.0", null, null);

            dom.AppendChild(decl);

            XmlNode lang = dom.CreateElement("Data");

            XmlAttribute langtype = dom.CreateAttribute("language");
            langtype.Value = m_Language;
            lang.Attributes.Append(langtype);

            foreach (string toplevel in m_Sections.Keys)
            {
                XmlNode topnode = dom.CreateElement("section");

                XmlAttribute topname = dom.CreateAttribute("name");
                topname.Value = toplevel;

                topnode.Attributes.Append(topname);

                Dictionary<string, string> hash;
                m_Sections.TryGetValue(toplevel, out hash);

                foreach (string lowlevel in hash.Keys)
                {
                    XmlNode entrynode = dom.CreateElement("entry");

                    XmlAttribute name = dom.CreateAttribute("name");
                    name.Value = lowlevel;
                    entrynode.Attributes.Append(name);

                    XmlAttribute val = dom.CreateAttribute("text");
                    string value;
                    hash.TryGetValue(lowlevel, out value);
                    val.Value = value;
                    entrynode.Attributes.Append(val);

                    topnode.AppendChild(entrynode);
                }

                lang.AppendChild(topnode);
            }

            dom.AppendChild(lang);

            dom.Save(filename);
        }
        
        public static TextProvider Deserialize(XmlDocument dom)
        {
            XmlNode data = dom.ChildNodes[1];

            TextProvider text = new TextProvider();

            text.m_Language = data.Attributes["language"].Value;

            foreach (XmlNode section in data.ChildNodes)
            {
                string topkey = section.Attributes["name"].Value;

                Dictionary<string, string> hash = new Dictionary<string, string>();

                foreach (XmlNode entry in section.ChildNodes)
                {
                    string lowkey = entry.Attributes["name"].Value;
                    string t = entry.Attributes["text"].Value;

                    hash.Add(lowkey, t);
                }

                text.m_Sections.Add(topkey, hash);
            }

            return text;
        }

        #endregion
    }
}