using UnityEngine;
using UnityEngine.UI;

public class LevelQuestion : MonoBehaviour
{
    [Header("Options")]
    public Button[] optionButtons;
    public int correctIndex = 0;

    [Header("Sprites")]
    public Sprite normalSprite;   // default sprite
    public Sprite selectedSprite; // selected sprite

    [Header("Submit Button")]
    public Button submitButton;   // submit button

    [Header("Retry System")]
    public GameObject retryPanel;   // retry panel with Retry Button
    public Button retryButton;      // retry button
    private bool hasRetried = false; // sirf 1 chance dena

    private int selected = -1;
    private Button selectedButton;

    [Header("Next Level screen")]
    public GameObject nextQuestionScreen;

    void Start()
    {
        // option button listeners
        for (int i = 0; i < optionButtons.Length; i++)
        {
            int idx = i;
            optionButtons[i].onClick.AddListener(() => OnOptionSelected(idx));
        }

        // submit listener
        submitButton.onClick.AddListener(CheckAnswer);

        // retry button listener
        if (retryButton != null)
            retryButton.onClick.AddListener(OnRetry);

        if (retryPanel != null)
            retryPanel.SetActive(false);
    }

    void OnOptionSelected(int index)
    {
        // Reset old selection
        if (selectedButton != null)
        {
            selectedButton.image.sprite = normalSprite;
        }

        // Set new selection
        selected = index;
        selectedButton = optionButtons[index];
        selectedButton.image.sprite = selectedSprite;
    }

    void CheckAnswer()
    {
        if (selected == -1) return; // no option selected

        if (selected == correctIndex)
        {
            //  Correct
            GameManager.Instance.AddScore(6);
            GameManager.Instance.progressBar.value += 5f;

            nextQuestionScreen.SetActive(true);
            LockAll();
        }
        else
        {
            //  Wrong
            if (!hasRetried)
            {
                hasRetried = true;
                if (retryPanel != null)
                    retryPanel.SetActive(true); // show retry window
            }
            else
            {
                //  Second time bhi galat
                GameManager.Instance.AddRisk(5);

                // Ab next screen bhi load ho
                nextQuestionScreen.SetActive(true);

                LockAll();
            }
        }
    }

    void OnRetry()
    {
        if (retryPanel != null)
            retryPanel.SetActive(false);

        // reset selection
        if (selectedButton != null)
        {
            selectedButton.image.sprite = normalSprite;
            selectedButton = null;
        }

        selected = -1;

        // enable options again
        foreach (Button btn in optionButtons)
            btn.interactable = true;

        // re-enable submit
        submitButton.interactable = true;
    }

    void LockAll()
    {
        foreach (Button btn in optionButtons)
            btn.interactable = false;

        submitButton.interactable = false;
    }

    public void OnCheckpointPassed()
    {
        GameManager.Instance.NextLevel();
    }
}
