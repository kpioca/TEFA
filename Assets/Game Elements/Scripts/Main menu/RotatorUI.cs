using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorUI : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float smooth = 0.025f;

    Coroutine coroutine;
    void Start()
    {
        coroutine = StartCoroutine(RotatingCoroutine(this.gameObject, speed, smooth));
    }

    private IEnumerator RotatingCoroutine(GameObject gameObject, float speed = 10, float smooth = 0.05f)
    {
        while (true)
        {
            gameObject.transform.eulerAngles += new Vector3(0.0f, 0.0f, speed);
            yield return new WaitForSeconds(smooth);
        }
    }

    private void OnEnable()
    {
        if(coroutine != null)
            StopCoroutine(coroutine);
        StartCoroutine(RotatingCoroutine(this.gameObject, speed, smooth));
    }

    public void ResetRotation() => gameObject.transform.eulerAngles = new Vector3(0, 0, 0);

}
