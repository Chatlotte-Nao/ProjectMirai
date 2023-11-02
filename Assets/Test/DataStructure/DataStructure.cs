using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStructure : MonoBehaviour
{
    private void Start()
    {
        // #region ArrayStack
        //
        // ArrayStack arrayStack = new ArrayStack(3);
        // arrayStack.Push(1);
        // arrayStack.Push(2);
        // arrayStack.Push(3);
        //
        // Debug.Log("peek:"+arrayStack.Peek());
        // Debug.Log("pop:" + arrayStack.Pop());
        //
        // arrayStack.Push(6);
        // while (!arrayStack.IsEmpty())
        // {
        //     Debug.Log("pop while:"+arrayStack.Pop());
        // }
        //
        // #endregion
        
        // #region LinkNodeStack
        //
        // NodeStack<string> linkNoeStack = new NodeStack<string>();
        // linkNoeStack.Push("a");
        // linkNoeStack.Push("b");
        // linkNoeStack.Push("d");
        //
        // Debug.Log("linkNodeStack:" + linkNoeStack.Pop());
        // Debug.Log("linkNodeStack:" + linkNoeStack.Pop());
        // Debug.Log("linkNodeStack:" + linkNoeStack.Pop());
        // Debug.Log("linkNodeStack:" + linkNoeStack.Pop());
        //
        // linkNoeStack.Push("q");
        // linkNoeStack.Push("x");
        // linkNoeStack.Push("z");
        //
        // while (!linkNoeStack.IsEmpty())
        // {
        //     Debug.Log("Stack While:"+linkNoeStack.Pop());
        // }
        // #endregion

        // #region ArrayQueue
        // ArrayQueue<int> arrayQueue = new ArrayQueue<int>(3);
        //
        // arrayQueue.Enqueue(1);
        // arrayQueue.Enqueue(2);
        // arrayQueue.Enqueue(3);
        // arrayQueue.Enqueue(4);
        //
        // while (!arrayQueue.IsEmpty())
        // {
        //     Debug.Log("While Dequeue :"+arrayQueue.Dequeue());
        // }
        //
        // Debug.Log("arrayQueue Dequeue :"+arrayQueue.Dequeue());
        // Debug.Log("arrayQueue Dequeue :"+arrayQueue.Dequeue());
        // arrayQueue.Enqueue(6);
        // arrayQueue.Enqueue(7);
        //
        // while (!arrayQueue.IsEmpty())
        // {
        //     Debug.Log("While Dequeue :"+arrayQueue.Dequeue());
        // }
        //
        // #endregion

        // #region LinkNodeQueue
        //
        // LinkNodeQueue<int> linkNodeQueue = new LinkNodeQueue<int>();
        //
        // linkNodeQueue.Enqueue(3);
        // linkNodeQueue.Enqueue(2);
        // linkNodeQueue.Enqueue(7);
        //
        // while (!linkNodeQueue.IsEmpty())
        // {
        //     Debug.Log("Queue:"+linkNodeQueue.Dequeue());
        // }
        // linkNodeQueue.Enqueue(7);
        // linkNodeQueue.Enqueue(8);
        // linkNodeQueue.Dequeue();
        // linkNodeQueue.Enqueue(10);
        //
        // while (!linkNodeQueue.IsEmpty())
        // {
        //     Debug.Log("Queue2:"+linkNodeQueue.Dequeue());
        // }
        // #endregion

        #region BinaryTree
        
        BinaryTree binaryTree = new BinaryTree();
        
        binaryTree.Insert(8);
        binaryTree.Insert(12);
        binaryTree.Insert(7);
        binaryTree.Insert(13);

        
        // binaryTree.MiddleTravel(rootNode);
        // binaryTree.FinalTravel(rootNode);

        #endregion
        
        
    }
}

/// <summary>
/// 数组栈
/// </summary>
public class ArrayStack
{
    private int[] _array;
    private int _capacity;
    private int _top = -1;
    public ArrayStack(int capacity)
    {
        _array = new int[capacity];
        _capacity = capacity;
    }

    public bool IsEmpty()
    {
        return _top == -1;
    }

    public bool IsMax()
    {
        return _top == _capacity-1;
    }

    public void Push(int item)
    {
        if (!IsMax())
        {
            _array[++_top] = item;
        }
        else
        {
            Debug.LogError("Stack Is Max!");
        }
    }

    public int Peek()
    {
        int data = 0;
        if (!IsEmpty())
        {
            data=_array[_top];
        }
        else
        {
            Debug.LogError("Stack Is Empty!");
        }

        return data;
    }

    public int Pop()
    {
        int data = 0;
        if (!IsEmpty())
        {
            data = _array[_top];
            _top--;
        }
        else
        {
            Debug.LogError("Stack Is Empty!");
        }

        return data;
    }
}
/// <summary>
/// 链表栈
/// </summary>
/// <typeparam name="T"></typeparam>
public class NodeStack<T>
{
    private T _data;
    private NodeStack<T> _next;
    public NodeStack<T> Top;
    
    public void Push(T item)
    {
        NodeStack<T> nextNode = new NodeStack<T>();
        nextNode._data = item;
        nextNode._next = Top;
        Top = nextNode;
    }

    public T Pop()
    {
        T rtnData = _data;
        if (!IsEmpty())
        {
            rtnData = Top._data;
            Top = Top._next;
        }
        else
        {
            Debug.Log("Stack Is Empty");
        }

        return rtnData;
    }

    public bool IsEmpty()
    {
        return Top== null;
    }
}
/// <summary>
/// 数组队列
/// </summary>
/// <typeparam name="T"></typeparam>
public class ArrayQueue<T>
{
    private T[] _array;
    private int _capacity;
    private int _finalIndex = -1;
    private int _dequeueIndex;
    public ArrayQueue(int capacity)
    {
        _array = new T[capacity];
        _capacity = capacity;
    }

    public void Enqueue(T item)
    {
        if (!IsMax())
        {
            _array[++_finalIndex] = item;
        }
        else
        {
            Debug.Log("ArrayQueue is Max");
        }
    }

    public T Dequeue()
    {
        T data=default;
        if (!IsEmpty())
        {
            data=_array[_dequeueIndex++];
            if (_dequeueIndex > _finalIndex)
            {
                _dequeueIndex = 0;
                _finalIndex = -1;
            }
        }
        else
        {
            Debug.Log("ArrayQueue is Empty");
        }

        return data;
    }

    public bool IsMax()
    {
        return _finalIndex == _capacity - 1;
    }

    public bool IsEmpty()
    {
        return _finalIndex == -1;
    }
}

public class Node<T>
{
    public T Data { get; set; }
    
    public Node<T> Next { get; set; }

    public Node(T data)
    {
        Data = data;
    }
}
/// <summary>
/// 链表队列
/// </summary>
/// <typeparam name="T"></typeparam>
public class LinkNodeQueue<T>
{
    public Node<T> Front;
    
    public Node<T> Rear;
    
    public void Enqueue(T item)
    {
        Node<T> newNode = new Node<T>(item);
        if (Rear == null)
        {
            Front = newNode;
            Rear = newNode;
        }
        else
        {
            Rear.Next = newNode;
            Rear = newNode;
        }
    }

    public T Dequeue()
    {
        T rtndata = default(T);
        if (!IsEmpty())
        {
            rtndata = Front.Data;
            Front = Front.Next;
            if (Front == null)
            {
                Rear = null;
            }
        }
        else
        {
            Debug.Log("empty");
        }

        return rtndata;
    }

    public bool IsEmpty()
    {
        return Rear == null;
    }
}

public class TreeNode
{
    public int Data { get; set; }
    public TreeNode Left { get; set; }
    public TreeNode Right { get; set; }

    public TreeNode(int data)
    {
        Data = data;
        Left = null;
        Right = null;
    }
}
/// <summary>
/// 二叉树装的是能够比较大小的元素
/// </summary>
/// <typeparam name="T"></typeparam>
public class BinaryTree
{
    public TreeNode Root { get; private set; }

    public BinaryTree()
    {
        Root = null;
    }

    // 插入节点的方法
    public void Insert(int data)
    {
        Root = InsertRec(Root, data);
    }

    private TreeNode InsertRec(TreeNode root, int data)
    {
        if (root == null)
        {
            root = new TreeNode(data);
            return root;
        }

        if (data < root.Data)
        {
            root.Left = InsertRec(root.Left, data);
        }
        else if (data > root.Data)
        {
            root.Right = InsertRec(root.Right, data);
        }

        return root;
    }

    // 中序遍历二叉树
    public void InOrderTraversal(TreeNode node)
    {
        if (node != null)
        {
            InOrderTraversal(node.Left);
            Console.Write(node.Data + " ");
            InOrderTraversal(node.Right);
        }
    }
}





