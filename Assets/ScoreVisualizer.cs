using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreVisualizer : MonoBehaviour
{
    private TextMeshProUGUI _scoreText;


    // Start is called before the first frame update
    void Start()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _scoreText.text = "Score: " + GameManager.Instance.Score.ToString();
    }
}
