using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionZone : MonoBehaviour
{
    [SerializeField] private GameObject movablePart;
    public GameObject MovablePart => movablePart;
    [SerializeField] private GameObject mainObject;
    [SerializeField] private GameObject player;

    [SerializeField] private int speed = 10;
    [SerializeField] private float intervalBetweenRotation = 0.05f;

    Coroutine seeCoroutine;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (player == null)
                player = other.gameObject;
            seeCoroutine = StartCoroutine(SeeCoroutine());
        }
    }

    IEnumerator SeeCoroutine()
    {
        while (true)
        {
            Vector3 direction = player.transform.position + new Vector3(0, 0, 0.3f) - movablePart.transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);

            //movablePart.transform.LookAt(player.transform.position);
            movablePart.transform.rotation = Quaternion.Lerp(movablePart.transform.rotation, rotation, speed * Time.deltaTime);

            yield return new WaitForSeconds(intervalBetweenRotation);
        }

    }

    public void turnToInitialState()
    {
        movablePart.transform.localRotation = Quaternion.identity;
    }
}
