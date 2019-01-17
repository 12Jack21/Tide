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
        AlertDialog alert;
        int coin = 0;//金币
        int timeSpan;//学习时长


        public Timer(DispatcherTimer t, MusicManager m, int time, MainWindow w, int num, bool f,BitmapImage photo,string choice)
        {
            pm = new ProcManager();
            pm.init();
            
            timeSpan = int.Parse(choice);
            disTimer = t;      //初始化
            mc = m;
            learningTime = CountSecond = time;   //时间
            InitializeComponent();
            main = w;                                               //初始化主界面对象
            NowNo = num;                                      //将文件中数据数传入，防止覆盖
            first = f;
            Start();  //调用开始计时函数
            if (photo != null)
                timer1.Source = photo;
        }

        public int NowNo { get; private set; }
        public bool first { get; private set; }

        //设置监控约束时间
        public void setControlTime(int time)
        {
            pm.setControlTime(time);
        }
        //设置监控表
        public void setGameList(List<string> list)
        {
            foreach(var s in list)
            {
                pm.addGameName(s);
            }
        }

        public void setCoin(int coin)
        {
            this.coin = coin;
        }
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
            //存放当前一次学习中使用的程序信息，用List存储，并将其传到学习记录窗口
            List<Proc> procResult = pm.countResult();
            mc.Puase();
            disTimer.Stop();
            
            MessageBoxResult quit = MessageBox.Show("你确定要放弃本次学习吗？", "提示", MessageBoxButton.OKCancel);
            if(quit == MessageBoxResult.OK)
            {
                disTimer.Tick -= new EventHandler(disTimer_Tick);

                int length = learningTime - CountSecond;  //已学习时长
                                                          //转换为字符串，传入参数
                String timeRecord = String.Format("{0:D2}", length / 60 / 60) + ":" + String.Format("{0:D2}", (length / 60) % 60) + ":" + String.Format("{0:D2}", length % 60);
                LearningRecordWindow lrw = new LearningRecordWindow(NowNo, false, timeRecord, procResult,0);
                lrw.ShowDialog();
                NowNo++;
                LearningRecordService.ShowAll();
                //实现主界面的显示，本界面的关闭
                this.Close();
                main.Visibility = Visibility.Visible;
                main.InitializeComponent();
            }
            else
            {
                disTimer.Start();
                mc.play();
            }   
        }

        //显示警告对话框
        void showAlertDialog()
        {
            alert = new AlertDialog(pm.getGameName(), pm.getTime());
            alert.ShowDialog();
        }
        //时钟Tick
        void disTimer_Tick(object sender, EventArgs e)   
        {
            pm.onceMonitor();
            //游戏超时
            if (!pm.checkGameTime())
            {
                mc.Puase();
                disTimer.Stop();

                //显示警告框
                showAlertDialog();
                if (alert.isConfirm)//确定不学了
                {
                    disTimer.Stop(); //关闭计时器
                    mc.StopT(); //关闭音乐               

                    this.Close();  //关闭当前窗口
                    //存放当前一次学习中使用的程序信息，用List存储，并将其传到学习记录窗口
                    List<Proc> procResult = pm.countResult();
                    String timeRecord = String.Format("{0:D2}", learningTime / 60 / 60) + ":" + String.Format("{0:D2}", (learningTime / 60) % 60) + ":" + String.Format("{0:D2}", learningTime % 60);
                    LearningRecordWindow lrw = new LearningRecordWindow(NowNo, false, timeRecord, procResult, 0);
                    lrw.ShowDialog();
                    NowNo++;
                    LearningRecordService.ShowAll();

                    disTimer.Tick -= new EventHandler(disTimer_Tick);
                    main.Visibility = Visibility.Visible;  //显示主窗口
                }
                else
                {
                    pm.clearGameTime();
                    mc.play();
                    disTimer.Start();
                    return;
                }
            }

            int temp = CountSecond;
            if (CountSecond == 40)  //为了显示效果，故此处设置为-1
            {
                MessageBox.Show("你已成功完成本次学习，金币加 "+timeSpan+" !");
                disTimer.Stop(); //关闭计时器
                mc.StopT(); //关闭音乐               
                this.Close();  //关闭当前窗口
                //存放当前一次学习中使用的程序信息，用List存储，并将其传到学习记录窗口
                List<Proc> procResult = pm.countResult();
                String timeRecord = String.Format("{0:D2}", learningTime / 60 / 60) + ":" + String.Format("{0:D2}", (learningTime / 60) % 60) + ":" + String.Format("{0:D2}", learningTime % 60);

                main.coin += timeSpan;
                LearningRecordWindow lrw = new LearningRecordWindow(NowNo, true, timeRecord, procResult,timeSpan);
                lrw.ShowDialog();
                NowNo++;
                LearningRecordService.ShowAll();

                disTimer.Tick -= new EventHandler(disTimer_Tick);
                main.Visibility = Visibility.Visible;  //显示主窗口
                StreamWriter sw2 = new StreamWriter(@"1.txt", false, Encoding.UTF8);
                sw2.WriteLine(main.coin);
                sw2.WriteLine(main.lock1);
                sw2.WriteLine(main.lock2);
                sw2.Close();
                main.MoneyL.Content = "当前金币数："+main.coin;

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
