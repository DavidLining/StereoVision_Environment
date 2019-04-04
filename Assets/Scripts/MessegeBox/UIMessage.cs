using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMessage : MonoBehaviour
{
    public Text Title;
    public Text Content;//这个是Content下的text
    public Button Sure;
    public Button Cancel;
    void Start()
    {
        Sure.onClick.AddListener(MessageBox.Sure);
        Cancel.onClick.AddListener(MessageBox.Cancel);
        Title.text = MessageBox.TitleStr;
        Content.text = MessageBox.ContentStr;
        MessageBox.confim += copyToClipboard;
    }

    private void copyToClipboard() {
        TextEditor te = new TextEditor();
        te.text = Content.text;
        te.SelectAll();
        te.Copy();
    }
}
