using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;

public class ScreenShotHandler : MonoBehaviour
{
   public RectTransform AreaToCapture;
    public string FileName; //filename needs the direction, name & extension, ex: \varios\myphoto.png

    
    public string ScreenshotPath;
    public enum RectMode { RectStretched, RectCentered }
    public RectMode RectTransformMode;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Process.Start("cmd.exe");
        }
    }
    public void TakeScreenShoot()
    {
        StartCoroutine(coroutineTakeScreenShoot());
    }
 
    private IEnumerator coroutineTakeScreenShoot()
    {
        yield return new WaitForEndOfFrame();
 
        var width = 0;
        var height = 0;
        var startX = 0f;
        var startY = 0f;
 
        if (RectTransformMode == RectMode.RectStretched)
        {
            //these var are correct only for a panel setted to stretch
            width = (int)AreaToCapture.rect.width;
            height = (int)AreaToCapture.rect.height;
            startX = AreaToCapture.offsetMin.x;
            startY = AreaToCapture.offsetMin.y;
 
            var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
            Rect rex = new Rect(startX, startY, width, height);
 
            tex.ReadPixels(rex, 0, 0);
            tex.Apply();
            // Encode texture into PNG
            var bytes = tex.EncodeToPNG();
            Destroy(tex);
            File.WriteAllBytes(FileName, bytes);
        }
 
        if (RectTransformMode == RectMode.RectCentered)
        {
            /*Debug.Log("tamaño y: " + AreaToCapture.rect.height);
            Debug.Log("tamaño x: " + AreaToCapture.rect.width);
            Debug.Log("localposition x: " + AreaToCapture.transform.localPosition.x);
            Debug.Log("localposition y: " + AreaToCapture.transform.localPosition.y);
            Debug.Log("offsetMin y: " + AreaToCapture.offsetMin.y);
            Debug.Log("offsetMin y: " + -AreaToCapture.offsetMin.y);*/
 
            Vector2 min = AreaToCapture.anchorMin;
            min.x *= Screen.width;
            min.y *= Screen.height;
 
            min += AreaToCapture.offsetMin;
 
            Vector2 max = AreaToCapture.anchorMax;
            max.x *= Screen.width;
            max.y *= Screen.height;
 
            max += AreaToCapture.offsetMax;
 
            //Debug.Log(min + " " + max);
 
            //these var are correct only for a panel setted to stretch
            width = (int)AreaToCapture.rect.width;
            height = (int)AreaToCapture.rect.height;
            startX = min.x;
            startY = min.y;
 
            var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
            Rect rex = new Rect(startX, startY, width, height);
 
            tex.ReadPixels(rex, 0, 0);
            tex.Apply();
            // Encode texture into PNG
            var bytes = tex.EncodeToPNG();
            Destroy(tex);
            ScreenshotPath = Path.Combine(Application.persistentDataPath, UIController.instance.NameOfTheStudent + ".png");
           
            File.WriteAllBytes(ScreenshotPath, bytes);
        }
 
       
    }
}

