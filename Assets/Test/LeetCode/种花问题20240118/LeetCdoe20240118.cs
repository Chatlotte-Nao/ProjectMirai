using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeetCdoe20240118 : MonoBehaviour
{
    
    // 假设有一个很长的花坛，一部分地块种植了花，另一部分却没有。可是，花不能种植在相邻的地块上，它们会争夺水源，两者都会死去。
    // 给你一个整数数组 flowerbed 表示花坛，由若干 0 和 1 组成，
    // 其中 0 表示没种植花，1 表示种植了花。另有一个数 n ，
    // 能否在不打破种植规则的情况下种入 n 朵花？能则返回 true ，不能则返回 false 。
    
    public bool CanPlaceFlowers(int[] flowerbed, int n) {
        int cnt = flowerbed.Length;
        int i = 0;
        while(i < cnt){
            // 如果当前地块没种花
            if(flowerbed[i] == 0){
                // 边界条件，如果当前为最后一个且没种花，直接种
                if(i == cnt - 1){
                    n --;
                    break;
                }
                // 如果下一个位置没种花
                // 即 00 的情况
                if(flowerbed[i + 1] == 0){
                    // 则当前位置可以种花
                    n --;
                    // 根据规则，此时下一个位置就没法种花，直接跳到下下个位置
                    i += 2;
                }
                // 如果下一个位置种花了
                // 即 010 的情况
                else if(flowerbed[i + 1] == 1){
                    // 根据规则，当前位置不能种花，下一个位置的下一个位置也不能种花，直接跳到下下下个位置
                    i += 3;
                }
            }
            // 如果当前地块种花
            // 即 10 的情况
            else if(flowerbed[i] == 1){
                // 根据规则，下个位置不能种花，直接跳到下下个位置
                i += 2;
            }
        }
        if(n > 0){
            return false;
        }
        return true;
    }
}
