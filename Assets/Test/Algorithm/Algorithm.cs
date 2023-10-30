using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorithm : MonoBehaviour
{
    private void Start()
    {
        GraphNode nodeA = new GraphNode(1);
        GraphNode nodeB = new GraphNode(2);
        GraphNode nodeC = new GraphNode(3);
        
        nodeA.AddNeighbor(nodeB);
        nodeA.AddNeighbor(nodeC);
        nodeB.AddNeighbor(nodeC);

        Graph graph = new Graph();
        graph.AddNode(nodeA);
        graph.AddNode(nodeB);
        graph.AddNode(nodeC);
        
        
    }

    private void BubbleSort(int[] arr)
    {
        for (int i = 0; i < arr.Length-1; i++)
        {
            bool swapped=false;
            for (int j = 0; j < arr.Length-1-i; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                    swapped = true;
                }
            }
            if (!swapped)
                break;
        }
    }

    private void QuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            int partitionindex = Partition(arr,low,high);
            QuickSort(arr,low,partitionindex-1);
            QuickSort(arr,partitionindex+1,high);
        }
    }

    private int Partition(int[] arr, int low, int high)
    {
        int pivot = arr[high];
        int i = low - 1;
        for (int j = low; j < high; j++)
        {
            if (arr[j] < pivot)
            {
                i++;
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }
        int temp2 = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = temp2;

        return i + 1;
    }

    private int BinarySearch(int[] arr,int target)
    {
        int left = 0;
        int right = arr.Length - 1;
        while (left<=right)
        {
            //确定中间数的索引
            int mid = left + (right - left) / 2;
            if (arr[mid] == target)
            {
                return mid;
            }
            if (arr[mid] < target)
            {
                left = mid + 1;
            }
            else if(arr[mid]>target)
            {
                right = mid - 1;
            }
        }

        return -1;
    }

    private void DepthFirstSearch(GraphNode startNode)
    {
        if (startNode == null)
            return ;
        startNode.IsVisited = true;
        Debug.Log("Node Value Is:"+startNode.Data);
        foreach (var item in startNode.Negihbors)
        {
            if (!item.IsVisited)
            {
                DepthFirstSearch(item);
            }
        }
    }

    private void BreadthFirstSearch(GraphNode startNode)
    {
        if(startNode==null)
            return;
        startNode.IsVisited = true;
        Queue<GraphNode> queue = new Queue<GraphNode>();
        
        queue.Enqueue(startNode);

        while (queue.Count>0)
        {
            var currentNode= queue.Dequeue();
            Debug.Log("BFS Node Value:" + currentNode.Data);
            foreach (var item in currentNode.Negihbors)
            {
                if (!item.IsVisited)
                {
                    queue.Enqueue(item);
                    item.IsVisited = true;
                }
            }
        }
    }
    
}


public class GraphNode
{
    public int Data { get; }
    public List<GraphNode> Negihbors { get; }
    public bool IsVisited;
    public GraphNode(int value)
    {
        Data = value;
        Negihbors = new List<GraphNode>();
    }

    public void AddNeighbor(GraphNode graphNode)
    {
        Negihbors.Add(graphNode);
    }
}

public class Graph
{
    public List<GraphNode> Nodes { get; }

    public void AddNode(GraphNode graphNode)
    {
        Nodes.Add(graphNode);
    }
}
