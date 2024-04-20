using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private GameManager manager;
    [Header("Parameters")]
    [SerializeField] private float speedMovement = 1f;



    void Start()
    {
        //setting parameters and references
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        speedMovement = manager.SpeedRouteMovement;
        GlobalEventManager.OnGameOver += GameOver;
        GlobalEventManager.OnChangeSpeedRouteMovement += ChangeSpeedMovement;
        GlobalEventManager.OnUnSubscribe += unSubscribe;
    }

    // Update is called once per frame

    void unSubscribe()
    {
        GlobalEventManager.OnGameOver -= GameOver;
        GlobalEventManager.OnChangeSpeedRouteMovement -= ChangeSpeedMovement;
        GlobalEventManager.OnUnSubscribe -= unSubscribe;
    }

    void GameOver()
    {
        this.enabled = false;
    }

    void ChangeSpeedMovement(float speed)
    {
        speedMovement = speed;
    }

    void Update()
    {
        Vector3 new_position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speedMovement/2.4f);
        transform.position = Vector3.Lerp(transform.position, new_position, speedMovement/2.4f * Time.deltaTime);

        //transform.Translate(Vector3.back * speedMovement * Time.deltaTime);
    }
}
