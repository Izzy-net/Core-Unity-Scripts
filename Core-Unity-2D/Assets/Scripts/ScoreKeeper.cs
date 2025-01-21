using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] float currentScore = 0f;
    [SerializeField] TextMeshProUGUI scoreText;
    GameManager gameManager;

    private void Start() 
    {
        gameManager = FindAnyObjectByType<GameManager>();
        currentScore = gameManager.GetScoreStore();
        Debug.Log(currentScore);
        scoreText.text = currentScore.ToString();
    }

    public void ChangeCurrentScore(float amount)
    {
        currentScore += amount;
        gameManager.SetScoreStore(currentScore);
        scoreText.text = currentScore.ToString();
    }
}
