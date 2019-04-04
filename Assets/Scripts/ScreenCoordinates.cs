using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenCoordinates : MonoBehaviour
{
    public GameObject sphere1, sphere2, sphere3, sphere4, sphere5, sphere6, btnH, txtResolutionX, txtResolutionY;
    public Camera cam;
    Vector3 sv1Position, sv2Position, sv4Position, sv6Position;
    Vector3 pv1Position, pv2Position, pv4Position, pv6Position;
    Text myText;
    float pw, ph;
    float x_limit, y_limit;
    // Use this for initialization
    void Start()
    {
        myText = GetComponent<Text>();
        btnH = GameObject.Find("BtnH");
        txtResolutionX = GameObject.Find("ResolutionX");
        txtResolutionY = GameObject.Find("ResolutionY");
        btnH.GetComponent<Button>().onClick.AddListener(handleBtnH);
        //获取屏幕分辨率
        getScreenResolution();      
    }

    // Update is called once per frame
    void Update()
    {
        getScreenResolution();
        //Camera的空间坐标
        Vector3 camVector = cam.gameObject.transform.position;

        sv1Position = new Vector3(sphere1.gameObject.transform.position.x, sphere1.gameObject.transform.position.y, sphere1.gameObject.transform.position.z);
        sv2Position = new Vector3(sphere2.gameObject.transform.position.x, sphere2.gameObject.transform.position.y, sphere2.gameObject.transform.position.z);
        sv4Position = new Vector3(sphere4.gameObject.transform.position.x, sphere4.gameObject.transform.position.y, sphere4.gameObject.transform.position.z);
        sv6Position = new Vector3(sphere6.gameObject.transform.position.x, sphere6.gameObject.transform.position.y, sphere6.gameObject.transform.position.z);

        //sv1Position = sphere1.gameObject.transform.position.x;
        //sv2Position = sphere2.gameObject.transform.position;
        ////sv3Position = sphere3.gameObject.transform.position;
        //sv4Position = sphere4.gameObject.transform.position;
        ////sv5Position = sphere5.gameObject.transform.position;
        //sv6Position = sphere6.gameObject.transform.position;

        x_limit = Screen.width * 0.25f;
        y_limit = Screen.height * 0.3f;

        //空间坐标转换为屏幕坐标
        pv1Position = limit_2d_position((cam.WorldToScreenPoint(sv1Position).x - pw), (cam.WorldToScreenPoint(sv1Position).y - ph), x_limit, y_limit);
        pv2Position = limit_2d_position((cam.WorldToScreenPoint(sv2Position).x - pw), (cam.WorldToScreenPoint(sv2Position).y - ph), x_limit, y_limit);
        pv4Position = limit_2d_position((cam.WorldToScreenPoint(sv4Position).x - pw), (cam.WorldToScreenPoint(sv4Position).y - ph), x_limit, y_limit);
        pv6Position = limit_2d_position((cam.WorldToScreenPoint(sv6Position).x - pw), (cam.WorldToScreenPoint(sv6Position).y - ph), x_limit, y_limit);
        //pv1Position = cam.WorldToScreenPoint(sv1Position);
        //pv2Position = cam.WorldToScreenPoint(sv2Position);
        //pv4Position = cam.WorldToScreenPoint(sv4Position);
        //pv6Position = cam.WorldToScreenPoint(sv6Position);


        string cv = "\nCamera:" + camVector + "\n";
        string sv = "3D:" + "\n" + "Red:" + sv1Position.ToString("F8") + "\n" + "Green:" + sv2Position.ToString("F8") + "\n" + "Yellow:" + sv4Position.ToString("F8") + "\n" + "Blue:" + sv6Position.ToString("F8") + "\n\n";
        string pv = "2D:" + "\n" + "Red:" + pv1Position.ToString("F8") + "\n" + "Green:" + pv2Position.ToString("F8") + "\n" + "Yellow:" + pv4Position.ToString("F8") + "\n" + "Blue:" + pv6Position.ToString("F8") + "\n";
        string screeninfo = "Screen width and height: " + Screen.width + ", " + Screen.height;
        myText.text = screeninfo + cv + sv + pv;
    }

    private Vector3 limit_2d_position(float x, float y, float x_max, float y_max)
    {
        Vector3 position;
        if ((x >= x_max) || (y >= y_max)|| (x < 0) || (y < 0))
        {
            position = new Vector3(x, y, 1.000f); // if z axis not equal to zero, it means that the point has exceeded the area 

        }
        else
        {
            position = new Vector3(x, y, 0.000f);
        }
        return position;
    }

    private void getScreenResolution()
    {
        //Debug.Log("martin:width" + Screen.width + "martin:height" + Screen.height);

        if ("Camera1" == cam.name)
        {
            pw = Screen.width * 0.2f;
            ph = Screen.height * 0.7f;
        }
        else if ("Camera2" == cam.name)
        {
            pw = Screen.width * 0.47f;
            ph = Screen.height * 0.7f;
        }
        else if ("Camera3" == cam.name)
        {
            pw = Screen.width * 0.74f;
            ph = Screen.height * 0.7f;
        }
        else if ("Camera4" == cam.name)
        {
            pw = Screen.width * 0.2f;
            ph = Screen.height * 0.35f;
        }
        else if ("Camera5" == cam.name)
        {
            pw = Screen.width * 0.74f;
            ph = Screen.height * 0.35f;
        }
        else if ("Camera6" == cam.name)
        {
            pw = Screen.width * 0.2f;
            ph = 0.0f;
        }
        else if ("Camera7" == cam.name)
        {
            pw = Screen.width * 0.47f;
            ph = 0.0f;
        }
        else if ("Camera8" == cam.name)
        {
            pw = Screen.width * 0.74f;
            ph = 0.0f;
        }
        else
        {
            pw = 0.0f;
            ph = 0.0f;
        }

        //Debug.Log("martin:Camera name :" + cam.name+"pw:"+pw+";ph:"+ph);
    }

    private void handleBtnH()
    {        
        if (null != txtResolutionX.GetComponent<InputField>().text && "" != txtResolutionX.GetComponent<InputField>().text && null != txtResolutionY.GetComponent<InputField>().text && "" != txtResolutionY.GetComponent<InputField>().text)
        {           
            Screen.SetResolution(int.Parse(txtResolutionX.GetComponent<InputField>().text), int.Parse(txtResolutionY.GetComponent<InputField>().text), false);                 
        }      
    }
}
