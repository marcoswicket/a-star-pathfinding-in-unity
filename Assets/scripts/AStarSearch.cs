using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Priority_Queue;

public class AStarSearch
{
    public Dictionary<Position, Position> cameFrom = new Dictionary<Position, Position>();
    public Dictionary<Position, int> costSoFar = new Dictionary<Position, int>();

    static public int Heuristic(Position a, Position b)
    {
        return Math.Abs(a.x - b.x) + (a.y - b.y);
    }

    public AStarSearch(WeightedGraph<Position> graph, Position start, Position goal)
    {
        var frontier = new SimplePriorityQueue<Position>();
        frontier.Enqueue(start, 0);

        cameFrom[start] = start;
        costSoFar[start] = 0;

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();

            if(current.Equals(goal))
            {
                break;
            }

            foreach (var next in graph.Neighbors(current))
            {
                int newCost = costSoFar[current] + graph.Cost(current, next);
                if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                {
                    costSoFar[next] = newCost;
                    int priority = newCost + Heuristic(next, goal);
                    frontier.Enqueue(next, priority);
                    cameFrom[next] = current;
                }
            }
        } 
    }
}