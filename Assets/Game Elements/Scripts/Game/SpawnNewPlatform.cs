using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNewPlatform : MonoBehaviour
{
    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "player")
        GlobalEventManager.OnPathWaySpawn();
    }
}
