using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace OSWorldPing
{
    public partial class Form1 : Form
    {
        string urlStart = "oldschool";
        string urlEnd = ".runescape.com";

        string[] freeWorlds = { "1", "8", "16", "26", "35", "81", "82", "83", "84", "85", "93", "94"};
        string[] memberWorlds = {"2", "3", "4", "5", "6", "7", "9", "10", "11", "12", "13", "14", "15", "17", "18", "19", "20", "21"
                                ,"22", "23", "24", "25", "27", "28", "29", "30", "31", "32", "33", "34", "36", "37", "38", "39", "40", "41"
                                ,"42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59"
                                ,"60", "61", "62", "65", "66", "67", "68", "69", "70", "73", "74", "75", "76", "77", "78", "86", "87", "88"
                                ,"89", "90", "91", "92", "101", "105"};

        Ping pingSender = new Ping();
        PingReply reply;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            startPings();
        }

        private void startPings()
        {           
            if (checkBox1.Checked && checkBox2.Checked)            
                pingAllWorlds();  
            else if (checkBox1.Checked)
                pingFreeWorlds();
            else if(checkBox2.Checked)            
                pingMemberWorlds();            
            else            
                textBox1.Text += "Please select a box!\r\n";                       
        }

        private void pingFreeWorlds()
        {
            foreach(string w in freeWorlds)
            {
                reply = pingSender.Send(urlStart + w + urlEnd);
                textBox1.AppendText("World " + w + "\t Ping: " + reply.RoundtripTime + "ms\r\n");
            }            
        }

        private void pingMemberWorlds()
        {  
            foreach (string w in memberWorlds)
            {
                reply = pingSender.Send(urlStart + w + urlEnd);
                textBox1.AppendText("World " + w + "\t Ping: " + reply.RoundtripTime + "ms\r\n");
            }
        }

        private void pingAllWorlds()
        {
            //Merge both lists and order by value so 1 - ...
            string[] merged = freeWorlds.Concat(memberWorlds).ToArray();
            var allWorlds = from x in merged orderby x.Length, x select x;

            foreach (string w in allWorlds)
            {
                reply = pingSender.Send(urlStart + w + urlEnd);
                textBox1.AppendText("World " + w + "\t Ping: " + reply.RoundtripTime + "ms\r\n");
            }
        }
    }
}
