using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnableInventoryPage
{
    private List<ISpawnableItem> mItems = new List<ISpawnableItem>();
    public string label { get; private set; }

    public SpawnableInventoryPage(string label)
    {
        this.label = label;
    }

    public void AddItem(ISpawnableItem item)
    {
        mItems.Add(item);
    }

    public List<ISpawnableItem> GetList()
    {
        return mItems;
    }
}

public class SpawnableLoadedEventArgs : EventArgs
{
    public SpawnableLoadedEventArgs(ISpawnableItem item, SpawnableInventoryPage page)
    {
        Item = item;
        Page = page;
    }
    public ISpawnableItem Item;
    public SpawnableInventoryPage Page;
}

public class InventoryPageAddedEventArgs : EventArgs
{
    public InventoryPageAddedEventArgs(SpawnableInventoryPage page)
    {
        Page = page;
    }
    public SpawnableInventoryPage Page;
}

public class SpawnableInventory : MonoBehaviour {

    private List<SpawnableInventoryPage> mPages = new List<SpawnableInventoryPage>();

    // List of loader objects to populate menu on start
    public List<SpawnableLoader> spawnableLoaders;

    // Called every time a spawnable is added to a menu page
    public event EventHandler<SpawnableLoadedEventArgs> SpawnableAdded;

    // Called every time a new menu page is created
    public event EventHandler<InventoryPageAddedEventArgs> PageAdded;

    public void AddItem(ISpawnableItem item, SpawnableInventoryPage page)
    {
        page.AddItem(item);
        if (SpawnableAdded != null)
        {
            SpawnableAdded(this, new SpawnableLoadedEventArgs(item, page));
        }
    }

    public void AddList(List<ISpawnableItem> items, SpawnableInventoryPage page)
    {
        foreach (ISpawnableItem i in items)
        {
            AddItem(i, page);
        }
    }

    public SpawnableInventoryPage AddPage(string label)
    {
        SpawnableInventoryPage newPage = new SpawnableInventoryPage(label);
        mPages.Add(newPage);
        if (PageAdded != null)
        {
            PageAdded(this, new InventoryPageAddedEventArgs(newPage));
        }
        return newPage;
    }

    public void Load()
    {
        // Loop through lists of SpawnableLoaders and add them to SpawnableInventory
        for (int i = 0; i < spawnableLoaders.Count; i++)
        {
            SpawnableInventoryPage pageToAdd;
            List<string> pageLabels = mPages.Select(o => o.label).ToList();
            int pageFoundIndex = pageLabels.IndexOf(spawnableLoaders[i].label);
            // If no page with this label already exists, make a new one
            if (pageFoundIndex < 0)
            {
                pageToAdd = AddPage(spawnableLoaders[i].label);

            }
            // Else, to the existing page
            else
            {
                pageToAdd = mPages[pageFoundIndex];
            }
            AddList(spawnableLoaders[i].Load(), pageToAdd);
        }
        // Clear load on start list after load
        spawnableLoaders.Clear();
    }

    private void Start()
    {
    }
}
