using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause()
    {
        Time.timeScale = Time.timeScale == 1.0f? 0.0f : 1.0f;
    }

    public void Volume()
    {
        AudioListener.volume = AudioListener.volume == 1.0f? 0.0f : 1.0f;
    }
}
