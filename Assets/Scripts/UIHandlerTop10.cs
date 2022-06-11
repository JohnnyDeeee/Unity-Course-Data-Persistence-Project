using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandlerTop10 : MonoBehaviour
{
    public GameObject HighScorePanel;
    public GameObject HighScorePrefab;

    private void Start()
    {
        // TODO: Load highscores
        // TODO: Add highscores to panel

        if(MenuManager.Instance.AllHighScores.Count == 0)
        {
            GameObject gameObject = Instantiate(HighScorePrefab, HighScorePanel.transform);
            gameObject.GetComponent<Text>().text = "No High Scores yet, go play a round!";
        } else
        {
            MenuManager.Instance.AllHighScores.ForEach(x =>
            {
                int position = MenuManager.Instance.AllHighScores.IndexOf(x) + 1;
                GameObject gameObject = Instantiate(HighScorePrefab, HighScorePanel.transform);
                gameObject.GetComponent<Text>().text = $"{position}. {x.Playername}:{x.HighScore}";
            });
        }
    }

    public void StartMenu()
    {
        SceneManager.LoadScene(0);
    }
}
