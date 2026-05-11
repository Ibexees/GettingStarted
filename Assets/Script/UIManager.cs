using DG.Tweening;
using System;
using System.Collections;
using System.IO.Compression;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI coinCounterText;
    [SerializeField] private PlayerCharacter character;
    [SerializeField] private Image healthBar;

    [SerializeField] private TextMeshProUGUI GameOverText;
    [SerializeField] private CanvasGroup GameOverGroup;
    [SerializeField] private CanvasGroup Health;
    [SerializeField] private CanvasGroup Coin;
    [SerializeField] private Button tryAgainButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Image victoryImage;
    [SerializeField] private AudioSource unlockSound;

    private static UIManager instance = null;
    public static UIManager Instance => instance;
    [SerializeField] private float duration; 

    public int coinCounter = 0;

    private void Awake()
    { 
        instance = this; 
    }

    public void unlock()
    {
        unlockSound.Play();
    }

    public void CollectCoin()

    { 
        this.coinCounter++;
        string coinText = coinCounter.ToString();
        this.coinCounterText.text = coinText;
    }

    public void GameOver()
    {
        StartCoroutine(FadeIn(GameOverGroup));
        StartCoroutine(FadeOut(Health,false));
        StartCoroutine(FadeOut(Coin,false));
    }

    public void Victory()
    {
        Color placeHolder = victoryImage.color;
        placeHolder.a = 1;
        victoryImage.color = placeHolder;

        string victory = "Victory";
        GameOverText.text = victory;
        StartCoroutine(FadeIn(GameOverGroup));
    
    }


    

    IEnumerator FadeIn(CanvasGroup canvasGroup)
    {
       
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;

            canvasGroup.alpha = Mathf.Lerp(0f, 1f, time / duration);

            yield return null;
        }

        canvasGroup.alpha = 1f;

    }

    IEnumerator FadeOut(CanvasGroup canvasGroup, Boolean withReset)
    {

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;

            canvasGroup.alpha = Mathf.Lerp(1f, 0f, time / duration);

            yield return null;
        }

        canvasGroup.alpha = 0f;
        if (withReset)
        {
            DOTween.KillAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    private void Start()
    {
        
        Color placeHolder = victoryImage.color;
        placeHolder.a = 0;
        victoryImage.color = placeHolder;

        GameOverGroup.alpha = 0f;

        this.tryAgainButton.onClick.AddListener(TryAgain);
        this.quitButton.onClick.AddListener(Quit);
    }

    void TryAgain()
    {
        StartCoroutine(FadeOut(GameOverGroup, true));     
    }
    void Quit()
    { 
        Application.Quit();
    }


    private void Update()
    {
        float healthPercentage = this.character.GetCurrentHealth() / this.character.GetMaxHealth();
        healthBar.fillAmount = healthPercentage;
        //character.InflictDamage(healthPercentage);
    
    }

    



}
