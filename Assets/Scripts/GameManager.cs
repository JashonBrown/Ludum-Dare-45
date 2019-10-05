using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool[][] Player1Data;

    # region Singleton
    public static GameManager instance = null;
    void Awake()
    {
        if (instance == null) {
            instance = this;
        } 
        else if (instance != this) {
            Destroy(gameObject);    
        }
        DontDestroyOnLoad(gameObject);
    }
    # endregion
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
