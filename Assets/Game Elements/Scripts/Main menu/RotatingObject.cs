using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float smooth = 0.025f;
    void Start()
    {
        StartCoroutine(RotatingCoroutine(this.gameObject, speed, smooth));
    }

    private IEnumerator RotatingCoroutine(GameObject gameObject, float speed = 10, float smooth = 0.05f)
    {
        while (true)
        {
            gameObject.transform.eulerAngles += new Vector3(0.0f, speed, 0.0f);
            yield return new WaitForSeconds(smooth);
        }
    }

}
