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
    /// Buy.xaml 的交互逻辑
    /// </summary>
    public partial class Buy : Window
    {
        int Money;
        public Buy(int money)
        {
            Money = money;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Money > 50)
            {
                this.DialogResult = true; MessageBox.Show("解锁成功");
            }
            else
            {
                MessageBox.Show("金币不足，多多学习积攒金币");
                this.DialogResult = false;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
