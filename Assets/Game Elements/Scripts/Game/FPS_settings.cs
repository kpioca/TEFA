using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_settings : MonoBehaviour
{
    [SerializeField] int fps_value = 60;
    public int Fps_value => fps_value;
    void Start()
    {
        Application.targetFrameRate = fps_value;
    }

}
