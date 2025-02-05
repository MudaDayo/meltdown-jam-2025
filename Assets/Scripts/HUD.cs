using UnityEngine;
using UnityEngine.UIElements;

public class HUD : MonoBehaviour
{
    private UIDocument _attachedDocument = null;
    private VisualElement _root = null;
    private Label _timer = null;

    float _elapsedTime = 0;
    int _timerSeconds = 0;

    void Start()
    {
        _attachedDocument = GetComponent<UIDocument>();
        if (_attachedDocument)
        {
            _root = _attachedDocument.rootVisualElement;
        }
        if (_root != null)
        {
            _timer = _root.Q("Timer") as Label;
            _timer.text = _timerSeconds.ToString();
        }
    }

        // Update is called once per frame
    void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > _timerSeconds + 1)
        {
            _timerSeconds += 1;
            _timer.text = _timerSeconds.ToString();
        }
    }
}
