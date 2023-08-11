using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System;
using System.IO;

public class CMDHandler : MonoBehaviour
{
    public static CMDHandler instance;
    public string API_KEY;

    string filepath;
    private void Awake()
    {
        instance = this;
    }
    

    public void ExecuteCommand(string file, int i)
    {
        string fullPath = Path.Combine(Application.dataPath, "removebg.exe");
        filepath = file;

        ProcessStartInfo startInfo = new ProcessStartInfo(fullPath);
        startInfo.WindowStyle = ProcessWindowStyle.Normal;
        //startInfo.Arguments = "--api-key hs1Z7dB7dCkFwwJWFm2LXMGX " + Application.dataPath + "/" + FindObjectOfType<TakeSelfie>().Username + ".png";
        startInfo.Arguments = "--api-key " + API_KEY + " " + filepath;

        Process.Start(startInfo);

        StartCoroutine(CropImage(i));
        //hs1Z7dB7dCkFwwJWFm2LXMGX
    }

    IEnumerator CropImage(int i)
    {
        yield return new WaitForSeconds(4f);
        UIController.instance.SetFilePath(Application.dataPath + "/" + FindObjectOfType<TakeSelfie>().Username + "" + i + "-removebg.png");
        FindObjectOfType<TakeSelfie>().gameObject.SetActive(false);
    }
   
}
