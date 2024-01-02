using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class LeetCode20231231 : MonoBehaviour
{
    //交替合并字符串
    // 给你两个字符串 word1 和 word2 。请你从 word1 开始，通过交替添加字母来合并字符串。如果一个字符串比另一个字符串长，就将多出来的字母追加到合并后字符串的末尾。
    //
    // 返回 合并后的字符串 。
    //
    //
    //
    // 示例 1：
    //
    // 输入：word1 = "abc", word2 = "pqr"
    // 输出："apbqcr"
    // 解释：字符串合并情况如下所示：
    // word1：  a   b   c
    //     word2：    p   q   r
    //     合并后：  a p b q c r
    // 示例 2：
    //
    // 输入：word1 = "ab", word2 = "pqrs"
    // 输出："apbqrs"
    // 解释：注意，word2 比 word1 长，"rs" 需要追加到合并后字符串的末尾。
    // word1：  a   b 
    // word2：    p   q   r   s
    // 合并后：  a p b q   r   s
    // 示例 3：
    //
    // 输入：word1 = "abcd", word2 = "pq"
    // 输出："apbqcd"
    // 解释：注意，word1 比 word2 长，"cd" 需要追加到合并后字符串的末尾。
    // word1：  a   b   c   d
    // word2：    p   q 
    // 合并后：  a p b q c   d
    //
    //
    // 提示：
    //
    // 1 <= word1.length, word2.length <= 100
    // word1 和 word2 由小写英文字母组成

    private void Start()
    {
        string world1 = "abc";
        string world2 = "xqcdf";
        MergeAlternately(world1, world2);
    }
    /// <summary>
    /// 自己想的解法
    /// </summary>
    /// <param name="word1"></param>
    /// <param name="word2"></param>
    /// <returns></returns>
    public string MergeAlternately(string word1, string word2)
    {
        StringBuilder strBuilder = new StringBuilder();
        int k = 0;
        int j = 0;
        int temp = Math.Min(word1.Length, word2.Length) * 2;
        for (int i = 0; i < temp; i++)
        {
            if (i % 2 == 0)
            {
                strBuilder.Append(word1[k++]);
            }
            else if(i%2==1)
            {
                strBuilder.Append(word2[j++]);
            }
        }
        if (word1.Length < word2.Length)
        {
            for (int i = 0; i < word2.Length-word1.Length; i++)
            {
                strBuilder.Append(word2[j++]);
            }
        }
        else if(word1.Length > word2.Length)
        {
            for (int i = 0; i < word1.Length-word2.Length; i++)
            {
                strBuilder.Append(word1[k++]);
            }
        }

        return strBuilder.ToString();
    }
    // 作者：力扣官方题解
    //     链接：https://leetcode.cn/problems/merge-strings-alternately/solutions/1913930/jiao-ti-he-bing-zi-fu-chuan-by-leetcode-ac4ih/
    // 来源：力扣（LeetCode）
    // 著作权归作者所有。商业转载请联系作者获得授权，非商业转载请注明出处。
    public string OfficialMergeAlternately(string word1, string word2) {
        int m = word1.Length, n = word2.Length;
        int i = 0, j = 0;

        StringBuilder ans = new StringBuilder();
        while (i < m || j < n) {
            if (i < m) {
                ans.Append(word1[i]);
                ++i;
            }
            if (j < n) {
                ans.Append(word2[j]);
                ++j;
            }
        }
        return ans.ToString();
    }
}
