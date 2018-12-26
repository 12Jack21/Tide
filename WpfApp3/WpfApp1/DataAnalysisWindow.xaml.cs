using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// DataAnalysisWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DataAnalysisWindow : Window, INotifyPropertyChanged
    {
        private LearningRecordManager nowRecord;
        private List<string> procName;//所有程序的名字
        private List<int> procTime;//所有程序的其他数据
        private int no;
        //用来绑定前端的NoLabel内容
        public event PropertyChangedEventHandler PropertyChanged;
        public int No
        {
            get
            {
                return no;
            }
            set
            {
                no = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("No"));
                }
            }
        }
        public DataAnalysisWindow(int no, List<string> procName, List<int> procTime, List<LearningRecordManager> recordList)
        {
            this.procName = procName;
            this.procTime = procTime;
            this.no = no;
            //从已有的记录里面，根据点击的数据行号（已经与每一行数据的recordNo关联）获取相应的record（如果不用，可以删了）
            nowRecord = recordList[no-1];
            
            InitializeComponent();
            this.NoLabel.DataContext = No;

        }
        


    }
}
