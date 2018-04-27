using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ImageButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField]
    private string label;
    [SerializeField]
    private Sprite img;
  
    private GameObject childTextObject;
    private GameObject childImgObject;

    public ImageButton(string label, Sprite img)
    {
        this.label = label;
        this.img = img;
    }

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
