#pragma warning disable RCS1110
using System;
using System.Collections.Generic;
using System.Linq;

public static class System___Collections___Generic__Queue
{
    public static void RemoveAll<T>(this Queue<T> queue, Func<T, bool> predicate)
    {
        Queue<T> temp = new Queue<T>(queue.Where(predicate));
        queue.Clear();
        temp.ToList().ForEach(n => queue.Enqueue(n));
        temp.Clear();
    }
}