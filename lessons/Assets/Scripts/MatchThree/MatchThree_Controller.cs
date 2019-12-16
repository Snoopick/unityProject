using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchThree_Controller : MonoBehaviour
{
    [SerializeField] private MatchThree_Field m_Field;
    [SerializeField] private MatchThree_Types m_Types;

    public static Camera MainCamera;
    
    /// <summary>
    ///проверяем можем ли мы поместить в данную ячейку такую кофету -  нужно избегать ситуаций с 3 и более в ряд на старте игры
    /// </summary>
    /// <param name="targetCell">Стартовая клетка</param>
    /// <param name="id">Id конфеты</param>
    /// <returns></returns>
    public static bool IsFreeCandyPlacement(MatchThree_Cell targetCell, int id)
    {
        //Direction от 0 до 4 соответсвует enum
        for (int i = 0; i < 4; i++)
        {
            Direction direction = (Direction) i;
            Direction prevDirection = direction;
            int counter = 0;
            int repeated = 0;

            MatchThree_Cell cell = targetCell;
            MatchThree_Cell prevCell;
            
            while (counter < 2)
            {
                cell = cell.GetNeighbour(direction);

                if (direction == (Direction) 0)
                {
                    prevDirection = (Direction) 1;
                } 
                else if (direction == (Direction) 1)
                {
                    prevDirection = (Direction) 0;
                }
                else if (direction == (Direction) 2)
                {
                    prevDirection = (Direction) 3;
                }
                else if (direction == (Direction) 3)
                {
                    prevDirection = (Direction) 2;
                }

                if (!cell || !cell.Candy)
                {
                    break;
                }

                prevCell = cell.GetNeighbour(prevDirection);
                
                if (!prevCell || !prevCell.Candy)
                {
                    break;
                }
                
                if (prevCell.Candy.CandyData.Id == id)
                {
                    repeated++;
                }

                if (cell.Candy.CandyData.Id == id)
                {
                    repeated++;
                }

                counter++;
            }

            if (repeated >= 2)
            {
                return false;
            }
        }

        return true;
    }
    
    private void Start()
    {
        MainCamera = Camera.main;
        
        m_Field.Init();

        var firstCell = MatchThree_Field.GetCell(0, 0);

        MatchThree_Cell cell = firstCell;
        while (cell)
        {
            SetupCandiesLine(cell, Direction.Right);
            cell = cell.GetNeighbour(Direction.Up);
        }
    }

    private void SetupCandiesLine(MatchThree_Cell firstCell, Direction direction)
    {
        MatchThree_Cell cell = firstCell;
        while (cell)
        {
            MatchThree_Candy newCandy = m_Types.GetRandomCandy();
            //пробуем генерить пока не получим разрешенную кoнефетку
            //типов конфеток должно быть 5 или более - иначе возможен вариант вечного цикла
            while (!IsFreeCandyPlacement(cell, newCandy.CandyData.Id))
            {
                Destroy(newCandy.gameObject);
                newCandy = m_Types.GetRandomCandy();
            }

            cell.Candy = newCandy;
            cell.Candy.transform.position = cell.transform.position;
            cell = cell.GetNeighbour(direction);
        }
    }

    public static void DeleteLine(MatchThree_Candy obj, MatchThree_Cell targetCell, int id)
    {
        //Direction от 0 до 4 соответсвует enum
        for (int i = 0; i < 4; i++)
        {
            Direction direction = (Direction) i;
            int counter = 0;
            MatchThree_Cell cell = targetCell;
            
            while (counter < 2)
            {
                cell = cell.GetNeighbour(direction);
                if (!cell || !cell.Candy)
                {
                    break;
                }

                if (cell.Candy.CandyData.Id == id)
                {
                    Destroy(cell.Candy.gameObject);
//                    DragCandiesLine(cell);
                }
                else
                {
                    break;
                }

                counter++;
            }
            
        }
        
        Destroy(obj.gameObject);
//        DragCandiesLine(targetCell);
    }

    public static void DragCandiesLine(MatchThree_Cell curCell)
    {
        MatchThree_Cell upCell = curCell.GetNeighbour(Direction.Up); 
        float emptyCells = MatchThree_Field.filedCellSize;
        float curLine = upCell.transform.position.y / .6f;

        for (float y = curLine; y < MatchThree_Field.filedHeight; y++)
        {
            if (upCell.Candy != null)
            {
                Vector3 newPos = new Vector3(upCell.transform.position.x, upCell.transform.position.y - emptyCells, 0f);
                upCell.Candy.transform.position = newPos;
            }
            else
            {
                emptyCells += MatchThree_Field.filedCellSize;
            }

            if (upCell == null)
            {
                break;
            }
            
            upCell = upCell.GetNeighbour(Direction.Up);
        }

        MatchThree_Controller mc = new MatchThree_Controller();
//        mc.SetupCandiesLine(upCell, Direction.None);
//        SetCandy(upCell, mc.m_Types);
    }

    public static void DragCandies(string from)
    {
        Debug.Log(from);
        for (int x = 0; x < MatchThree_Field.filedWidth; x++)
        {
            for (int y = 0; y < MatchThree_Field.filedHeight; y++)
            {
                MatchThree_Cell cell = MatchThree_Field.GetCell(x, y);

                if (!cell.Candy)
                {
                    Debug.Log(x + ", " + y + ", 0" + " Empty!");
                    
                    MatchThree_Cell upCell = cell.GetNeighbour(Direction.Up);
                    float emptyCells = MatchThree_Field.filedCellSize;

                    while (upCell)
                    {
                        Debug.Log("emptyCells " + emptyCells);
                        
                        if (upCell.Candy)
                        {
                            Vector3 newPos = new Vector3(upCell.transform.position.x, upCell.transform.position.y - emptyCells, 0f);
                            upCell.Candy.transform.position = newPos;
                        }
                        else
                        {
                            emptyCells += MatchThree_Field.filedCellSize;
                        }
                        
                        upCell = upCell.GetNeighbour(Direction.Up);
                    }
                }
                else
                {
                    Debug.Log(x + ", " + y + ", 0" + " Have! " + cell.Candy.name);
                }
            }
        }
    }
    
    private static void SetCandy(MatchThree_Cell firstCell, MatchThree_Types types)
    {
        MatchThree_Cell cell = firstCell;
        MatchThree_Candy newCandy = types.GetRandomCandy();

        while (!IsFreeCandyPlacement(cell, newCandy.CandyData.Id))
        {
            Destroy(newCandy.gameObject);
            newCandy = types.GetRandomCandy();
        }

        cell.Candy = newCandy;
        cell.Candy.transform.position = cell.transform.position;
    }
}