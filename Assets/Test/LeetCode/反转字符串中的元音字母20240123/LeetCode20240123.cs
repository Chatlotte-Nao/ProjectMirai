using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeetCode20240123 : MonoBehaviour
{
    public string ReverseVowels(string s)
    {
        List<char> tempList = new List<char>
        {
            'a',
            'e',
            'i',
            'o',
            'u'
        };
        int head = 0;
        int back = s.Length - 1;
        for (int i = 0; i < s.Length; i++)
        {
            if (tempList.Contains(char.ToLower(s[head])))
            {
                
            }
            head++;
            if (tempList.Contains(char.ToLower(s[back])))
            {
                
            }
            back--;
        }
    }
}
