using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using TMPro;
using UnityEngine;

public class Scores : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _tMPro;

    private static Scores instance;

    private int _scoreP1;
    private int _scoreP2;

    private Scores() { }

    public static Scores Instance { get { return instance; } }

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);    

        instance = this;
        Debug.Log(instance);
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
        _tMPro.text = _scoreP1 + " - " + _scoreP2;
    }

    public void Testing()
    {
       //Debug.Log("Mes couilles");
    }
}
