using System;
using System.Collections.Generic;
using System.Linq;

public static class EnumerableExtensions
{
    public static T? GetMax<T>(this IEnumerable<T> collection, Func<T, float> convertToNumber) where T : class
    {
        if (collection == null || !collection.Any())
            throw new ArgumentException("Collection is empty or null");

        T? maxElement = null;
        float maxValue = float.MinValue;

        foreach (var item in collection)
        {
            float value = convertToNumber(item);
            if (value > maxValue)
            {
                maxValue = value;
                maxElement = item;
            }
        }

        return maxElement;
    }
}
