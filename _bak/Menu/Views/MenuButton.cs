using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using VRTK;

// Todo: refactor so SpawnButton is on the root of SpawnButtonPrefab
public class MenuButton : Button {

    public static Rect iconSize = new Rect(0, 0, 256, 256);

    public ISpawnableItem item { get; set; }

    protected Text label;
    protected Image icon;
    


    public void Initialize(ISpawnableItem item)
    {
        // Sets reference to label and hides it to start
        label = GetComponentInChildren<Text>(true);
        label.gameObject.SetActive(false);
        // Sets reference to icon image
        icon = transform.parent.Find("Image").GetComponentInChildren<Image>();
        // Sets label text using spawnable item name
        label.text = item.Name;
        // Sets icon image using spawnable item icon
        icon.sprite = item.Image;
        // Sets SpawnableItem to given item
        this.item = item;
    }

    // Spawn item on click
    public override void OnPointerClick(PointerEventData eventData)
    {
        GameObject controller = VRTK_ControllerReference.GetControllerReference((uint)eventData.pointerId).scriptAlias.gameObject;
        PointerSpawnHandler spawnHandler = controller.GetComponent<PointerSpawnHandler>();
        if (spawnHandler != null)
        {
            spawnHandler.SpawnItem(item);
        }
        
        base.OnPointerClick(eventData);
    }

    // Show label on mouse over
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        label.gameObject.SetActive(true);
    }

    // Hide label on mouse exit
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        label.gameObject.SetActive(false);
    }

}
