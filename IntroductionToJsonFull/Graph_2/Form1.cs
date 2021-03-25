using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace Graph_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        public void Form1_Load(object sender, EventArgs e)
        {
            // For GDP
            string txt = "TextFile2.Txt";
            StreamReader stream = new StreamReader(txt);
            string text = stream.ReadToEnd();
            IDictionary<string, double> GDP = JsonConvert.DeserializeObject<Dictionary<string, double>>(text);
            //write key along with dashes to represent int value
            Console.WriteLine("Enter 3 letter key");
            string input = Console.ReadLine();
            chart1.Titles.Add("GDP");
            foreach (KeyValuePair<string, double> item in GDP)
            {
                Console.WriteLine(item.ToString());
                int num = Convert.ToInt32(Math.Round(item.Value, 1));
                Console.WriteLine($"Num is {num}");
                chart1.Series["GDP"].Points.AddXY(item.Key, item.Value.ToString());
            }
            //Console.WriteLine(GDP[input]);
            
            chart1.Titles.Add("GDP Chart");
        }
    }
}
