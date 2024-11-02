using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject pauseMenu;

    IEnumerator PauseCoroutine()
    {
        pauseMenu.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 0;
    }

    public void PauseMenuActivate()
    {
        pauseMenu.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void PauseMenuClose()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        gameManager.player_Control.enabled = false;
        Animator animator = gameManager.contentPlayer.gameObject.GetComponent<Animator>();
        animator.enabled = false;
        animator.Rebind();
        gameManager.resultMenu.setResult(gameManager.pathCounter.PathScore, gameManager.pathCounter.currentStageDistance.Value[1], gameManager.FishMoney, gameManager.Food, gameManager.FishMultiplier);
        gameManager.StopAllCoroutines();
        gameManager.unSubscribe();
        GlobalEventManager.GameOver();
        ResumeGame();
        SceneManager.LoadSceneAsync(1);
    }

    public void ReturnMainMenu()
    {
        gameManager.player_Control.enabled = false;
        Animator animator = gameManager.contentPlayer.gameObject.GetComponent<Animator>();
        animator.enabled = false;
        animator.Rebind();
        gameManager.resultMenu.setResult(gameManager.pathCounter.PathScore, gameManager.pathCounter.currentStageDistance.Value[1], gameManager.FishMoney, gameManager.Food, gameManager.FishMultiplier);
        gameManager.StopAllCoroutines();
        gameManager.unSubscribe();
        GlobalEventManager.GameOver();
        ResumeGame();
        SceneManager.LoadSceneAsync(0);
    }
}
