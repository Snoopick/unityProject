using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerDriverInput : MonoBehaviour
{
    [SerializeField] private Transform m_steerWheelTransform;
    [SerializeField] [Range(0, 180f)] private float m_maxStreetAngle = 90;
    [SerializeField] [Range(0f, 1f)] private float m_steerAcceleration = 0.25f;

    private float steerAxis;

    public float SteerAxis
    {
        get => steerAxis;
        set => steerAxis = Mathf.Lerp(steerAxis, value, m_steerAcceleration);
    }

    private Vector2 startSteerWheelPoint;
    private Camera mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        startSteerWheelPoint = mainCamera.WorldToScreenPoint(m_steerWheelTransform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float angle = Vector2.Angle(Vector2.up, (Vector2) Input.mousePosition - startSteerWheelPoint);

            angle /= m_maxStreetAngle;
            angle = Mathf.Clamp01(angle);

            if (Input.mousePosition.x > startSteerWheelPoint.x)
            {
                angle *= -1f;
            }

            SteerAxis = angle;
        }
        else
        {
            SteerAxis = 0;
        }
        
        m_steerWheelTransform.localEulerAngles = new Vector3(0f,0f,steerAxis * m_maxStreetAngle);
    }
}
