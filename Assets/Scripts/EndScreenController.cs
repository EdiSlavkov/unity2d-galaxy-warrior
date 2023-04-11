using UnityEngine;
using UnityEngine.UIElements;

public class EndScreenController : MonoBehaviour
{
    private SceneController sceneController;
    private ScoreKeeper scoreKeeper;
    private VisualElement root;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        sceneController = FindObjectOfType<SceneController>();
        root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<Button>("PlayAgainButton").clicked += () => {
            StartCoroutine(sceneController.RestartTheGame());
        };
        root.Q<Button>("QuitButton").clicked += () => Application.Quit();
        root.Q<Label>("MaxScore").text = scoreKeeper.GetMaxScore().ToString("000000000");
        root.Q<Label>("GameScore").text = scoreKeeper.GetTotalScore().ToString("000000000");
    }
}