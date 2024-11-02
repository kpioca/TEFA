using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Base demo class.
/// </summary>
public abstract class Demo : MonoBehaviour
{
  [SerializeField]
  private float fadeImageTime;

  [SerializeField]
  private RawImage fadeImage;

  [Space(10.0f), SerializeField]
  private float fadeTextTime;

  [SerializeField]
  private RawImage logoImage;

  [SerializeField]
  private float readInfoTime;

  [SerializeField]
  private Text infoText;

  [Space(10.0f), SerializeField]
  private Text creditsText;

  [SerializeField]
  private float readCreditsTime;

  [SerializeField]
  private Color textColor;

  [Space(10.0f), SerializeField]
  private float volumeMusic;

  [SerializeField]
  private AudioSource audioSource = null;

  [SerializeField]
  private AudioClip music = null;

  protected abstract void OnDemoEnable();
  protected abstract void OnDemoReady();
  protected abstract void OnDemoUpdate();
  protected abstract void OnDemoEnd();

  protected TaskQueue taskQueue = new TaskQueue();

  private bool demoReady = false;

  public static Color TransparentColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);

  private void Awake()
  {
    fadeImage.gameObject.SetActive(true);
    logoImage.gameObject.SetActive(false);
    infoText.gameObject.SetActive(false);
    creditsText.gameObject.SetActive(false);

    if (audioSource != null && music != null)
    {
      audioSource.loop = true;
      audioSource.clip = music;
      audioSource.volume = 0.0f;
    }
  }

  private void OnEnable()
  {
    OnDemoEnable();

    taskQueue.Enqueue(fadeImageTime,
                      () => fadeImage.gameObject.SetActive(true),
                      (progress) => fadeImage.color = Color.Lerp(Color.black, TransparentColor, progress),
                      () => fadeImage.gameObject.SetActive(false));

    // Text fade in.
    taskQueue.Enqueue(fadeTextTime,
                      () =>
                      {
                        logoImage.gameObject.SetActive(true);
                        infoText.gameObject.SetActive(true);
                      },
                      (progress) =>
                      {
                        infoText.color = Color.Lerp(TransparentColor, textColor, progress);
                        logoImage.color = Color.Lerp(TransparentColor, Color.white, progress);
                      });

    // Text wait.
    taskQueue.Enqueue(readInfoTime);

    // Text fade out & demo ready.
    taskQueue.Enqueue(fadeTextTime,
                      () => { if (audioSource != null) audioSource.Play(); },
                      (progress) =>
                      {
                        infoText.color = Color.Lerp(textColor, TransparentColor, progress);
                        logoImage.color = Color.Lerp(Color.white, TransparentColor, progress);
                        if (audioSource != null)
                          audioSource.volume = Mathf.Lerp(0.0f, volumeMusic, progress);
                      },
                      () =>
                      {
                        infoText.gameObject.SetActive(false);
                        logoImage.gameObject.SetActive(false);
                        OnDemoReady();
                        demoReady = true;
                      });
  }

  private void Update()
  {
    taskQueue.Update(Time.deltaTime);

    if (demoReady == true)
      OnDemoUpdate();
  }

  protected void DemoQuit()
  {
    demoReady = false;

    OnDemoEnd();

    // Text fade in.
    taskQueue.Enqueue(fadeTextTime,
                      () => creditsText.gameObject.SetActive(true),
                      (progress) => creditsText.color = Color.Lerp(TransparentColor, textColor, progress));

    // Text wait.
    taskQueue.Enqueue(readCreditsTime);

    // Fade out.
    taskQueue.Enqueue(fadeTextTime,
                     () => fadeImage.gameObject.SetActive(true),
                     (progress) =>
                     {
                       fadeImage.color = Color.Lerp(TransparentColor, Color.black, progress);
                       creditsText.color = Color.Lerp(textColor, TransparentColor, progress);
                       if (audioSource != null)
                         audioSource.volume = Mathf.Lerp(volumeMusic, 0.0f, progress);
                     },
                     () => creditsText.gameObject.SetActive(false));
  }
}
