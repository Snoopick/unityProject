using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSnake_LevelGenerator : MonoBehaviour
{
    [SerializeField] private ColorSnake_Types m_Types;
    [SerializeField] private ColorSnake_GameController m_Controller;

    private int line = 1;//номер генерируей линии препятствий
    private List<GameObject> obstacles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        var upBorder = m_Controller.Bounds.Up;
        while (line * 2f < upBorder + 2f)
        {
            GenerateObstacles();
        }
    }

    void Update()
    {
        var upBorder = m_Controller.Bounds.Up + m_Controller.MainCamera.transform.position.y;
        if (line * 2f > upBorder + 2f)
            return;

        GenerateObstacles();
        var downBorder = m_Controller.MainCamera.transform.position.y + m_Controller.Bounds.Down;
        if(obstacles[0].transform.position.y + 4f < downBorder)
        {
            Destroy(obstacles[0]);
            obstacles.RemoveAt(0);
        }
        
    }
    private void GenerateObstacles()
    {
        var template = m_Types.GetRandomTemplate();
        var obstacle = new GameObject("Obstacle_" + line);
        foreach (var point in template.Points)
        {
            var objType = m_Types.GetRandomObjectType();
            var colorType = m_Types.GetRandomColorType();

            var obj = Instantiate(objType.Object, point.transform.position, point.transform.rotation);
            obj.transform.parent = obstacle.transform;

            obj.GetComponent<SpriteRenderer>().color = colorType.Color;

            var obstacleController = obj.AddComponent<ColorSnake_Obstacle>();
            obstacleController.ColorId = colorType.Id;
        }

        Vector3 pos = obstacle.transform.position;
        pos.y = line * 2f;//2 - расстояние между препятсвиями
        obstacle.transform.position = pos;

        line++;

        obstacles.Add(obstacle);
    }
}

