using System;
using System.Collections.Generic;
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
using Visifire.Charts;

namespace VisifireShow
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    
    public class Proc
    {
        public String Name { get; set; }
        public int Time { get; set; }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }


        private void ButPie_Click(object sender, RoutedEventArgs e)
        { 
            //模拟传参过程
            Proc proc1 = new Proc();
            proc1.Name = "saferi";
            proc1.Time = 10;
            List<Proc> procs = new List<Proc>();
            procs.Add(proc1);
            Proc proc2 = new Proc();
            proc2.Name = "word";
            proc2.Time = 20;
            procs.Add(proc2);
            Window1 window = new Window1(procs);
            window.ShowDialog();
        }
    }
}
