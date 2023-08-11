using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Unity.VisualScripting;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    // SCREEN 3 BEGIN
    public TMP_InputField inputField;
    public TMP_Text EnterText;
    public GameObject Screen2;
    public Button NextButton;
    public Button BackButton;
    public Button readyButton;
    public string NameOfTheStudent;
    public int AgeOfStudent;
    public GameObject screen3;
    // SCREEN 3 END

    // screen 4 Begin
    public GameObject screen4;
    public Button cameraClickBtn;
    public TMP_Text HeaderText;
    public TMP_Text BottomText;
    public TMP_Text countText;
    public TakeSelfie takeSelfie;
    private int timer = 3;
    public GameObject screen5;
    public Button retakeButton;
    public Texture camershowsprite;
    // Screen 4 end


    // Screen 5 begin
    public Button YesBtn;
    public GameObject screen6;
    public RawImage capturedImage;
    // Screen 5 End

    // Answer Submit Begin
    public RawImage CurrentUserImage;
    public GameObject capturedimageparent;
    public RawImage TemporaryImage;
    public string[] Allcomments;
    public TMP_Text commentText;
    public GameObject hourglass, partyicon, postbutton, header;
    [SerializeField] int timetoWait = 3;

    private void Awake()
    {
        instance = this;

        NextButton.onClick.AddListener(() => { NextButtonPressed(); });
        BackButton.onClick.AddListener(() => { BackButtonPressed(); });
        readyButton.onClick.AddListener(() => { ReadyButtonPressed(); });
        cameraClickBtn.onClick.AddListener(() => { CameraButtonPressed(); });
        retakeButton.onClick.AddListener(() => { RetakeButtonPressed(); });
        YesBtn.onClick.AddListener(() => { YesBpressed(); });
    }
    void Start()
    {
        timer = timetoWait;
    }
    public void BackButtonPressed()
    {
        inputField.text = string.Empty;
        NameOfTheStudent = string.Empty;
        AgeOfStudent = 0;
        EnterText.text = "Let's Begin with your name";
        Screen2.SetActive(true);
        screen3.SetActive(false);
        NextButton.gameObject.SetActive(true);
        readyButton.gameObject.SetActive(false);
    }

    public void NextButtonPressed()
    {
        if (string.IsNullOrEmpty(inputField.text))
            return;

        
        NameOfTheStudent = inputField.text;
        takeSelfie.GetComponent<TakeSelfie>().SetUserName(NameOfTheStudent);
        inputField.text = string.Empty;
        EnterText.text = string.Empty;
        NextButton.gameObject.SetActive(false);
        readyButton.gameObject.SetActive(true);
        EnterText.text = "How old are you ?";
    }

    public void ReadyButtonPressed()
    {
        int AgeCheck = int.Parse(inputField.text);
        if (!string.IsNullOrEmpty(inputField.text) && (AgeCheck > 3 && AgeCheck <= 20))
        {
            AgeOfStudent = int.Parse(inputField.text);
            inputField.text = string.Empty;
            EnterText.text = "Let's Begin\r\nwith your name";
            screen3.SetActive(false);
            screen4.SetActive(true);
        }


    }

    public void CameraButtonPressed()
    {
        HeaderText.text = "Make sure your face\nis inside the circle";
        BottomText.text = string.Empty;
       // takeSelfie.gameObject.SetActive(false);
       // takeSelfie.gameObject.SetActive(false);
        takeSelfie.gameObject.SetActive(true);

        cameraClickBtn.gameObject.SetActive(false);
        countText.gameObject.SetActive(true);
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        while (timer != 0)
        {
            timer--;
            countText.text = timer.ToString();
            if (timer == 0)
            {
                HeaderText.text = "Snap";
                screen4.SetActive(false);
                screen5.SetActive(true);
                StopAllCoroutines();
                takeSelfie.TakePic();
            }

            yield return new WaitForSeconds(1f);
        }

    }

    public void RetakeButtonPressed()
    {
        screen5.SetActive(false);
        screen4.SetActive(true);
        takeSelfie.ClearTexture(camershowsprite);
        cameraClickBtn.gameObject.SetActive(true);
        countText.gameObject.SetActive(false);
        timer = timetoWait;
        HeaderText.text = "Now lets take your photograph";
        BottomText.text = "We're going to count down 10s\n before taking your photograpth.\r\n";
    }

    public void YesBpressed()
    {
        screen5.SetActive(false);
        screen6.SetActive(true);
    }

    public void SetFilePath(string path)
    {
        //PlayerPrefs.SetString("pic", path);
        LoadPNG(path);
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
            CurrentUserImage.texture = tex;
            TemporaryImage.transform.GetChild(0).GetComponent<RawImage>().texture = tex;

           
        }

    }

    public void SetComments()
    {
        StartCoroutine(CommentingOnPic());
    }

    IEnumerator CommentingOnPic()
    {
        yield return new WaitForSeconds(1f);
        commentText.text = Allcomments[0];

        yield return new WaitForSeconds(2f);
        commentText.text = Allcomments[1];

        yield return new WaitForSeconds(3f);
        commentText.text = Allcomments[2];

        yield return new WaitForSeconds(4f);
        commentText.text = Allcomments[3];

        hourglass.SetActive(false);
        partyicon.SetActive(true);
        postbutton.SetActive(true);
        header.SetActive(false);
        capturedimageparent.gameObject.SetActive(true);
        TemporaryImage.gameObject.SetActive(false);

        FindObjectOfType<ScreenShotHandler>().TakeScreenShoot();

        yield return new WaitForSeconds(2f);
        GameManager.Instance.SetPathToUploadPic(FindObjectOfType<ScreenShotHandler>().ScreenshotPath);
        FindObjectOfType<Example>().sendEmail();
    }
}
