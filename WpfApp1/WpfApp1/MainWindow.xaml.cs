using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        Process[] process;
        List<int> time = new List<int>();
        List<string> threads = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            MyInit();
        }

        void MyInit()
        {
            process = Process.GetProcesses(".");//获得本机的进程
            for (int i = 0; i < 18; i++)    //我觉得18个已经很多了
            {
                time.Add((int)(500 * (Math.Sin(i) + 1)));
                threads.Add(process[i].ToString());

            }
        }

        //我本是本着尽量方便你的想法这么写的
        //MyBar的构造函数一个是List<string>,另一个是List<int>,
        private void ButColumn_Click(object sender, RoutedEventArgs e)
        {
            MyBar myBar = new MyBar(threads, time);
            myBar.ShowDialog();

        }
    }
}
