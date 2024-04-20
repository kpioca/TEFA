using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMoving : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private GameManager manager;
    [Header("Parameters")]
    [SerializeField] private float speedMovement = 1f;

    private float despawnPosZ;
    private float startPosZ = -5f;
    private float startSpawnPosZ;
    private float posDifference = 74.43f;
    void Start()
    {
        //setting parameters and references
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        GlobalEventManager.OnGameOver += GameOver;
        GlobalEventManager.OnChangeSpeedRouteMovement += ChangeSpeedMovement;

        speedMovement = manager.SpeedRouteMovement;
        despawnPosZ = startPosZ - posDifference;
    }

    void GameOver()
    {
        this.enabled = false;
    }

    void ChangeSpeedMovement(float speed)
    {
        speedMovement = speed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 new_position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speedMovement / 2.4f);
        transform.position = Vector3.Lerp(transform.position, new_position, speedMovement / 2.4f * Time.deltaTime);

        if (transform.position.z <= despawnPosZ)
        {
            startSpawnPosZ = transform.position.z + posDifference*2;
            transform.position = new Vector3(transform.position.x, transform.position.y, startSpawnPosZ);
        }
    }
}
