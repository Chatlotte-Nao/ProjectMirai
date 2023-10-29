using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorithm : MonoBehaviour
{
    private void Start()
    {
        
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
    
}

