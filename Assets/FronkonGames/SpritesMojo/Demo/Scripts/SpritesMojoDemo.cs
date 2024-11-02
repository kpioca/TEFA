using UnityEngine;

/// <summary>
/// Sprites Mojo demo.
/// </summary>
public class SpritesMojoDemo : Demo
{
  [Space(10.0f), SerializeField]
  private Deck deck;

  protected override void OnDemoEnable()
  {
    deck.gameObject.SetActive(false);
    deck.transform.position = new Vector3(deck.transform.position.x, -7.0f, deck.transform.position.z);
  }

  protected override void OnDemoReady()
  {
    taskQueue.Enqueue(1.0f,
                      () => deck.gameObject.SetActive(true),
                      (progress) => deck.transform.position = new Vector3(deck.transform.position.x, Mathf.Lerp(-7.0f, -3.5f, progress), deck.transform.position.z));
  }

  protected override void OnDemoUpdate()
  {
#if !UNITY_WEBGL
    if (Input.GetKeyDown(KeyCode.Escape) == true)
      DemoQuit();
#endif
  }

  protected override void OnDemoEnd()
  {
    deck.gameObject.SetActive(false);
  }
}
