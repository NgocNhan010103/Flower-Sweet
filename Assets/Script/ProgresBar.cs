using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    #region Singleton class: ProgressBar
    public static ProgressBar Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    #endregion

    [Header("Star")]
    [SerializeField] public Image star1;
    [SerializeField] public Image star2;
    [SerializeField] public Image star3;
    [SerializeField] public Image active1;
    [SerializeField] public Image active2;
    [SerializeField] public Image active3;

    [SerializeField] public TMP_Text currentScoreText;
    [SerializeField] public TMP_Text targetScoreText;
    [SerializeField] public TMP_Text nextLevel;
    [SerializeField] public Image fillBar;
    [SerializeField] public float currentAmount = 0;
    [SerializeField] public int level = 0;
    [SerializeField] public int targetScore = 100;
    [SerializeField] public int currentScore = 0;
    [SerializeField] public int score;
    public Coroutine routine;

    private void OnEnable()
    {
        level = 0;
        currentAmount = 0;
        targetScore = 100;
        currentScore = 0;
        UpdateLevel(level);
        UpdateTargetScore(targetScore);
    }

    public void TickScore()
    {
        StartCoroutine(IncreaseScoreOverTime());
        
    }

    IEnumerator IncreaseScoreOverTime()
    {
        for (int i = 0;i< score;i++)
        {
            currentScore++; 
            UpdateCurrentScore(currentScore);
            UpdateProgress(1 / float.Parse(targetScoreText.text));
            yield return new WaitForSeconds(0.06f);
        }
    }

    public void UpdateProgress(float amount, float durection = 0.1f)
    {
        if (routine != null)
        {
            StopCoroutine(routine);
        }
        float target = currentAmount + amount;
        routine = StartCoroutine(FillRoutine(target, durection));
    }

    private IEnumerator FillRoutine(float target, float durection)
    {

        float time = 0;
        float tempAmount = currentAmount;
        float diff = target - tempAmount;
        currentAmount = target;

        while (time < durection)
        {
            time += Time.deltaTime;
            float percent = time/durection;
            fillBar.fillAmount = tempAmount + diff * percent;
            yield return null; 
        }

        if (currentAmount >= 1)
        {
            Invoke("LevelUp", 3f);

        }

        if (currentAmount >= 0.2)
        {
            star1.gameObject.SetActive(false);
            active1.gameObject.SetActive(true);
        } 
        if (currentAmount >= 0.4)
        {
            star2.gameObject.SetActive(false) ;
            active2.gameObject.SetActive(true) ;
        }
        if (currentAmount >= 0.8)
        {
            star3.gameObject.SetActive(false) ;
            active3.gameObject.SetActive(true) ;
        }
    }

    private void LevelUp()
    {
        star1.gameObject.SetActive(true);
        active1.gameObject.SetActive(false) ;
        star2.gameObject.SetActive(true) ;
        active2.gameObject.SetActive(false) ;
        star3.gameObject.SetActive(true) ;
        active3.gameObject.SetActive(false);
        UpdateLevel(level + 1);
        if (currentScore == targetScore)
        {
            UpdateCurrentScore(0);
        }
        else if (currentScore > targetScore)
        {
            UpdateCurrentScore(currentScore - targetScore);
        }
        UpdateTargetScore(targetScore + 50);
        UpdateProgress(-1f, 0.2f);
    }

    private void UpdateLevel(int level)
    {
        this.level = level;
        nextLevel.text = (level +1).ToString();
    }

    private void UpdateTargetScore(int score)
    {
        this.targetScore = score;
        targetScoreText.text = score.ToString();
    }

    private void UpdateCurrentScore(int score)
    {
        this.currentScore = score;
        currentScoreText.text = score.ToString();
    }
}
