using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStatus
{
    StartGame,
    DeathPlayer,
    WinPlayer
}

public class GameManager : MonoBehaviour
{
    [Header("Set Info About Level")]
    [SerializeField] private int _currentLevel;
    [SerializeField] private int _nextLevel;

    private GameStatus _status;
    private static GameManager _instance;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Debug.LogWarning("Manager is already exist");
            Destroy(this.gameObject);
        }
        _instance = this;
        _status = GameStatus.StartGame;
    }

    private void Start()
    {
        //StartUIDraw
    }


    private void OnEnable()
    {
        Player.Death += LoseGame;
        FinishLocation.Finish += WinGame;
    }

    private void OnDisable()
    {
        Player.Death -= LoseGame;
        FinishLocation.Finish -= WinGame;
    }

    private void LoseGame()
    {
        _status = GameStatus.DeathPlayer;
        SceneManager.LoadScene(_currentLevel);
    }
    private void WinGame()
    {
        _status = GameStatus.WinPlayer;
        SceneManager.LoadScene(_nextLevel);
    }
}
