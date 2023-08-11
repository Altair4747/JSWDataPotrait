using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class TakeSelfie : MonoBehaviour
{
    [SerializeField] RawImage renderShow;

    WebCamTexture webCamTexture;
    [SerializeField] public string Username;

    public RawImage selfieImage;
    int i = 0;

    private void OnEnable()
    {
        webCamTexture = new WebCamTexture();
        renderShow.texture = webCamTexture;
       webCamTexture.Play();
    }
    
    public void SetUserName(string name){
        Username = name;
    }

    public void StartCamera()
    {
        webCamTexture = new WebCamTexture();
        renderShow.texture = webCamTexture;
        webCamTexture.Play();
    }
    public void TakePic()
    {
        StartCoroutine(TakePhoto());
    }

    public void ClearTexture(Texture s){
        Debug.Log("we have  ");
        renderShow.texture = null;
        renderShow.texture = s;
    }
    IEnumerator TakePhoto()
    {
        yield return new WaitForEndOfFrame();

        renderShow.material.mainTexture = null;
        selfieImage.texture = webCamTexture;
        selfieImage.gameObject.SetActive(true);

        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels()); 
        photo.Apply();
        webCamTexture.Stop();
        i++;
       
        byte[] bytes = photo.EncodeToPNG();
        string file = Path.Combine(Application.dataPath, Username + "" + i + ".png");
        File.WriteAllBytes(file, bytes);
        //yield return new WaitForSeconds(4f);
        CMDHandler.instance.ExecuteCommand(file, i);
       // UIController.instance.SetFilePath(file);
     //  gameObject.SetActive(false);
    }

   
}
