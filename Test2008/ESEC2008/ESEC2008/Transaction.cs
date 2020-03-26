using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Reflection;

namespace ESEC2008

{
    public class Transaction
    {
        [XmlElementAttribute("Name")]
        public string Name { get; set; }

        private int tid = 0;

        [XmlElementAttribute("TID")]
        public int TID
        {
            get
            {
                if (tid == 0)
                {
                    tid = TransactionStatic.GetVid();

                }
                return tid;
            }
            set
            {
                tid = value;
            }
        }


        private MQTTMessageType type = MQTTMessageType.EventReport;
        [XmlElementAttribute("Type")]
        public MQTTMessageType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        private Dictionary<string, string> items = new Dictionary<string, string>();

        [XmlIgnore]
        public Dictionary<string, string> Items
        {
            get { return items; }
            set { items = value; }
        }

        public Transaction()
        {
            
        }

        public Transaction(string name)
        {
            this.Name = name;
        }

        public Transaction(Transaction tran,MQTTMessageType mqtype)
        {
            this.Name = tran.Name;
            this.TID = tran.TID;
            this.Type = mqtype;            
        }

        public void FromStringToObject(string xmlstring)
        {
            try
            {

                XDocument xdoc = XDocument.Parse(xmlstring);
                this.Name = xdoc.Root.Name.ToString();

                Type type = this.GetType();
                System.Reflection.PropertyInfo[] ps = type.GetProperties();
                foreach (PropertyInfo i in ps)
                {
                    if (xdoc.Root.Attribute(i.Name) != null)
                    {

                        if (i.PropertyType.Name.ToLower().Contains("string"))
                        {
                            i.SetValue(this, xdoc.Root.Attribute(i.Name).Value, null);
                        }
                        else if (i.PropertyType.Name.ToLower().Contains("int"))
                        {
                            i.SetValue(this, int.Parse(xdoc.Root.Attribute(i.Name).Value), null);
                        }
                        else if (i.PropertyType.Name.ToLower().Contains("bool"))
                        {
                            i.SetValue(this, bool.Parse(xdoc.Root.Attribute(i.Name).Value.ToLower()), null);
                        }
                        else if (i.PropertyType.Name.ToLower().Contains("boolean"))
                        {
                            i.SetValue(this, bool.Parse(xdoc.Root.Attribute(i.Name).Value.ToLower()), null);
                        }
                        else if (i.PropertyType.Name.ToLower().Contains("mqttmessagetype"))
                        {
                            i.SetValue(this, Enum.Parse(typeof(MQTTMessageType), xdoc.Root.Attribute(i.Name).Value.ToLower(), true), null);
                        }
                    }
                }



                foreach (var ss in xdoc.Root.Elements())
                {
                    Items.Add(ss.Name.ToString(), ss.Value);
                }



            }
            catch (Exception e)
            {
                Log.Logger.Error(e);

            }
        }

        public string CreateXMLstring()
        {
            try
            {

                XElement contacts = new XElement("Transaction");
                contacts.SetAttributeValue("Name", Name);

               
                contacts.SetAttributeValue("TID", TID);


               
                contacts.SetAttributeValue("Type", Type);


                foreach (var ss in Items)
                {
                    XElement childcontacts = new XElement(ss.Key, ss.Value);
                    contacts.Add(childcontacts);
                }


                return contacts.ToString();
            }
            catch (Exception e)
            {
                Log.Logger.Error(e);
                return null;
            }
        }


        public void Add(string item, string value)
        {
            try
            {
                if (string.IsNullOrEmpty(item)) return;
                if (Items.ContainsKey(item))
                {
                    Items[item] = value;
                }
                else
                {
                    Items.Add(item, value);
                }
            }

            catch (Exception e)
            {
                Log.Logger.Error(e);
            }
        }

    }


    public static class TransactionStatic
    {
        static int tid = 1;
        static object obj = new object();

        public static int GetVid()
        {
            lock (obj)
            {
                return tid++;
            }
        }
        
    }

    public enum MQTTMessageType
    {
        Request,
        Reply,
        EventReport
    }


}
