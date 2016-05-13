using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace GuideBoard
{
    public partial class XML_Window : Form
    {
        private XmlDocument _xmlDocument;
        public XML_Window(string mainStr)
        {
            InitializeComponent();
            GetInfomation(mainStr);
        }

        private void GetInfomation(string str)
        {
            MemoryStream memory=new MemoryStream();
            _xmlDocument=new XmlDocument();
            _xmlDocument.LoadXml(str);
            _xmlDocument.Save(memory);
            memory.Seek(0, SeekOrigin.Begin);
            dataSet.ReadXml(memory);
            dataGridView1.DataSource = dataSet.Tables[0];
        }
    }
}
