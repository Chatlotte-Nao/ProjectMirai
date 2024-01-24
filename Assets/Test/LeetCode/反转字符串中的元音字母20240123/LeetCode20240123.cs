using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class LeetCode20240123 : MonoBehaviour
{
    // 给你一个字符串 s ，仅反转字符串中的所有元音字母，并返回结果字符串。
    //
    // 元音字母包括 'a'、'e'、'i'、'o'、'u'，且可能以大小写两种形式出现不止一次。
    //
    //
    //
    // 示例 1：
    //
    // 输入：s = "hello"
    // 输出："holle"
    // 示例 2：
    //
    // 输入：s = "leetcode"
    // 输出："leotcede"

    private void Start()
    {
        string s = "hello";
        Debug.Log(ReverseVowels(s)); 
    }
    //双指针判断需要注意头指针不能大于尾指针
    public string ReverseVowels(string s)
    {
        int head = 0;
        int back = s.Length - 1;
        StringBuilder sb = new StringBuilder(s);
        
        while (head<back)
        {
            if (IsYuanYin(sb[head]) && !IsYuanYin(sb[back]))
            {
                back--;
            }
            else if (!IsYuanYin(sb[head]) && IsYuanYin(sb[back]))
            {
                head++;
            }
            else if (!IsYuanYin(sb[head]) && !IsYuanYin(sb[back]))
            {
                back--;
                head++;
            }
            else
            {
                char tempchar = sb[head];
                sb[head] = sb[back];
                sb[back] = tempchar;
                head++;
                back--;
            }
        }

        return sb.ToString();
    }

    private bool IsYuanYin(char target)
    {
        List<char> tempList = new List<char>
        {
            'a',
            'e',
            'i',
            'o',
            'u'
        };
        return tempList.Contains(char.ToLower(target));
    }
    
    /// <summary>
    /// 最优解使用栈
    /// </summary>
    public string FastReverseVowels(string s) {
        string goalChar = "aeiouAEIOU";
        Stack<char> str = new Stack<char>();
        for (int i = 0; i < s.Length; i++)
        {
            if (goalChar.Contains(s[i]))
            {
                str.Push(s[i]);
            }
        }

        return new string(s.Select(x => goalChar.Contains(x) ? str.Pop() : x).ToArray());
    }
}
