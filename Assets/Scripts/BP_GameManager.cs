using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

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

    //public GameObject GoP1;
    //public GameObject GoP2;

    public float timer = 0;
    public bool timerOn = true;


    public bool Phase1 = true;
    //public GameObject Phase1PlaceHolder;
    public GameObject Phase2;

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
    public GameObject lag;
    public GameObject respawn;


    public bool Action0 = false;
    public bool Action1 = false;
    public bool Action2 = false;


    public int action = 0;

    private static BP_GameManager instance;

    public static BP_GameManager Instance { get { return instance; } }

    public AudioSource zikPhase1;
    public AudioSource zikPhase2;
    public AudioSource endPhase1;
    
    public Slider m_timerSlider;
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
        
        //Phase1PlaceHolder.SetActive(false);
        PhasePrep();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Phase1)
        {
            zikPhase1.volume = 0;
            zikPhase2.volume = 0.5f;
            m_timerSlider.gameObject.SetActive(false);
            Phase2.SetActive(true);
        }
        else
        {
            zikPhase1.volume = 0.5f;
            zikPhase2.volume = 0;
            m_timerSlider.gameObject.SetActive(true);
            Phase2.SetActive(false);
        }

        if (timerOn) 
        {
            timer = timer + 1 * Time.deltaTime;
            m_timerSlider.value = timer;
        }
        
        if (timer > 5) 
        {
            //Phase1PlaceHolder.SetActive(true);
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

            //BarreDeChargement.transform.localScale = BarreDeChargement.transform.localScale += new Vector3(0f,0f, 1f); 

        }

        if(timer > 16) 
        {
            endPhase1.Play(); 
            killme.SetActive(false);
            killHim.SetActive(false);
            jump.SetActive(false);
            freeze.SetActive(false);
            m_timerSlider.gameObject.SetActive(false);
            Phase2.SetActive(true);
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
                P1.GetComponent<CharacterController>().enabled = false;
                P1.transform.position = SpawnP1.transform.position;
                P1.GetComponent<CharacterController>().enabled = true;
                //P2.transform.position = SpawnP2.transform.position;
                timerOn = true;
                if(timer > 1) 
                {
                    ScriptHolderP2.gameObject.GetComponent<SC_FPSController>()._canMove = false;
                    P2.GetComponent<CharacterController>().enabled = false;
                    P2.transform.position = SpawnP2.transform.position;
                    P2.GetComponent<CharacterController>().enabled = true;
                    
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
                    //Phase1PlaceHolder.SetActive(false);
                    Clean();
                    PhasePrep();
                }

              
            }


            if (P2Die == true)
            {
                ScriptHolderP2.gameObject.GetComponent<SC_FPSController>()._canMove = false;
                P2.GetComponent<CharacterController>().enabled = false;
                P2.transform.position = SpawnP2.transform.position;
                P2.GetComponent<CharacterController>().enabled = true;
                timerOn = true;
                if(timer > 1)
                {
                    ScriptHolderP1.gameObject.GetComponent<SC_FPSController>()._canMove = false;
                    P1.GetComponent<CharacterController>().enabled = false;
                    P1.transform.position = SpawnP1.transform.position;
                    P1.GetComponent<CharacterController>().enabled = true;
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
                    //Phase1PlaceHolder.SetActive(false);
                    Clean();
                    PhasePrep();
                }


            }
           
        }

        //0 Instant Kill me
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

        // 1 Instant kill him 
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
