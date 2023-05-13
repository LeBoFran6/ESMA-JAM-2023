using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_GameManager : MonoBehaviour
{
    public GameObject ScriptHolderP1;
    public GameObject ScriptHolderP2;

    public bool P1Die = false;
    public bool P2Die = false;

    public GameObject P1;
    public GameObject P2;

    public GameObject SpawnP1;
    public GameObject SpawnP2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(P1Die == true) 
        {
            ScriptHolderP1.gameObject.GetComponent<SC_FPSController>().canMove = false;
            P1.transform.position = SpawnP1.transform.position;
            ScriptHolderP1.gameObject.GetComponent<SC_FPSController>().canMove = false;
            P1Die = false;
        }
        if (P2Die == true)
        {
            ScriptHolderP2.gameObject.GetComponent<SC_FPSController>().canMove = false;
            P2.transform.position = SpawnP2.transform.position;
            ScriptHolderP2.gameObject.GetComponent<SC_FPSController>().canMove = false;
            P2Die = false;
        }
    }
}
