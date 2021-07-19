using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;

namespace KeqingNiuza.Update
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                var path = "https://cdn.jsdelivr.net/gh/Scighost/KeqingNiuza@cdn/KeqingNiuza/KeqingNiuza.zip";
                var client = new HttpClient();
                try
                {
                    var bytes = client.GetByteArrayAsync(path).Result;
                    File.WriteAllBytes("KeqingNiuza.zip", bytes);
                    Util.MessageBox(0, "已下载最新版本，文件名 KeqingNiuza.zip", "刻记牛杂店", 0);
                }
                catch (Exception ex)
                {
                    Util.MessageBox(0, ex.ToString(), "刻记牛杂店 - 无法下载最新版本", 0);
                }
                return;
            }

            if (args[0] == "KeqingNiuza.Update")
            {
                Thread.Sleep(500);
                var proes = Process.GetProcessesByName("KeqingNiuza");
                if (proes.Any())
                {
                    Array.ForEach(proes, x => x.Kill());
                }
                Log.OutputLog(LogType.Info, "Start update");
                Directory.CreateDirectory(".\\dll");
                var result = Util.MoveAllFile();
                if (File.Exists("update\\ShowUpdateLog"))
                {
                    File.Create("resource\\ShowUpdateLog");
                }
                Log.OutputLog(LogType.Info, "Update finished");
                try
                {
                    Directory.Delete(".\\update", true);
                }
                catch { }
                if (!result)
                {
                    Util.MessageBox(0, "更新过程中遇到错误，详见日志。\n运行update\\Update.exe直接下载最新版。", "刻记牛杂店", 0);
                }
                return;
            }

            if (args[0] == "KeqingNiuza.Update.ShowResult")
            {
                Thread.Sleep(500);
                var proes = Process.GetProcessesByName("KeqingNiuza");
                if (proes.Any())
                {
                    Array.ForEach(proes, x => x.Kill());
                }
                Log.OutputLog(LogType.Info, "Start update");
                Directory.CreateDirectory(".\\dll");
                var result = Util.MoveAllFile();
                if (File.Exists("update\\ShowUpdateLog"))
                {
                    File.Create("resource\\ShowUpdateLog");
                }
                Log.OutputLog(LogType.Info, "Update finished");
                try
                {
                    Directory.Delete(".\\update", true);
                }
                catch { }
                if (result)
                {
                    Util.MessageBox(0, "更新完成", "刻记牛杂店", 0);
                }
                else
                {
                    Util.MessageBox(0, "更新过程中遇到错误，详见日志。\n运行update\\Update.exe直接下载最新版。", "刻记牛杂店", 0);
                }
                return;
            }

            if (args[0] == "KeqingNiuza.Update.Restart")
            {
                Thread.Sleep(500);
                var proes = Process.GetProcessesByName("KeqingNiuza");
                if (proes.Any())
                {
                    Array.ForEach(proes, x => x.Kill());
                }
                Log.OutputLog(LogType.Info, "Start update");
                Directory.CreateDirectory(".\\dll");
                var result = Util.MoveAllFile();
                if (File.Exists("update\\ShowUpdateLog"))
                {
                    File.Create("resource\\ShowUpdateLog");
                }
                Log.OutputLog(LogType.Info, "Update finished");
                try
                {
                    Directory.Delete(".\\update", true);
                }
                catch { }
                if (result)
                {
                    Process.Start("KeqingNiuza.exe");
                }
                else
                {
                    Util.MessageBox(0, "更新过程中遇到错误，详见日志。\n运行update\\Update.exe直接下载最新版。", "刻记牛杂店", 0);
                }
                return;
            }
        }
    }
}
