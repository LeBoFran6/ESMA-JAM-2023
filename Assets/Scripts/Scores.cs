using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using TMPro;
using UnityEngine;

public class Scores : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _txtScoreP1;
    [SerializeField]
    TextMeshProUGUI _txtScoreP2;
    [SerializeField]
    TextMeshProUGUI _txtTimer;

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
