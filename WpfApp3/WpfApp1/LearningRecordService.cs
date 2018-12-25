using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace WpfApp1
{
    //学习记录管理
    class LearningRecordService
    {
        public static Dictionary<int, LearningRecordManager> RecordDictionary = new Dictionary<int, LearningRecordManager>();
        public LearningRecordService()
        {

        }
        
        //展示所有的记录
        public static List<LearningRecordManager> ShowAll()
        {
            return RecordDictionary.Values.ToList();
        }
        public static List<LearningRecordManager> FindByLearnState(bool ls)
        {
            List<LearningRecordManager> findResult = new List<LearningRecordManager>();
            List<LearningRecordManager> text = RecordDictionary.Values.ToList();
            foreach (LearningRecordManager lrm in text)
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
            string pattern1 = @"^20[0-9]{2}$";
            string pattern2 = @"^20[0-9]{2}-[0-1][0-9]$";
            string pattern3 = @"^20[0-9]{2}-[0-1][0-9]-[0-3][0-9]$";
            Match match1 = Regex.Match(dateString, pattern1);
            Match match2 = Regex.Match(dateString, pattern2);
            Match match3 = Regex.Match(dateString, pattern3);
            //将输入的字符串拆分为年月日并进行判断
            if (match3.Success)
            {
                string[] result = dateString.Split(new char[] { '-' });
                int.TryParse(result[0], out int year);
                int.TryParse(result[1], out int month);
                int.TryParse(result[2], out int day);
                var find = RecordDictionary.Values.Where(record => (record.LearnDate.Year == year) && (record.LearnDate.Month == month) && (record.LearnDate.Day == day));
                return find.ToList();
            }
            else if(match2.Success)
            {
                string[] result = dateString.Split(new char[] { '-' });
                int.TryParse(result[0], out int year);
                int.TryParse(result[1], out int month);
                var find = RecordDictionary.Values.Where(record => (record.LearnDate.Year == year) && (record.LearnDate.Month == month));
                return find.ToList();
            }
            else if(match1.Success)
            {
                int year = int.Parse(dateString);
                var find = RecordDictionary.Values.Where(record => (record.LearnDate.Year == year));
                return find.ToList();
            }
            else
            {
                return null; 
            }
        }
        //设置序列化文件名
        public static string ExportDocumentName()
        {
            DateTime nameTime = DateTime.Now;           
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
        public static List<LearningRecordManager> Import(string path)
        {
            if (Path.GetExtension(path) != ".xml")
            {
                throw new Exception("It's not a xml file.");
            }
            XmlSerializer xs = new XmlSerializer(typeof(List<LearningRecordManager>));
            List<LearningRecordManager> recordResult = new List<LearningRecordManager>();
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                List<LearningRecordManager> temp = (List<LearningRecordManager>)xs.Deserialize(fs);
                foreach(var record in temp)
                {
                    if (!RecordDictionary.Keys.Contains(record.RecordNo + temp.Count))
                    {
                        RecordDictionary[record.RecordNo + temp.Count] = record;
                        recordResult.Add(record);   
                    }
                }
            }
            return recordResult;
        }

    }
}
