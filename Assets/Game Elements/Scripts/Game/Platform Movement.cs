using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    private GameManager manager;
    private float speedMovement = 1f;
    Sequence _animation;


    public void Initialize(GameManager gameManager)
    {
        //setting parameters and references
        manager = gameManager;
        speedMovement = manager.SpeedRouteMovement;
    }

    // Update is called once per frame

    private void OnEnable()
    {
        GlobalEventManager.OnGameOver += GameOver;
        GlobalEventManager.OnChangeSpeedRouteMovement += ChangeSpeedMovement;

        //StartMovement();
    }

    private void OnDisable()
    {
        GlobalEventManager.OnGameOver -= GameOver;
        GlobalEventManager.OnChangeSpeedRouteMovement -= ChangeSpeedMovement;

        //StopMovement();
    }

    void unSubscribe()
    {
        GlobalEventManager.OnGameOver -= GameOver;
        GlobalEventManager.OnChangeSpeedRouteMovement -= ChangeSpeedMovement;
    }

    void GameOver()
    {
        unSubscribe();
        this.enabled = false;
    }

    void ChangeSpeedMovement(float speed)
    {
        speedMovement = speed;
    }

    /*
    void StartMovement()
    {
        _animation = DOTween.Sequence();
        _animation.Append(transform.DOMoveZ(transform.position.z - speedMovement / 2.4f, 0.5f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.InOutQuad));
    }

    void StopMovement()
    {
        if (_animation != null && _animation.active)
            _animation.Kill();
    }
    */

    
    void Update()
    {
        Vector3 new_position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speedMovement/2.4f);
        transform.position = Vector3.Lerp(transform.position, new_position, speedMovement/2.4f * Time.deltaTime);

        //transform.Translate(Vector3.back * speedMovement * Time.deltaTime);
    }
    
}
