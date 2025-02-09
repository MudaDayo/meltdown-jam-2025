using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private VisualElement _ui;
    protected Button _startButton;
    protected Button _optionsButton;

    protected virtual void OnEnable()
    {
        _ui = GetComponent<UIDocument>().rootVisualElement;
        _startButton = _ui.Q("StartButton") as Button;
        if (_startButton != null)
            _startButton.RegisterCallback<ClickEvent>(OnStartButtonClick);

        _optionsButton = _ui.Q("OptionsButton") as Button;
        if (_optionsButton != null)
            _optionsButton.RegisterCallback<ClickEvent>(OnOptionsButtonClick);
    }

    void OnDisable()
    {
        if (_startButton != null)
            _startButton.UnregisterCallback<ClickEvent>(OnStartButtonClick);
        if (_optionsButton != null)
            _optionsButton.UnregisterCallback<ClickEvent>(OnOptionsButtonClick);
    }

    void OnStartButtonClick(ClickEvent evt)
    {
        SceneManager.LoadScene("SampleScene");
    }
    void OnOptionsButtonClick(ClickEvent evt)
    {
        Application.Quit();
    }
}
