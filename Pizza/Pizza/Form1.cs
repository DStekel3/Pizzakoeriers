﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pizza
{
    public partial class Form1 : Form
    {
        My_Console c;

        public Form1()
        {
            InitializeComponent();
            c = new My_Console();
            this.Close();
        }
    }
}
