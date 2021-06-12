using System;
using System.IO;

namespace Shimakaze.LogTracer
{
    /// <summary>
    /// 日志输出
    /// </summary>
    public static class Logger
    {
        public static TextWriter LogOutput { get; set; } = Console.Error;

        public static void Debug(string msg) => WriteLine("DEBUG", msg);

        public static void Debug(object? msg) => WriteLine("DEBUG", msg?.ToString());

        public static void Detailed(string msg) => WriteLine("DETAILED", msg);

        public static void Detailed(object? msg) => WriteLine("DETAILED", msg?.ToString());

        public static void Error(string msg) => WriteLine("ERROR", msg);

        public static void Error(object? msg) => WriteLine("ERROR", msg?.ToString());

        public static void Fatal(string msg) => WriteLine("FATAL", msg);

        public static void Fatal(object? msg) => WriteLine("FATAL", msg?.ToString());

        public static void Info(string msg) => WriteLine("INFO", msg);

        public static void Info(object? msg) => WriteLine("INFO", msg?.ToString());

        public static void Trace(string msg) => WriteLine("TRACE", msg);

        public static void Trace(object? msg) => WriteLine("TRACE", msg?.ToString());

        public static void Warn(string msg) => WriteLine("WARN", msg);

        public static void Warn(object? msg) => WriteLine("WARN", msg?.ToString());

        private static uint GetColor(string rank) => rank.ToUpper() switch
        {
            "DETAILED" => 0x333333,
            "DEBUG" => 0x666666,
            "TRACE" => 0x999999,
            "INFO" => 0xFFFFFF,
            "WARN" => 0xFFFF00,
            "ERROR" => 0xFF0000,
            "FATAL" => 0x990000,
            _ => 0
        };

        private static void WriteLine(string rank, string? message)
        {
            LogOutput.WriteLine("{0}{1}:{2}",
                $"{DateTime.Now:O}".ToColorString(0x99),
                rank.ToUpper().PadLeft(10).ToColorString(GetColor(rank)),
                message);
        }
    }
}
