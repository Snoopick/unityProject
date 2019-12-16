using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSnake_GameController : MonoBehaviour
{
    //
    public class CameraBounds
    {
        public float Left;
        public float Right;
        public float Up;
        public float Down;
    }

    [SerializeField] private Camera m_MainCamera;
    public Camera MainCamera => m_MainCamera;
    [SerializeField] private ColorSnake_Snake m_Snake;
    [SerializeField] private ColorSnake_Types m_Types;

    public ColorSnake_Types Types => m_Types;
    private CameraBounds bounds;
    public CameraBounds Bounds
    {
        get => bounds;
        private set => bounds = value;
    }

    private void Awake()
    {
        Vector2 minScreen = m_MainCamera.ScreenToWorldPoint(Vector3.zero);
        bounds = new CameraBounds()
        {
            Left = minScreen.x,
            Right = Mathf.Abs(minScreen.x),
            Up = Mathf.Abs(minScreen.y),
            Down = minScreen.y
        };
    }

    private void Reset()
    {
        m_MainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Time.deltaTime * 3f * Vector3.up;
        m_MainCamera.transform.Translate(movement);
        m_Snake.transform.Translate(movement);
    }
}
