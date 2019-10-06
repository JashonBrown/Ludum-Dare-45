using System.Collections;
using System.Collections.Generic;
using LudumDare;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool[][] Player1Data;

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
        DataManager.Instance.Money += 100;
        DataManager.Instance.Level++;
        SceneManager.LoadScene("Ship Editor");
    }

    public void Lose() {
        DataManager.Instance.Money = 0;
        DataManager.Instance.Level = 0;
        SceneManager.LoadScene("Main Menu");        
    }
}
