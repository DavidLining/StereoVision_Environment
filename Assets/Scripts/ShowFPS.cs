using System.IO;
using UnityEngine;
public class ShowFPS : MonoBehaviour
{
    //更新的时间间隔
    public float UpdateInterval = 0.5F;
    //最后的时间间隔
    private float _lastInterval;
    //帧[中间变量 辅助]
    private int _frames = 0;
    //当前的帧率
    private float _fps;

    private int _targetFrames;

    void Awake()
    {
        ReadFPSConfig("FPSConfig.txt");
        Application.targetFrameRate = _targetFrames;
    }

    void Start()
    {
        //Application.targetFrameRate=60;
        UpdateInterval = Time.realtimeSinceStartup;
        _frames = 0;
    }
    void Update()
    {
        ++_frames;
        if (Time.realtimeSinceStartup > _lastInterval + UpdateInterval)
        {
            _fps = _frames / (Time.realtimeSinceStartup - _lastInterval);
            _frames = 0;
            _lastInterval = Time.realtimeSinceStartup;
        }
        //Debug.LogWarning(_fps);
    }

    private void ReadFPSConfig(string filePath)
    {
        StreamReader sr = new StreamReader(filePath);
        string all = sr.ReadToEnd();
        string[] s = all.Split('\n');       
        for (int i = 0; i < s.Length; i++)
        {
            string[] ss = s[i].Split(':');
            if ("FPS" == ss[0]) {
                _targetFrames =int.Parse(ss[1]);
            }                     
        }
        sr.Close();
    }
}
