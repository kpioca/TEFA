using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClosePauseMenu : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] PauseMenu pauseMenu;
    public event Action<ClosePauseMenu> Click;
    public void OnPointerClick(PointerEventData eventData) => Click?.Invoke(this);

    private void OnEnable()
    {
        Click += ResumeGame;
    }

    private void OnDisable()
    {
        Click -= ResumeGame;
    }

    public void ResumeGame(ClosePauseMenu closePauseMenu)
    {
        pauseMenu.PauseMenuClose();
    }


}
