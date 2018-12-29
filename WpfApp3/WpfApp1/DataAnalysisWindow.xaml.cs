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
using System.Windows.Shapes;
using Visifire.Charts;

namespace WpfApp1
{
    /// <summary>
    /// MyBar.xaml 的交互逻辑
    /// </summary>
    public partial class DataAnalysisWindow : Window
    {
        string HeadLine = "柱状图";
        List<string> Threads = new List<string>();
        List<int> Times = new List<int>();
        DateTime dateTime;
        TimeSpan timeSpan;
        bool finish;


        private List<string> strListx = new List<string>();//饼状图的数据
        private List<string> strListy = new List<string>();
        public DataAnalysisWindow(List<string> threads, List<int> times, DateTime dateTime, TimeSpan timeSpan, bool finish)
        {
            InitializeComponent();
            this.dateTime = dateTime;
            this.timeSpan = timeSpan;
            this.finish = finish;
            //int n;
            //string s;
            //int temp = 0;
            for (int i = 0; i < threads.Count; i++)
            {
                //n = threads[i].IndexOf("(");
                //temp = threads[i].Length - n - 2 < 10 ? threads[i].Length - n - 2 : 10;
                //s = threads[i].Substring(n + 1, temp);
                //Threads.Add(s);
                Threads.Add(threads[i]);
                Times.Add(times[i]);
                strListx.Add(threads[i]);
                strListy.Add(times[i].ToString());
            }

        }

        private void ButColumn_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("柱状图");

            Simon.Children.Clear();

            CreateChartColumn(HeadLine, Threads, Times);
        }

        //柱状图的函数
        private void CreateChartColumn(string name, List<string> valuex, List<int> valuey)
        {
            //创建一个图标
            Chart chart = new Chart();

            //设置图标的宽度和高度
            chart.Width = 580;
            chart.Height = 380;
            chart.Margin = new Thickness(100, 5, 10, 5);  //上下左右
            //是否启用打印和保持图片
            chart.ToolBarEnabled = false;

            //设置图标的属性
            chart.ScrollingEnabled = false;//是否启用或禁用滚动
            chart.View3D = true;//3D效果显示

            //创建一个标题的对象
            Title title = new Title();

            //设置标题的名称
            title.Text = name;
            title.Padding = new Thickness(0, 10, 5, 0);
            title.MouseLeftButtonDown += new MouseButtonEventHandler(title_MouseLeftButtonDown);

            //向图标添加标题
            chart.Titles.Add(title);

            Axis yAxis = new Axis();
            //设置图标中Y轴的最小值永远为0           
            yAxis.AxisMinimum = 0;
            //设置图表中Y轴的后缀          
            yAxis.Suffix = "秒";
            chart.AxesY.Add(yAxis);

            // 创建一个新的数据线。               
            DataSeries dataSeries = new DataSeries();

            // 设置数据线的格式
            dataSeries.RenderAs = RenderAs.StackedColumn;//柱状Stacked


            // 设置数据点              
            MyDataPoint dataPoint;
            for (int i = 0; i < valuex.Count; i++)
            {
                // 创建一个数据点的实例。                   
                dataPoint = new MyDataPoint();
                // 设置X轴点                    
                dataPoint.AxisXLabel = valuex[i];
                //设置Y轴点                   
                dataPoint.YValue = valuey[i];
                //添加一个点击事件        
                //dataPoint.MouseLeftButtonDown += new MouseButtonEventHandler(dataPoint_MouseLeftButtonDown);
                //添加数据点                   
                dataSeries.DataPoints.Add(dataPoint);
            }

            // 添加数据线到数据序列。                
            chart.Series.Add(dataSeries);

            //将生产的图表增加到Grid，然后通过Grid添加到上层Grid.         

            Grid gr = new Grid();

            //Simon.Children.Add(gr);
            //Grid gr = new Grid();
            Button button = new Button();
            button.Content = "jvhgh";
            button.Padding = new Thickness(0, 20, 25, 0);
            chart.Href = "";
            chart.HrefTarget = 0;
            gr.Children.Add(chart);

            Simon.Children.Add(gr);



        }

        private void title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ButPie_Click(object sender, RoutedEventArgs e)
        {
            Simon.Children.Clear();
            CreateChartPie("进程使用说明", strListx, strListy);
        }

        #region 饼状图
        public void CreateChartPie(string name, List<string> valuex, List<string> valuey)
        {
            //创建一个图标
            Chart chart = new Chart();

            //设置图标的宽度和高度
            chart.Width = 580;
            chart.Height = 380;
            chart.Margin = new Thickness(100, 5, 10, 5);
            //是否启用打印和保持图片
            chart.ToolBarEnabled = false;

            //设置图标的属性
            chart.ScrollingEnabled = false;//是否启用或禁用滚动
            chart.View3D = true;//3D效果显示

            //创建一个标题的对象
            Title title = new Title();

            //设置标题的名称
            title.Text = name;
            title.Padding = new Thickness(0, 10, 5, 0);

            //向图标添加标题
            chart.Titles.Add(title);


            // 创建一个新的数据线。               
            DataSeries dataSeries = new DataSeries();

            // 设置数据线的格式
            dataSeries.RenderAs = RenderAs.Pie;//柱状Stacked

            double sum = 0;
            for (int i = 0; i < strListy.Count; i++)
            {
                sum = sum + double.Parse(valuey[i]);
            }
            // 设置数据点              
            DataPoint dataPoint;
            for (int i = 0; i < valuex.Count; i++)
            {
                // 创建一个数据点的实例。                   
                dataPoint = new DataPoint();
                // 设置X轴点                    
                dataPoint.AxisXLabel = valuex[i];

                dataPoint.LegendText = "##" + valuex[i];
                //设置Y轴点
                double data = double.Parse(valuey[i]) / sum;
                dataPoint.YValue = data;
                //添加一个点击事件        
                dataPoint.MouseLeftButtonDown += new MouseButtonEventHandler(dataPoint_MouseLeftButtonDown);
                //添加数据点                   
                dataSeries.DataPoints.Add(dataPoint);
            }

            // 添加数据线到数据序列。                
            chart.Series.Add(dataSeries);

            //将生产的图表增加到Grid，然后通过Grid添加到上层Grid.           
            Grid gr = new Grid();
            gr.Children.Add(chart);
            Simon.Children.Add(gr);
        }

        private void dataPoint_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

        private void ButSpline_Click(object sender, RoutedEventArgs e)
        {
            Simon.Children.Clear();

            Ana();
        }

        private void Create(string headLine, List<string> threads, List<int> times)
        {

        }

        private void Ana()
        {
            //Threads.Add(threads[i]);
            //Times.Add(times[i]);

            int AllTime = Times.Sum();   //其他软件的总时长
            int OtherWork = Threads.Count;     //其他软件的数量
            int second = (int)(this.timeSpan).TotalSeconds;
            string _1stimulate = Stimulate(AllTime, OtherWork, second);            //一个关于学习报告的string

            string tim = new MyTime(AllTime).ToString();
            string s1 = "     在" + dateTime.ToString() + "的学习过程当中，您总计使用了" + OtherWork + "个其他类型的软件，累计时长达" + tim + "," + _1stimulate + "\n";
            //s1就是第一段
            string s2 = "     " + S2(Threads, Times) + "\n";
            string s3 = "     " + S3();
            TextBlock textBlock = new TextBlock();
            textBlock.Width = 580;
            textBlock.Height = 380;
            textBlock.Margin = new Thickness(130, 50, 90, 5);  //左上下右
            textBlock.FontSize = 15;
            textBlock.TextWrapping = (TextWrapping)2;
            textBlock.Text = s1 + s2 + s3;

            Grid gr = new Grid();
            gr.Children.Add(textBlock);

            Simon.Children.Add(gr);



        }
        private string S3()
        {
            string d1 = "针对您这次的学习情况，我们给出了以下的建议:";
            string d2 = "";
            double rate = (double)(Times.Sum()) / timeSpan.TotalSeconds;
            if (rate < 0.05)
            {
                return d1 + "你已经可以很好的控制自己学习的状态了，再接再厉吧，不要懈怠呦";
            }
            else if (rate < 0.2)
            {
                string d3 = "你在这次的学习过程中还不够专注，会时常打开一些期其他的东西。";
                string d4 = "这次专注中你较多的使用了" + LongSoft(25);
                string d5 = "不过，Tide相信你会越来越好的。";
                return d3 + d4 + d5;
            }
            else if (rate < 0.5)
            {
                string d6 = "好气呀，你怎么能像高战立一样呢";
                string d7 = "让我们来看看你都使用了哪些软件吧，其中" + LongSoft(18) + "这些软件的用时有点多呀。";
                string d8 = "";
            }
            return "";
        }
        private string LongSoft(int a)
        {
            List<int> l = new List<int>();
            double temp = (double)(Times.Sum()) / a;
            for (int i = 0; i < Threads.Count; i++)
            {
                if (Times[i] > temp)
                {
                    l.Add(i);
                }
            }
            string t = "";
            for (int i = 0; i < l.Count; i++)
            {
                t += Threads[l[i]] + ",";
            }
            return t;
        }
        private string S2(List<string> threads, List<int> times)
        {
            string d1 = "在本次学习过程中，您使用时长最长的软件是" + threads[Max(times)] + ",时间是" + times[Max(times)]
                + "秒;您使用时长最短的软件是" + threads[min(times)] + ",时间是" + times[min(times)] + "秒。";
            string temp = "";
            for (int i = 0; i < threads.Count; i++)
            {
                if ((i == Max(times)) || (i == min(times)))
                {
                    continue;
                }
                else
                {
                    temp += threads[i] + ",";
                }
            }
            string LastOfd2 = "";
            double rate = (double)(times.Sum()) / timeSpan.TotalSeconds;
            if (rate > 0.75)
            {
                LastOfd2 = "你使用这些软件的时间太长了,应当减少些";
            }
            else if (rate > 0.4)
            {
                LastOfd2 = "这些软件有些影响你的平时歇息工作了，可以适当删除一些";
            }
            else if (rate > 0.2)
            {
                LastOfd2 = "你这次表现还行，还可以做的更好幼";
            }
            else if (rate > 0.07)
            {
                LastOfd2 = "你的表现棒极了，继续保持呦";
            }
            else
            {
                LastOfd2 = "你简直太厉害，可以这么好的控制自己";
            }
            string d2 = "除此之后，你还使用了" + temp + "这些软件。总体来说呢，" + LastOfd2;
            return d1 + d2;
        }
        private string Stimulate(int time, int task, int second)
        {
            if (!this.finish)
            {
                return "您未完成本次学习。";
            }
            if (time * 2 > second)
            {
                if (task == 1)
                {
                    return "您在本次学习过程中大部分时间在使用" + this.Threads[0] + "。";
                }
                else if (task < 5)
                {
                    return "您在本次专注中没有完全集中注意力，使用了较多的其他软件。";
                }
                else if (task < 9)
                {
                    return "您本次学习的效率并不高，大多数时间用来浏览其他软件了。";
                }
                else if (task > 8)
                {
                    return "这种情况应该只是玩玩这个软件。";
                }
                else
                {
                    return "";
                }
            }
            else if (time * 3 > second)
            {
                if (task == 1)
                {
                    return "您在本次学习过程中使用" + this.Threads[0] + "来调节了学习过程，但是你用时稍微有点多。";
                }
                else if (task < 5)
                {
                    return "如果没有繁华的吵闹，或许您可以学习更多的知识。";
                }
                else if (task > 4)
                {
                    return "你电脑上影响专注地东西有点多呦。";
                }
                else
                {
                    return "";
                }

            }
            else if (time * 5 > second)
            {
                if (task == 1)
                {
                    return "您在本次学习过程中使用" + this.Threads[0] + "来调节了学习过程。";
                }
                else if (task < 6)
                {
                    return "其实，专注是可以成为一种习惯的";
                }
                else if (task > 5)
                {
                    return "好可惜呀，再努力一点就可以学到更多的知识了。";
                }
                else
                {
                    return "";
                }

            }
            else if (time * 7 > second)
            {
                if (task == 1)
                {
                    return "您在本次学习过程中使用" + this.Threads[0] + "来略微放松了一下。";
                }
                else if (task < 6)
                {
                    return "专注时候稍微放松一下，也是可以提高后续效率的呦。";
                }
                else if (task > 5)
                {
                    return "或许你感到了疲惫，但是付出终会有回报的";
                }
                else
                {
                    return "";
                }
            }

            else if (time * 9 > second)
            {
                if (task < 3)
                {
                    string temp = "";
                    for (int i = 0; i < Threads.Count; i++)
                    {
                        temp += Threads[i] + ",";
                    }
                    return "您在本次学习过程中使用" + temp + "来放松了一下。";
                }
                if (task > 2)
                {
                    return "学习之余，适当的划水，只是为了更好地学习。";
                }
                else
                {
                    return "";
                }
            }
            else
            {
                if (time < 3)                   //小于三秒就认为没有玩过
                {
                    return "太棒了，骚年，你已经可以完全专注自我了";
                }
                else
                {
                    return "你可以很好的控制自己，加油";
                }
            }



        }
        private int Max(List<int> times)
        {
            int max = 0;
            for (int i = 1; i < times.Count; i++)
            {
                if (times[i] > times[max])
                {
                    max = i;
                }
            }
            return max;
        }
        private int min(List<int> times)
        {
            int min = 0;
            for (int i = 1; i < times.Count; i++)
            {
                if (times[i] < times[min])
                {
                    min = i;
                }
            }
            return min;
        }
        class MyTimeSpan
        {
            private int time;
            public MyTimeSpan(TimeSpan timeSpan)
            {

            }
        }
        class MyTime
        {
            private int Seconds { set; get; }
            private int Minutes { set; get; }
            private int Hours { set; get; }
            public MyTime(int seconds)
            {
                this.Hours = seconds / 60;
                this.Hours = (seconds % 60) / 60;
                this.Seconds = ((seconds % 60) % 60);
            }
            public override string ToString()
            {
                //string s = this.Hours + ":" + this.Minutes + ":" + this.Seconds;
                return this.Hours + ":" + this.Minutes + ":" + this.Seconds;
            }
        }
        class MyDataPoint : DataPoint         //这个类用来处理面板信息
        {
            public override string TextParser(string unParsed)
            {
                string parent = base.TextParser(unParsed);
                int n = parent.IndexOf(",");
                int m = parent.IndexOf("秒");
                if ((n == -1) || (m == -1) || (n >= m))
                {
                    return parent;
                }
                int time = int.Parse(parent.Substring(n + 1, m - n - 1));
                string thread = parent.Substring(0, n);
                string my = TextPrompt(thread, time);
                return my;
            }
            private string TextPrompt(string s, int t)
            {
                MyTime myTime = new MyTime(t);
                string time = myTime.ToString();
                string retur = "您在上次学习过程中使用" + s + "软件的时间为" + time;
                return retur;
            }
        }
        class Analyze
        {
            static public List<string> thr = new List<string>();
            static public List<int> tim = new List<int>();
            public Analyze(List<string> threads, List<int> times)
            {
                for (int i = 0; i < threads.Count; i++)
                {
                    thr.Add(threads[i]);
                    tim.Add(times[i]);
                }
            }
        }
    }
}

