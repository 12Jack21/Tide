using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfApp1
{
    //学习记录管理
    class LearningRecordService
    {
        public static Dictionary<int, LearningRecordManager> RecordDictionary= new Dictionary<int, LearningRecordManager>();
        public LearningRecordService()
        {
            
        }
        //待定
        //public void AddLearningRecord(LearningRecordManager lr)
        //{
        //    if (RecordDictionary.ContainsKey(lr.RecordNo))
        //        throw new Exception("The record has exited.");
        //    RecordDictionary[lr.RecordNo] = lr;
        //}
        //展示所有的记录
        public static List<LearningRecordManager> ShowAll()
        {
            return RecordDictionary.Values.ToList();
        }
        public static List<LearningRecordManager> FindByLearnState(bool ls)
        {
            List<LearningRecordManager> findResult = new List<LearningRecordManager>();
            List<LearningRecordManager> text = RecordDictionary.Values.ToList();
            foreach(LearningRecordManager lrm in text)
            {
                if (lrm.LearnState == true)
                {
                    findResult.Add(lrm);
                }
            }

            return findResult;
        }
        //通过记录的序号查找
        public static List<LearningRecordManager> FindByRecordNo(int rn)
        {
            List<LearningRecordManager> findResult = new List<LearningRecordManager>();
            if (RecordDictionary.ContainsKey(rn))
            {
                findResult.Add(RecordDictionary[rn]);
                return findResult;
            }
            return null;
        }
        //通过记录的日期查找
        public static List<LearningRecordManager> FindByLearnDate(string dateString)
        {
            //将输入的字符串拆分为年月日并进行判断
            string[] result = dateString.Split(new char[] { '-' });
            int.TryParse(result[0], out int year);
            int.TryParse(result[1], out int month);
            int.TryParse(result[2], out int day);
            var find = RecordDictionary.Values.Where(record => (record.LearnDate.Year == year)&& (record.LearnDate.Month == month)&&(record.LearnDate.Day == day));
            return find.ToList();
        }
        //设置序列化文件名
        public static string ExportDocumentName()
        {
            DateTime nameTime = DateTime.Now;
            //string fileName = "records_" + nameTime.Year + "_" + nameTime.Month + "_" + nameTime.Day + "_" + nameTime.Hour + "_" + nameTime.Minute + ".xml";
            string fileName = "records" + ".xml";
            return fileName;
        }
        //序列化
        public static void Export(string fn)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<LearningRecordManager>));
            using (FileStream fs = new FileStream(fn, FileMode.Create))
            {
                xs.Serialize(fs, RecordDictionary.Values.ToList());
            }
        }
        //反序列化
        public static List<LearningRecordManager> Import(string path, bool isFirst)
        {
            if (Path.GetExtension(path) != ".xml")
            {
                throw new Exception("It's not a xml file.");
            }
            XmlSerializer xs = new XmlSerializer(typeof(List<LearningRecordManager>));
            List<LearningRecordManager> recordResult = new List<LearningRecordManager>();
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                List<LearningRecordManager> temp = (List<LearningRecordManager>)xs.Deserialize(fs);
                temp.ForEach(record =>
                {
                    if (isFirst == true)
                    {
                        if (!RecordDictionary.Keys.Contains(record.RecordNo))
                        {
                            RecordDictionary[record.RecordNo] = record;
                            recordResult.Add(record);
                        }
                    }
                    else
                    {
                        RecordDictionary[record.RecordNo] = record;
                        recordResult.Add(record);
                    }
                });
            }
            return recordResult;
        }
        
    }
}
