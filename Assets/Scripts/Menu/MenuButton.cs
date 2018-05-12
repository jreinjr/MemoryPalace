using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButton : Button {

    private string _label;
    public string Label
    {
        get
        {
            return _label;
        }
        set
        {
            _label = value;
            transform.Find("Text").GetComponent<Text>().text = _label;
        }

    }

    private Sprite _background;
    public Sprite Background
    {
        get
        {
            return _background;
        }
        set
        {
            _background = value;
            transform.Find("Image").GetComponent<Image>().sprite = _background;
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerId);
        base.OnPointerClick(eventData);
    }

}
