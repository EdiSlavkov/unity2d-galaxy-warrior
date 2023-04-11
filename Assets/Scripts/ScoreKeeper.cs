using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI maxHpText;
    private float totalScore;
    private float maxScore;
    private static ScoreKeeper scoreKeeper;
    private const string PlayerMaxScore = "maxScore";

    private void Awake()
    {
        if (scoreKeeper != null)
        {
            Destroy(gameObject);
        }
        else
        {
            scoreKeeper = this;
            DontDestroyOnLoad(gameObject);
        }
        maxScore = PlayerPrefs.GetFloat(PlayerMaxScore);
    }

    public void SetMaxScore()
    {
        if (totalScore > maxScore)
        {
            PlayerPrefs.SetFloat(PlayerMaxScore, totalScore);
        }
    }

    public void DestroyScoreKeeper()
    {
        Destroy(gameObject);
    }

    public void AddToScore(float score)
    {
        totalScore += score;
    }

    public void RemoveFromScore(float score)
    {
        totalScore -= score;
        if (totalScore <= 0)
        {
            totalScore = 0;
        }
    }

    public float GetTotalScore()
    {
        return totalScore;
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score:" + totalScore.ToString("000000000");
    }

    public void UpdateHPText(float hp)
    {
        hpText.text = hp.ToString("00000") + " /";
    }

    public void UpdateMaxHealthText(float maxHp)
    {
        maxHpText.text = maxHp.ToString("00000");
    }

    public float GetMaxScore()
    {
        return maxScore;
    }
}