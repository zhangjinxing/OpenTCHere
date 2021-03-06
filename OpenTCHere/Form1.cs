﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;


namespace OpenTCHere
{
    public partial class Form1 : Form
    {
        private string tc, label, key1, key1a, key2, key2a, url;

        public Form1()
        {
            InitializeComponent();
        }

        public Form1(string tcPath)
        {
            tc = tcPath;
            label = "Open TC Here";
            key1 = @"HKEY_CLASSES_ROOT\Directory\shell\totalcmd";
            key1a = key1 + @"\command";
            key2 = @"HKEY_CLASSES_ROOT\*\shell\totalcmd";
            key2a = key2 + @"\command";
            url = @"https://github.com/yanxyz/OpenTCHere";

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var name = Registry.GetValue(key1, "", null);
            var value = Registry.GetValue(key1a, "", null);
            if (name != null && value != null)
            {
                txtName.Text = name.ToString();
                txtArgs.Text = value.ToString().Replace("\"" + tc + "\" ", "");
            }
            else
            {
                txtName.Text = label;
                btnUnreg.Enabled = false;
            }
         }

        private void Form1_Shown(object sender, EventArgs e)
        {
            txtArgs.Focus();
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            string args = txtArgs.Text.Trim();
            if (string.IsNullOrEmpty(args))
            {
                txtArgs.Focus();
                return;
            }

            string name = txtName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                name = label;
            }
            

            args = "\"" + tc + "\" " + args;

            Registry.SetValue(key1, "", name);
            Registry.SetValue(key1, "icon", tc);
            Registry.SetValue(key1a, "", args);

            Registry.SetValue(key2, "", name);
            Registry.SetValue(key2, "icon", tc);
            Registry.SetValue(key2a, "", args);
            
            btnUnreg.Enabled = true;
            success();

        }

        private void btnUnreg_Click(object sender, EventArgs e)
        {

            Registry.ClassesRoot.DeleteSubKeyTree(@"Directory\shell\totalcmd");
            Registry.ClassesRoot.DeleteSubKeyTree(@"*\shell\totalcmd");

            txtArgs.Text = "";
            txtArgs.Focus();
            btnUnreg.Enabled = false;
            success();

        }

        private void success()
        {
            MessageBox.Show("操作成功", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(url);
        }


    }
}
