using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// SetPro.xaml 的交互逻辑
    /// </summary>
    public partial class Select : Window
    {
        public List<String> monitorList1; //给多选框用的
        public List<String> monitorList2; //给ListView框用的
        public String m;
        public Select()
        {
            monitorList1 = new List<String>();
            monitorList2 = new List<String>();
            init();
            InitializeComponent();
        }

        public void init()
        {
            readMonitorApp();
        }

        

        //从文件中读取记录的监控软件
        public void readMonitorApp()
        {
            FileStream fs = null;
            StreamReader sr = null;
            try
            {
                fs = File.OpenRead(@"..\..\monitorApp.txt");
                sr = new StreamReader(fs);

                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    monitorList2.Add(s);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("读取监控软件文件 出错");

                Console.WriteLine(e.Message);
            }
            finally
            {
                if(sr != null)
                    sr.Close();
                if(fs != null)
                    fs.Close();
            }
            
        }
        //获取listView里的软件名
        public List<string> getItems()
        {
            List<string> sl = new List<string>();
            string s = null;
            for(int i = 0;i < listview.Items.Count;i++)
            {
                s = listview.Items[i].ToString().ToLower();
                if(!sl.Contains(s))
                    sl.Add(s);
            }
            return sl;
        }

        //获取checkoBox里的软件名
        public List<string> getLists()
        {
            return monitorList1;
        }

        public int getControlTime()
        {
            int time = Int32.Parse(comboBox1.Text);
            return time;
        }

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            m = this.comboBox1.Text;
            this.DialogResult = true;
        }

        private void CheckBox1(object sender, RoutedEventArgs e)
        {
            if ((bool)this.check1.IsChecked)
            {
                string s = (String)this.check1.Content;
                if (!monitorList1.Contains(s))
                    monitorList1.Add(s.ToLower());
            }
            else
            {
                monitorList1.Remove(((String)this.check1.Content).ToLower());
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void check2_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)this.check2.IsChecked)
            {
                string s = (String)this.check2.Content;
                if (!monitorList1.Contains(s))
                    monitorList1.Add(s.ToLower());
            }
            if ((bool)this.check2.IsChecked)
            {
                monitorList1.Remove(((String)this.check2.Content).ToLower());
            }
        }

        private void check3_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)this.check3.IsChecked)
            {
                monitorList1.Add((String)this.check3.Content);
            }
            else
            {
                monitorList1.Remove((String)this.check3.Content);
            }
        }

        private void check4_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)this.check4.IsChecked)
            {
                string s = (String)this.check4.Content;
                if (!monitorList1.Contains(s))
                    monitorList1.Add(s.ToLower());
            }
            else
            {
                monitorList1.Remove(((String)this.check4.Content).ToLower());
            }
        }

        

        //添加
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string s = text.Text.ToLower();
            
            if(!listview.Items.Contains(s))
                listview.Items.Add(s);
            text.Text = "";
        }
        //双击删除
        private void OnListViewItemDoubleClick(object sender, MouseButtonEventArgs e)
        {
          
            int selectedNo = this.listview.SelectedIndex;        
            listview.Items.RemoveAt(selectedNo);          
        }

        //private void Button2_Click(object sender, RoutedEventArgs e)
        //{
        //    listview.Items.Remove(text.Text);

        //}

        //刷新记录文件
        public void flushMonitorApp()
        {
            FileStream fs = File.Create(@"../../monitorApp.txt");
            StreamWriter sw = new StreamWriter(fs);
            foreach(var s in getItems())
            {
                sw.WriteLine(s);
            }
            sw.Flush();

            sw.Close();
            fs.Close();
        }
        //按下确定键
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            flushMonitorApp();
            MainWindow myMain = (MainWindow)this.Owner;
            myMain.controlTime = getControlTime();

            foreach (var s in getItems())
            {
                if (!myMain.gameList.Contains(s.ToLower()))
                    myMain.gameList.Add(s.ToLower());
            }

            foreach (var s in monitorList1)
            {
                if (!myMain.gameList.Contains(s.ToLower()))
                    myMain.gameList.Add(s.ToLower());
            }
            this.Close();
        }

        //初始化时显示从文件中读取的监控软件
        private void listview_Initialized(object sender, EventArgs e)
        {
            foreach (var s in monitorList2)
            {
                listview.Items.Add(s);
            }
        }
        //按下Enter键也可以添加
        private void text_KeyUp(object sender, KeyEventArgs e)
        {

            Key myKey = e.Key;
            if(myKey == Key.Enter)
            {
                string s = text.Text.ToLower();

                if (!listview.Items.Contains(s))
                    listview.Items.Add(s);
                text.Text = "";
            }
        }
    }
}
