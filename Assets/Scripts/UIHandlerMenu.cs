using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandlerMenu : MonoBehaviour
{
    public InputField PlayernameInput;

    // Start is called before the first frame update
    void Start()
    {
        PlayernameInput.text = MenuManager.Instance.Playername;
    }

    public void StartGame()
    {
        MenuManager.Instance.Playername = PlayernameInput.text;
        MenuManager.Instance.SaveData();

        SceneManager.LoadScene(1);
    }

    public void StartTop10()
    {
        SceneManager.LoadScene(2);
    }

    public void DeleteSavedData()
    {
        MenuManager.Instance.DeleteSavedData();
    }
}
