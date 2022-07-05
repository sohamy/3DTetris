using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOver, gamePause;
    public static bool isPause = false;

    // Start is called before the first frame update
    void Start()
    {
        gameOver.SetActive(false);
        gamePause.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (TetrisBlock.isGameOver)
        {
            gameOver.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Pause();
        }
    }
    public void Retry()
    {
        TetrisBlock.isGameOver = false;
        gameOver.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Pause()
    {
        isPause = true;
        gamePause.SetActive(true);

    }
    public void continueTo()
    {
        isPause = false;
        gamePause.SetActive(false);
    }
}
