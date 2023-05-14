using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scores : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _txtScoreP1;
    [SerializeField]
    TextMeshProUGUI _txtScoreP2;
    [SerializeField]
    TextMeshProUGUI _txtTimer;

    private BP_GameManager _gameManager;
    
    public GameObject Win;
    public GameObject Win2;
    public GameObject Loose;
    public GameObject Loose2;

    public GameObject LoseFractur1;
    public GameObject LoseFractur2;

    private static Scores instance;

    private int _scoreP1;
    private int _scoreP2;

    [SerializeField]
    private float _setTime;

    private float _timer;
    private bool _doOnce = true;

    private Scores() { }

    public static Scores Instance { get { return instance; } }

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);    

        instance = this;
    }

    private void Start()
    {
        _gameManager = gameObject.GetComponent<BP_GameManager>();
        LaunchTimer();
    }

    private void Update()
    {
        Debug.Log(_gameManager.timerOn);
        if (_doOnce)
        {
            if (_gameManager.m_timerSlider.gameObject)
            {
                StopAllCoroutines();
                //_timer = 100;
                //_txtTimer.text = "Phase 1";
                LaunchTimer();
                Debug.Log(_txtTimer.text);
                _doOnce = false;
            }
            else
            {
                //_txtTimer.text = Math.Round(_timer, 1).ToString();
                _doOnce = true;
                Debug.Log(_txtTimer.text);
            }
        }
    }

    public void LaunchTimer()
    {
        _timer = _setTime;
        _txtTimer.text = "Phase 1";
        StartCoroutine(DecreaseTimer());
    }

    private IEnumerator DecreaseTimer()
    {
        yield return new WaitForSeconds(16);
        while (_timer >= 0)
        {
            _timer -= 1;
            _txtTimer.text = Math.Round(_timer, 1).ToString();
            yield return new WaitForSeconds(1);
        }
        TimerEnd();
    }

    private void TimerEnd()
    {
        if(_scoreP1 > _scoreP2)
        {
            Win.SetActive(true);
            Loose2.SetActive(true);

            LoseFractur2.SetActive(true);
        }
        else if (_scoreP1 < _scoreP2)
        {
            Win2.SetActive(true);
            Loose.SetActive(true);

            LoseFractur1.SetActive(true);
        }
        else
        {
            Win.SetActive(false);
            Win2.SetActive(false);
            Loose.SetActive(false);
            Loose2.SetActive(false);

            LoseFractur1.SetActive(false);
            LoseFractur2.SetActive(false);
        }
        StartCoroutine(LoadMenu());
    }

    private IEnumerator LoadMenu()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }

    public void IncreaseScore(int player)
    {
        if (player == 0)
            _scoreP1++;
        else
            _scoreP2++;
        UpdateScores();
        
    }

    private void UpdateScores()
    {
        _txtScoreP1.text = _scoreP1.ToString();
        _txtScoreP2.text = _scoreP2.ToString();
    }
}
