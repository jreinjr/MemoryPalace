using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace
{
    // The inventory represents available resources that can be loaded with the MenuController
    // and spawned with the SpawnController
    public class InventoryHandler : MonoBehaviour
    {
        // Maps filename (used as ID) to a sprite
        public Dictionary<string, Sprite> idSpriteMap { get; protected set; }

        // Load all posters in given filepath
        // TODO: Get rid of static function
        public List<SpawnMenuItem> LoadPosters(string filePath)
        {
            List<SpawnMenuItem> posters = new List<SpawnMenuItem>();
            string posterPrefabPath = "Prefabs/Poster";

            Object[] sprites = Resources.LoadAll(filePath, typeof(Sprite));
            foreach (Object s in sprites)
            {
                Sprite sprite = (Sprite)s;

                idSpriteMap.Add(s.name, sprite);

                PosterData newPosterData = new PosterData(posterPrefabPath, s.name, sprite.texture);

                // TODO : Parse name (human-readable) into something prettier?
                SpawnMenuItem newPoster = new SpawnMenuItem(s.name, sprite, newPosterData);
                posters.Add(newPoster);
            }

            return posters;
        }

    }
}