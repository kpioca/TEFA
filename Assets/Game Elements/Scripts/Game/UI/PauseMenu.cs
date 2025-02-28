using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] GameObject _pausePanel;
    Sequence _animation;

    

    public void PauseMenuActivate()
    {
        PauseGame();
        _animation = DOTween.Sequence();
        _animation.Append(_pausePanel.transform.DOScale(1f, 1f).From(0f))
                  .SetUpdate(true);
        _pauseMenu.SetActive(true);
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
        if (_animation != null && !_animation.active)
        {
            _animation = DOTween.Sequence();
            _animation.Append(_pausePanel.transform.DOScale(0f, 1f).From(1f))
                      .SetUpdate(true)
                      .OnComplete(() =>
                      {
                          _pauseMenu.SetActive(false);
                          ResumeGame();
                      });
        }
    }

    public void RestartGame()
    {
        _gameManager.player_Control.enabled = false;
        Animator animator = _gameManager.contentPlayer.gameObject.GetComponent<Animator>();
        animator.enabled = false;
        animator.Rebind();
        _gameManager.resultMenu.setResult(_gameManager.pathCounter.PathScore, _gameManager.pathCounter.currentStageDistance.Value[1], _gameManager.FishMoney, _gameManager.Food, _gameManager.FishMultiplier);
        _gameManager.StopAllCoroutines();
        _gameManager.unSubscribe();
        GlobalEventManager.GameOver();
        ResumeGame();
        SceneManager.LoadSceneAsync(1);
    }

    public void ReturnMainMenu()
    {
        _gameManager.player_Control.enabled = false;
        Animator animator = _gameManager.contentPlayer.gameObject.GetComponent<Animator>();
        animator.enabled = false;
        animator.Rebind();
        _gameManager.resultMenu.setResult(_gameManager.pathCounter.PathScore, _gameManager.pathCounter.currentStageDistance.Value[1], _gameManager.FishMoney, _gameManager.Food, _gameManager.FishMultiplier);
        _gameManager.StopAllCoroutines();
        _gameManager.unSubscribe();
        GlobalEventManager.GameOver();
        ResumeGame();
        SceneManager.LoadSceneAsync(0);
    }
}
