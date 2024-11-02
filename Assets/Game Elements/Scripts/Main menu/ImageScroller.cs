using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ImageScroller : MonoBehaviour
{
    private RawImage _image;

    [SerializeField, Range(0, 10)] private float _scrollSpeed = 0.1f;

    [SerializeField, Range(-1, 1)] private float _xDirection = 1;
    [SerializeField, Range(-1, 1)] private float _yDirection = 1;

    [SerializeField, Range(-1, 1)] private float _smoothSpeed = 1;

    Coroutine coroutine;
    private void Awake() => _image = GetComponent<RawImage>();

    private void Start()
    {
        coroutine = StartCoroutine(ScrollerCoroutine(_smoothSpeed));
    }
    private IEnumerator ScrollerCoroutine(float smoothSpeed)
    {
        while (true)
        {
            _image.uvRect = new Rect(_image.uvRect.position + new Vector2(-_xDirection * _scrollSpeed, _yDirection * _scrollSpeed) * Time.deltaTime, _image.uvRect.size);
            yield return new WaitForSeconds(smoothSpeed);
        }
    }

    private void OnEnable()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        StartCoroutine(ScrollerCoroutine(_smoothSpeed));
    }
}

