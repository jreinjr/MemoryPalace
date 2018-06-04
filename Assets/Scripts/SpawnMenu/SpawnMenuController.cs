using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SpawnMenuController : MonoBehaviour {
    public SpawnableInventory Inventory;
    public SpawnMenuViewport Viewport;
    public SpawnMenuScrollView ScrollView;
    public PageTabs pageTabs;
    public GameObject spawnButtonPrefab;
    public GameObject pageTabButtonPrefab;
    public GameObject spawnMenuPagePrefab;

    public SpawnMenuPage currentPage { get; private set; }

    private List<SpawnMenuPage> spawnMenuPages;

    private void Start()
    {
        Inventory.SpawnableAdded += SpawnableInventory_SpawnableAdded;
        Inventory.PageAdded += SpawnableInventory_PageAdded;
        spawnMenuPages = new List<SpawnMenuPage>();
    }

    private void SpawnableInventory_SpawnableAdded(object sender, SpawnableLoadedEventArgs e)
    {
        List<string> labels = spawnMenuPages.Select(o => o.name).ToList();                    
        SpawnMenuPage addToMenuPage = spawnMenuPages[labels.IndexOf(e.Page.label)];

        // Todo: refactor so SpawnButton is on the root of SpawnButtonPrefab
        SpawnButton newSpawnButton = Instantiate(spawnButtonPrefab, addToMenuPage.transform).GetComponentInChildren<SpawnButton>();
        addToMenuPage.mButtons.Add(newSpawnButton);
        newSpawnButton.Initialize(e.Item);
    }

    private void SpawnableInventory_PageAdded(object sender, InventoryPageAddedEventArgs e)
    {
        SpawnMenuPage newSpawnMenuPage = Instantiate(spawnMenuPagePrefab, Viewport.transform).GetComponent<SpawnMenuPage>();

        spawnMenuPages.Add(newSpawnMenuPage);
        newSpawnMenuPage.name = e.Page.label;

        Button newPageTabButton = Instantiate(pageTabButtonPrefab, pageTabs.transform).GetComponent<Button>();
        newPageTabButton.GetComponentInChildren<Text>().text = e.Page.label;
        newPageTabButton.onClick.AddListener(delegate { ChangePage(newSpawnMenuPage); });
    }


    public void ChangePage(SpawnMenuPage page)
    {
        if (currentPage != null) currentPage.gameObject.SetActive(false);
        page.gameObject.SetActive(true);
        // Sets the content field of the ScrollView to given SpawnMenuPag
        ScrollView.gameObject.GetComponent<ScrollRect>().content = page.gameObject.GetComponent<RectTransform>();
        currentPage = page;
    }
}
