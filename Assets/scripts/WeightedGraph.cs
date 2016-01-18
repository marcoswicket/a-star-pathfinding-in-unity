using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface WeightedGraph<L>
{
    int Cost(Position a, Position b);
    IEnumerable<Position> Neighbors (Position id);
}

public struct Position
{
    public readonly int x, y;
    public Position(int x, int y) { this.x = x; this.y = y; }
}

public class SquareGrid : WeightedGraph<Position>
{
    //RIGHT, UP, LEFT, DOWN
    public static readonly Position[] DIRECTIONS = new[]
    {
        new Position(1, 0),
        new Position(0, -1),
        new Position(-1, 0),
        new Position(0, 1)
    };

    public int width, height;
    public HashSet<Position> walls = new HashSet<Position>();

    public SquareGrid(int width, int height) { this.height = height; this.width = width; }
    public bool InBounds(Position id) { return 0 <= id.x && id.x < width && 0 <= id.y && id.y < height; }
    public bool Passable(Position id) { return !walls.Contains(id); }
    public int Cost(Position a, Position b) { return 1; }

    public IEnumerable<Position> Neighbors(Position id)
    {
        foreach (var dir in DIRECTIONS)
        {
            Position next = new Position(id.x + dir.x, id.y + dir.y);
            if(InBounds(next) && Passable(next))
            {
                //return each element one at time;
                yield return next;
            }
        }
    }
}

