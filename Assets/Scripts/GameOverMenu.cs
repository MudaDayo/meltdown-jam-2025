using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverMenu : MonoBehaviour
{
    private VisualElement _ui;
    protected Button _restartButton;

    protected virtual void OnEnable()
    {
        _ui = GetComponent<UIDocument>().rootVisualElement;
        _restartButton = _ui.Q("RestartButton") as Button;
        if (_restartButton != null)
            _restartButton.RegisterCallback<ClickEvent>(OnRestartButtonClick);
    }

    void OnDisable()
    {
        if (_restartButton != null)
            _restartButton.UnregisterCallback<ClickEvent>(OnRestartButtonClick);
    }

    void OnRestartButtonClick(ClickEvent evt)
    {
        SceneManager.LoadScene("MainMenu");
    }
}
