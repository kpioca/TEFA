using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoverBullets : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out ContentBullet content))
            content.Remove();
    }

}
