using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public string Playername;
    public int HighScore;
    public List<HighScoreDataStructure> AllHighScores = new List<HighScoreDataStructure>();
    public static MenuManager Instance;

    private const string saveDataFilename = "saveData.json";

    // Start is called before the first frame update
    void Start()
    {
        if(Instance != null)
        {
            Destroy(gameObject);

            // Make sure to reload the data anytime we start the menu scene
            // This way when we go to the top10 scene we have loaded the
            // new highscores
            Instance.LoadData();

            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadData();
    }

    private void LoadData()
    {
        string path = Application.persistentDataPath + "/" + saveDataFilename;
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveDataStructure savedData = JsonUtility.FromJson<SaveDataStructure>(json);
            
            if(savedData == null)
            {
                // Reset any saved data in memory
                Playername = "";
                HighScore = 0;
                AllHighScores = new List<HighScoreDataStructure>();

                Debug.Log("Saved data is empty, nothing to load in");
                return;
            }
            
            Playername = savedData.Playername;
            HighScore = savedData.HighScore;
            AllHighScores = savedData.AllHighScores;

            Debug.Log("Loaded saved data: " + json);
        } else
        {
            Debug.Log("Trying to load non-existing saveData file: " + path);
        }
    }

    public void SaveData()
    {
        SaveDataStructure data = new SaveDataStructure();
        data.Playername = Playername;
        data.HighScore = HighScore;
        data.AllHighScores = AllHighScores;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/" + saveDataFilename, json);
    }

    public void AddHighScore(int highScore)
    {
        HighScoreDataStructure data = new HighScoreDataStructure();
        data.Playername = Playername;
        data.HighScore = highScore;

        AllHighScores.Add(data);

        // We don't have to sort a list that now contains only 1 item
        if (AllHighScores.Count == 1)
        {
            return;
        }

        // Sort from highest highscore to lowest
        AllHighScores.Sort();
        AllHighScores.Reverse();

        // Make sure we only keep the top 10
        if (AllHighScores.Count > 10)
        {
            AllHighScores.RemoveRange(10, AllHighScores.Count - 10);
        }
    }

    public void DeleteSavedData()
    {
        File.WriteAllText(Application.persistentDataPath + "/" + saveDataFilename, "");
        LoadData(); // Clears any data out of memory
        Debug.Log("Deleted all saved data!");
    }

    [Serializable]
    public class SaveDataStructure
    {
        public string Playername;
        public int HighScore;
        public List<HighScoreDataStructure> AllHighScores;
    }

    [Serializable]
    public class HighScoreDataStructure: IComparable
    {
        public string Playername;
        public int HighScore;

        public int CompareTo(object other)
        {
            if (other == null)
            {
                return 1;
            }

            HighScoreDataStructure otherData = (HighScoreDataStructure)other;
            return HighScore.CompareTo(otherData.HighScore);
        }
    }
}
