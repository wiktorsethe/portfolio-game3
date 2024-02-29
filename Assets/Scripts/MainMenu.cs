using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private RectTransform mainMenu;
    [SerializeField] private RectTransform optionsMenu;
    private float speed = 0.3f;

    [SerializeField] private Animator transition;
    [SerializeField] private GameObject levelLoader;
    //[SerializeField] private AudioSource buttonSound;
    private void Start()
    {
        mainMenu.DOAnchorPos(Vector2.zero, speed).SetUpdate(true);
        optionsMenu.DOAnchorPos(new Vector2(-800, 0), speed).SetUpdate(true);
    }
    public void Options()
    {
        //buttonSound.Play();
        mainMenu.DOAnchorPos(new Vector2(800, 0), speed).SetUpdate(true);
        optionsMenu.DOAnchorPos(new Vector2(0, 0), speed).SetUpdate(true);
    }
    public void Main()
    {
        //buttonSound.Play();
        mainMenu.DOAnchorPos(new Vector2(0, 0), speed).SetUpdate(true);
        optionsMenu.DOAnchorPos(new Vector2(-800, 0), speed).SetUpdate(true);
    }
    public void QuitGame()
    {
        //buttonSound.Play();
        Application.Quit();
    }
    public void Play()
    {
        //buttonSound.Play();
        StartCoroutine("LoadLevel");
    }
    private IEnumerator LoadLevel()
    {
        levelLoader.SetActive(true);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadSceneAsync(1);
    }
}