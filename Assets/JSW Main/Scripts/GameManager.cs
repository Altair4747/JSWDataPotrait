using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject IConnect, ICare, ISolve, IExplore;
    GameObject questionBox;

    [HideInInspector]
    public string file;
    public RawImage[] rawImages;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        string a = PlayerPrefs.GetString("pic");
        LoadPNG(a);
    }
    public void GenerateQuestionByIndex(int questionIndex, string nameofobject)
    {
        DisableAllQuestionBox();

        if (nameofobject == IConnect.name) { questionBox = IConnect; }
        else if (nameofobject == ICare.name) { questionBox = ICare; }
        else if (nameofobject == ISolve.name) { questionBox = ISolve; }
        else if (nameofobject == IExplore.name) { questionBox = IExplore; }

        questionBox.transform.GetChild(questionIndex).gameObject.SetActive(true);
    }

    public void SetPathToUploadPic(string file){
        this.file = file;
    }

    void DisableAllQuestionBox(){
        IConnect.transform.GetChild(0).gameObject.SetActive(false);
        IConnect.transform.GetChild(1).gameObject.SetActive(false);

        ICare.transform.GetChild(0).gameObject.SetActive(false);
        ICare.transform.GetChild(1).gameObject.SetActive(false);

        ISolve.transform.GetChild(0).gameObject.SetActive(false);
        ISolve.transform.GetChild(1).gameObject.SetActive(false);

        IExplore.transform.GetChild(0).gameObject.SetActive(false);
        IExplore.transform.GetChild(1).gameObject.SetActive(false);
    }

     public void LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);
            rawImages[0].texture = tex;
           
        }

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
             SceneManager.LoadScene(0);
    }
}
