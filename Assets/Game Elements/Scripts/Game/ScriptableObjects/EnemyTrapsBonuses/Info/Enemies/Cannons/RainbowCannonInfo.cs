using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RainbowCannon_Properties", menuName = "LevelProperties/Enemy/New RainbowCannon_Properties")]
public class RainbowCannonInfo : CannonInfo
{

    [SerializeField] float intervalBetweenShots = 0.5f;
    public float IntervalBetweenShots => intervalBetweenShots;

    public override Enemy createEnemy(GameObject cannonObject, Stamp stamp, GameObject[] objParameters, out Dictionary<string, float> numParameters)
    {
        objParameters[0].SetActive(false);
        numParameters = new Dictionary<string, float>();
        float intervalBetweenShots = 0;
        int n_shots = 0;
        RainbowCannon cannon = new RainbowCannon(this, objParameters[0], out intervalBetweenShots, out n_shots, cannonObject, stamp);
        numParameters["intervalBetweenShots"] = intervalBetweenShots;
        numParameters["n_shots"] = n_shots;
        return cannon;
    }


}
