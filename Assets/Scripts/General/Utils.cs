using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public enum GetExtremeFromCollectionParam
    {
        Minimum,
        Maximum
    }
    public static T GetExtremeFromCollection<T>(IList<T> collection, System.Func<T, float> getValue, GetExtremeFromCollectionParam param)
    {
        if (collection.Count == 0) return default(T);

        float extremeValue = param == GetExtremeFromCollectionParam.Maximum ? float.MinValue : float.MaxValue;
        T extremeItem = collection[0];

        foreach (T item in collection)
        {
            float value = getValue(item);
            bool moreExtreme = (param == GetExtremeFromCollectionParam.Maximum && value > extremeValue) || (param == GetExtremeFromCollectionParam.Minimum && value < extremeValue);
            if (moreExtreme)
            {
                extremeValue = value;
                extremeItem = item;
            }
        }

        return extremeItem;
    }
}
