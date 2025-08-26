using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalExamManager : MonoBehaviour
{
    public static FinalExamManager Instance;   // Singleton

    public int totalQuestions = 0;
    public int correctAnswers = 0;
    public int score = 0;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI percentageText;
    public GameObject congratsPanel;
    public GameObject failPanel;
    float percentage;


    [Header("Result things")]
    public TextMeshProUGUI ResultScoreText;
    public TextMeshProUGUI ResultPersentageText;
    public int resultScore = 0;
    [Header("fail pannel")]
    public TextMeshProUGUI failScoreText;
    public TextMeshProUGUI failPersentageText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }



    private void Update()
    {
        ResultPersentageText.text = Mathf.RoundToInt(percentage) + "%";
        failPersentageText.text = Mathf.RoundToInt(percentage) + "%";
        resultScore = score;
        ResultScoreText.text = resultScore.ToString();
        failScoreText.text = resultScore.ToString();
    }

    public void RegisterQuestion()
    {
        totalQuestions++;
        UpdateUI();
    }

    public void AnswerQuestion(bool isCorrect)
    {
        if (isCorrect)
        {
            correctAnswers++;
            score += 20; //  Har sahi jawab pe +10
        }
        else
        {
            score -= 5;  //  Galat jawab pe -5
            if (score < 0) score = 0;
        }

        UpdateUI();
    }

    // UI update karna (score + percentage)
    void UpdateUI()
    {
        int maxScore = totalQuestions * 20;  // max possible marks
       // percentage = (totalQuestions > 0) ? ((float)correctAnswers / totalQuestions) * 100f : 0f;
        //percentage = (maxScore > 0) ? ((float)score / maxScore) * 100f : 0f;
        percentage = (resultScore > 0) ? ((float)resultScore / 100) * 100f : 0f;

        if (scoreText != null)
            scoreText.text = "Score: " + score;

        if (percentageText != null)
            percentageText.text = "Percentage: " + Mathf.RoundToInt(percentage) + "%";
    }

    // Exam khatam hone pe result dikhana
    public void ShowResult()
    {
        int maxScore = totalQuestions ;
        //float finalPercentage = (totalQuestions > 0) ? ((float)correctAnswers / totalQuestions) * 100f : 0f;
        //float finalPercentage = (maxScore > 0) ? ((float)score / maxScore) * 100f : 0f;
        float finalPercentage = (resultScore > 0) ? ((float)resultScore / 100) * 100f : 0f;

        if (finalPercentage >= 80)
            congratsPanel.SetActive(true);
        else
            failPanel.SetActive(true);
    }

}






