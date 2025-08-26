//using UnityEngine;
//using UnityEngine.UI;

//public class QuizManager : MonoBehaviour
//{
//    [Header("Option Buttons")]
//    public Button[] optionButtons;    // 3 option buttons
//    public Button tickButton;
//    public Button crossButton;

//    [Header("Windows")]
//    public GameObject successWindow;  // Show if answer is correct
//    public GameObject[] errorWindows; // One error window per wrong option

//    [Header("Correct Answer")]
//    public int correctAnswerIndex = 0; // set in Inspector (0 = first button, 1 = second, etc.)

//    private int selectedOption = -1;
//    private bool isLocked = false; // Lock after checking answer

//    void Start()
//    {
//        tickButton.gameObject.SetActive(false);
//        crossButton.gameObject.SetActive(false);

//        // setup buttons
//        for (int i = 0; i < optionButtons.Length; i++)
//        {
//            int index = i;
//            optionButtons[i].onClick.AddListener(() => OnOptionSelected(index));
//        }

//        tickButton.onClick.AddListener(CheckAnswer);
//        crossButton.onClick.AddListener(CancelSelection);
//    }

//    void OnOptionSelected(int index)
//    {
//        if (isLocked) return;

//        selectedOption = index;

//        tickButton.gameObject.SetActive(true);
//        crossButton.gameObject.SetActive(true);
//    }

//    void CheckAnswer()
//    {
//        if (isLocked) return;
//        isLocked = true;

//        if (selectedOption == correctAnswerIndex)
//        {
//            if (successWindow != null) successWindow.SetActive(true);

//            GameManager.Instance.AddScore(10);      // score increase
//            GameManager.Instance.progressBar.value += 3f;      // score increase
//            //GameManager.Instance.NextLevel();       // go to next level
//        }
//        else
//        {
//            if (selectedOption >= 0 && selectedOption < errorWindows.Length)
//            {
//                errorWindows[selectedOption].SetActive(true);
//            }

//            GameManager.Instance.AddRisk(5);        // risk increase
//        }

//        tickButton.gameObject.SetActive(false);
//        crossButton.gameObject.SetActive(false);

//        DisableOptionButtons();
//    }

//    void CancelSelection()
//    {
//        if (isLocked) return;

//        selectedOption = -1;
//        tickButton.gameObject.SetActive(false);
//        crossButton.gameObject.SetActive(false);
//    }

//    void DisableOptionButtons()
//    {
//        foreach (var btn in optionButtons)
//        {
//            btn.interactable = false;
//        }
//    }
//}









using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [Header("Option Buttons")]
    public Button[] optionButtons;    // 3 option buttons
    public Button tickButton;
    public Button crossButton;

    [Header("Windows")]
    public GameObject successWindow;  // Show if answer is correct
    public GameObject[] errorWindows; // One error window per wrong option

    [Header("Sprites")]
    public Sprite normalSprite;       // Default button sprite
    public Sprite selectedSprite;     // Selected button sprite

    [Header("Correct Answer")]
    public int correctAnswerIndex = 0; // set in Inspector (0 = first button, 1 = second, etc.)

    private int selectedOption = -1;
    private bool isLocked = false; // Lock after checking answer
    private Button selectedButton; // Store selected button reference

    void Start()
    {
        tickButton.gameObject.SetActive(false);
        crossButton.gameObject.SetActive(false);

        // setup buttons
        for (int i = 0; i < optionButtons.Length; i++)
        {
            int index = i;
            optionButtons[i].onClick.AddListener(() => OnOptionSelected(index));
        }

        tickButton.onClick.AddListener(CheckAnswer);
        crossButton.onClick.AddListener(CancelSelection);
    }

    void OnOptionSelected(int index)
    {
        if (isLocked) return;

        // Reset previously selected button
        if (selectedButton != null)
        {
            selectedButton.image.sprite = normalSprite;
        }

        selectedOption = index;
        selectedButton = optionButtons[index];
        selectedButton.image.sprite = selectedSprite;

        tickButton.gameObject.SetActive(true);
        crossButton.gameObject.SetActive(true);
    }

    void CheckAnswer()
    {
        if (isLocked) return;
        isLocked = true;

        if (selectedOption == correctAnswerIndex)
        {
            if (successWindow != null) successWindow.SetActive(true);

            GameManager.Instance.AddScore(10);
            GameManager.Instance.progressBar.value += 7f;
            //GameManager.Instance.NextLevel();       
        }
        else
        {
            if (selectedOption >= 0 && selectedOption < errorWindows.Length)
            {
                errorWindows[selectedOption].SetActive(true);
            }

            GameManager.Instance.AddRisk(5);
        }

        tickButton.gameObject.SetActive(false);
        crossButton.gameObject.SetActive(false);

        DisableOptionButtons();
    }

    void CancelSelection()
    {
        if (isLocked) return;

        // Reset selection
        if (selectedButton != null)
        {
            selectedButton.image.sprite = normalSprite;
            selectedButton = null;
        }

        selectedOption = -1;

        tickButton.gameObject.SetActive(false);
        crossButton.gameObject.SetActive(false);
    }

    void DisableOptionButtons()
    {
        foreach (var btn in optionButtons)
        {
            btn.interactable = false;
        }
    }
}


