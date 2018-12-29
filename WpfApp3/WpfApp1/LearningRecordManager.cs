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
        public List<Proc> pm;
        private int coin;
        public LearningRecordManager() { }
        public LearningRecordManager(int rn, DateTime ld, TimeSpan lt, bool ls, List<Proc>pm, int c)
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
            this.pm = pm;
            this.coin = c;
        }
        //用于数据绑定
        public event PropertyChangedEventHandler PropertyChanged;
        List<Proc> Pm
        {
            get
            {
                return pm;
            }
            set
            {
                pm = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Proc"));
                }
            }
        }
        public int Coin
        {
            get
            {
                return coin;
            }
            set
            {
                coin = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Coin"));
                }
            }
        }
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
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("RecordNo"));
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
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("LearnDate"));
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
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("LearnStateResult"));
                }
            }
        }
        public bool LearnState
        {
            get
            {
                return learnState;
            }
            set
            {
                learnState = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("LearnState"));
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
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("LearnTime"));
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
