using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace comp_lab1
{
    public class Parameter
    {
        public string Name
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public string Type
        {
            get;
            set;
        }
        public string Value
        {
            get;
            set;
        }
    }

    public partial class ParameterControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        public void SetControl(Parameter par, int i)
        {
            NameText.Text = par.Name;
            DescriptionText.Text = par.Description;
            if (par.Type == "String")
            {
                TextValue.Visible = true;
                TextValue.Text = par.Value;
                TypeSelect.SelectedIndex = 0;
            }
            if (par.Type == "Int")
            {
                NumInput.Visible = true;
                NumInput.Text = par.Value;
                TypeSelect.SelectedIndex = 1;
            }
            if (par.Type == "Bool")
            {
                TypeSelect.SelectedIndex = 2;
                CheckInput.Visible = true;
                if (par.Value == "True")
                {
                    CheckInput.Checked = true;
                }
                else
                {
                    CheckInput.Checked = false;
                }
            }

            //NumInput.TextChanged += new EventHandler(NumInput_TextChanged);
            //NumInput.AutoPostBack = true;

            Delete.ID = String.Format("{0}_Delete", i);
            Delete.Click += new EventHandler(Delete_ServerClick);

            TypeSelect.ID = String.Format("{0}_TypeSelect", i);
            TypeSelect.SelectedIndexChanged += new EventHandler(TypeSelect_ServerChange);
            TypeSelect.AutoPostBack = true;
        }

        public void GetParameter(ref Parameter par)
        {
            par.Name = NameText.Text;
            par.Description = DescriptionText.Text;
            par.Type = TypeSelect.Text;

            if (TypeSelect.Text == "String")
            {
                par.Value = TextValue.Text;
            }
            if (par.Type == "Int")
            {
                par.Value = NumInput.Text;
            }
            if (par.Type == "Bool")
            {
                par.Value = (CheckInput.Checked).ToString();
            }
        }

        void TypeSelect_ServerChange(object sender, EventArgs e)
        {
            DropDownList list;
            list = (DropDownList)sender;
            _Default def = new _Default();
            def.SetType(int.Parse(list.ID.Replace("_TypeSelect", "")), list.SelectedValue);
        }

        void Delete_ServerClick(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            _Default def = new _Default();
            def.DeleteElement(int.Parse(b.ID.Replace("_Delete", "")));
        }
    }
}