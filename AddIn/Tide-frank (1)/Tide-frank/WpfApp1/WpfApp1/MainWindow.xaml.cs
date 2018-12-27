using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
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
        private int hour, second, minute;  //用来显示
        private bool isOK = false;  // 表示暂停按钮是否被点击；
        DispatcherTimer disTimer = new DispatcherTimer();  //定时器                                                
        MusicManager mc = new MusicManager();   //播放音乐
       
        public MainWindow()
        {
            InitializeComponent();        
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)  //选择文件目录
        {
            //System.Diagnostics.Process.Start("Explorer.exe", "c:\\");
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件(*.*)|*.*";
            if (fileDialog.ShowDialog() == true)   //如果点击“打开”键
            {
                mc.FileName = fileDialog.FileName;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)  //暂停
        {
            
            if(isOK == false)
            {
                mc.Puase();  //暂停音乐
                disTimer.Stop();
                button1.Content = "继续";
                isOK = true;
            }
            else
            {
                mc.play();
                disTimer.Start();
                button1.Content = "暂停";
                isOK = false;
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)  //退出程序
        {
            System.Environment.Exit(System.Environment.ExitCode);
            //this.Dispose();
            this.Close();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)    //按钮点击，切换图片
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件(*.*)|*.*";
            if (fileDialog.ShowDialog() == true)   //如果点击“打开”键
            {
                bg.Source = new BitmapImage(new Uri(fileDialog.FileName));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)   //开始专注点击事件
        {
            if (comboBox.Text == null)
                MessageBox.Show("未选择时间！");
            countSecond = int.Parse(comboBox.Text)*60; //获取选择时间                    
            disTimer.Interval = new TimeSpan(0, 0, 0, 1); //参数为:天 小时 分和秒
            disTimer.Tick += new EventHandler(disTimer_Tick);   //每一秒执行一次的方法
            //mc.FileName = @"E:\视频\影音\往后余生 马良.mp3";
            mc.play();  //开始播放
            disTimer.Start();  //开始计时            
        }

        void disTimer_Tick(object sender, EventArgs e)
        {

            if (countSecond == -1)  //为了显示效果，故此处设置为-1
            {
                MessageBox.Show("结束");           
                disTimer.Stop(); //关闭计时器
                mc.StopT(); //关闭音乐
            }
            else
            {              
                second = countSecond % 60;
                minute = (countSecond / 60)%60;
                hour = countSecond / 60 /60;
                lable1.Content = hour.ToString() + ":" + minute.ToString()+":"+second.ToString();
                countSecond--;
            }

            
        }
    }
}
