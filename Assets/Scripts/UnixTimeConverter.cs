using System;
using System.Collections.Generic;

public class UnixTimeConverter
{
    // Unix 时间戳起始时间 (1970-01-01 00:00:00 UTC)
    private static readonly DateTime UnixEpoch =
        new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// 将本地时间转换为 Unix 时间戳（秒）
    /// </summary>
    /// <param name="localTime">本地时间</param>
    /// <returns>Unix 时间戳（秒）</returns>
    public static long ToUnixTimestamp(DateTime localTime)
    {
        // 将本地时间转换为 UTC 时间
        DateTime utcTime = localTime.ToUniversalTime();

        // 计算时间差并转换为秒
        TimeSpan timeDiff = utcTime - UnixEpoch;
        return (long)timeDiff.TotalSeconds;
    }

    /// <summary>
    /// 将本地时间转换为 Unix 时间戳（毫秒）
    /// </summary>
    public static long ToUnixTimestampMilliseconds(DateTime localTime)
    {
        DateTime utcTime = localTime.ToUniversalTime();
        TimeSpan timeDiff = utcTime - UnixEpoch;
        return (long)timeDiff.TotalMilliseconds;
    }

    /// <summary>
    /// 将 Unix 时间戳（秒）转换为本地时间
    /// </summary>
    public static DateTime FromUnixTimestamp(long timestamp)
    {
        DateTime utcTime = UnixEpoch.AddSeconds(timestamp);
        return utcTime.ToLocalTime();
    }

    /// <summary>
    /// 将 Unix 时间戳（毫秒）转换为本地时间
    /// </summary>
    public static DateTime FromUnixTimestampMilliseconds(long milliseconds)
    {
        DateTime utcTime = UnixEpoch.AddMilliseconds(milliseconds);
        return utcTime.ToLocalTime();
    }

    public static bool IsMillisecondsTimestamp(long timestamp)
    {
        // 判断逻辑
        if (timestamp > 1_000_000_000_000) // 13位或以上
        {
            return true; // 毫秒级
        }
        return false;
    }

    /// <summary>
    /// 将秒数转换为天、小时、分钟、秒的字符串表示
    /// </summary>
    /// <param name="totalSeconds">总秒数</param>
    /// <param name="showZeroValues">是否显示零值单位</param>
    /// <returns>格式化的时间字符串</returns>
    public static (long, long, long, long) ConvertSeconds(long totalSeconds)
    {
        if (totalSeconds < 0)
        {
            throw new ArgumentException("输入秒数不能为负数", nameof(totalSeconds));
        }

        // 计算各个时间单位
        long seconds = totalSeconds;
        long days = seconds / 86400;
        seconds %= 86400;
        long hours = seconds / 3600;
        seconds %= 3600;
        long minutes = seconds / 60;
        seconds %= 60;


        return (days,hours,minutes,seconds);
    }
}