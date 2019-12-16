using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Direction
{
    None = -1,
    Left = 0,
    Right = 1,
    Up = 2,
    Down = 3
}

public class Neighbour
{
    public Direction Direction;
    public MatchThree_Cell Cell;
}

public class MatchThree_Cell : MonoBehaviour
{
    public MatchThree_Candy Candy;
    private readonly Neighbour[] neighbours = new Neighbour[4];

    public MatchThree_Cell GetNeighbour(Direction direction)
    {
        return neighbours.FirstOrDefault(n => n != null && n.Direction == direction)?.Cell;
    }

    public void SetNeighbour(Direction direction, MatchThree_Cell cell)
    {
        if(GetNeighbour(direction))
        {
            return;
        }

        neighbours[(int) direction] = new Neighbour()
        {
            Direction = direction,
            Cell = cell
        };
    }
}
