using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("HUD")]
    public Slider progressBar;
    public Slider riskBar;
    public TextMeshProUGUI scoreText;

    [Header("Levels")]
    public GameObject[] levels; // drag your level panels here
    public GameObject finalExam;

    private int currentLevel = 0;
    private int score = 0;


    


    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        //ShowLevel(0);
        UpdateHUD();
    }
    private void Update()
    {
        //resultScore = +score;
    }
    public void ShowLevel(int index)
    {
        foreach (var lvl in levels) lvl.SetActive(false);
        finalExam.SetActive(false);

        if (index < levels.Length)
        {
            currentLevel = index;
            levels[index].SetActive(true);
        }
        else
        {
            finalExam.SetActive(true);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateHUD();
    }

    public void AddRisk(int points)
    {
        //riskBar.value += points;
        riskBar.value = Mathf.Clamp(riskBar.value + points, 0, riskBar.maxValue);
    }

    public void NextLevel()
    {
        ShowLevel(currentLevel + 1);
        progressBar.value = currentLevel + 1;

    }

    void UpdateHUD()
    {
        scoreText.text = "Integrity Shards: " + score;
        progressBar.value = currentLevel;
    }


    public void Exit()
    {
        // Editor me Game ko stop karega
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Build hone ke baad game completely exit ho jaye ga
        Application.Quit();
#endif
    }


    // Function jo Scene ka naam le kar load kare
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Function jo Scene index se load kare
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }



}
