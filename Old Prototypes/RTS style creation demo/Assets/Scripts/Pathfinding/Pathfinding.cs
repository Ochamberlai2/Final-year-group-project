﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class Pathfinding : MonoBehaviour {

    PathRequestManager requestManager;

    Grid grid;

    private void Awake()
    {
        grid = GetComponent<Grid>();
        requestManager = GetComponent<PathRequestManager>();
    }

    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;
        //get the position on the grid of both positions
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);
        if(startNode.walkable && targetNode.walkable)
        { 
        //set the open and closed sets then add the start node to the open set
        Heap<Node> openSet = new Heap<Node>(grid.maxSize);
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

            //while open set is not empty
            while (openSet.Count > 0)
            {
                //set the current node to the first in the set
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                //then we check to see if the current node is the target, if so return out
                if (currentNode == targetNode)
                {
                    sw.Stop();
                    print("Path found: " + sw.ElapsedMilliseconds + " ms");
                    pathSuccess = true;
                    break;
                }
                foreach (Node neighbour in grid.GetNeighbours(currentNode))
                {
                    //search through all neighbours in order to see if they are walkable or in the closed set
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }
                    //get the movement cost to that neighbour node            
                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                    //then if the movement cost is less than the cost for moving to the neighbour, or the open set doesnt contain the neighbour
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        //set the g and h costs and also set the parent
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;
                    }
                    //if open doesnt contain the neighbour, we need to add it
                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                    else
                    {
                        openSet.UpdateItem(neighbour);
                    }
                }
            }
        }
        yield return null;
        if(pathSuccess)
        {
            waypoints =  retracePath(startNode, targetNode);

        }
        requestManager.FinishedProcessingPath(waypoints, pathSuccess);
    }

    Vector3[] retracePath(Node startNode, Node targetNode)
    {
        //set an empty list
        List<Node> path = new List<Node>();
        //set the current node to being the target
        Node currentNode = targetNode;

        //and while the current node isnt the start node
        while(currentNode != startNode)
        {
            //continue adding the nodes to the path
            path.Add(currentNode);
            //and alter current node to be the parent of the previous one
            currentNode = currentNode.parent;
        }
        Vector3[] waypoints = simplifyPath(path);
        Array.Reverse(waypoints);

        return waypoints;
    }


    Vector3[] simplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for(int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if(directionNew != directionOld)
            {
                waypoints.Add(path[i].worldPosition);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }

    //get the heuristic between two nodes
    int GetDistance(Node nodeA, Node nodeB)
    {
        //get the absolute value of the x and y 
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        //then if x is bigger than y
        if (distanceX > distanceY)
            return 14 * distanceY + 10 * (distanceX - distanceY);

        //otherwise
        return 14 * distanceX + 10 * (distanceY - distanceX);
    }
}
