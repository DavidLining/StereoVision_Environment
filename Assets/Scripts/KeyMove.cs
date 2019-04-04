using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMove : MonoBehaviour
{
    private float Speed = 3.0f;
    public static bool isMove = false;
    // Use this for initialization
    void Start()
    {

    }

    public static bool get_isMove_status()
    {
        return isMove;
    }

    // Update is called once per frame
    void Update()
    {
        keyMove();
        if (isMove)
        {
            MoveRandom();                   
        }
    }

    private void keyMove()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveForward();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveBack();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }
        if (Input.GetKey(KeyCode.PageUp))
        {
            MoveUp();
        }
        if (Input.GetKey(KeyCode.PageDown))
        {
            MoveDown();
        }
        if (Input.GetKey(KeyCode.Q))
        {
            Lrotate();
        }
        if (Input.GetKey(KeyCode.E))
        {
            Rrotate();
        }
        if (Input.GetKey(KeyCode.R))
        {
            isMove = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            isMove = false;        
            //transform.position = new Vector3(0, 0, 0);
        }
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);
    }
    void MoveBack()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * -Speed);
    }
    void MoveLeft()
    {
        transform.Translate(Vector3.left * Time.deltaTime * Speed);
    }
    void MoveRight()
    {
        transform.Translate(Vector3.left * Time.deltaTime * -Speed);
    }
    void MoveUp()
    {
        transform.Translate(Vector3.up * Time.deltaTime * Speed);
    }
    void MoveDown()
    {
        transform.Translate(Vector3.up * Time.deltaTime * -Speed);
    }

    public void MoveRandom()
    {
        transform.Rotate(new Vector3(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360)));
        transform.position = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(0.0f, 8.0f), Random.Range(-4.0f, 4.0f));
    }
    void Lrotate()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * Speed);
    }
    void Rrotate()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * -Speed);
    }

    void RandomRotate()
    {
        transform.Rotate(new Vector3(Random.Range(-360,360), Random.Range(-360, 360), Random.Range(-360, 360)));
    }
}
