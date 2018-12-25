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

        private List<LearningRecordManager> recordList = new List<LearningRecordManager>();//用于List的数据绑定
        private List<CategoryInfo> categoryList = new List<CategoryInfo>();//用于combobox的选项数据绑定
        private int nowNo;
        private bool first;

        //用于combobox的选择项
        public class CategoryInfo
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }

        public LearningRecordWindow(int nowNo, bool first, bool IsFinal, bool chaxun, String timeLength)
        {

            this.nowNo = nowNo;
            this.first = first;
            InitializeComponent();

            //combobox的选项
            categoryList.Add(new CategoryInfo { Name = "按照学习日期查询", Value = 2 });
            categoryList.Add(new CategoryInfo { Name = "按照学习记录序号查询", Value = 1 });
            categoryList.Add(new CategoryInfo { Name = "显示所有学习记录", Value = 0 });
            categoryList.Add(new CategoryInfo { Name = "显示学习成功的记录", Value = 3 });
            selectComboBox.ItemsSource = categoryList;
            selectComboBox.DisplayMemberPath = "Name";
            selectComboBox.SelectedValuePath = "Value";

            //数据初始化(问题：监控程序如何传值构造）            
            string path = @"F:\Study\ThirdTerm\Tide\WpfApp3\WpfApp1\bin\Debug";//指定目录
            NewFileInfo nf = NewFileInfo.GetLastFile(path, ".xml");

            if (nf == null)//如果没有xml文件，先生成一个空的xml
            {
                String fileName = LearningRecordService.ExportDocumentName();
                LearningRecordService.Export(fileName);
                NewFileInfo nf2 = NewFileInfo.GetLastFile(path, ".xml");
                string newPath = nf2.FileName;
                recordList = LearningRecordService.Import(newPath, first);
            }
            else//如果已有则直接导入
            {
                string newPath = nf.FileName;
                recordList = LearningRecordService.Import(newPath, first);
            }


            if (IsFinal == true && chaxun == false)  //如果完成学习
            {
                if (recordList.Count == 0)
                {
                    DateTime dt1 = DateTime.Now;
                    TimeSpan ts1 = TimeSpan.Parse(timeLength);
                    recordList.Add(new LearningRecordManager(nowNo, dt1, ts1, true));
                }
                else
                {
                    nowNo = recordList[recordList.Count - 1].recordNo + 1;
                    DateTime dt2 = DateTime.Now;
                    TimeSpan ts2 = TimeSpan.Parse(timeLength);
                    recordList.Add(new LearningRecordManager(nowNo, dt2, ts2, true));
                }

            }
            if (IsFinal == false && chaxun == false)   //代表没有查询，放弃本次学习。
            {
                if (recordList.Count == 0)
                {
                    DateTime dt1 = DateTime.Now;
                    TimeSpan ts1 = TimeSpan.Parse(timeLength);
                    recordList.Add(new LearningRecordManager(nowNo, dt1, ts1, false));
                }
                else
                {
                    nowNo = recordList[recordList.Count - 1].recordNo + 1;
                    DateTime dt2 = DateTime.Now;
                    TimeSpan ts2 = TimeSpan.Parse(timeLength);
                    recordList.Add(new LearningRecordManager(nowNo, dt2, ts2, false));
                }
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
                    break;
                case 2:
                    string datetime = textBox.Text;
                    RecordList.ItemsSource = LearningRecordService.FindByLearnDate(datetime);
                    break;
                case 3:
                    RecordList.ItemsSource = LearningRecordService.FindByLearnState(true);
                    break;

            }
        }
        //用于获取最新的xml文件
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
    }
    }