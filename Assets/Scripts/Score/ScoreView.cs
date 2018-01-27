using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView: MonoBehaviour
{
    private Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        ScoreModel.OnScoreChanged += Score_Changed;
    }

    private void OnDisable()
    {
        ScoreModel.OnScoreChanged -= Score_Changed;
    }

    private void Score_Changed(int score)
    {
        _text.text = "" + score;
    }
}
