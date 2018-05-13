using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace
{
    // The inventory represents available resources that can be loaded with the MenuController
    // and spawned with the SpawnController
    public class InventoryHandler : MonoBehaviour
    {
        // Load all posters in given filepath
        // TODO: Get rid of static function
        public List<SpawnMenuItem> LoadPosters(string texturePath)
        {
            List<SpawnMenuItem> posters = new List<SpawnMenuItem>();
            string posterPrefabPath = Poster.PrefabPath;

            Object[] sprites = Resources.LoadAll(texturePath, typeof(Sprite));
            foreach (Object s in sprites)
            {
                Sprite sprite = (Sprite)s;

                PosterInitData newPosterData = new PosterInitData(posterPrefabPath, s.name, sprite.texture);

                // TODO : Parse name (human-readable) into something prettier?
                SpawnMenuItem newPoster = new SpawnMenuItem(s.name, sprite, newPosterData);
                posters.Add(newPoster);
            }

            return posters;
        }

    }
}