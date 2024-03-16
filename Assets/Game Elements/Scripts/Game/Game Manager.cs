using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [Header("Route parameters")]
    [SerializeField] private float speedMovement = 5f;
    public float SpeedMovement => speedMovement;

    private float speedCount;
    public float SpeedCount => speedCount;



    void Awake()
    {
        speedCount = speedMovement / 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
