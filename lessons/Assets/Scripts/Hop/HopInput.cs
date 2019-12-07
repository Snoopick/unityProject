using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopInput : MonoBehaviour
{
    private float strafe;
    public float Strafe => strafe;
    private float screenCenter;

    // Start is called before the first frame update
    void Start()
    {
        screenCenter = Screen.width * .5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Input.GetMouseButton(0))
        {
            return;
        }

        var mousePosition = Input.mousePosition.x;
        if (mousePosition > screenCenter)
        {
            strafe = (mousePosition - screenCenter) / screenCenter;
        } 
        else
        {
            strafe = 1f - mousePosition / screenCenter;
            strafe *= -1f;
        }
    }
}
