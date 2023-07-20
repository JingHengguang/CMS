using System;
using System.IO;
using Cms.Backend.Db;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;


namespace Cms.Backend.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //获取当前项目所在的绝对路径
            string filePath = Directory.GetCurrentDirectory();
            string currentDate = "Audit"+DateTime.Now.ToString("yyyyMMddHHmmss").Trim().Replace(" ","-");;
        //    string currentDate="abc";
            /// <summary>
            /// 日志的保存路径
            /// </summary>
            /// <returns></returns>

            if (!string.IsNullOrWhiteSpace(filePath) && !string.IsNullOrWhiteSpace(currentDate))
            {
                // 生成Log对象
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Information()    //输出的最小日志等级
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .WriteTo.Console()             //输出到控制台
                     //, rollingInterval: RollingInterval.Minute输出日志文件,并按日期分割
                    .WriteTo.File(filePath + "/logs/" + currentDate + ".log")  
                    .CreateLogger();
            }



            try
            {
                Log.Information(currentDate+"[程序启动中...]");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();            //重置全局设置
            }

            // --------------------------------------------------------------------------------
            // CreateHostBuilder(args).Build().Run();
        }




        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            // -------------------
            .ConfigureLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();     //去掉默认日志提供程序
                    loggingBuilder.AddSerilog();         //引用Serilog
                })
                // ------------
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
