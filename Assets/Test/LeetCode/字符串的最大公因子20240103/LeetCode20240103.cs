using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class LeetCode20240102 : MonoBehaviour
{
    // 对于字符串 s 和 t，只有在 s = t + ... + t（t 自身连接 1 次或多次）时，我们才认定 “t 能除尽 s”。
    //
    // 给定两个字符串 str1 和 str2 。返回 最长字符串 x，要求满足 x 能除尽 str1 且 x 能除尽 str2 。
    // 示例 1：
    //
    // 输入：str1 = "ABCABC", str2 = "ABC"
    // 输出："ABC"
    // 示例 2：
    //
    // 输入：str1 = "ABABAB", str2 = "ABAB"
    // 输出："AB"
    // 示例 3：
    //
    // 输入：str1 = "LEET", str2 = "CODE"
    // 输出：""
    public string GcdOfStrings(string str1, string str2)
    {
        int len1 = str1.Length, len2 = str2.Length;
        for (int i = Math.Min(len1, len2); i >= 1; --i)
        {
            // 从长度大的开始枚举
            if (len1 % i == 0 && len2 % i == 0)
            {
                String X = str1.Substring(0, i);
                if (check(X, str1) && check(X, str2))
                {
                    return X;
                }
            }
        }

        return string.Empty;
    }
    
    public bool check(String t, String s) {
        int lenx = s.Length / t.Length;
        StringBuilder ans = new StringBuilder();
        for (int i = 1; i <= lenx; ++i) {
            ans.Append(t);
        }
        return ans.ToString().Equals(s);
    }
}
