using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using VRTK;

public class AssetMenuButton : Button {

    public AssetItem assetItem;


    protected GameObject childTextObject;
    protected GameObject childImgObject;


    protected override void Start()
    {
        base.Start();
        UpdateGraphic();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        GameObject controller = VRTK_ControllerReference.GetControllerReference((uint)eventData.pointerId).scriptAlias.gameObject;
        controller.GetComponent<SpawnAsset>().Spawn(assetItem);
        assetItem.reference.Spawn();
        base.OnPointerClick(eventData);
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

    void UpdateGraphic()
    {
        childTextObject = GetComponentInChildren<Text>(true).gameObject;
        childTextObject.SetActive(false);

        childImgObject = transform.parent.Find("Image").gameObject;

        if (assetItem.label != null) childTextObject.GetComponent<Text>().text = assetItem.label;
        if (assetItem.icon != null) childImgObject.GetComponent<Image>().sprite = assetItem.icon;
    }

}
