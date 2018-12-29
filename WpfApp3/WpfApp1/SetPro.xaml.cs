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
    /// SetPro.xaml 的交互逻辑
    /// </summary>
    public partial class SetPro : Window
    {
        public List<String> list = new List<String>();
        public String m;
        public SetPro()
        {
            InitializeComponent();
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
                list.Add((String)this.check1.Content);
            }
            else
            {
                list.Remove((String)this.check1.Content);
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void check2_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)this.check2.IsChecked)
            {
                list.Add((String)this.check2.Content);
            }
            if ((bool)this.check2.IsChecked)
            {
                list.Remove((String)this.check2.Content);
            }
        }

        private void check4_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)this.check4.IsChecked)
            {
                list.Add((String)this.check4.Content);
            }
            else
            {
                list.Remove((String)this.check4.Content);
            }
        }

        private void check3_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)this.check3.IsChecked)
            {
                list.Add((String)this.check3.Content);
            }
            else
            {
                list.Remove((String)this.check3.Content);
            }
        }
    }
}
