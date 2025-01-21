using UnityEngine;

public class Coin : MonoBehaviour, IPickup
{
    [SerializeField] float scoreForPickup;
    ScoreKeeper scoreKeeper;

    private void Awake() 
    {
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
    }
    public void PickMeUp()
    {
        scoreKeeper.ChangeCurrentScore(scoreForPickup);
        Destroy(gameObject);
    }
}
