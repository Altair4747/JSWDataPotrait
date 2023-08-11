using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerSelectionBanner : MonoBehaviour
{
   public Texture Banner;
   public RawImage capturedImage;
   public Image bgImage;

    Color color1;

    public string hexcode;

   public void SetBanner(){
 
        capturedImage.transform.GetChild(0).GetComponent<Image>().enabled = true;
        capturedImage.transform.GetChild(0).GetComponent<Image>().material.mainTexture = Banner;

        ColorUtility.TryParseHtmlString(hexcode, out color1);
        bgImage.color = color1;

    }
}
