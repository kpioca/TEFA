using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject[] pauseButtonIcons;
    public void ChangeStatePauseButton()
    {
        if(pauseButtonIcons.Length == 2)
        {
            if (pauseButtonIcons[0].activeInHierarchy)
            {
                PauseGame();
                pauseButtonIcons[0].SetActive(false);
                pauseButtonIcons[1].SetActive(true);
            }
            else
            {
                ResumeGame();
                pauseButtonIcons[0].SetActive(true);
                pauseButtonIcons[1].SetActive(false);
            }
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
