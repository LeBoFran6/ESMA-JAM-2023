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
        LaunchTimer();
    }

    public void LaunchTimer()
    {
        _timer = _setTime;
        StartCoroutine(DecreaseTimer());
    }

    private IEnumerator DecreaseTimer()
    {
        while (_timer >= 0)
        {
            _timer -= 0.1f;
            _txtTimer.text = Math.Round(_timer, 1).ToString();
            yield return new WaitForSeconds(0.1f);
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
