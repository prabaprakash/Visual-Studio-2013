using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Win32;

namespace SP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private List<it> its = new List<it>();
        private List<symboltab> symboltabs = new List<symboltab>();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            int counter = 0;
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Files|*.txt";
            var a = op.ShowDialog();
            if (a == true)
            {
                rt1.Document.Blocks.Clear();
                its.Clear();
                String dd = "";
                using (StreamReader sr = new StreamReader(@"instruct_table.txt"))
                {
                    while ((dd = sr.ReadLine()) != null)
                    {
                        var f = dd.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                        its.Add(new it() {itname = f[0], itval = f[1]});
                    }
                }

                StringBuilder onepass = new StringBuilder();
                StringBuilder symtab = new StringBuilder();
                StringBuilder twopass = new StringBuilder();
                using (StreamReader sr = new StreamReader(op.FileName))
                {

                    while ((dd = sr.ReadLine()) != null)
                    {
                        var f = dd.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                        if (f[1] == "START")
                        {
                            counter = int.Parse(f[2]);
                            onepass.Append(String.Format("{0} {1} {2} {3}\n{4} ", "**", f[0], f[1], f[2], counter));
                        }
                        foreach (var it in its)
                        {
                            if (it.itname == f[1])
                            {
                                counter = counter + 3;
                                onepass.Append(String.Format("{0} {1} {2}\n{3} ", f[0], f[1], f[2], counter));
                            }
                        }
                        if (f[1] == "RESW" || f[1] == "RESB" || f[1] == "WORD")
                        {
                            symboltabs.Add(new symboltab() {name = f[0], loc = counter.ToString()});
                            counter = counter + 3;
                            onepass.Append(String.Format("{0} {1} {2}\n{3} ", f[0], f[1], f[2], counter));
                        }
                        if (f[1] == "BYTE")
                        {
                            symboltabs.Add(new symboltab() {name = f[0], loc = counter.ToString()});
                            counter = counter + 1;
                            onepass.Append(String.Format("{0} {1} {2}\n{3} ", f[0], f[1], f[2], counter));
                        }
                        if (f[1] == "END")
                        {

                            onepass.Append(String.Format("{0} {1} {2}", f[0], f[1], f[2]));
                        }
                    }

                }
                rt1.AppendText("Pass One\n\n");
                rt1.AppendText(onepass.ToString());
                foreach (var variable in symboltabs)
                {
                    symtab.Append(string.Format("{0} {1}\n", variable.name, variable.loc));
                }
                rt1.AppendText("\nSymbol Table\n");
                rt1.AppendText(symtab.ToString());

                
                    foreach (var ve in onepass.ToString().Split(new char[] {'\n'}, StringSplitOptions.RemoveEmptyEntries))
                    {


                        var f = ve.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                       // MessageBox.Show(f[0]);
                        if (f[2] == "START")
                        {

                            twopass.Append(String.Format("{0} {1} {2} {3}\n", f[0], f[1], f[2],f[3]));
                        }
                        foreach (var it in its)
                        {
                            if (it.itname == f[2])
                            {
                                counter = counter + 3;
                                var d = (from x in symboltabs where x.name == f[3] select x.loc).Take(1);
                                foreach (var VARIABLE in d)
                                {
                                    twopass.Append(String.Format("{0} {1} {2} {3} {4}\n", f[0], f[1], f[2], f[3]
                                        , it.itval + VARIABLE));
                                }

                            }
                        }
                        if (f[2] == "RESW" || f[2] == "RESB" || f[2] == "WORD")
                        {
                            twopass.Append(String.Format("{0} {1} {2} {3}\n", f[0], f[1], f[2], f[3]));
                        }
                        if (f[2] == "BYTE")
                        {
                            twopass.Append(String.Format("{0} {1} {2} {3} {4}\n", f[0], f[1], f[2], f[3]
                                , f[3]));
                        }
                        if (f[2] == "END")
                        {

                            twopass.Append(String.Format("{0} {1} {2} {3}\n", f[0], f[1], f[2], f[3]));
                        }
                    }
                    rt1.AppendText("\nTwo Pass\n");
                    rt1.AppendText(twopass.ToString());
                }
                


                
            }
            catch (Exception exception)
                {
                    MessageBox.Show(exception.StackTrace);
                }
        }
    }




        public class it
        {
            public String itname { get; set; }
            public String itval { get; set; }
        }
        public class symboltab
        {
            public string name { get; set; }
            public string loc { get; set; }
        }
    
}
//Reference

//using (FileStream fs = new FileStream(op.FileName, FileMode.Open))
//{
//    //StringBuilder sc = new StringBuilder();
//    //byte[] b = new byte[1024];
//    //UTF8Encoding temp = new UTF8Encoding(true);
//    //while (fs.Read(b, 0, b.Length) > 0)
//    //{
//    //    sc.Append(temp.GetString(b));
//    //}
//    //rt1.AppendText(sc.ToString());
//    TextRange aa = new TextRange(rt1.Document.ContentStart, rt1.Document.ContentEnd);
//    aa.Load(fs, DataFormats.Text);
//}


//var aaa = from x in aa.Split(new char[]{' '})  select x;
//StringBuilder d=new StringBuilder();
//foreach (var VARIABLE in aaa)
//{
//    d.Append(VARIABLE+"\n");
//}
//MessageBox.Show(d.ToString());