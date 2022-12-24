using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] GameObject gameOverMenu;
    public Text finalScoreText;
    public Text highScoreText;
    private int m_topScore = 0;
    private bool m_restart = false;

    public void ShowMenu(int i_Score)
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;

        if (i_Score > m_topScore)
        {
            m_topScore = i_Score;
        }

        SetGameOverMenuTexts(i_Score);
    
    }

    void SetGameOverMenuTexts(int i_FinalScore)
    {
        finalScoreText.text = $"{i_FinalScore} POINTS";
        highScoreText.text = $"HIGH SCORE: {m_topScore} POINTS";
    }

    public void Retry()
    {
        m_restart = true;
    }

    private void Update()
    {
        if (m_restart)
        {
            m_restart = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            print("The reset button is working");
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
