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

	public void Write(string mapLoc, int i)
	{
	  /*SaveFileDialog sfd = new SaveFileDialog();
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
	  */
	  string name = "Results";
	  string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
	  string path = mapLoc + @"\" + name;
      if (!(System.IO.Directory.Exists(path)))
	  {
		System.IO.Directory.CreateDirectory(path);
	  }

	  File.WriteAllLines(path+@"\"+i.ToString()+".txt", str);

	}

  }
}
