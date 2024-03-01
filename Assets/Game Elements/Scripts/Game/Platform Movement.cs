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
        speedMovement = manager.SpeedMovement;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * speedMovement * Time.deltaTime);
    }
}
