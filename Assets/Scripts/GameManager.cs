using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Win Target")]
    public int hamburgerTarget = 9;
    public int cakeTarget = 9;

    private int hamburgerMatched = 0;
    private int cakeMatched = 0;

    [Header("Timer")]
    public float timeLimit = 60f;
    private float currentTime;
    private bool gameEnded = false;

    [Header("Slot System")]
    public Image[] slotImages;

    private List<ItemType> slotItems = new List<ItemType>();
    private List<Sprite> slotSprites = new List<Sprite>();

    [Header("UI")]
    public TMP_Text targetText;
    public TMP_Text timerText;

    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject pausePanel;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        currentTime = timeLimit;

        winPanel.SetActive(false);
        losePanel.SetActive(false);
        pausePanel.SetActive(false);

        ClearSlotUI();
        UpdateTargetText();
        UpdateTimerText();
    }

    private void Update()
    {
        if (gameEnded) return;

        currentTime -= Time.deltaTime;
        UpdateTimerText();

        if (currentTime <= 0)
        {
            LoseGame();
        }
    }

    public void CollectItem(CollectibleItem item)
    {
        if (gameEnded) return;

        slotItems.Add(item.itemType);
        slotSprites.Add(item.itemIcon);

        Destroy(item.gameObject);

        CheckTripleMatch(item.itemType);
        UpdateSlotUI();

        if (slotItems.Count >= 7)
        {
            LoseGame();
        }
    }

    private void CheckTripleMatch(ItemType type)
    {
        int count = 0;

        for (int i = 0; i < slotItems.Count; i++)
        {
            if (slotItems[i] == type)
                count++;
        }

        if (count < 3) return;

        int removed = 0;

        for (int i = slotItems.Count - 1; i >= 0; i--)
        {
            if (slotItems[i] == type)
            {
                slotItems.RemoveAt(i);
                slotSprites.RemoveAt(i);
                removed++;

                if (removed >= 3)
                    break;
            }
        }

        if (type == ItemType.Hamburger)
            hamburgerMatched += 3;

        if (type == ItemType.Cake)
            cakeMatched += 3;

        UpdateTargetText();
        UpdateSlotUI();

        if (hamburgerMatched >= hamburgerTarget && cakeMatched >= cakeTarget)
        {
            WinGame();
        }
    }

    private void UpdateSlotUI()
    {
        ClearSlotUI();

        for (int i = 0; i < slotItems.Count; i++)
        {
            slotImages[i].sprite = slotSprites[i];
            slotImages[i].enabled = true;
        }
    }

    private void ClearSlotUI()
    {
        foreach (Image img in slotImages)
        {
            img.sprite = null;
            img.enabled = false;
        }
    }

    private void UpdateTargetText()
    {
        targetText.text =
            "Burger: " + hamburgerMatched + "/" + hamburgerTarget +
            "\nCake: " + cakeMatched + "/" + cakeTarget;

        targetText.transform.DOKill();
        targetText.transform
            .DOPunchScale(Vector3.one * 0.12f, 0.18f)
            .SetEase(Ease.OutQuad);
    }

    private void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.CeilToInt(currentTime);
    }

    private void WinGame()
    {
        gameEnded = true;
        winPanel.SetActive(true);
        AnimatePanel(winPanel);
        Time.timeScale = 0f;
    }

    private void LoseGame()
    {
        gameEnded = true;
        losePanel.SetActive(true);
        AnimatePanel(losePanel);
        Time.timeScale = 0f;
    }

    public void PauseGame()
    {
        if (gameEnded) return;

        pausePanel.SetActive(true);
        AnimatePanel(pausePanel);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void AnimatePanel(GameObject panel)
    {
        RectTransform panelRect = panel.GetComponent<RectTransform>();
        panelRect.localScale = Vector3.zero;

        Sequence seq = DOTween.Sequence();
        seq.SetUpdate(true);

        seq.Append(panelRect.DOScale(1.15f, 0.25f).SetEase(Ease.OutBack));
        seq.Append(panelRect.DOScale(0.95f, 0.15f).SetEase(Ease.InOutQuad));
        seq.Append(panelRect.DOScale(1f, 0.1f).SetEase(Ease.OutQuad));
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}