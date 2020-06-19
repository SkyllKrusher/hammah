using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private List<Level> levels;
    private int currentLevel;
    private int maxLevel;

    #endregion

    #region Private Methods -------------------------
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        currentLevel = 0;
        LoadLevel(currentLevel);
        maxLevel = levels.Count;
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
        levels[index].levelGameObj.SetActive(true);
        playerMovement.ResetPlayer(levels[index].playerStartPos.position);
    }

    #endregion

    #region  Public Methods ---------------------------
    public void HandleLevelComplete()
    {
        if (currentLevel < maxLevel - 1)
        {
            NextLevel();
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
        UnloadLevel(currentLevel);
        currentLevel = 0;
        LoadLevel(currentLevel);
    }
    #endregion
}
