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
    public GameObject MenuButtonPrefab;
    public GameObject pageTabButtonPrefab;
    public GameObject MenuPagePrefab;

    public MenuPage currentPage { get; private set; }

    private List<MenuPage> MenuPages;

    private void Start()
    {
        Inventory.SpawnableAdded += SpawnableInventory_SpawnableAdded;
        Inventory.PageAdded += SpawnableInventory_PageAdded;
        MenuPages = new List<MenuPage>();
    }

    private void SpawnableInventory_SpawnableAdded(object sender, SpawnableLoadedEventArgs e)
    {
        List<string> labels = MenuPages.Select(o => o.name).ToList();                    
        MenuPage addToMenuPage = MenuPages[labels.IndexOf(e.Page.label)];

        // Todo: refactor so MenuButton is on the root of MenuButtonPrefab
        MenuButton newMenuButton = Instantiate(MenuButtonPrefab, addToMenuPage.transform).GetComponentInChildren<MenuButton>();
        addToMenuPage.mButtons.Add(newMenuButton);
        newMenuButton.Initialize(e.Item);
    }

    private void SpawnableInventory_PageAdded(object sender, InventoryPageAddedEventArgs e)
    {
        MenuPage newMenuPage = Instantiate(MenuPagePrefab, Viewport.transform).GetComponent<MenuPage>();

        MenuPages.Add(newMenuPage);
        newMenuPage.name = e.Page.label;

        Button newPageTabButton = Instantiate(pageTabButtonPrefab, pageTabs.transform).GetComponent<Button>();
        newPageTabButton.GetComponentInChildren<Text>().text = e.Page.label;
        newPageTabButton.onClick.AddListener(delegate { ChangePage(newMenuPage); });
    }


    public void ChangePage(MenuPage page)
    {
        if (currentPage != null) currentPage.gameObject.SetActive(false);
        page.gameObject.SetActive(true);
        // Sets the content field of the ScrollView to given SpawnMenuPag
        ScrollView.gameObject.GetComponent<ScrollRect>().content = page.gameObject.GetComponent<RectTransform>();
        currentPage = page;
    }
}
