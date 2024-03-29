using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class LevelMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject waveText;

    private float targetPauseTimeScale = 0f;
    private float targetResumeTimeScale = 1f;
    private float duration = 1f;
    private float initialTimeScale = 1f;
    private bool isPaused = false;
    private PlayerController playerController;
    private WeaponController weaponController;
    private void Start()
    {
        playerController = GameObject.FindObjectOfType(typeof(PlayerController)) as PlayerController;
        weaponController = GameObject.FindObjectOfType(typeof(WeaponController)) as WeaponController;
        Color waveTextColor = waveText.GetComponent<TMP_Text>().color;
        waveTextColor.a = 0f;
        waveText.GetComponent<TMP_Text>().color = waveTextColor;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Cursor.lockState = CursorLockMode.None;
                StartCoroutine(PauseAnimation());
                playerController.enabled = false;
                weaponController.enabled = false;
                isPaused = true;
            }
        }
        Debug.Log(Time.timeScale);
    }
    public void ShowWaveText(int waveValue)
    {
        TMP_Text text = waveText.GetComponent<TMP_Text>();
        text.text = "Wave " + waveValue;
        Sequence textSequence = DOTween.Sequence();

        textSequence.Append(text.DOFade(1f, 1f));

        textSequence.AppendInterval(3f);

        textSequence.Append(text.DOFade(0f, 1f));
    }
    private IEnumerator PauseAnimation()
    {
        float timer = 0f;
        while (timer < duration)
        {
            Time.timeScale = Mathf.Lerp(initialTimeScale, targetPauseTimeScale, timer / duration);
            timer += Time.unscaledDeltaTime;
            yield return null;
        }
        
        Time.timeScale = targetPauseTimeScale;

        if (Time.timeScale == 0f)
        {
            mainMenu.SetActive(false);
            pauseMenu.SetActive(true);
            initialTimeScale = 1f;
        }
    }
    public void Resume()
    {
        if (isPaused)
        {
            mainMenu.SetActive(true);
            pauseMenu.SetActive(false);
            playerController.enabled = true;
            weaponController.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            StartCoroutine(ResumeAnimation());
            isPaused = false;
        }
    }
    private IEnumerator ResumeAnimation()
    {
        float timer = 0f;
        while (timer < duration)
        {
            Time.timeScale = Mathf.Lerp(initialTimeScale, targetResumeTimeScale, timer / duration);
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = targetResumeTimeScale;

        if (Time.timeScale == 1f)
        {
            initialTimeScale = 0f;
        }
    }
}
