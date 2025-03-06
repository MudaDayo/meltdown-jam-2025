using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    // private UIDocument _attachedDocument = null;
    // private VisualElement _root = null;

    public Text text;
    public GameObject _timer = null;

    float _elapsedTime = 0;
    public static int _timerSeconds = 0;

    public static HUD Instance;

    private void Start()
    {
        InitializeTimer();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeTimer();
    }

    private void InitializeTimer()
    {
        _timer = GameObject.FindWithTag("Timer");
        if (_timer != null)
        {
            text = _timer.GetComponent<Text>();
        }
        else
        {
            Debug.LogError("Timer TextMeshProUGUI not found in the scene.");
        }
    }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > _timerSeconds + 1 && SceneManager.GetActiveScene().name != "WinScreen")
        {
            _timerSeconds += 1;
            text.text = _timerSeconds.ToString();
        }
        else if (SceneManager.GetActiveScene().name == "WinScreen")
        {
            text.text = "TIME: " + _timerSeconds.ToString() + "s";
        }
    }
}
