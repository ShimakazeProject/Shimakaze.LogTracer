using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Shimakaze.LogTracer
{
    /// <summary>
    /// 控制台扩展
    /// </summary>
    /// <remarks>
    /// 24位色控制台支持
    /// </remarks>
    public static class ConsoleEx
    {
        /// <summary>
        /// 为字符串染色
        /// </summary>
        /// <param name="message">字符串</param>
        /// <param name="foreground">前景色</param>
        /// <param name="background">背景色</param>
        /// <param name="resetColorWhenWriteEnd">字符串结束清除颜色</param>
        /// <returns>染色后的字符串</returns>
        public static string ColorString(string message, uint? foreground = null, uint? background = null, bool resetColorWhenWriteEnd = true)
            => message.ToColorString(foreground, background, resetColorWhenWriteEnd);

        /// <summary>
        /// 清除控制台颜色
        /// </summary>
        public static void ResetColor()
            => Write("".ToColorString(null, null, true));

        /// <summary>
        /// 设置控制台颜色
        /// </summary>
        /// <param name="foreground">前景色(0xRRGGBB)</param>
        /// <param name="background">背景色(0xRRGGBB)</param>
        public static void SetColor(uint? foreground = null, uint? background = null)
            => Write("".ToColorString(foreground, background, false));

        /// <summary>
        /// 在控制台输出消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="foreground">前景色(0xRRGGBB)</param>
        /// <param name="background">背景色(0xRRGGBB)</param>
        /// <param name="resetColorWhenWriteEnd">输出完成后重置</param>
        public static void Write(string? message, uint? foreground = null, uint? background = null, bool resetColorWhenWriteEnd = true)
            => Console.Write(message.ToColorString(foreground, background, resetColorWhenWriteEnd));

        /// <summary>
        /// 调用<see cref="object.ToString()"/>方法并在控制台输出
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="foreground">前景色(0xRRGGBB)</param>
        /// <param name="background">背景色(0xRRGGBB)</param>
        /// <param name="resetColorWhenWriteEnd">输出完成后重置</param>
        public static void Write(object? obj, uint? foreground = null, uint? background = null, bool resetColorWhenWriteEnd = true)
            => Write(obj?.ToString(), foreground, background, resetColorWhenWriteEnd);

        /// <summary>
        /// 在控制台换行
        /// </summary>
        public static void WriteLine() => Console.WriteLine();

        /// <summary>
        /// 在控制台输出消息后换行
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="foreground">前景色(0xRRGGBB)</param>
        /// <param name="background">背景色(0xRRGGBB)</param>
        /// <param name="resetColorWhenWriteEnd">输出完成后重置</param>
        public static void WriteLine(string? message, uint? foreground = null, uint? background = null, bool resetColorWhenWriteEnd = true)
            => Console.WriteLine(message.ToColorString(foreground, background, resetColorWhenWriteEnd));

        /// <summary>
        /// 调用<see cref="object.ToString()"/>方法并在控制台输出后换行
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="foreground">前景色(0xRRGGBB)</param>
        /// <param name="background">背景色(0xRRGGBB)</param>
        /// <param name="resetColorWhenWriteEnd">输出完成后重置</param>
        public static void WriteLine(object? obj, uint? foreground = null, uint? background = null, bool resetColorWhenWriteEnd = true)
            => WriteLine(obj?.ToString(), foreground, background, resetColorWhenWriteEnd);

        /// <summary>
        /// 字符串染色逻辑代码
        /// </summary>
        internal static string ToColorString(this string? message, uint? foreground = null, uint? background = null, bool resetColorWhenWriteEnd = true)
        {
            StringBuilder sb = new((message?.Length ?? 0) + 42);

            if (foreground is not null)
                sb.Append($"\u001b[38;2;{foreground & 0x00FF0000};{foreground & 0x0000FF00};{foreground & 0x000000FF}m");

            if (background is not null)
                sb.Append($"\u001b[48;2;{background & 0x00FF0000};{background & 0x0000FF00};{background & 0x000000FF}m");

            sb.Append(message);

            if (resetColorWhenWriteEnd)
                sb.Append("\u001b[0m");

            return sb.ToString();
        }



        //private const int STD_OUTPUT_HANDLE = -11;
        //private const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;
        //private const uint DISABLE_NEWLINE_AUTO_RETURN = 0x0008;

        //[DllImport("kernel32.dll")]
        //private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        //[DllImport("kernel32.dll")]
        //private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        //[DllImport("kernel32.dll", SetLastError = true)]
        //private static extern IntPtr GetStdHandle(int nStdHandle);

        //[DllImport("kernel32.dll")]
        //public static extern uint GetLastError();

        //public static void WindowsConsoleInit()
        //{
        //    var iStdOut = GetStdHandle(STD_OUTPUT_HANDLE);
        //    if (!GetConsoleMode(iStdOut, out uint outConsoleMode))
        //    {
        //        Console.WriteLine("failed to get output console mode");
        //        return;
        //    }

        //    outConsoleMode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING | DISABLE_NEWLINE_AUTO_RETURN;
        //    if (!SetConsoleMode(iStdOut, outConsoleMode))
        //    {
        //        Console.WriteLine($"failed to set output console mode, error code: {GetLastError()}");
        //        return;
        //    }
        //}
    }
}