using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace comp_lab1
{
    public partial class _Default : System.Web.UI.Page
    {
        public static List<Parameter> data;
        public static bool reload;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !reload)
                data = XmlParser(@"D:\home\MyLabs\comp_lab1\Input.xml");//(AppDomain.CurrentDomain.BaseDirectory + "/Input.xml");
            for (int i = 0; i < data.Count; i++)
            {
                ParameterControl elemControl = (ParameterControl)LoadControl("ParameterControl.ascx");
                elemControl.SetControl(data[i], i);
                elemControl.ID = String.Format("{0}", i);
                form1.Controls.Add(elemControl);
            }
            reload = false;

            Button add = new Button();
            add.ID = "Add";
            add.Text = "Add";
            add.Click += new EventHandler(Add_ServerClick);
            form1.Controls.Add(add);

            Button save = new Button();
            save.ID = "Save";
            save.Text = "Save";
            save.Click += new EventHandler(save_Click);
            form1.Controls.Add(save);

            this.DataBind();
        }

        void save_Click(object sender, EventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Parameter>));
            TextWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/Output.xml");

            data.Clear();
            for (int j = 0; j < form1.Controls.Count; j++)
            {
                Type type_ = form1.Controls[j].GetType();
                ParameterControl elemControl = (ParameterControl)LoadControl("ParameterControl.ascx");
                if (type_.ToString() == elemControl.GetType().ToString())
                {
                    Parameter parametr = new Parameter();
                    elemControl = (ParameterControl)form1.Controls[j];
                    elemControl.GetParameter(ref parametr);
                    data.Add(parametr);
                }
            }

            serializer.Serialize(writer, data);
            writer.Close();
        }

        void Add_ServerClick(object sender, EventArgs e)
        {
            Parameter new_element = new Parameter();
            new_element.Type = "String";
            data.Add(new_element);

            reload = true;
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.ToString());
        }
        public List<Parameter> XmlParser(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Parameter>),
                    new XmlRootAttribute("Parameters"));
                List<Parameter> parameters = (List<Parameter>)serializer.Deserialize(fs);
                return parameters;
            }
        }
        public void DeleteElement(int index)
        {
            data.RemoveAt(index);
            reload = true;
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.ToString());
        }

        public void SetType(int index, string type)
        {
            data[index].Type = type;
            reload = true;
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.ToString());
        }
    }
}
