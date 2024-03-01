using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMoving : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private GameManager manager;
    [Header("Parameters")]
    [SerializeField] private float speedMovement = 1f;

    private float endPosZ = -239.5f;
    private float startPosZ = 210.5f;
    void Start()
    {
        //setting parameters and references
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        speedMovement = manager.SpeedMovement;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * speedMovement * Time.deltaTime);
        if (transform.position.z >= endPosZ)
            transform.position = new Vector3(transform.position.x, transform.position.y, startPosZ);
    }
}
