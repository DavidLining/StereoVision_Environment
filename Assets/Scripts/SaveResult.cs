using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveResult : MonoBehaviour {
    public Text tex1, tex2, tex3, tex4, tex5, tex6, tex7, tex8;
    FileReadWrite frw;
    public static bool if_save = true;
    string result = "";
    // Use this for initialization
    void Start () {
        frw = new FileReadWrite();
    }
	
	// Update is called once per frame
	void Update () {
        result = "camera1:\n" + tex1.text + "\n\n" + "camera2:\n" + tex2.text + "\n\n" + "camera3:\n" + tex3.text + "\n\n" + "camera4:\n" + tex4.text + "\n\n" + "camera5:\n" + tex5.text + "\n\n" + "camera6:\n" + tex6.text + "\n\n" + "camera7:\n" + tex7.text + "\n\n" + "camera8:\n" + tex8.text + "\n\n";
        string curr_dir = System.Environment.CurrentDirectory;
        if (if_save)
        {
            if (File.Exists(curr_dir + "\\" + "Coordinates_last.txt"))
            {
                string pre_data = frw.ReadFileAll(curr_dir, "Coordinates_last.txt");
                frw.CreateFile(curr_dir, "Coordinates_sec_last.txt", pre_data);
            }
            if (File.Exists(curr_dir + "\\" + "Coordinates.txt"))
            {
                string pre_data = frw.ReadFileAll(curr_dir, "Coordinates.txt");
                frw.CreateFile(curr_dir, "Coordinates_last.txt", pre_data);

            }
            frw.CreateFile(curr_dir, "Coordinates.txt", result);
        }
 //       if (KeyMove.get_isMove_status())
 //       {
//            BtnAndPositionHandle.collect_data();
//        }
    }
    public static void set_save_status(bool status)
    {
        if_save = status;
    }
}
