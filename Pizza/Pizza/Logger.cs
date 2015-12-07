using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pizza
{
    class Logger
    {
        string[] str;
        int n;
        public Logger(int g)
        {
            str = new string[g + 1];
            n = 0;
        }     
        
        public void AddLine(string s)
        {
            str[n] = s;
            n++;
        } 

        public void Write()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.CreatePrompt = true;
            sfd.OverwritePrompt = true;
            sfd.FileName = "log";
            sfd.DefaultExt = "txt";
            sfd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            DialogResult dr = sfd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                File.WriteAllLines(sfd.FileName, str);
            }

        }

    }
}
