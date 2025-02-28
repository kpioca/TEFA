using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNewPlatform : MonoBehaviour
{
    public GeneratorLevel generatorLevel { get; private set; }
    public void Initialize(GeneratorLevel generatorLevel)
    {
        this.generatorLevel = generatorLevel;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "despawnBorder")
        {
            if(generatorLevel != null)
                generatorLevel.spawnNewPathWay();
        }
    }
}
