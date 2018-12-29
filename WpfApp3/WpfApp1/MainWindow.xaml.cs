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
using System.IO;

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
        //时钟样式
        BitmapImage TimerPhoto = null;
        public int coin;//金币
        public bool lock1;
        public bool lock2;

        public MainWindow()
        {
            MoneyGet();
            InitializeComponent();
           //mc.FileName = @"E:\视频\影音\往后余生 - 1王贰浪.mp3"; //默认音乐路径
            NowNo = 1;
            first = true;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        } 
        public int NowNo { get; private set; }
        public bool first { get; private set; }
        private void MoneyGet()//获取金币
        {
            StreamReader sr = new StreamReader(@"1.txt", Encoding.Default);
            String line;
            line = sr.ReadLine();
            coin = int.Parse(line);
            line = sr.ReadLine();
            lock1 = Boolean.Parse(line);
            line = sr.ReadLine();
            lock2 = Boolean.Parse(line);
            sr.Close();
        }
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
            t = new Timer(disTimer, mc, countSecond, this,NowNo, first,TimerPhoto, chooseTimeString);
            t.setCoin(coin);
            t.ShowDialog();
            t.CountSecond = countSecond;
        }
        private void ComboBoxItem1(object sender, RoutedEventArgs e)
        {
            MusicChoice m = new MusicChoice();
            mc.FileName = m.Forest();
            bg.Source = new BitmapImage(new Uri(m.ForestPicture()));
            TimerPhoto = new BitmapImage(new Uri(m.ForestTimer()));

        }
        private void ComboBoxItem2(object sender, RoutedEventArgs e)
        {
            MusicChoice m = new MusicChoice();
            mc.FileName = m.Rain();
            bg.Source = new BitmapImage(new Uri(m.RainPicture()));
            TimerPhoto = new BitmapImage(new Uri(m.RainTimer()));

        }
        private void ComboBoxItem3(object sender, RoutedEventArgs e)
        {

            if (lock2)
            {
                MusicChoice m = new MusicChoice();
                mc.FileName = m.Waves();
                //---------------=-----------------------------------------------------------修改
                bg.Source = new BitmapImage(new Uri(m.WavesPicture()));
                TimerPhoto = new BitmapImage(new Uri(m.WavesTimer()));
            }
            else
            {
                Buy buy = new Buy(coin);
                buy.ShowDialog();
                if (buy.DialogResult == true)
                {
                    coin -= 50;
                    buy.Close();
                    lock2 = true;
                    MusicChoice m = new MusicChoice();
                    mc.FileName = m.Waves();
                    bg.Source = new BitmapImage(new Uri(m.WavesPicture()));
                    TimerPhoto = new BitmapImage(new Uri(m.WavesTimer()));
                    StreamWriter sw2 = new StreamWriter(@"1.txt", false, Encoding.UTF8);
                    sw2.WriteLine(coin);
                    sw2.WriteLine(lock1);
                    sw2.WriteLine(lock2);
                    sw2.Close();
                    MoneyL.Content = "当前金币数：" + coin;
                }
                else
                {
                    buy.Close();
                    
                }
            }
        }
        private void ComboBoxItem4(object sender, RoutedEventArgs e)
        {
            MusicChoice m = new MusicChoice();
            mc.FileName = System.IO.Directory.GetCurrentDirectory() + @"\Music\Forest2.mp3";
            bg.Source = new BitmapImage(new Uri(m.DefaultPicture()));
            TimerPhoto = null;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            SetPro setPro = new SetPro();
            setPro.ShowDialog();
            if (setPro.DialogResult == true)
            {
                string m = setPro.m;
                setPro.Close();
            }
        }

        private void Init(object sender, EventArgs e)
        {

        }

        private void InitMonL(object sender, EventArgs e)
        {
            MoneyL.Content = "当前金币数：" + coin;
        }
    }
}
