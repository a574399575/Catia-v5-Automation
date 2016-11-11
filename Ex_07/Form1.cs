﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex_07
{
    public partial class Form1 : Form
    {
        INFITF.Application Catia = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Catia = (INFITF.Application)Marshal.GetActiveObject("CATIA.Application");
            }
            catch
            {
                Catia = (INFITF.Application)Activator.CreateInstance(Type.GetTypeFromProgID("CATIA.Application"));
                Catia.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 카티아가 실행중?
            if (Catia == null)
            {
                //MessageBox.Show("Please run CATIA");
                label1.Text = "Please run CATIA";
                return;
            }

            // 활성 문서가 있는가?
            if (Catia.ActiveDocument == null)
            {
                //MessageBox.Show("활성 문서가 없습니다");
                label1.Text = "활성 문서가 없습니다";
            }

            // Point 선택 기능 실행
            INFITF.Selection Sel = null;
            Sel = Catia.ActiveDocument.Selection;

            Object[] InputObjectType = { "Plane", "Line" , "Point"};
            string Status;
            Sel.Clear();
            Status = Sel.SelectElement2(InputObjectType, "Select a Elements", false);
            Catia.ActiveDocument.Selection.Copy();

            if (Status != "Normal")
            {
                label1.Text = "취소됨";
                return;
            }

            if (Sel.Count < 1)
            {
                label1.Text = "선택";
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Catia.ActiveDocument.Selection.Paste();
        }
    }
}