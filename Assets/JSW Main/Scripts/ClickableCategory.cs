using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClickableCategory : MonoBehaviour
{
   private Button clickableButton;
   public Category category;

    private void Awake()
    {
        clickableButton = GetComponent<Button>();
        clickableButton.onClick.AddListener(() => { SetCategory(); });
    }

    public void SetCategory()
    {
        GameManager.Instance.GenerateQuestionByIndex(0, category.QuestionBoxName);
    }
}
