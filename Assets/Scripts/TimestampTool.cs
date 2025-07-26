using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimestampTool : MonoBehaviour
{
    //��ǰʱ���
    public TextMeshProUGUI m_txtCurUnix;
    public Button m_btnCurUnix;

    //ʱ���ת����
    public TMP_InputField m_inputUnix;
    public Button m_btnUnix;
    public TextMeshProUGUI m_textUnix;

    //����תʱ���
    public TMP_InputField m_inputDate;
    public Button m_btnDate;
    public TextMeshProUGUI m_textDate;

    //ʱ���
    public TMP_Dropdown m_dropTimeDiff;
    public TMP_InputField m_inputTimeDiff;
    public Button m_btnTimeDiff;
    public TextMeshProUGUI m_textTimeDiff;

    // Start is called before the first frame update
    void Start()
    {
        m_btnCurUnix.onClick.AddListener(BtnConverterCurUnix);
        m_btnUnix.onClick.AddListener(OnBtnUnix2Date);
        m_btnDate.onClick.AddListener(OnBtnDate2Unix);
        m_btnTimeDiff.onClick.AddListener (OnBtnTimeDiff);
    }

    void OnBtnTimeDiff()
    {
        string sDate = m_inputTimeDiff.text;
        if (long.TryParse(sDate, out long timeDiff) == false)
        {
            return;
        }
        if (m_dropTimeDiff.value == (int)EnSecondType.MilliSecond)
        {
            //����תΪ��
            timeDiff = timeDiff / 1000;
        }

        (long day, long hour,long minute,long second )= UnixTimeConverter.ConvertSeconds(timeDiff);
        string sOut = $"{day}��{hour}ʱ{minute}��{second}��";
        m_textTimeDiff.text = sOut;
        GUIUtility.systemCopyBuffer = sOut;
    }

    void OnBtnDate2Unix()
    {
        string sDate = m_inputDate.text;
        string[] arrDate = sDate.Split('.');
        if (arrDate.Length != 6)
        {
            return;
        }

        if (!(int.TryParse(arrDate[0], out int year) && int.TryParse(arrDate[1], out int month) && int.TryParse(arrDate[2], out int day)
            && int.TryParse(arrDate[3], out int hour) && int.TryParse(arrDate[4], out int minute) && int.TryParse(arrDate[5], out int second) 
            ))
        {
            return;
        }
        DateTime now = new DateTime(year,month,day,hour,minute,second);
        long timestampSeconds = 0;
        // ת��Ϊ Unix ʱ������룩
        timestampSeconds = UnixTimeConverter.ToUnixTimestamp(now);
        m_textDate.text = timestampSeconds.ToString();
        GUIUtility.systemCopyBuffer = timestampSeconds.ToString();

    }
    void OnBtnUnix2Date()
    {
        string sTimestamp = m_inputUnix.text;
        if (long.TryParse(sTimestamp, out long unix) == false)
        {
            return;
        }
        DateTime dateTime;
        if (UnixTimeConverter.IsMillisecondsTimestamp(unix))
        {
            //����
            dateTime = UnixTimeConverter.FromUnixTimestampMilliseconds(unix);
        }
        else
        {
            dateTime = UnixTimeConverter.FromUnixTimestamp(unix);
        }
        m_textUnix.text = dateTime.ToString();
        GUIUtility.systemCopyBuffer = dateTime.ToString();
    }

    public void BtnConverterCurUnix()
    {
        // ��ǰ����ʱ��
        DateTime now = DateTime.Now;
        long timestampSeconds = 0;
        // ת��Ϊ Unix ʱ������룩
        timestampSeconds = UnixTimeConverter.ToUnixTimestamp(now);
        m_txtCurUnix.text = timestampSeconds.ToString();
        GUIUtility.systemCopyBuffer = timestampSeconds.ToString();
    }


}

public enum EnSecondType
{ 
    Sceond,
    MilliSecond,
}
