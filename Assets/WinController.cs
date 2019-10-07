using UnityEngine;

public class WinController : MonoBehaviour
{
    void End(int won) {
        if (won == 1) {
            GameManager.Instance.Win();
        }
        else {
            GameManager.Instance.Lose();
        }
    }
}
