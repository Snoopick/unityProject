﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitInTheHole_FigureTweener : MonoBehaviour
{
    private Vector3 fromPosition;
    private Vector3 toPosition;

    private Vector3 rotationPoint;
    private float rotationAngle;
    private float rotationDirection;
    private float speedMultiplayer = 4f;

    float timer;


    public void Tween(Vector3 from, Vector3 to)
    {
        fromPosition = from;
        toPosition = to;

        rotationAngle = 90f;

        //проверяем нужно ли смещение по оси Х
        bool isX = Mathf.Abs(from.x - to.x) > 0.01f;
        //проверяем нужно ли смещение по оси Y
        bool isY = Mathf.Abs(from.y - to.y) > 0.01f;

        //Находим точку вращения
        rotationPoint = Vector3.Lerp(from, to, 0.5f);
        rotationDirection = 1;

        //Если нужно смещение по обеим осям
        if (isX && isY)
        {
            rotationAngle = 180f;
            rotationDirection = to.x < from.x ? 1 : -1;
            return;
        }


        if (isX)
        {
            //смещение по оси Х делаем только влево или вправи по нижней грани
            rotationPoint.y -= 0.5f;
            rotationDirection = to.x < from.x ? 1 : -1;
        }
        else
        {
            //TODO - корректное смещение по по оси Y не поддерживается            
        }
    }

    void Update()
    {
        transform.RotateAround(rotationPoint, Vector3.forward,
            Time.deltaTime * speedMultiplayer * rotationAngle * rotationDirection);

        timer += Time.deltaTime * speedMultiplayer;
        timer = Mathf.Clamp01(timer);

        if (timer < 0.999f)
            return;
        transform.position = toPosition;
        transform.rotation = Quaternion.identity;

        Destroy(this);
    }
}