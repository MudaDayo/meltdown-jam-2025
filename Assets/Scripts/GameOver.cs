using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("WinScreen");
    }
    private void OnEnable()
    {
        SceneManager.LoadScene("WinScreen");
    }
}
