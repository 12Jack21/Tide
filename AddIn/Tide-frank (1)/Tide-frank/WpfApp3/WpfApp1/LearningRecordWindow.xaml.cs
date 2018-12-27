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
using Microsoft.Win32;
using System.IO;


namespace WpfApp1
{
    /// <summary>
    /// LearningRecordWindow.xaml 的交互逻辑
    /// </summary>

    public partial class LearningRecordWindow : Window
    {

        private List<LearningRecordManager> recordList;//用于List的数据绑定
        private List<CategoryInfo> categoryList = new List<CategoryInfo>();//用于combobox的选项数据绑定
        private int nowNo;
        private List<List<Proc>> procResult = new List<List<Proc>>();
        //用于生成相应的分析报告窗口
        private DataAnalysisWindow daw;
        //用于combobox的选择项
        public class CategoryInfo
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }

        public void init()
        {
            //combobox的选项
            categoryList.Add(new CategoryInfo { Name = "按照学习日期查询", Value = 2 });
            categoryList.Add(new CategoryInfo { Name = "按照学习记录序号查询", Value = 1 });
            categoryList.Add(new CategoryInfo { Name = "显示所有学习记录", Value = 0 });
            categoryList.Add(new CategoryInfo { Name = "显示学习成功的记录", Value = 3 });
            selectComboBox.ItemsSource = categoryList;
            selectComboBox.DisplayMemberPath = "Name";
            selectComboBox.SelectedValuePath = "Value";
            //数据初始化            
            string path = @"..\..\bin\Debug\";//指定目录
            NewFileInfo nf = NewFileInfo.GetLastFile(path, ".xml");

            if (nf == null)//如果没有xml文件，先生成一个空的xml
            {
                String fileName = LearningRecordService.ExportDocumentName();
                LearningRecordService.Export(fileName);
                NewFileInfo nf2 = NewFileInfo.GetLastFile(path, ".xml");
                string newPath = nf2.FileName;
                recordList = LearningRecordService.Import(newPath);
                
            }
            else//如果已有则直接导入
            {
                string newPath = nf.FileName;
                recordList = LearningRecordService.Import(newPath);
                
            }
        }

        public LearningRecordWindow()
        {
            InitializeComponent();
            init();
            RecordList.ItemsSource = recordList;

            //传递给learningRecordService的数据（dictionary转化为list）           
            LearningRecordService.RecordDictionary = recordList.ToDictionary(key => key.recordNo, value => value);
        }

        public LearningRecordWindow(int nowNo, bool IsFinal,String timeLength, List<Proc>pr)
        {

            this.nowNo = nowNo;
            
            InitializeComponent();
            
            init();
            
            if (IsFinal == true)  //如果完成学习
            {
                if (recordList.Count == 0)
                {
                    DateTime dt1 = DateTime.Now;
                    TimeSpan ts1 = TimeSpan.Parse(timeLength);
                    
                    recordList.Add(new LearningRecordManager(nowNo, dt1, ts1, true, pr));
                }
                else
                {
                    nowNo = recordList[recordList.Count - 1].recordNo + 1;
                    DateTime dt2 = DateTime.Now;
                    TimeSpan ts2 = TimeSpan.Parse(timeLength);
                    
                    recordList.Add(new LearningRecordManager(nowNo, dt2, ts2, true, pr));
                }

            }
            if (IsFinal == false)   //代表没有查询，放弃本次学习。
            {
                if (recordList.Count == 0)
                {
                    DateTime dt1 = DateTime.Now;
                    TimeSpan ts1 = TimeSpan.Parse(timeLength);
                    
                    recordList.Add(new LearningRecordManager(nowNo, dt1, ts1, false, pr));
                }
                else
                {
                    nowNo = recordList[recordList.Count - 1].recordNo + 1;
                    DateTime dt2 = DateTime.Now;
                    TimeSpan ts2 = TimeSpan.Parse(timeLength);
                    
                    recordList.Add(new LearningRecordManager(nowNo, dt2, ts2, false, pr));
                }
            }
            foreach (var n in recordList)
            {
                procResult.Add(n.pm);
            }
            
            RecordList.ItemsSource = recordList;

            //传递给learningRecordService的数据（dictionary转化为list）           
            LearningRecordService.RecordDictionary = recordList.ToDictionary(key => key.recordNo, value => value);
            //保存数据
            String filename = LearningRecordService.ExportDocumentName();
            LearningRecordService.Export(filename);
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {

            switch (selectComboBox.SelectedValue)
            {
                case 0:
                    RecordList.ItemsSource = recordList;
                    break;
                case 1:
                    int id = 0;
                    int.TryParse(textBox.Text, out id);
                    RecordList.ItemsSource = LearningRecordService.FindByRecordNo(id);
                    if (LearningRecordService.FindByRecordNo(id) == null)
                    {
                        MessageBox.Show("无匹配的学习记录，请重新搜索。");
                    }
                    RecordList.ItemsSource = recordList;
                    break;
                case 2:
                    string datetime = textBox.Text;
                    RecordList.ItemsSource = LearningRecordService.FindByLearnDate(datetime);
                    if(LearningRecordService.FindByLearnDate(datetime) == null)
                    {
                        MessageBox.Show("无匹配的学习记录，请重新搜索。");
                    }
                    RecordList.ItemsSource = recordList;
                    break;
                case 3:
                    RecordList.ItemsSource = LearningRecordService.FindByLearnState(true);
                    break;
            }
        }
        //用于获取最新的xml文件（本用来实现不同时间的xml文件各不相同）
        public class NewFileInfo
        {
            public string FileName;
            public DateTime FileCreateTime;
            public static NewFileInfo GetLastFile(string d, string n)
            {
                List<NewFileInfo> fileList = new List<NewFileInfo>();
                DirectoryInfo di = new DirectoryInfo(d);
                foreach (FileInfo nfi in di.GetFiles())
                {
                    if (nfi.Extension == n)
                    {
                        fileList.Add(new NewFileInfo()
                        {
                            FileName = nfi.FullName,
                            FileCreateTime = nfi.CreationTime

                        });
                    }
                }
                var query = from x in fileList orderby x.FileCreateTime select x;
                return query.LastOrDefault();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        //双击数据行时生成相应的学习分析报告
        private void OnListViewItemDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //用于确定选定的数据行数，将其传递给下一窗口
            int selectedNo = this.RecordList.SelectedIndex+1;
            List<string> procName = new List<string>();
            List<int> procTime = new List<int>();
            foreach (var m in recordList[this.RecordList.SelectedIndex].pm)
            {
                procName.Add(m.Name);
                procTime.Add(m.Time);
            }
            daw = new DataAnalysisWindow( procName, procTime);
            daw.ShowDialog();
        }

        private void ShowHelp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("1.日期查询支持查询某一年的记录，如2018，也支持类似2018-08-14或者2018-08来查询对应的天和月份记录;" +
                "2.查询输入的数据须符合要求");
        }
    }
}