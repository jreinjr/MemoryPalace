using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using VRTK;

public class ImageButton : VRTK_UIDraggableItem, IPointerEnterHandler, IPointerExitHandler {

    public string label;
    public Sprite img;
  
    private GameObject childTextObject;
    private GameObject childImgObject;


    private void Start()
    {
        UpdateGraphic();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        childTextObject.SetActive(true);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        childTextObject.SetActive(false);
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        // Gets our current sibling index (for the new instance to use)
        int siblingIndex = transform.GetSiblingIndex();
        GameObject newInstance = Instantiate(gameObject, transform.parent, true);
        // Sets new instance sibling index to original object index
        newInstance.transform.SetSiblingIndex(siblingIndex);
        // Sets original object index to end of list
        this.transform.SetSiblingIndex(transform.parent.childCount);
        base.OnBeginDrag(eventData);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        Destroy(gameObject);
    }

    private void OnValidate()
    {
        UpdateGraphic();
    }

    void UpdateGraphic()
    {
        childTextObject = GetComponentInChildren<Text>(true).gameObject;
        childTextObject.SetActive(false);

        childImgObject = transform.Find("Image").gameObject;

        if (label != null) childTextObject.GetComponent<Text>().text = label;
        if (img != null) childImgObject.GetComponent<Image>().sprite = img;
    }
}
