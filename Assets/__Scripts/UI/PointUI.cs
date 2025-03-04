using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointUI : MonoBehaviour
{
    private TextMeshProUGUI _score;

    private void OnEnable()
    {
        Fruit.FruitClicked += ChangeScore;
    }

    private void OnDisable()
    {
        Fruit.FruitClicked -= ChangeScore;
    }

    private void Start()
    {
        _score = GetComponent<TextMeshProUGUI>();
        _score.text = "0";
    }

    private void ChangeScore(int obj)
    {
        int score = int.Parse(_score.text);
        score += obj;
        _score.text = score.ToString();
    }
}
