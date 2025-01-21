using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    [SerializeField] private float scoreStore;
    private void Awake() 
    {
        ManageSingleton();
    }

    private void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public float GetScoreStore()
    {
        return scoreStore;
    }

    public void SetScoreStore(float score)
    {
        scoreStore = score;
    }

    public void ResetScore()
    {
        scoreStore = 0f;
    }
}
