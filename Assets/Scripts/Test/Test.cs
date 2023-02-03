using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Test : MonoBehaviour
{
    //[SerializeField] List<string> data = new List<string> { "fname", "lname", "home", "home", "company" };
    private void Start()
    {
        int[] names = new int[] { 1,2,2,4,1 };

        var duplicatesWithIndices = names
            // Associate each name/value with an index
            .Select((Name, Index) => new { Name, Index })
            // Group according to name
            .GroupBy(x => x.Name)
            //descending order
            // Only care about Name -> {Index1, Index2, ..}
            .Select(xg => new {
                Name = xg.Key,
                Indices = xg.Select(x => x.Index)
            })
            .OrderByDescending(x => x.Name)
            // And groups with more than one index represent a duplicate key
            .Where(x => x.Indices.Count() > 1);

        // Now, duplicatesWithIndices is typed like:
        //   IEnumerable<{Name:string,Indices:IEnumerable<int>}>
        // Let's say we print out the duplicates (the ToArray is for .NET 3.5):
        foreach (var g in duplicatesWithIndices)
        {
            if (duplicatesWithIndices.First() == g)
            {
                Debug.Log("Have duplicate " + g.Name + " with indices " +
                string.Join(",", g.Indices.ToArray()));
            }
            else
                break;
        }

        // The output for the above input is:
        // > Have duplicate a with indices 0,1,4
        // > Have duplicate b with indices 3,5
    }
}
