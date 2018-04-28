using System.Collections;
using System.Collections.Generic;
using System.Web;
using UnityEngine;

public class WebSearch : MonoBehaviour {

    
    [SerializeField]
    private GameObject displayImages;
    private Renderer[] displayImage;

    private void Start()
    {
        displayImage = displayImages.GetComponentsInChildren<Renderer>();
    }


    public void SearchForKeyword(string keyword)
    {
        StartCoroutine(Search(KeywordToSearchURL(WWW.EscapeURL(keyword))));
    }

    public string KeywordToSearchURL(string keyword)
    {
        string url = "https://www.startpage.com/do/asearch?cmd=process_search&language=english&enginecount=11&pl=&abp=1&ff=&theme=&flag_ac=0&lui=english&cat=pics&ycc=0&flimgsize=isz%3Am&flimgcolor=&flimgtype=jpg&t=air&nj=0&query=" + keyword;
        Debug.Log(url);
        return url;
    }

    IEnumerator Search(string url)
    {
        using (WWW www = new WWW(url))
        {
            yield return www;
            string returnStr = www.text;
            string startString = "THUMBURL\":\"";
            string endString = "\"";
            string[] subStrings = returnStr.Split(new string[] { startString }, System.StringSplitOptions.None);
            for (int i = 0; i < displayImage.Length; i++)
            {
                returnStr = subStrings[i + 1];
                int endIndex = returnStr.IndexOf(endString);
                returnStr = returnStr.Substring(0, endIndex);
                StartCoroutine(GetImageFromURL(returnStr, i));
            }
            /*
            int startIndex = www.text.IndexOf(startString);
            returnStr = returnStr.Substring(startIndex + startString.Length);
            
            int endIndex = returnStr.IndexOf(endString);
            returnStr = returnStr.Substring(0, endIndex);
            StartCoroutine(GetImageFromURL(returnStr));
            */
        }
    }

    IEnumerator GetImageFromURL(string url, int index)
    {
        using (WWW www = new WWW(url))
        {
            yield return www;

            displayImage[index].material.mainTexture = www.texture;
        }
    }
}
