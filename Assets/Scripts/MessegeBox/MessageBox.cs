using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public delegate void Confim();
    public class MessageBox
    {
        static GameObject Messagebox;
        static int Result = -1;
        public static Confim confim;
        public static string TitleStr;
        public static string ContentStr;
        public static void Show(string content)
        {
            if (null!=Messagebox) {
                Cancel();
            }        
            TitleStr = "Command Args";
            ContentStr = content;
            Messagebox = (GameObject)Resources.Load("Prefab/background");
            Messagebox = GameObject.Instantiate(Messagebox, GameObject.Find("Canvas_Camera9").transform) as GameObject;
           // Messagebox.transform.localScale = new Vector3(1, 1, 1);
            Messagebox.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            Messagebox.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            Messagebox.GetComponent<RectTransform>().offsetMax = Vector2.zero;           
        }      
        public static void Sure()
        {
            if (confim != null)
            {
                confim();
                GameObject.Destroy(Messagebox);
                TitleStr = "标题";
                ContentStr = null;               
            }
        }
        public static void Cancel()
        {
            Result = 2;
            GameObject.Destroy(Messagebox);
            TitleStr = "标题";
            ContentStr = null;           
        }
    }
}
