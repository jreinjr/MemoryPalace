using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace
{
    public class Menu
    {
        List<MenuPage> pages;
        static Action<string> cbMenuPageAdded;

        public Menu()
        {
            pages = new List<MenuPage>();
        }

        // Raises MenuPageAdded callback when Page is added.
        public MenuPage AddPage(string label)
        {
            List<string> labels = pages.Select(page => page.label).ToList();
            if (label == null)
            {
                Debug.LogError("Trying to pass a null string to Menu.AddPage");
                return null;
            }
            else if (labels.Contains(label))
            {
                Debug.LogErrorFormat("A page with the label {0} already exists", label);
                return null;
            }

            MenuPage newPage = new MenuPage(label);

            pages.Add(newPage);
            if (cbMenuPageAdded != null)
            {
                cbMenuPageAdded(label);
            }
            return newPage;
        }

        public MenuPage GetPage(string label)
        {
            List<string> labels = pages.Select(page => page.label).ToList();
            if (labels.Contains(label) == false)
            {
                Debug.LogErrorFormat("No page with the label {0} was found in the menu", label);
                return null;
            }
            // Get a MenuPage from pages by label
            return pages.First(page => page.label == label);
        }

        public static void RegisterInventoryPageAddedCallback(Action<string> callback)
        {
            cbMenuPageAdded += callback;
        }
        public static void UnregisterInventoryPageAddedCallback(Action<string> callback)
        {
            cbMenuPageAdded -= callback;
        }
    }
}

