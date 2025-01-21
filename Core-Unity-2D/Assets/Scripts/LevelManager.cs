using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    GameManager gameManager;

    private void Awake() 
    {
        gameManager = FindAnyObjectByType<GameManager>(FindObjectsInactive.Exclude);
    }
    public void Replay()
    {
        gameManager.ResetScore();
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
