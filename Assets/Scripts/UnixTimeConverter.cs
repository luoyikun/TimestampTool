using System;
using System.Collections.Generic;

public class UnixTimeConverter
{
    // Unix ʱ�����ʼʱ�� (1970-01-01 00:00:00 UTC)
    private static readonly DateTime UnixEpoch =
        new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// ������ʱ��ת��Ϊ Unix ʱ������룩
    /// </summary>
    /// <param name="localTime">����ʱ��</param>
    /// <returns>Unix ʱ������룩</returns>
    public static long ToUnixTimestamp(DateTime localTime)
    {
        // ������ʱ��ת��Ϊ UTC ʱ��
        DateTime utcTime = localTime.ToUniversalTime();

        // ����ʱ��ת��Ϊ��
        TimeSpan timeDiff = utcTime - UnixEpoch;
        return (long)timeDiff.TotalSeconds;
    }

    /// <summary>
    /// ������ʱ��ת��Ϊ Unix ʱ��������룩
    /// </summary>
    public static long ToUnixTimestampMilliseconds(DateTime localTime)
    {
        DateTime utcTime = localTime.ToUniversalTime();
        TimeSpan timeDiff = utcTime - UnixEpoch;
        return (long)timeDiff.TotalMilliseconds;
    }

    /// <summary>
    /// �� Unix ʱ������룩ת��Ϊ����ʱ��
    /// </summary>
    public static DateTime FromUnixTimestamp(long timestamp)
    {
        DateTime utcTime = UnixEpoch.AddSeconds(timestamp);
        return utcTime.ToLocalTime();
    }

    /// <summary>
    /// �� Unix ʱ��������룩ת��Ϊ����ʱ��
    /// </summary>
    public static DateTime FromUnixTimestampMilliseconds(long milliseconds)
    {
        DateTime utcTime = UnixEpoch.AddMilliseconds(milliseconds);
        return utcTime.ToLocalTime();
    }

    public static bool IsMillisecondsTimestamp(long timestamp)
    {
        // �ж��߼�
        if (timestamp > 1_000_000_000_000) // 13λ������
        {
            return true; // ���뼶
        }
        return false;
    }

    /// <summary>
    /// ������ת��Ϊ�졢Сʱ�����ӡ�����ַ�����ʾ
    /// </summary>
    /// <param name="totalSeconds">������</param>
    /// <param name="showZeroValues">�Ƿ���ʾ��ֵ��λ</param>
    /// <returns>��ʽ����ʱ���ַ���</returns>
    public static (long, long, long, long) ConvertSeconds(long totalSeconds)
    {
        if (totalSeconds < 0)
        {
            throw new ArgumentException("������������Ϊ����", nameof(totalSeconds));
        }

        // �������ʱ�䵥λ
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