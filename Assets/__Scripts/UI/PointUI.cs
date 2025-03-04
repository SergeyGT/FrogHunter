using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PointUI : MonoBehaviour
{
    private TextMeshProUGUI _score;

    private static PointUI _instance;

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
        if (_instance != null && _instance != this)
        {
            Debug.LogWarning("PointUI is already exist");
            Destroy(this.gameObject);
        }
        _instance = this;
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
