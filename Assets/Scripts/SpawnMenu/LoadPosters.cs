using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPosters : SpawnableLoader {

    public GameObject posterPrefab;
    public string filePath;

    public override string label
    {
        get
        {
            return "Posters";
        }
    }

    // Creates a Poster for each Texture in filePath and returns their list
    public override List<ISpawnableItem> Load()
    {
        List<ISpawnableItem> items = new List<ISpawnableItem>();

        Object[] textures = Resources.LoadAll(filePath, typeof(Sprite));
        foreach (Object s in textures)
        {
            Poster newPoster = new Poster();
            newPoster.Name = s.name;
            newPoster.Image = s as Sprite;
            items.Add(newPoster);
        }
        return items;
    }
}
