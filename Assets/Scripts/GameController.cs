using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Level
{
    public GameObject levelGameObj;
    public Transform playerStartPos;
}
public class GameController : MonoBehaviour
{
    #region Variables --------------------
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject timerObj;
    [SerializeField] private Text timerText;
    [SerializeField] private Text finishTimeText;
    [SerializeField] private List<Level> levels;
    private int currentLevel;
    private int maxLevel;
    private int timePassed;
    private Coroutine timerCoroutine;

    #endregion

    #region Private Methods -------------------------
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        AudioManager.instance.PlayMusic();
        currentLevel = 0;
        LoadLevel(currentLevel);
        maxLevel = levels.Count;
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            timerText.text = timePassed.ToString() + "s";
            timePassed++;
            yield return new WaitForSeconds(1f);
        }
    }

    private void StartTimer()
    {
        timePassed = 0;
        timerCoroutine = StartCoroutine(Timer());
    }

    private void StopTimer()
    {
        StopCoroutine(timerCoroutine);
    }

    private void ShowTimer()
    {
        timerObj.SetActive(true);
    }

    private void HideTimer()
    {
        timerObj.SetActive(false);
    }

    private void ShowFinishTimeText()
    {
        finishTimeText.gameObject.SetActive(true);
    }

    private void HideFinishTimeText()
    {
        finishTimeText.gameObject.SetActive(false);
    }

    private void UpdateFinishTimeText()
    {
        finishTimeText.text = "You completed the game in " + timePassed.ToString() + "s";
    }

    private void NextLevel()
    {
        UnloadLevel(currentLevel);
        currentLevel += 1;
        LoadLevel(currentLevel);
    }

    private void UnloadLevel(int index)
    {
        levels[index].levelGameObj.SetActive(false);
    }

    private void LoadLevel(int index)
    {
        Debug.Log("Level Index: " + index);
        levels[index].levelGameObj.SetActive(true);
        playerMovement.ResetPlayer(levels[index].playerStartPos.position);
    }

    #endregion

    #region  Public Methods ---------------------------
    public void HandleLevelComplete()
    {
        AudioManager.instance.PlayLevelCompleteSFX();
        Debug.Log("Level Complete");
        if (currentLevel < maxLevel - 1)
        {
            NextLevel();
            if (currentLevel == 1)
            {
                ShowTimer();
                StartTimer();
            }
            else if (currentLevel == maxLevel - 1)
            {
                StopTimer();
                HideTimer();
                UpdateFinishTimeText();
                ShowFinishTimeText();
            }
        }
        else
        {
            RestartGame();
        }

    }

    public void ResetCurrentLevel()
    {
        playerMovement.ResetPlayer(levels[currentLevel].playerStartPos.position);
    }

    public void RestartGame()
    {
        HideFinishTimeText();
        UnloadLevel(currentLevel);
        currentLevel = 0;
        LoadLevel(currentLevel);
    }
    #endregion
}
