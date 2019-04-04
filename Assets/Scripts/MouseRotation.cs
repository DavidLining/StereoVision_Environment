using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotation : MonoBehaviour {

    public Camera cam;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 fwd = cam.transform.forward;
        fwd.Normalize();
        if (Input.GetMouseButton(0))
        {
            Vector3 vaxis = Vector3.Cross(fwd, Vector3.right);
            transform.Rotate(vaxis, -Input.GetAxis("Mouse X"), Space.World);
            Vector3 haxis = Vector3.Cross(fwd, Vector3.up);
            transform.Rotate(haxis, -Input.GetAxis("Mouse Y"), Space.World);
        }
	}
}
