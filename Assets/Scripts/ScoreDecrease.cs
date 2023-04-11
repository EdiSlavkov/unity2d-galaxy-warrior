using UnityEngine;

public class ScoreDecrease : MonoBehaviour
{
    [SerializeField] float playerScoreDecrease;
    [SerializeField] float playerScoreDecreasePerLevel;
    private GameController gameController;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void Start()
    {
        if (gameController.ShouldUpgrade())
        {
            playerScoreDecrease += playerScoreDecreasePerLevel * gameController.GetCurrentLevel();

        }
    }

    public float PlayerScoreDecrease()
    {
        return playerScoreDecrease;
    }
}