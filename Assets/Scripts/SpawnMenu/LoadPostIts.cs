using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPostIts : SpawnableLoader {

    public GameObject postItPrefab;
    public Color[] colors;

    public override string label
    {
        get
        {
            return "PostIts";
        }
    }

    // Creates a PostIt for each Color in colors and returns their list
    public override List<ISpawnableItem> Load()
    {
        List<ISpawnableItem> items = new List<ISpawnableItem>();

        foreach (Color c in colors)
        {
            PostIt newPostIt = new PostIt(c.ToString(), c);
            items.Add(newPostIt);
        }
        return items;
    }

}
