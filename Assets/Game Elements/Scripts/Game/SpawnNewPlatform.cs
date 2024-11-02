using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNewPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "despawnBorder")
        {
            GlobalEventManager.SpawnPathWay();
        }
    }
}
