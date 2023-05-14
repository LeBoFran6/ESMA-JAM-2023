using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BP_GameManager : MonoBehaviour
{
    public GameObject BarreDeChargement;

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

    //public GameObject GoP1;
    //public GameObject GoP2;

    public float timer = 0;
    public bool timerOn = true;


    public bool Phase1 = true;
    public GameObject Phase1PlaceHolder;


    //buttons
     public GameObject _0D;
     public GameObject _1F;
     public GameObject _2G;
     public GameObject _0J;
     public GameObject _1K;
     public GameObject _2L;
 



    public GameObject killme;
    public GameObject killHim;
    public GameObject jump;
    public GameObject freeze;



    public bool Action0 = false;
    public bool Action1 = false;
    public bool Action2 = false;


    public int action = 0;

    private static BP_GameManager instance;

    public static BP_GameManager Instance { get { return instance; } }

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _0D.SetActive(false);
        _1F.SetActive(false);
        _2G.SetActive(false);
        _0J.SetActive(false);
        _1K.SetActive(false);
        _2L.SetActive(false);

        Phase1PlaceHolder.SetActive(false);
        PhasePrep();
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
            
            Phase1PlaceHolder.SetActive(true);
            //GoP1.SetActive(true);
            //.SetActive(true);

            if (action == 0)
            {
                killme.SetActive(true);
            }
            if (action == 1)
            {
                killHim.SetActive(true);
            }
            if (action == 2)
            {
                jump.SetActive(true);
            }
            if (action == 3)
            {
                freeze.SetActive(true);
            }

            BarreDeChargement.transform.localScale = BarreDeChargement.transform.localScale += new Vector3(0f,0f, 1f); 

        }
        if(timer > 5.5) 
        {
            killme.SetActive(false);
            killHim.SetActive(false);
            jump.SetActive(false);
            freeze.SetActive(false);
            //GoP1.SetActive(false);
            //GoP2.SetActive(false);
            timerOn = false;
            timer = 0;
            Phase1 = false;
            P1Die = false;
            P2Die = false;
        }

        if(Phase1 == false) 
        { 

            if(P1Die == true) 
            {
                ScriptHolderP1.gameObject.GetComponent<SC_FPSController>()._canMove = false;
                //ScriptHolderP2.gameObject.GetComponent<SC_FPSController>().canMove = false;
                P1.transform.position = SpawnP1.transform.position;
                //P2.transform.position = SpawnP2.transform.position;
                timerOn = true;
                if(timer > 1) 
                {
                    ScriptHolderP2.gameObject.GetComponent<SC_FPSController>()._canMove = false;
                    P2.transform.position = SpawnP2.transform.position;
                }
                if(timer > 2)
                {
                    ScriptHolderP1.gameObject.GetComponent<SC_FPSController>()._canMove = true;
                    ScriptHolderP2.gameObject.GetComponent<SC_FPSController>()._canMove = true;
                    timerOn = false;
                    timer = 0;
                    P1Die = false;
                    P2Die = false;
                    timerOn = true;
                    Phase1 = true;
                    Phase1PlaceHolder.SetActive(false);
                    Clean();
                    PhasePrep();

                }

              
            }


            if (P2Die == true)
            {
                //ScriptHolderP1.gameObject.GetComponent<SC_FPSController>().canMove = false;
                ScriptHolderP2.gameObject.GetComponent<SC_FPSController>()._canMove = false;
                //P1.transform.position = SpawnP1.transform.position;
                P2.transform.position = SpawnP2.transform.position;
                timerOn = true;
                if(timer > 1)
                {
                    ScriptHolderP1.gameObject.GetComponent<SC_FPSController>()._canMove = false;
                    P1.transform.position = SpawnP1.transform.position;
                }
                if (timer > 2)
                {
                    ScriptHolderP1.gameObject.GetComponent<SC_FPSController>()._canMove = true;
                    ScriptHolderP2.gameObject.GetComponent<SC_FPSController>()._canMove = true;
                    timerOn = false;
                    timer = 0;
                    P1Die = false;
                    P2Die = false;
                    timerOn = true;
                    Phase1 = true;
                    Phase1PlaceHolder.SetActive(false);
                    Clean();
                    PhasePrep();
                }


            }
           
        }

        //0
        if (Action0 == true && Phase1 == false)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                P1Die = true;
                Action0 = false;
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                P2Die = true;
                Action0 = false;
            }
        }

        //1
        if (Action1 == true && Phase1 == false)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                P2Die = true;
                Action1 = false;
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                P1Die = true;
                Action1 = false;
            }
        }

        //2
        if (Action2 == true && Phase1 == false)
        {
            if (Input.GetKeyDown(KeyCode.G) && timerOn == false)
            {
                ScriptHolderP2.gameObject.GetComponent<SC_FPSController>()._canMove = false;
                timerOn = true;
                if(timer > 2) 
                {
                    ScriptHolderP2.gameObject.GetComponent<SC_FPSController>()._canMove = true;
                    Action2 = false;
                    timerOn = false;
                    timer = 0;
                }
                
            }
            if (Input.GetKeyDown(KeyCode.L) && timerOn == false)
            {
                ScriptHolderP1.gameObject.GetComponent<SC_FPSController>()._canMove = false;
                timerOn = true;
                if (timer > 2)
                {
                    ScriptHolderP1.gameObject.GetComponent<SC_FPSController>()._canMove = true;
                    Action2 = false;
                    timerOn = false;
                    timer = 0;
                }
            }
        }
        
    }
   
    public void Clean() 
    {
        _0D.SetActive(false);
        _0J.SetActive(false);
        _1F.SetActive(false);
        _1K.SetActive(false);
        _2L.SetActive(false);
        _2G.SetActive(false);
    }

    public void PhasePrep() 
    {
        action = Random.Range(0, 3);
        if(action == 0) 
        {
            //killme.SetActive(true);
            _0D.SetActive(true);
            _0J.SetActive(true);
            Action0 = true;
        }
        if (action == 1)
        {
            //killHim.SetActive(true);
            _1F.SetActive(true);
            _1K.SetActive(true);

            Action1 = true;
        }
        if (action == 2 && action == 3)
        {
            //freeze.SetActive(true);
            _2L.SetActive(true);
            _2G.SetActive(true);

            Action2 = true;
        }
    }
}
