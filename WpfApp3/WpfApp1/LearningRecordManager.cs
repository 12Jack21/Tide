using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfApp1
{
    [Serializable]
    public class LearningRecordManager : INotifyPropertyChanged
    {
        public int recordNo;
        private DateTime learnDate;
        private TimeSpan learnTime;
        private bool learnState;//判定学习是否完成
        private string learnStateResult;//根据学习状态的判定打印相应的信息
        public LearningRecordManager() { }
        public LearningRecordManager(int rn, DateTime ld, TimeSpan lt, bool ls)
        {
            recordNo = rn;
            learnTime = lt;
            learnDate = ld;
            learnState = ls;
            if (ls)
            {
                learnStateResult = "已完成";
            }
            else
            {
                learnStateResult = "未完成";
            }
            //monitor = m;
        }
        //用于数据绑定
        public event PropertyChangedEventHandler PropertyChanged;
        public int RecordNo
        {
            get
            {
                return recordNo;
            }
            set
            {
                recordNo = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Age"));
                }
            }
        }
        public DateTime LearnDate
        {
            get { return learnDate; }
            set
            {
                learnDate = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Age"));
                }
            }
        }
        public string LearnStateResult
        {
            get { return learnStateResult; }
            set
            {
                learnStateResult = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Age"));
                }
            }
        }
        public bool LearnState
        {
            get
            {
                return learnState;
            }
        }
        [XmlIgnore]
        public TimeSpan LearnTime
        {
            get { return learnTime; }
            set
            {
                learnTime = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Age"));
                }
            }
        }
        [XmlElement("learnTime")]
        public long learnTimeSpan
        {
            get { return learnTime.Ticks; }
            set { learnTime = new TimeSpan(value); }
        }
        
    }

}
