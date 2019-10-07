using Doozy.Engine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    public float delayInSeconds;
    public string currentText;
    public string fullText;

    public GameObject playerImageGo;
    public GameObject otherImageGo;

    public GameObject yesBtn;
    public GameObject noBtn;

    private Image _playerImage;
    private Image _otherImage;

    private int _i = 0;

    private TextMeshProUGUI _tmp;

    private void Awake()
    {
        _tmp = GetComponent<TextMeshProUGUI>();
        _playerImage = playerImageGo.GetComponent<Image>();
        _otherImage = otherImageGo.GetComponent<Image>();
    }

    // ---------------------------------------------------------------------

    public void PrintText(string text, StoryCharacter speaker)
    {
        yesBtn.SetActive(false);
        noBtn.SetActive(false);

        fullText = text;
        currentText = "";
        _i = 0;

        if (speaker.IsPlayer)
        {
            playerImageGo.SetActive(true);
            otherImageGo.SetActive(false);
        }
        else
        {
            _otherImage.sprite = speaker.portraitSprite;
            playerImageGo.SetActive(false);
            otherImageGo.SetActive(true);
        }

        StartCoroutine(WriteText());
    }

    // ---------------------------------------------------------------------

    public void ShowChoices()
    {
        _tmp.text = "";
        fullText = "";
        currentText = "";
        yesBtn.SetActive(true);
        noBtn.SetActive(true);
        playerImageGo.SetActive(false);
        otherImageGo.SetActive(false);
    }

    // ---------------------------------------------------------------------

    IEnumerator WriteText()
    {
        for (_i = 0; _i < fullText.Length; _i++)
        {
            currentText = fullText.Substring(0, _i);
            _tmp.text = currentText;
            yield return new WaitForSeconds(delayInSeconds);
        }
    }
}
