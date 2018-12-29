using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfApp1
{
    [Serializable]
    public class Proc
    {
        public String Name { get; set; }
        public int Time { get; set; }
    }
    [Serializable]
    public class ProcManager
    {
        List<Proc> Procs;
        List<String> gameList;
        string lastGameName;
        int gameTime;


        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        //使其能够序列化
        public ProcManager()
        {
            Procs = new List<Proc>();
        }

        public void init()
        {
            gameList = new List<string>();
        }

        //添加需要监控的游戏名称
        public void addGameName(string name)
        {
            if (!gameList.Contains(name))
                gameList.Add(name);
        }

        //对游戏的监控,计算连续使用某个游戏的时长
        void gameMonitor(string name)
        {
            //att: 避免大小写字母的区别，统一用小写字母的字符串形式进行比较
            if (!gameList.Contains(name))
            {
                gameTime = 0;
                return;
            }
            if (lastGameName == null)
            {
                lastGameName = name;
                gameTime = 1;
            }
            else
            {
                if (lastGameName == name)
                    gameTime++;
                else
                {
                    lastGameName = name;
                    gameTime = 1;
                }
            }
        }

        //检查游戏时长是否超时 false为超时
        public bool checkGameTime()
        {
            if (gameTime > 2)
                return false;
            return true;
        }

        public void clearGameTime()
        {
            gameTime = 0;
        }

        //监控的一个周期（持续一秒）
        public void onceMonitor()
        {
            //Thread.Sleep(1000);
            IntPtr hWnd = GetForegroundWindow();
            uint procId = 0;
            GetWindowThreadProcessId(hWnd, out procId);
            var process = Process.GetProcessById((int)procId);
            try
            {
                var query = Procs.Where(p => p.Name == process.ProcessName).SingleOrDefault();
                if (query != null)
                {
                    query.Time++;
                    //判断游戏
                    gameMonitor(query.Name);
                }
                else
                {
                    Proc proc = new Proc();
                    proc.Name = process.ProcessName;
                    proc.Time = 1;
                    Procs.Add(proc);
                }

                Console.WriteLine(process.ProcessName);


            }
            catch (Exception e)
            {
                Console.WriteLine("Get this process fail");
                Console.WriteLine(e.Message);
            }
        }

        //统计监控结束后的结果
        public List<Proc> countResult()
        {
            //pm.countResult();
            return Procs;
        }

        //测试用
        public static void Main0(string[] args)
        {
            ProcManager pm = new ProcManager();
            pm.init();
            pm.gameList.Add("TIM");
            int i = 0;
            while (i <= 20)
            {
                pm.onceMonitor();
                i++;
                if (!pm.checkGameTime())
                {
                    Console.WriteLine("Game above the required time!!!");
                    break;
                }
            }
            pm.countResult();
        }
    }
}
