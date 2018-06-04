using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;
using VRTK.UnityEventHelper;

namespace MemoryPalace
{
    // The MenuController loads items from the inventory and creates UI Gameobjects
    // It links the UI Gameobjects with the appropriate behaviors (e.g. spawn, switch tabs)
    public class MenuController : MonoBehaviour
    {
        // Required references
        public InventoryHandler inventoryHandler;
        public SpawnHandler spawnHandler;

        public Menu Menu { get; protected set; }
        public Transform MenuPageTabTransform;

        // Prefab of the whole scrollable menu page
        public GameObject MenuScrollViewPrefab;
        // Prefab of the button you click to change pages
        public GameObject MenuPageTabPrefab;
        // Prefab of the button you click to spawn items
        public GameObject MenuButtonPrefab;

        // Label of the current menu page
        public string currentMenuPage { get; protected set; }

        // These dictionaries link page labels of MenuPages to corresponding GameObjects
        Dictionary<string, GameObject> pageLabelScrollViewMap;
        Dictionary<string, GameObject> pageLabelPageTabMap;

        private void Start()
        {
            // Initialize menu
            Menu = new Menu();

            pageLabelScrollViewMap = new Dictionary<string, GameObject>();
            pageLabelPageTabMap = new Dictionary<string, GameObject>();

            // Set up callbacks
            Menu.RegisterInventoryPageAddedCallback(OnMenuPageAdded);
            MenuPage.RegisterInventoryItemAddedCallback(OnMenuItemAdded);

            // Add posters from inventory
            AddPostersToMenu();

           // AddWorldMenu();


        }

        // TODO: Automate this by getting list of folders from InventoryHandler
        void AddPostersToMenu()
        {
            MenuPage posterAJPage = Menu.AddPage("Posters A-J");
            
            List<SpawnMenuItem> posters = inventoryHandler.LoadPosters("Textures/A-J");
            foreach (SpawnMenuItem p in posters)
            {
                posterAJPage.AddItem(p);
            }
            
        }

        // When a MenuPage data object is added, create GameObjects for the page and its tab button
        void OnMenuPageAdded(string label)
        {
            MenuPage page = Menu.GetPage(label);

            // Menu.GetPage will have already called an error for us
            if (page == null) return;

            ///////////////////////////
            /// Add a scrollable page
            /////////////////////////// 
            GameObject newMenuScrollView = Instantiate(MenuScrollViewPrefab, this.transform);
            // If we have no active page, set this as it; otherwise, start hidden
            if (currentMenuPage == null)
            {
                currentMenuPage = label;
            }
            else
            {
                newMenuScrollView.SetActive(false);
            }
            // Link the MenuPage label to its newly created ScrollView
            pageLabelScrollViewMap.Add(label, newMenuScrollView);

            ///////////////////////////
            /// Add a tab to change pages
            ///////////////////////////
            GameObject newMenuPageTab = Instantiate(MenuPageTabPrefab, MenuPageTabTransform);
            // Set up text field of new MenuPageTab
            newMenuPageTab.GetComponentInChildren<Text>().text = label;
            // Link the PageTab button to change to the appropriate page
            newMenuPageTab.GetComponent<Button>().onClick.AddListener(() => { ChangeMenuPage(label); });
            // Link the MenuPage label to its newly created PageTab
            pageLabelPageTabMap.Add(label, newMenuPageTab);
        }


        void OnMenuItemAdded(string pageLabel, SpawnMenuItem item)
        {
            ///////////////////////////
            /// Add a menu item button
            /////////////////////////// 
            Transform parent = pageLabelScrollViewMap[pageLabel].transform.Find("Viewport/Content");
            MenuButton menuButton = Instantiate(MenuButtonPrefab, parent).GetComponent<MenuButton>();

            if(menuButton == null)
            {
                Debug.LogError("A MenuButtonPrefab was instantiated without the MenuButton component - no bueno");
                return;
            }

            // Link the menu button to a SpawnController method
            menuButton.onClick.AddListener(() => { spawnHandler.OnSpawnMenuButtonClicked(menuButton, item.ObjectInitData); });
            menuButton.Label = item.Label;
            menuButton.Background = item.Icon;
            
        }


        public void ChangeMenuPage(string label)
        {
            MenuPage page = Menu.GetPage(label);
            if (page == null)
            {
                Debug.LogErrorFormat("Unable to change menu to page {0}: page not found", label);
                return;
            }
            else if (label == currentMenuPage)
            {
                Debug.Log("Attempting to change to current menu page");
                return;
            }
            pageLabelScrollViewMap[currentMenuPage].SetActive(false);
            pageLabelScrollViewMap[label].SetActive(true);
            // Update currentMenuPage parameter
            currentMenuPage = label;
        }

        void OnMenuPageRemoved(string label)
        {
            
        }
    }
}