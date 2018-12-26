using Microsoft.Win32;
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
using System.Diagnostics;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private int countSecond;  //设置多少秒后提醒
        DispatcherTimer disTimer = new DispatcherTimer();  //定时器                                                
        MusicManager mc = new MusicManager();   //播放音乐
        Timer t;

        public MainWindow()
        {
            InitializeComponent();
           //mc.FileName = @"E:\视频\影音\往后余生 - 1王贰浪.mp3"; //默认音乐路径
            NowNo = 1;
            first = true;
        }

        public int NowNo { get; private set; }
        public bool first { get; private set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)  //查询学习记录
        {
            LearningRecordWindow lrw = new LearningRecordWindow();
            lrw.ShowDialog();
            LearningRecordService.ShowAll();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)   //开始专注点击事件
        {        
          
            if (comboBox.Text == null)
                MessageBox.Show("未选择时间！");
            string chooseTimeString = comboBox.Text;
            string[] chooseTime = chooseTimeString.Split(' ');
            countSecond = int.Parse(chooseTime[0]) * 60;//获取选择时间 
     
          
            //主界面的隐藏
            //显示时钟界面
            MainForm.Visibility = Visibility.Hidden;
            t = new Timer(disTimer, mc, countSecond, this,NowNo, first);
            t.ShowDialog();
            t.CountSecond = countSecond;
        }
        private void ComboBoxItem1(object sender, RoutedEventArgs e)
        {
            MusicChoice m = new MusicChoice();
            mc.FileName = m.Forest();
            bg.Source = new BitmapImage(new Uri(m.ForestPicture()));
        }
        private void ComboBoxItem2(object sender, RoutedEventArgs e)
        {
            MusicChoice m = new MusicChoice();
            mc.FileName = m.Rain();
            bg.Source = new BitmapImage(new Uri(m.RainPicture()));
        }
        private void ComboBoxItem3(object sender, RoutedEventArgs e)
        {
            MusicChoice m = new MusicChoice();
            mc.FileName = m.Waves();
            bg.Source = new BitmapImage(new Uri(m.WavesPicture()));
        }
    }
}
