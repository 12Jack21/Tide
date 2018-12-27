//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;

//namespace WpfApp1
//{
//    /// <summary>
//    /// DataAnalysisWindow.xaml 的交互逻辑
//    /// </summary>
//    public partial class DataAnalysisWindow : Window, INotifyPropertyChanged
//    {
//        private LearningRecordManager nowRecord;
//        private List<string> procName;//所有程序的名字
//        private List<int> procTime;//所有程序的其他数据
//        private int no;
//        //用来绑定前端的NoLabel内容
//        public event PropertyChangedEventHandler PropertyChanged;
//        public int No
//        {
//            get
//            {
//                return no;
//            }
//            set
//            {
//                no = value;
//                if (PropertyChanged != null)
//                {
//                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("No"));
//                }
//            }
//        }
//        public DataAnalysisWindow(int no, List<string> procName, List<int> procTime, List<LearningRecordManager> recordList)
//        {
//            this.procName = procName;
//            this.procTime = procTime;
//            this.no = no;
//            //从已有的记录里面，根据点击的数据行号（已经与每一行数据的recordNo关联）获取相应的record（如果不用，可以删了）
//            nowRecord = recordList[no-1];

//            InitializeComponent();
//            this.NoLabel.DataContext = No;

//        }



//    }
//}
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

        private List<string> strListx = new List<string>();//饼状图的数据
        private List<string> strListy = new List<string>();
        public DataAnalysisWindow(List<string> threads, List<int> times)
        {
            InitializeComponent();
            int n;
            string s;
            int temp = 0;
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
            CreateChartPie("线程使用说明", strListx, strListy);
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

