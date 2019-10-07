using System.Collections;
using System.Collections.Generic;
using LudumDare;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    # region Singleton
    public static GameManager Instance = null;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } 
        else if (Instance != this) {
            Destroy(gameObject);    
        }
        DontDestroyOnLoad(gameObject);
    }
    # endregion

    public void Win() {
        DataManager.Instance.Level++;
        if (DataManager.Instance.Level > 11) SceneManager.LoadScene("Win Screen");
        
        DataManager.Instance.Money += DataManager.Instance.Level * 50;
        SceneManager.LoadScene("Ship Editor");
    }

    public void Lose() {
        DataManager.Instance.Money = 200;
        DataManager.Instance.Level = 0;
        DataManager.Instance.PlayerRaft = new Raft(DataManager.Instance.RaftWidth, DataManager.Instance.RaftHeight);;
        SceneManager.LoadScene("Main Menu");        
    }
}
