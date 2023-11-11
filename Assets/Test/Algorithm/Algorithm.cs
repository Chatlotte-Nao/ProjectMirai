using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    
    public void MergeSort(int[] arr, int left, int right)
    {
        if (left < right)
        {
            int middle = (left + right) / 2;

            // 递归排序左半部分
            MergeSort(arr, left, middle);
            // 递归排序右半部分
            MergeSort(arr, middle + 1, right);

            // 合并左右两部分
            Merge(arr, left, middle, right);
        }
    }

    private void Merge(int[] arr, int left, int middle, int right)
    {
        int n1 = middle - left + 1;
        int n2 = right - middle;

        int[] leftArray = new int[n1];
        int[] rightArray = new int[n2];
        int i = 0, j = 0;
        // 复制数据到临时数组 leftArray 和 rightArray
        for (i = 0; i < n1; i++)
        {
            leftArray[i] = arr[left + i];
        }
        for (j = 0; j < n2; j++)
        {
            rightArray[j] = arr[middle + 1 + j];
        }

        // 合并 leftArray 和 rightArray
        int k = left;
        i = 0;
        j = 0;
        while (i < n1 && j < n2)
        {
            if (leftArray[i] <= rightArray[j])
            {
                arr[k] = leftArray[i];
                i++;
            }
            else
            {
                arr[k] = rightArray[j];
                j++;
            }
            k++;
        }

        // 处理剩余元素
        while (i < n1)
        {
            arr[k] = leftArray[i];
            i++;
            k++;
        }
        while (j < n2)
        {
            arr[k] = rightArray[j];
            j++;
            k++;
        }
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


class DijkstraAlgorithm
{
    // 计算从起始节点到所有其他节点的最短路径
    public static void Dijkstra(int[,] graph, int startNode)
    {
        int numNodes = graph.GetLength(0);
        int[] shortestDistances = new int[numNodes];
        bool[] visited = new bool[numNodes];

        for (int i = 0; i < numNodes; i++)
        {
            shortestDistances[i] = int.MaxValue;
            visited[i] = false;
        }

        shortestDistances[startNode] = 0;

        for (int count = 0; count < numNodes - 1; count++)
        {
            int minDistance = int.MaxValue;
            int minIndex = -1;

            // 选择当前距离起始节点最小的节点
            for (int node = 0; node < numNodes; node++)
            {
                if (!visited[node] && shortestDistances[node] < minDistance)
                {
                    minDistance = shortestDistances[node];
                    minIndex = node;
                }
            }

            visited[minIndex] = true;

            // 更新邻接节点的最短路径距离
            for (int node = 0; node < numNodes; node++)
            {
                if (!visited[node] && graph[minIndex, node] != 0 && shortestDistances[minIndex] != int.MaxValue &&
                    shortestDistances[minIndex] + graph[minIndex, node] < shortestDistances[node])
                {
                    shortestDistances[node] = shortestDistances[minIndex] + graph[minIndex, node];
                }
            }
        }
        // 打印最短路径
        Debug.Log("节点\t最短距离");
        for (int i = 0; i < numNodes; i++)
        {
            Debug.Log($"{i}\t{shortestDistances[i]}");
        }
    }

    static void Main(string[] args)
    {
        int[,] graph = {
            {0, 4, 0, 0, 0, 0, 0, 8, 0},
            {4, 0, 8, 0, 0, 0, 0, 11, 0},
            {0, 8, 0, 7, 0, 4, 0, 0, 2},
            {0, 0, 7, 0, 9, 14, 0, 0, 0},
            {0, 0, 0, 9, 0, 10, 0, 0, 0},
            {0, 0, 4, 14, 10, 0, 2, 0, 0},
            {0, 0, 0, 0, 0, 2, 0, 1, 6},
            {8, 11, 0, 0, 0, 0, 1, 0, 7},
            {0, 0, 2, 0, 0, 0, 6, 7, 0}
        };

        Dijkstra(graph, 0);
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
