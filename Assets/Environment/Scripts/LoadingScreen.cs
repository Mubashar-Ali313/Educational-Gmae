using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen Instance;

    public GameObject loadingPanel;
    public GameObject startGamePanel;
    public Slider slider;
    public TextMeshProUGUI progressText;


    void Awake()
    {
        if (Instance == null) Instance = this;
    }


    void Start()
    {
        startGamePanel.SetActive(false);
        StartCoroutine(ShowLoading());
    }

    public void StartLoadingScreen(GameObject nextScreen)
    {
        StartCoroutine(NextTimeLoading(nextScreen));
        
    }

    IEnumerator ShowLoading()
    {
        loadingPanel.SetActive(true);

        float progress = 0f;
        while (progress < 1f)
        {
            progress += Time.deltaTime * 0.3f; // loading speed
            slider.value = progress;
            progressText.text = "Loading...   " + Mathf.RoundToInt(progress * 100f) + "%";
            yield return null;
        }

        // Loading done
        loadingPanel.SetActive(false);
        startGamePanel.SetActive(true);
        progress = 0f;
    }

    IEnumerator NextTimeLoading( GameObject nextScreenEnable)
    {
        loadingPanel.SetActive(true);

        float progress = 0f;
        while (progress < 1f)
        {
            progress += Time.deltaTime * 0.3f; // loading speed
            slider.value = progress;
            progressText.text = "Loading...   " + Mathf.RoundToInt(progress * 100f) + "%";
            yield return null;
        }

        // Loading done
        loadingPanel.SetActive(false);
        nextScreenEnable.SetActive(true);
        progress = 0f;
    }


}
