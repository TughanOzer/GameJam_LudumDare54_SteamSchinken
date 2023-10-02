using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroText : MonoBehaviour
{
    #region Fields

    [SerializeField] private TextMeshProUGUI _introText;

    private readonly string _text1 = "Your brother is missing and was last seen around this area. Use your invisibility cloak to reach him. However, since you were too stingy to buy the full priced version, you have to do with only 3 limited uses.";

    private List<char> _charList = new List<char>();
    private string[] _introTexts;
    private string _tempText;

    private int _currentText = 0;
    private bool keyDown;

    #endregion


    private void Awake()
    {
        _introTexts = new string[] { _text1 };
    }

    // Start is called before the first frame update
    void Start()
    {      
        ReturnCharList(_text1);
        StartCoroutine(PrintSlow());
        _currentText++;
    }


    private void Update()
    {
        if (Input.anyKeyDown)
        {           
            keyDown = true;
            _tempText = "";
            _charList.Clear();
            keyDown = false;
            PrintIntroText();    
        }

        _introText.text = _tempText;
    }

    private void PrintIntroText()
    {
        if (_currentText < _introTexts.Length)
        {
            string temp = _introTexts[_currentText];
            ReturnCharList(temp);
            StartCoroutine(PrintSlow());
            _currentText++;
        }
        else
        {
            StopAllCoroutines();
            LoadHelper.LoadSceneWithLoadingScreen(SceneName.AAA_Finished);
        }       
    }

    private void ReturnCharList(string text)
    {
        foreach (char c in text)
        {
            _charList.Add(c);
        }
    }

    private IEnumerator PrintSlow()
    {
        foreach (char c in _charList)
        {
            _tempText += c;
            yield return new WaitForSeconds(0.05f);

            if (keyDown)
            {
                _tempText = "";
                StopAllCoroutines();
            }
        }
    }
}
