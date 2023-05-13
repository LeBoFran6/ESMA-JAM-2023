using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BP_GameManager : MonoBehaviour
{
    public GameObject ScriptHolderP1;
    public GameObject ScriptHolderP2;

    public bool P1Die = false;
    public bool P2Die = false;

    public int P1Score = 0;
    public int P2Score = 0;

    public GameObject P1;
    public GameObject P2;

    public GameObject SpawnP1;
    public GameObject SpawnP2;

    public float timer = 0;
    public bool timerOn = true;


    public bool Phase1 = true;
    public GameObject Phase1PlaceHolder;

    // Start is called before the first frame update
    void Start()
    {
        Phase1PlaceHolder.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn) 
        {
            timer = timer + 1 * Time.deltaTime;
        }
        if (timer > 5) 
        {
            timerOn = false;
            timer = 0;
            Phase1 = false;
            Phase1PlaceHolder.SetActive(true);
        }

        if(Phase1 == false) 
        { 

            if(P1Die == true) 
            {
                ScriptHolderP1.gameObject.GetComponent<SC_FPSController>().canMove = false;
                P1.transform.position = SpawnP1.transform.position;
                ScriptHolderP1.gameObject.GetComponent<SC_FPSController>().canMove = true;
                P1Die = false;
                P2Score = P2Score + 1;
                timerOn = true;
                Phase1 = true;
                Phase1PlaceHolder.SetActive(true);
            }
            if (P2Die == true)
            {
                ScriptHolderP2.gameObject.GetComponent<SC_FPSController>().canMove = false;
                P2.transform.position = SpawnP2.transform.position;
                ScriptHolderP2.gameObject.GetComponent<SC_FPSController>().canMove = true;
                P2Die = false;
                P1Score = P1Score + 1;
                timerOn = true;
                Phase1 = true;
                Phase1PlaceHolder.SetActive(true);
            }

        }
    }
}
