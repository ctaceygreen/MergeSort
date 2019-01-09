using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Solution
{

    // Complete the countInversions function below.
    static long countInversions(int[] arr)
    {

        //Plan is to implement merge sort and count how many times the Right element is chosen before the left.

        return MergeSortAndCountInversions(arr, new int[arr.Length]);

    }
    static long MergeSortAndCountInversions(int[] arr, int[] temp)
    {
        return MergeSortAndCountInversions(arr, temp, 0, arr.Length - 1);
    }
    static long MergeSortAndCountInversions(int[] arr, int[] temp, int leftStartIndex, int rightEndIndex)
    {
        if(leftStartIndex >= rightEndIndex)
        {
            return 0;
        }

        //Split array in half
        int middle = (leftStartIndex + rightEndIndex) / 2;

        //Merge sort left half
        long inv = MergeSortAndCountInversions(arr, temp, leftStartIndex, middle);

        //Merge sort right half
        inv+= MergeSortAndCountInversions(arr, temp, middle + 1, rightEndIndex);

        //Merge halves and count times right is chosen before left
        inv +=  MergeHalvesAndCountInversions(arr, temp, leftStartIndex, rightEndIndex);
        return inv;
    }

    static long MergeHalvesAndCountInversions(int[] arr, int[] temp, int leftStartIndex, int rightEndIndex)
    {
        int left = leftStartIndex;
        int leftEnd = (leftStartIndex + rightEndIndex) / 2;
        int right = leftEnd + 1;
        int index = leftStartIndex;
        long totalInversions = 0;

        while (left <= leftEnd && right <= rightEndIndex)
        {
            if (arr[left] <= arr[right])
            {
                temp[index] = arr[left];
                left++;
            }
            else
            {
                temp[index] = arr[right];
                totalInversions+=(leftEnd + 1 - left);
                right++;
            }
            index++;
        }

        if(left > leftEnd)
        {
            //Copy everything else in the right
            for(int i = right; i <= rightEndIndex; i++)
            {
                temp[index] = arr[i];
                index++;
            }
        }
        else
        {
            for(int i = left; i <= leftEnd; i++)
            {
                temp[index] = arr[i];
                index++;
            }
        }
        for(int i = leftStartIndex; i <= rightEndIndex; i++)
        {
            arr[i] = temp[i];
        }
        return totalInversions;
    }

    static void Main(string[] args)
    {
        //TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int t = Convert.ToInt32(Console.ReadLine());

        for (int tItr = 0; tItr < t; tItr++)
        {
            int n = Convert.ToInt32(Console.ReadLine());

            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), arrTemp => Convert.ToInt32(arrTemp))
            ;
            long result = countInversions(arr);

            //textWriter.WriteLine(result);
        }

        //textWriter.Flush();
        //textWriter.Close();
    }
}
