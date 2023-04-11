using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] AnimationClip transitionClip;
    [SerializeField] Animator menuAnimator;
    private const string FadeTrigger = "Fade";
    private const string StartTrigger = "Start";
    private ScoreKeeper scoreKeeper;

    private void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        menuAnimator.SetTrigger(StartTrigger);
    }

    public IEnumerator LoadEndScreen()
    {
        yield return new WaitForSeconds(3f);
        menuAnimator.SetTrigger(FadeTrigger);
        yield return new WaitForSeconds(transitionClip.length);
        SceneManager.LoadScene(2);
    }

    public IEnumerator StartGameScreen()
    {
        menuAnimator.SetTrigger(FadeTrigger);
        yield return new WaitForSeconds(transitionClip.length);
        SceneManager.LoadScene(1);
    }

    public IEnumerator RestartTheGame()
    {
        scoreKeeper.DestroyScoreKeeper();
        menuAnimator.SetTrigger(FadeTrigger);
        yield return new WaitForSeconds(transitionClip.length);
        SceneManager.LoadScene(1);
    }
}