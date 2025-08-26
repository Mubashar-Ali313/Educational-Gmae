using UnityEngine;
using UnityEngine.UI;

public class FinalExamQuestions : MonoBehaviour
{
    [Header("Options")]
    public Button[] optionButtons;
    public int correctIndex = 0;

    [Header("Sprites")]
    public Sprite normalSprite;
    public Sprite selectedSprite;

    [Header("Submit Button")]
    public Button submitButton;

    [Header("Retry System")]
    public GameObject retryPanel;    // retry panel (set active false by default in Inspector)
    public Button retryButton;       // retry button inside panel

    private int selected = -1;
    private Button selectedButton;
    private bool isAnswered = false;
    private bool hasRetried = false; // ek chance ka flag


    [Header("Next Questions")]
    public GameObject thisScreen;
    public GameObject nextlevelScreen;
    public GameObject nextScreen;

    void Start()
    {
        // Register this question in FinalExamManager
        FinalExamManager.Instance.RegisterQuestion();

        // Disable submit button at start
        if (submitButton != null)
            submitButton.interactable = false;

        // Add listeners to all option buttons
        for (int i = 0; i < optionButtons.Length; i++)
        {
            int idx = i;
            optionButtons[i].onClick.AddListener(() => OnOptionSelected(idx));
        }

        // Add submit listener
        if (submitButton != null)
            submitButton.onClick.AddListener(CheckAnswer);

        // Retry button listener
        if (retryButton != null)
            retryButton.onClick.AddListener(OnRetry);

        if (retryPanel != null)
            retryPanel.SetActive(false);
    }

    void OnOptionSelected(int index)
    {
        if (isAnswered) return;

        // Reset previous selection
        if (selectedButton != null)
            selectedButton.image.sprite = normalSprite;

        // Set new selection
        selected = index;
        selectedButton = optionButtons[index];
        selectedButton.image.sprite = selectedSprite;

        // Enable submit button once option selected
        if (submitButton != null)
            submitButton.interactable = true;
    }

    //void CheckAnswer()
    //{
    //    if (isAnswered) return;
    //    if (selected == -1) return; // nothing selected

    //    // Correct answer
    //    if (selected == correctIndex)
    //    {
    //        isAnswered = true;
    //        FinalExamManager.Instance.AnswerQuestion(true);

    //        // Next question call
    //        LoadNextQuestion();
    //    }
    //    else
    //    {
    //        if (!hasRetried)
    //        {
    //            // show retry panel
    //            hasRetried = true;
    //            if (retryPanel != null) retryPanel.SetActive(true);

    //            // disable buttons temporarily
    //            foreach (Button btn in optionButtons)
    //                btn.interactable = false;
    //            if (submitButton != null)
    //                submitButton.interactable = false;
    //        }
    //        else
    //        {
    //            // 2nd wrong  finalize answer
    //            isAnswered = true;
    //            FinalExamManager.Instance.AnswerQuestion(false);

    //            // disable all buttons
    //            foreach (Button btn in optionButtons)
    //                btn.interactable = false;
    //            if (submitButton != null)
    //                submitButton.interactable = false;

    //            // Show fail result
    //            FinalExamManager.Instance.ShowResult();
    //        }
    //    }
    //}



    void CheckAnswer()
    {
        if (isAnswered) return;
        if (selected == -1) return;

        // Sirf ek dafa RegisterQuestion() call karna
        FinalExamManager.Instance.RegisterQuestion();

        if (selected == correctIndex)
        {
            //  Sahi jawab
            isAnswered = true;
            FinalExamManager.Instance.AnswerQuestion(true);

            // Agla question load karna
            LoadNextQuestion();
        }
        else
        {
            if (!hasRetried)
            {
                //  Pehli dafa galat  retry panel
                hasRetried = true;
                if (retryPanel != null) retryPanel.SetActive(true);

                foreach (Button btn in optionButtons) btn.interactable = false;
                if (submitButton != null) submitButton.interactable = false;
            }
            else
            {
                //  Dusri dafa galat  finalize + agla question load
                isAnswered = true;
                FinalExamManager.Instance.AnswerQuestion(false);

                foreach (Button btn in optionButtons) btn.interactable = false;
                if (submitButton != null) submitButton.interactable = false;

                //  yahan bug fix: next screen bhi load karo
                LoadNextQuestion();
            }
        }
    }








    //void OnRetry()
    //{
    //    if (retryPanel != null)
    //        retryPanel.SetActive(false);

    //    // allow reselect again
    //    isAnswered = false;
    //    selected = -1;

    //    // reset selected button sprite
    //    if (selectedButton != null)
    //    {
    //        selectedButton.image.sprite = normalSprite;
    //        selectedButton = null;
    //    }

    //    // enable buttons again
    //    foreach (Button btn in optionButtons)
    //        btn.interactable = true;

    //    if (submitButton != null)
    //        submitButton.interactable = false;
    //}






    void OnRetry()
    {
        if (retryPanel != null)
            retryPanel.SetActive(false);

        // allow reselect again
        isAnswered = false;
        selected = -1;

        // reset selected button sprite safely
        if (selectedButton != null)
        {
            if (selectedButton.image != null) //  safe check
                selectedButton.image.sprite = normalSprite;

            selectedButton = null;
        }

        // enable buttons again safely
        if (optionButtons != null && optionButtons.Length > 0)
        {
            foreach (Button btn in optionButtons)
            {
                if (btn != null)
                    btn.interactable = true;
            }
        }

        if (submitButton != null)
            submitButton.interactable = false;

        Debug.Log("Retry Done. Ready for user to select again."); //  Debugging ke liye
    }









    void LoadNextQuestion()
    {
        Debug.Log(" Next Question Load karna hai yahan...");
        // Example: SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // ya apna next panel UI active karo

        nextScreen.SetActive(true);
    }


    public void nextQuestionScreenEnabling()
    {
        thisScreen.SetActive(false);
        nextlevelScreen.SetActive(true);
    }

}




