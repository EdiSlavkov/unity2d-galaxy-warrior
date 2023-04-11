using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    private VisualElement root;
    private Toggle toggle;
    private AudioPlayer audioPlayer;
    private SceneController sceneController;

    private void Awake()
    {
        sceneController = FindObjectOfType<SceneController>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<Button>("StartButton").clicked += () =>
        {
            StartCoroutine(sceneController.StartGameScreen());
        };
        root.Q<Button>("QuitButton").clicked += () => Application.Quit();
        toggle = root.Q<Toggle>("VibrationToggle");
        toggle.RegisterValueChangedCallback(ModifyVibrationState);
        root.Q<Label>("MaxScore").text = PlayerPrefs.GetFloat("maxScore").ToString("000000000");
    }

    private void ModifyVibrationState(ChangeEvent<bool> evt)
    {
        if (evt.newValue)
        {
            audioPlayer.SetShouldVibrate(false);
        }
        else
        {
            audioPlayer.SetShouldVibrate(true);
        }
    }
}