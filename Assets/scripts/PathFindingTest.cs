using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PathFindingTest : MonoBehaviour
{
    public GameObject mapGroup;

    void Start()
    {
        var graph = new SquareGrid(5, 5);
        for(int i = 0; i < 4; i++)
        {
            graph.walls.Add(new Position(i, 1));
        }

        ResetMapGroup(graph);

        var astar = new AStarSearch(graph, new Position(0, 0), new Position(0, 2));

        PaintMap(graph, astar);
    }

    void PaintMap(SquareGrid graph, AStarSearch astar)
    {
        for(int i = 0; i < graph.height; i++)
        {
            for(int j = 0; j < graph.width; j++)
            {
                Position id = new Position(i, j);
                Position ptr = id;
                if(!astar.cameFrom.TryGetValue(id, out ptr))
                {
                    ptr = id;
                    if (graph.walls.Contains(id))
                    {
                        GetImage((graph.height * i) + j).color = Color.green;
                    }
                    else
                    {
                        var go = mapGroup.transform.GetChild((graph.height * ptr.x) + ptr.y).gameObject;
                        go.GetComponent<Image>().color = Color.grey;
                    }
                }
                else
                {
                    var go = mapGroup.transform.GetChild((graph.height * ptr.x) + ptr.y).gameObject;
                    go.GetComponent<Image>().color = Color.red;
                }
            }
        }
    }

    void ResetMapGroup(SquareGrid graph)
    {
        for(int i = 0; i < graph.height; i++)
        {
            for (int j = 0; j < graph.width; j++)
            {
                GetImage((graph.height * i) + j).color = graph.walls.Contains(new Position(i, j)) ? Color.white : Color.gray;
            }
        }
    }

    Image GetImage(int i)
    {
        var go = mapGroup.transform.GetChild(i).gameObject;
        return go.GetComponent<Image>();
    }

    
}