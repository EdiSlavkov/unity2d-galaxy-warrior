using TMPro;
using UnityEngine;

public class DamageBuffController : MonoBehaviour
{
    [SerializeField] private GameObject timerIcon;
    [SerializeField] private TextMeshProUGUI timerText;
    private float boostTime;
    private bool isBuffed;

    private void Update()
    {
        if (boostTime > 0)
        {
            boostTime -= Time.deltaTime;
            DisplayTime(boostTime);
        }
        else if (isBuffed)
        {
            isBuffed = false;
            timerIcon.SetActive(false);
            boostTime = 0f;
        }
    }

    public void StartTimer(float time)
    {
        boostTime += time;
        timerIcon.SetActive(true);
        isBuffed = true;
    }

    private void DisplayTime(float timeToDisplay)
    {
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public bool IsBuffed()
    {
        return isBuffed;
    }
}
