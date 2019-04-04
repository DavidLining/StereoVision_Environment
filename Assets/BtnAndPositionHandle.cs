using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System.Threading;
using Common;
using System.IO;

public class BtnAndPositionHandle : MonoBehaviour
{
    private GameObject btnA, btnB, btnC, btnD, btnE, btnF, btnG, Btn_del, btn_auto, txtA, txtB, txtInputX, txtInputY, txtInputZ, txtInputVelocity, txtRotationX, txtRotationY, txtRotationZ, abc, txtInputCommand, txtInputFOV, DropDown_cameraA, DropDown_cameraB, DropDown_point, Camera1, Camera2, Camera3, Camera4, Camera5, Camera6, Camera7, Camera8, Camera, sphere1;
    static string curr_dir = System.Environment.CurrentDirectory;
    static string exe_filepath = curr_dir + "//executable//main.exe";
    private Vector3[] cameraPositions;
    private float positionX;
    private float positionY;
    private float positionZ;
    private float velocity;
    private bool isRun = false;
    static Process exeprocess = new Process();

    // Use this for initialization
    void Start()
    {
        btnA = GameObject.Find("BtnA");
        btnB = GameObject.Find("BtnB");
        btnC = GameObject.Find("BtnC");
        btnD = GameObject.Find("BtnD");
        btnE = GameObject.Find("BtnE");
        btnF = GameObject.Find("BtnF");
        btnG = GameObject.Find("BtnG");
        Btn_del = GameObject.Find("Btn_del");
        btn_auto = GameObject.Find("Btn_auto");
        txtA = GameObject.Find("TxtA");
        txtB = GameObject.Find("TxtB");
        txtInputX = GameObject.Find("PositionX");
        txtInputY = GameObject.Find("PositionY");
        txtInputZ = GameObject.Find("PositionZ");
        txtInputVelocity = GameObject.Find("Velocity");
        txtRotationX = GameObject.Find("RotationX");
        txtRotationY = GameObject.Find("RotationY");
        txtRotationZ = GameObject.Find("RotationZ");
        txtInputCommand = GameObject.Find("Input_command");
        txtInputFOV = GameObject.Find("Input_fov");
        DropDown_cameraA = GameObject.Find("DropDown_cameraA");
        DropDown_cameraB = GameObject.Find("DropDown_cameraB");
        DropDown_point = GameObject.Find("DropDown_point");
        abc = GameObject.Find("abc");
        sphere1 = GameObject.Find("Sphere1");
        Camera1 = GameObject.Find("Camera1");
        Camera2 = GameObject.Find("Camera2");
        Camera3 = GameObject.Find("Camera3");
        Camera4 = GameObject.Find("Camera4");
        Camera5 = GameObject.Find("Camera5");
        Camera6 = GameObject.Find("Camera6");
        Camera7 = GameObject.Find("Camera7");
        Camera8 = GameObject.Find("Camera8");
        Camera = GameObject.Find("Camera");
        //添加事件监听
        btnA.GetComponent<Button>().onClick.AddListener(handleBtnA);
        btnB.GetComponent<Button>().onClick.AddListener(handleBtnB);
        btnC.GetComponent<Button>().onClick.AddListener(handleBtnC);
        btnD.GetComponent<Button>().onClick.AddListener(handleBtnD);
        btnE.GetComponent<Button>().onClick.AddListener(handleBtnE);
        btnF.GetComponent<Button>().onClick.AddListener(handleBtnF);
        btnG.GetComponent<Button>().onClick.AddListener(handleBtnG);
        Btn_del.GetComponent<Button>().onClick.AddListener(handleBtn_del);
        btn_auto.GetComponent<Button>().onClick.AddListener(handleBtn_auto);

        //txtInputX.GetComponent<InputField>().onValueChanged.AddListener(handlePositionX);
        //txtInputY.GetComponent<InputField>().onValueChanged.AddListener(handlePositionY);
        //txtInputZ.GetComponent<InputField>().onValueChanged.AddListener(handlePositionZ);
        //txtRotationX.GetComponent<InputField>().onValueChanged.AddListener(handleRedPositionX);
        //txtRotationY.GetComponent<InputField>().onValueChanged.AddListener(handleRedPositionY);
        //txtRotationZ.GetComponent<InputField>().onValueChanged.AddListener(handleRedPositionZ);
        txtInputFOV.GetComponent<InputField>().onValueChanged.AddListener(handlePositionFOV);
        ReadCameraConfig("camerasPosition.txt");
        InitCamarasPosition();
        //ShowABCPosition();
        //ShowABCRotation();

    }

    // Update is called once per frame
    void Update()
    {
        if (isRun)
        {
            abc.transform.position = Vector3.MoveTowards(abc.transform.position, new Vector3(positionX, positionY, positionZ), velocity);
            if (abc.transform.position == new Vector3(positionX, positionY, positionZ))
            {
                isRun = false;
            }
        }
    }

    private void handleBtnA()
    {
        string args = "-c";
        txtA.GetComponent<Text>().text = "Command args: " + args;
        //MessageBox.Show(args);
        StartProcess(exe_filepath, args);
    }


    public static void collect_data()
    {
        string args = "-c";
        bool is_show_msg = false;
        //MessageBox.Show(args);
        StartProcess(exe_filepath, args, is_show_msg);
    }

    private void handleBtnB()
    {
        string camera1 = DropDown_cameraA.GetComponent<Dropdown>().captionText.text;
        string camera2 = DropDown_cameraB.GetComponent<Dropdown>().captionText.text;
        string point = DropDown_point.GetComponent<Dropdown>().captionText.text;
        string args = "-p " + camera1 + " " + camera2 + " " + point + " False" + " False";
        txtA.GetComponent<Text>().text = "Command args: " + args;

        StartProcess(exe_filepath, args);
    }

    private void handleBtnC()
    {
        InputField input_field = txtInputCommand.GetComponent<InputField>();
        txtA.GetComponent<Text>().text = "Command args: " + input_field.text;
        StartProcess(exe_filepath, input_field.text);
    }

    private void handleBtnD()
    {
        if (null == txtInputX.GetComponent<InputField>().text || "" == txtInputX.GetComponent<InputField>().text)
        {
            positionX = abc.transform.position.x;
        }
        else
        {
            positionX = float.Parse(txtInputX.GetComponent<InputField>().text);
        }

        if (null == txtInputY.GetComponent<InputField>().text || "" == txtInputY.GetComponent<InputField>().text)
        {
            positionY = abc.transform.position.y;
        }
        else
        {
            positionY = float.Parse(txtInputY.GetComponent<InputField>().text);
        }

        if (null == txtInputZ.GetComponent<InputField>().text || "" == txtInputZ.GetComponent<InputField>().text)
        {
            positionZ = abc.transform.position.z;
        }
        else
        {
            positionZ = float.Parse(txtInputZ.GetComponent<InputField>().text);
        }

        if (null == txtInputVelocity.GetComponent<InputField>().text || "" == txtInputVelocity.GetComponent<InputField>().text)
        {
            velocity = 0;
        }
        else
        {
            velocity = float.Parse(txtInputVelocity.GetComponent<InputField>().text);
        }
        isRun = true;
        //abc.transform.position = new Vector3(positionX, positionY, positionZ);
        //abc.transform.position = Vector3.MoveTowards(abc.transform.position, new Vector3(positionX, positionY, positionZ), velocity * Time.deltaTime);
    }

    private void handleBtnE()
    {
        ShowABCPosition();
    }

    private void handleBtnF()
    {
        float rotationX;
        float rotationY;
        float rotationZ;
        if (null == txtRotationX.GetComponent<InputField>().text || "" == txtRotationX.GetComponent<InputField>().text)
        {
            rotationX = 0;
        }
        else
        {
            rotationX = float.Parse(txtRotationX.GetComponent<InputField>().text);
        }
        if (null == txtRotationY.GetComponent<InputField>().text || "" == txtRotationY.GetComponent<InputField>().text)
        {
            rotationY = 0;
        }
        else
        {
            rotationY = float.Parse(txtRotationY.GetComponent<InputField>().text);
        }
        if (null == txtRotationZ.GetComponent<InputField>().text || "" == txtRotationZ.GetComponent<InputField>().text)
        {
            rotationZ = 0;
        }
        else
        {
            rotationZ = float.Parse(txtRotationZ.GetComponent<InputField>().text);
        }
        abc.transform.Rotate(new Vector3(rotationX, rotationY, rotationZ), Space.World);
    }

    private void handleBtnG()
    {
        ShowABCRotation();
    }

    private void handlePositionFOV(string arg0)
    {
        if (null != arg0 && "" != arg0)
        {
            Camera1.GetComponent<Camera>().fieldOfView = float.Parse(arg0);
            Camera2.GetComponent<Camera>().fieldOfView = float.Parse(arg0);
            Camera3.GetComponent<Camera>().fieldOfView = float.Parse(arg0);
            Camera4.GetComponent<Camera>().fieldOfView = float.Parse(arg0);
            Camera5.GetComponent<Camera>().fieldOfView = float.Parse(arg0);
            Camera6.GetComponent<Camera>().fieldOfView = float.Parse(arg0);
            Camera7.GetComponent<Camera>().fieldOfView = float.Parse(arg0);
            Camera8.GetComponent<Camera>().fieldOfView = float.Parse(arg0);
            Camera.GetComponent<Camera>().fieldOfView = float.Parse(arg0);
        }
    }
    private void handleBtn_del()
    {
        string args = "-d all";
        txtA.GetComponent<Text>().text = "Command args: " + args;

        StartProcess(exe_filepath, args);
    }

    private void handleBtn_auto()
    {
        string point = DropDown_point.GetComponent<Dropdown>().captionText.text;
        string args = "-a " + point;
        txtA.GetComponent<Text>().text = "Command args: " + args;

        StartProcess(exe_filepath, args);
    }



    public void handlePositionX(string arg0)
    {
        if (null != arg0 && "" != arg0)
        {
            abc.transform.position = new Vector3(float.Parse(arg0), abc.transform.position.y, abc.transform.position.z);
        }
    }

    private void handlePositionY(string arg0)
    {
        if (null != arg0 && "" != arg0)
        {
            abc.transform.position = new Vector3(abc.transform.position.x, float.Parse(arg0), abc.transform.position.z);
        }
    }

    private void handlePositionZ(string arg0)
    {
        if (null != arg0 && "" != arg0)
        {
            abc.transform.position = new Vector3(abc.transform.position.x, abc.transform.position.y, float.Parse(arg0));
        }
    }

    public void handleRedPositionX(string arg0)
    {
        if (null != arg0 && "" != arg0)
        {
            sphere1.transform.position = new Vector3(float.Parse(arg0), sphere1.transform.position.y, sphere1.transform.position.z);
        }
    }

    private void handleRedPositionY(string arg0)
    {
        if (null != arg0 && "" != arg0)
        {
            sphere1.transform.position = new Vector3(sphere1.transform.position.x, float.Parse(arg0), sphere1.transform.position.z);
        }
    }

    private void handleRedPositionZ(string arg0)
    {
        if (null != arg0 && "" != arg0)
        {
            sphere1.transform.position = new Vector3(sphere1.transform.position.x, sphere1.transform.position.y, float.Parse(arg0));
        }
    }

    public static bool StartProcess(string _exePathName, string _exeArgus, bool is_show_msg=true)
    {
        try
        {
            string msg = "";

            ProcessStartInfo startInfo = new ProcessStartInfo(_exePathName, _exeArgus);
            exeprocess.StartInfo = startInfo;
            exeprocess.StartInfo.UseShellExecute = false;
            exeprocess.StartInfo.RedirectStandardOutput = true;
            SaveResult.set_save_status(false);
            if (exeprocess.Start())
            {
                msg = exeprocess.StandardOutput.ReadToEnd();
                exeprocess.Close();
                if(is_show_msg)
                {
                    MessageBox.Show(msg);
                }
                SaveResult.set_save_status(true);
                return true;
            }

        }
        catch (Exception ex)
        {
            UnityEngine.Debug.Log("出错原因：" + ex.Message);
            SaveResult.set_save_status(true);
            return false;
        }
        SaveResult.set_save_status(true);
        return false;
    }

    private void ReadCameraConfig(string filePath)
    {
        StreamReader sr = new StreamReader(filePath);
        string all = sr.ReadToEnd();
        string[] s = all.Split('\n');
        cameraPositions = new Vector3[s.Length];
        for (int i = 0; i < s.Length; i++)
        {
            string[] ss = s[i].Split(':');
            string[] position = ss[1].Split(',');
            cameraPositions[i] = new Vector3(float.Parse(position[0]), float.Parse(position[1]), float.Parse(position[2]));
            //UnityEngine.Debug.Log(cameraPositions[i].ToString("F2"));
        }
        sr.Close();
    }

    private void InitCamarasPosition()
    {
        Camera1.GetComponent<Transform>().position = cameraPositions[0];
        Camera2.GetComponent<Transform>().position = cameraPositions[1];
        Camera3.GetComponent<Transform>().position = cameraPositions[2];
        Camera4.GetComponent<Transform>().position = cameraPositions[3];
        Camera5.GetComponent<Transform>().position = cameraPositions[4];
        Camera6.GetComponent<Transform>().position = cameraPositions[5];
        Camera7.GetComponent<Transform>().position = cameraPositions[6];
        Camera8.GetComponent<Transform>().position = cameraPositions[7];
        Camera.GetComponent<Transform>().position = cameraPositions[8];
    }

    private void ShowABCPosition()
    {

        txtInputX.GetComponent<InputField>().text = abc.transform.position.x.ToString("F8");
        txtInputY.GetComponent<InputField>().text = abc.transform.position.y.ToString("F8");
        txtInputZ.GetComponent<InputField>().text = abc.transform.position.z.ToString("F8");
    }

    private void ShowABCRotation()
    {
        //UnityEngine.Debug.LogWarning(abc.transform.eulerAngles.x+"  "+ abc.transform.eulerAngles.y+"  "+ abc.transform.eulerAngles.z);       
        if (abc.transform.eulerAngles.x >= 180)
        {
            txtRotationX.GetComponent<InputField>().text = (abc.transform.eulerAngles.x - 360).ToString("F8");
        }
        else {
            txtRotationX.GetComponent<InputField>().text = abc.transform.eulerAngles.x.ToString("F8");
        }
        if (abc.transform.eulerAngles.y > 180)
        {
            txtRotationY.GetComponent<InputField>().text = (abc.transform.eulerAngles.y - 360).ToString("F8");
        }
        else
        {
            txtRotationY.GetComponent<InputField>().text = abc.transform.eulerAngles.y.ToString("F8");
        }
        if (abc.transform.eulerAngles.z > 180)
        {
            txtRotationZ.GetComponent<InputField>().text = (abc.transform.eulerAngles.z - 360).ToString("F8");
        }
        else
        {
            txtRotationZ.GetComponent<InputField>().text = abc.transform.eulerAngles.z.ToString("F8");
        }       
    }
}
