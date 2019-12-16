using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchThree_Field : MonoBehaviour
{
    [SerializeField] private Camera m_MainCamera;
    [SerializeField] private GameObject m_Cell;
    [SerializeField] private float m_CellSize = .6f;
    [SerializeField] private int m_FieldWidth = 6;
    [SerializeField] private int m_FieldHeight = 8;

    public static int filedWidth = 0;
    public static int filedHeight = 0;
    public static float filedCellSize = 0;

    private static readonly List<List<MatchThree_Cell>> GameField = new List<List<MatchThree_Cell>>();

    public static float CurrentCellSize;

    public void Init()
    {
        GenerateField(m_FieldWidth, m_FieldHeight);
        CurrentCellSize = m_CellSize;

        filedWidth = m_FieldWidth;
        filedHeight = m_FieldHeight;
        filedCellSize = m_CellSize;
    }

    private void GenerateField(int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            GameField.Add(new List<MatchThree_Cell>());
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = new Vector3(x * m_CellSize, y * m_CellSize, 0f);
                var obj = Instantiate(m_Cell, pos, Quaternion.identity);
                obj.name = $"Cell {x} {y}";

                var cell = obj.AddComponent<MatchThree_Cell>();
                GameField[x].Add(cell);
                
                // соседи по горизонтали
                // не крайняя левая
                if (x > 0)
                {
                    cell.SetNeighbour(Direction.Left, GameField[x - 1][y]);
                    GameField[x - 1][y].SetNeighbour(Direction.Right, cell);
                }
                else
                {
                    cell.SetNeighbour(Direction.Left, null);
                }


                // крайняя правая 
                if (x == width - 1)
                {
                    
                }

                if (y > 0)
                {
                    cell.SetNeighbour(Direction.Down, GameField[x][y - 1]);
                    GameField[x][y - 1].SetNeighbour(Direction.Up, cell);
                }
            }
        }
        
        var cameraPosition = new Vector3(width * m_CellSize * .5f, height * m_CellSize * .5f, -1);
        m_MainCamera.transform.position = cameraPosition;
    }

    public static MatchThree_Cell GetCell(MatchThree_Candy candy)
    {
        foreach (var row in GameField)
        {
            foreach (var cell in row)
            {
                if (cell.Candy == candy)
                {
                    return cell;
                }
            }
        }

        return null;
    }

    public static MatchThree_Cell GetCell(int x, int y)
    {
        return GameField[x][y];
    }
}
