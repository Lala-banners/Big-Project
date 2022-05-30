using System.Collections.Generic;

using System;

public static class ListSortExt
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Make sure the type can be used and needs to have comparator.</typeparam>
    /// <param name="_list"></param>
    public static void BubbleSort<T>(this List<T> _list) where T : IComparable 
    {
        T temp;

        for (int j = 0; j <= _list.Count - 2;  j++)
        {
            for(int i = 0; i <= _list.Count - 2; i++)
            {
                //IComparables for both
                IComparable first = _list[i];
                IComparable second = _list[i + 1]; //Object passed in

                int comparison = first.CompareTo(second);

                //First is after second
                if(comparison > 0)
                {
                    temp = _list[i + 1];
                    _list[i + 1] = _list[i];
                    _list[i] = temp;
                }
            }
        }
    }
}
