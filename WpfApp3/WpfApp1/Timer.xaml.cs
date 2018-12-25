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
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Timer.xaml 的交互逻辑
    /// </summary>
    public partial class Timer : Window
    {
        private int hour, second, minute;  
        DispatcherTimer disTimer = new DispatcherTimer();  //定时器                                                
        MusicManager mc = new MusicManager();   //播放音乐
        public int CountSecond, learningTime;  //计时总秒数;
        MainWindow main;

        ProcManager pm;

        public Timer(DispatcherTimer t, MusicManager m, int time, MainWindow w, int num, bool f)
        {
            pm = new ProcManager();
            pm.init();
            pm.addGameName("TIM");
            pm.addGameName("chrome");

            disTimer = t;      //初始化
            mc = m;
            learningTime = CountSecond = time;   //时间
            InitializeComponent();
            main = w;                                               //初始化主界面对象
            NowNo = num;                                      //将文件中数据数传入，防止覆盖
            first = f;
            Start();  //调用开始计时函数
        }

        public int NowNo { get; private set; }
        public bool first { get; private set; }

        private void Pause(object sender, RoutedEventArgs e)  //暂停
        {

            mc.Puase();  //暂停音乐
            disTimer.Stop();
        }

         private void Continue(object sender, RoutedEventArgs e)  //继续
        {  
                mc.play();
                disTimer.Start();            
        }


        private void Start()    //初始化时调用该函数，开始计时
        {
            disTimer.Interval = new TimeSpan(0, 0, 0, 1); //参数为:天 小时 分和秒
            disTimer.Tick += new EventHandler(disTimer_Tick);   //添加
            mc.play();  //开始播放
            disTimer.Start();  //开始计时          
        }

        private void GiveUp(object sender, RoutedEventArgs e)  //放弃
        {
            pm.countResult(); //监控

            mc.Puase();
            disTimer.Stop();
            
            disTimer.Tick -= new EventHandler(disTimer_Tick);
            
            int length = learningTime - CountSecond;  //已学习时长
            //转换为字符串，传入参数
            String timeRecord = String.Format("{0:D2}", length / 60 / 60) + ":" + String.Format("{0:D2}", (length / 60) % 60) + ":" + String.Format("{0:D2}", length % 60);
            LearningRecordWindow lrw = new LearningRecordWindow(NowNo, first, false, false, timeRecord);
            lrw.ShowDialog();
            NowNo++;
            LearningRecordService.ShowAll();
            first = false;

            //实现主界面的显示，本界面的关闭
            this.Close();
            main.Visibility = Visibility.Visible;
            main.InitializeComponent();
        }
        
        //每个时间间隔都执行的处理函数（监控进程
        void disTimer_Tick(object sender, EventArgs e)   
        {
            pm.onceMonitor();
            //游戏超时
            if(!pm.checkGameTime())
            {
                mc.Puase();
                disTimer.Stop();
                MessageBoxResult result = MessageBox.Show("你的游戏时间太长了，需要结束学习吗?", "End confirm", MessageBoxButton.OKCancel);
                if(result == MessageBoxResult.OK)
                {
                    disTimer.Stop(); //关闭计时器
                    mc.StopT(); //关闭音乐               

                    this.Close();  //关闭当前窗口

                    String timeRecord = String.Format("{0:D2}", learningTime / 60 / 60) + ":" + String.Format("{0:D2}", (learningTime / 60) % 60) + ":" + String.Format("{0:D2}", learningTime % 60);
                    LearningRecordWindow lrw = new LearningRecordWindow(NowNo, first, true, false, timeRecord);
                    lrw.ShowDialog();
                    NowNo++;
                    LearningRecordService.ShowAll();
                    first = false;

                    disTimer.Tick -= new EventHandler(disTimer_Tick);

                    main.Visibility = Visibility.Visible;  //显示主窗口
                }
                else
                {
                    pm.clearGameTime();
                    mc.play();
                    disTimer.Start();
                }
            }

            //  cbssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss


            int temp = CountSecond;
            if (CountSecond == -1)  //为了显示效果，故此处设置为-1
            {             
                MessageBox.Show("结束");
                disTimer.Stop(); //关闭计时器
                mc.StopT(); //关闭音乐               

                this.Close();  //关闭当前窗口

                String timeRecord = String.Format("{0:D2}", learningTime / 60 / 60) + ":" + String.Format("{0:D2}", (learningTime / 60) % 60) + ":" + String.Format("{0:D2}", learningTime % 60);
                LearningRecordWindow lrw = new LearningRecordWindow(NowNo, first, true, false, timeRecord);
                lrw.ShowDialog();
                NowNo++;
                LearningRecordService.ShowAll();
                first = false;

                disTimer.Tick -= new EventHandler(disTimer_Tick);

                main.Visibility = Visibility.Visible;  //显示主窗口

                
            }
            else
            {
                second = CountSecond % 60;
                minute = (CountSecond / 60) % 60;
                hour = CountSecond / 60 / 60;
                timer2.Content = String.Format("{0:D2}", hour) + ":" + String.Format("{0:D2}", minute) + ":" + String.Format("{0:D2}", second);
                CountSecond--;
            }
        }

        //以下函数实现窗体的移动
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // 获取鼠标相对标题栏位置  
            Point position = e.GetPosition(gridTitle);


            // 如果鼠标位置在标题栏内，允许拖动  
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (position.X >= 0 && position.X < gridTitle.ActualWidth && position.Y >= 0 && position.Y < gridTitle.ActualHeight)
                {
                    this.DragMove();
                }
            }
        }  
    }
}
