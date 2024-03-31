using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
public class LevelMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject waveText;

    private float targetPauseTimeScale = 0f;
    private float targetResumeTimeScale = 1f;
    private float duration = 1f;
    private float initialTimeScale = 1f;
    private bool isPaused = false;
    private PlayerController playerController;
    private WeaponController weaponController;

    [SerializeField] private Animator transition;
    [SerializeField] private GameObject levelLoader;
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
            deathMenu.SetActive(false);
            initialTimeScale = 1f;
        }
    }
    public void Resume()
    {
        if (isPaused)
        {
            mainMenu.SetActive(true);
            pauseMenu.SetActive(false);
            deathMenu.SetActive(false);
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
    public void Quit()
    {
        Time.timeScale = 1f;
        StartCoroutine("LoadMenu");

    }
    private IEnumerator LoadMenu()
    {
        levelLoader.SetActive(true);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadSceneAsync(0);
    }
    public void DeathMenu()
    {
        Time.timeScale = 0f;
        isPaused = true;
        playerController.enabled = false;
        weaponController.enabled = false;
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        deathMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine("LoadRestart");
    }
    private IEnumerator LoadRestart()
    {
        levelLoader.SetActive(true);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
