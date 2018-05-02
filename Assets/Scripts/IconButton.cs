using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using VRTK;

public class IconButton : Button {

    public IconItem iconItem;
  
    protected GameObject childTextObject;
    protected GameObject childImgObject;


    protected override void Start()
    {
        base.Start();
        UpdateGraphic();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        childTextObject.SetActive(true);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        childTextObject.SetActive(false);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        GameObject controller = VRTK_ControllerReference.GetControllerReference((uint)eventData.pointerId).scriptAlias.gameObject;
        controller.GetComponent<SpawnPoster>().Spawn(iconItem.icon);
        base.OnPointerClick(eventData);
    }

    void UpdateGraphic()
    {
        childTextObject = GetComponentInChildren<Text>(true).gameObject;
        childTextObject.SetActive(false);

        childImgObject = transform.parent.Find("Image").gameObject;

        if (iconItem.label != null) childTextObject.GetComponent<Text>().text = iconItem.label;
        if (iconItem.icon != null) childImgObject.GetComponent<Image>().sprite = iconItem.icon;
    }
}
