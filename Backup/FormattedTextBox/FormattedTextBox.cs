using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Windows.Forms;


namespace FormattedTextBox
{
   // [ToolboxItem(true)]
   // [ToolboxBitmap(typeof(FormattedTextBox))]
    public class FormattedTextBox : TextBox
    {

        public enum  formnum{None,N0,N2,Custom}
        private formnum _format = formnum.None;
        private string format = "";
        private string form = "";
        private string tizedes = "";
        private string[] formatok = null;
        [Description("Form"),Category("Formatum")]
        public formnum Form
        {
            get
            {
                return _format;
            }
            set
            {
                _format = value;
                this.Invalidate();
            }
        }
        public String Tizedes
        {
            get { return tizedes; }
        }
        public string Format
        {
           get { return format; }
            set
            {
                format = value;
            }
        }

        public FormattedTextBox()
        {
        }
        protected override void OnEnter(EventArgs e)
        {
            RemoveFormatCharacters();
            base.OnEnter(e);
        }
        protected override void OnValidated(EventArgs e)
        {
            base.OnValidated(e);
            InsertFormatCharacters();
        }
        private void FormInit()
        {
            string numform = "";
            switch (format)
            {
                case "N0":
                    numform = "### ### ### ### ###";
                    break;
                case "N1":
                    numform = "### ### ### ### ###";
                    tizedes = ",0";
                    break;
                case "N2":
                    numform = "### ### ### ### ###";
                    tizedes = ",00";
                    break;

            }
            if (numform != "")
            {
                form = numform;
                this.TextAlign = HorizontalAlignment.Right;
            }
            if (form != "")
                formatok = new string[] { " " };
            else
            {
                form = format;
                ArrayList ar = new ArrayList();
                for (int i = 0; i < format.Length; i++)
                {
                    string egykar = format.Substring(i, 1);
                    if (egykar != "#" && !ar.Contains(egykar))
                        ar.Add(egykar);
                }
                if (ar.Count != 0)
                {
                    formatok = new string[ar.Count];
                    for (int i = 0; i < formatok.Length; i++)
                        formatok[i] = ar[i].ToString();
                }
            }
        }


        public void RemoveFormatCharacters()
        {
            if (form == "" && format != "")
                FormInit();
            if (formatok != null)
            {
                for (int i = 0; i < formatok.Length; i++)
                    this.Text = this.Text.Replace(formatok[i], "");
            }
        }

        public void InsertFormatCharacters()
        {
            if (form == "" && format != "")
                FormInit();
            if (formatok != null)
            {
                string tizedresz = "";
                if (this.TextAlign == HorizontalAlignment.Right)
                {
                    if (this.Text.Length == 0)
                    {
                        this.Text = "0"+tizedes;
                    }
                    string vesszonelkul = this.Text.Replace(",", "");
                    if (vesszonelkul.Length == this.Text.Length)
                        tizedresz = tizedes;
                    else
                    {
                        if(this.Text.LastIndexOf(",")==0)
                            this.Text = "0" + this.Text;
                        tizedresz = this.Text.Substring(this.Text.LastIndexOf(","));
                        this.Text = this.Text.Substring(0, this.Text.Length - tizedresz.Length);
                        if (tizedresz.Length > tizedes.Length)
                            tizedresz = tizedresz.Substring(0, tizedes.Length);
                        else
                        {
                            int kul = tizedes.Length - tizedresz.Length;
                            for ( int i = 0; i < kul; i++)
                                tizedresz += "0";
                        }
                    }
                }
                if (this.Text.Length != 0)
                {
                    for (int i = 0; i < this.Text.Length; i++)
                    {
                        if (form.Length > i)
                        {
                            int j;
                            int k;
                            if (this.TextAlign == HorizontalAlignment.Left)
                            {
                                j = i;
                                k = i;
                            }
                            else
                            {
                                j = form.Length - 1 - i;
                                k = this.Text.Length - i;
                            }

                            string egykar = form.Substring(j, 1);
                            if (egykar != "#")
                            {
                                this.Text = this.Text.Insert(k, egykar);
                            }
                        }
                    }
                }
                this.Text += tizedresz;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }
}
