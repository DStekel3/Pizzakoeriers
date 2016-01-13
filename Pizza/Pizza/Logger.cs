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

	public void Write(string mapLoc, string name, int i)
	{
	  string path = Path.GetDirectoryName(mapLoc) + @"\" + name;
      if (!(System.IO.Directory.Exists(path)))
	  {
		System.IO.Directory.CreateDirectory(path);
	  }

	  File.WriteAllLines(path+@"\"+i.ToString()+".txt", str);
	}

	public void Write(string mapLoc, string name)
	{
	  string path = Path.GetDirectoryName(mapLoc) + @"\" + name;
	  if (!(System.IO.Directory.Exists(path)))
	  {
		System.IO.Directory.CreateDirectory(path);
	  }

	  File.WriteAllLines(path +@"\gemiddeldes.txt", str);
	}
  }
}
