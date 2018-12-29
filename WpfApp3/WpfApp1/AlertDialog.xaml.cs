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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// AlertDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AlertDialog : Window
    {
        string gameName;
        int retime;
        //是否确认
        public bool isConfirm;

        public AlertDialog()
        {
            InitializeComponent();
            init();
        }

        public AlertDialog(string name,int time):this()
        {
            gameName = name;
            retime = time;
            TipLabel.Content = "你已经使用 " + gameName + " 超过 " + retime + " 秒了";
        }

        public void init()
        {
            double workHeight = SystemParameters.WorkArea.Height + 10;

            double workWidth = SystemParameters.WorkArea.Width + 10;

            ShowInTaskbar = false;
            Topmost = true;
            AllowsTransparency = true;
            //alert.Opacity = 10;
            WindowStyle = WindowStyle.None;
            //Height = MaxHeight = MinHeight = workHeight;
            //Width = MaxWidth = MinWidth = workWidth;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        //确定
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            isConfirm = true;
            this.Close();
        }
        //取消
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            isConfirm = false;
            this.Close();
        }
        //初始化提示的 Label
        private void TipLabel_Initialized(object sender, EventArgs e)
        {
            
        }
    }
}
