using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class mathutility {


    public static float AverageMean(this int[] array)
    {
        if (array.Length == 0)
        {
            return 0;
        }
        int sum = 0;
        for (int i = 0; i < array.Length; i++)
        {
            sum += array[i];
        }
        return (float)sum / array.Length;
    }

    public static float AverageMedian(this int[] array)
    {
        if (array.Length == 0)
        {
            return 0;
        }

        int[] clone = array.OrderBy(x => x).ToArray();

        if (array.Length % 2 == 0)
        {
            return (clone[array.Length / 2 - 1] + clone[array.Length / 2]) / 2f;
        }


        return clone[array.Length / 2];
    }

    public static int[] AverageMode(this int[] array)
    {
        if (array.Length == 0)
        {
            return new int[0];
        }
        //the first int represents the number, the second int represents how many times we have seen it

        Dictionary<int, int> keypairs = new();

        for (int i = 0; i < array.Length; i++)
        {
            if (!keypairs.TryAdd(array[i], 1))
            {
                keypairs[array[i]] += 1;
            }
        }

        List<int> answers = new();
        int currentmax = 0;

        foreach (var kfp in keypairs)
        {

            if (kfp.Value > currentmax)
            {
                answers.Clear();
                answers.Add(kfp.Key);
                currentmax = kfp.Value;

            } else if (kfp.Value == currentmax)
            {
                answers.Add(kfp.Key);
            }

        }

        return answers.ToArray();

    }

    public static int factorial (int num, int stop = 0)
    {
        
        if ((num) < 0 ||  stop < 0 )
        {
            throw new ArgumentException("factorial is not defined for negative numbers");
        }
        int result = 1;

        while (num > stop)
        {
            result *= num;
            num -= 1;

        }
        return result;
    }

  public static int statistics(int n, int k )
    {

        return factorial(n, n - k) / factorial(k);
    }
}
