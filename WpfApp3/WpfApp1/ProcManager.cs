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
        List<String> gameList; //要监控的程序列表
        string lastGameName; //最后统计的监控程序名
        int gameTime; //使用某程序的时间
        int controlTime; //使用某程序的限制时间

        [DllImport("user32.dll")]//获取窗体的进程ID
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]//获取最前端的窗体
        private static extern IntPtr GetForegroundWindow();

        //使其能够序列化
        public ProcManager()
        {
            Procs = new List<Proc>();
        }

        //设置约束时间
        public void setControlTime(int time)
        {
            controlTime = time;
        }
        public void init()
        {
            gameList = new List<string>();
        }

        public string getGameName()
        {
            return lastGameName;
        }

        public int getTime()
        {
            return gameTime;
        }

        //添加需要监控的游戏名称
        public void addGameName(string name)
        {
            if (!gameList.Contains(name))
                gameList.Add(name);
        }

        //对指定软件的监控,计算连续使用某个软件的时长
        void gameMonitor(string name)
        {
            if (!gameList.Contains(name))//是否在监控列表中
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
                if (lastGameName == name)//连续使用时的时间累加
                    gameTime++;
                else //若上个指定软件换成了另一个则重置软件名和时间
                {
                    lastGameName = name;
                    gameTime = 1;
                }
            }
        }

        //检查游戏时长是否超时 false为超时
        public bool checkGameTime()
        {
            //连续使用时间大于约束时间
            if (gameTime >= controlTime)
                return false;
            return true;
        }

        public void clearGameTime()//重置使用时间
        {
            gameTime = 0;
        }

        //监控的一个周期
        public void onceMonitor()
        {
            //Thread.Sleep(1000);
            IntPtr hWnd = GetForegroundWindow();
            uint procId = 0;
            GetWindowThreadProcessId(hWnd, out procId);
            var process = Process.GetProcessById((int)procId);
            try
            {
                //排除本软件名的影响
                if (process.ProcessName == "WpfApp1")
                    return;
                var query = Procs.Where(p => p.Name == process.ProcessName.ToLower())
                    .SingleOrDefault();
                if (query != null)//若进程List里已经存在此应用，则时间累加
                {
                    query.Time++;
                    //判断指定软件
                    gameMonitor(query.Name.ToLower());
                }
                else //若进程List中没有此应用，则创建新的Proc类加入进程List中
                {
                    Proc proc = new Proc();
                    proc.Name = process.ProcessName.ToLower();
                    proc.Time = 1;
                    Procs.Add(proc);
                }
                //调试时使用
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
