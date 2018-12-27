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
    public class LearningRecordManager:INotifyPropertyChanged
    {
        public int recordNo;
        private DateTime learnDate;
        private TimeSpan learnTime;
        private bool learnState;
        //public List<Monitor> monitor{get;set;}//添加监控的软件及使用时间
        public LearningRecordManager() { }
        public LearningRecordManager(int rn, DateTime ld, TimeSpan lt, bool ls/* List<Monitor> m*/)
        {
            recordNo = rn;
            learnTime = lt;
            learnDate = ld;
            learnState = ls;
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
                if(PropertyChanged != null)
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
        public bool LearnState
        {
            get { return learnState; }
            set
            {
                learnState = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Age"));
                }
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
        //public void AddMonitor(Monitor m)
        //{
        //    if (this.monitor.Contains(m))
        //    {
        //        throw new Exception("The monitor has existed, you don't need to add it.");
        //    }
        //    monitor.Add(m);
        // }
        
    }
    
}
