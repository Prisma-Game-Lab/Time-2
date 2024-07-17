using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    [SerializeField] GameObject transitionObject;
    public GameObject circleObject;
    GameObject player;
    Animator anim;
    bool isOnTransition;
    bool gameIsPaused;

    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject gameOverUI;

    public delegate void OnSceneTransition();
    public static OnSceneTransition onSceneTransition;

    public AudioSource music, sfx;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        transitionObject.SetActive(true);
        player = GameObject.Find("Player");
    }

    private void OnEnable()
    {
        onSceneTransition += AnimationTransition;
    }

    private void OnDisable()
    {
        onSceneTransition -= AnimationTransition;
    }

    private void FixedUpdate()
    {
        if (isOnTransition) 
        {
            circleObject.transform.position = Camera.main.WorldToScreenPoint(player.transform.position);
        }
    }

    private IEnumerator WaitForAnimation() 
    {
        transitionObject.SetActive(true);
        isOnTransition = true;
        yield return new WaitForSeconds(2);
        isOnTransition = false;
        transitionObject.SetActive(false);
    }

    public void AnimationTransition() 
    {
        anim.SetTrigger("LeavingScene");
        StartCoroutine(WaitForAnimation());
    }

    public void PauseGame() 
    {
        if (!gameIsPaused) 
        {
            Time.timeScale = 0;
            pauseUI.SetActive(true);
        }
        else 
        {
            Time.timeScale = 1;
            pauseUI.SetActive(false);
        }
        gameIsPaused = !gameIsPaused;
    }

    public void LoadMainMenu() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void LoadCurrentScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void GameOver() 
    {
        Time.timeScale = 0;
        gameOverUI.SetActive(true);
    }

    public void MusicVolume(float MusicValue)
    {
        AudioManager.instance.MusicVolume(MusicValue);
        music.volume = MusicValue;
    }

    public void SFXVolume(float SFXValue)
    {
        AudioManager.instance.SFXVolume(SFXValue);
        sfx.volume = SFXValue;
    }
}
