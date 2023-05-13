using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour
{
    public GameObject ScriptHolder;

    public GameObject Cam;

    public int _playerId;

    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    public Animation anim;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public float range = 5;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        anim = gameObject.GetComponent<Animation>();

        //Vector3 direction = Vector3.forward;

        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {


        if (Input.GetButton("Fire1" + (_playerId == 0 ? "" : 2)))
        {
            Vector3 characterPosition = Cam.transform.position;
            Quaternion characterRotation = Cam.transform.rotation;

            // Crée un rayon depuis le centre du personnage orienté vers l'avant
            Ray ray = new Ray(characterPosition, characterRotation * Vector3.forward);

            RaycastHit hit;


            if (Physics.Raycast(ray, out hit)) // Effectue le raycast et vérifie s'il y a une collision
            {
                Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green, 1f);
                if (hit.collider.CompareTag("P1")) // Vérifie si l'objet touché a le tag "Player"
                {
                    ScriptHolder.gameObject.GetComponent<BP_GameManager>().P1Die = true;
                    //Debug.Log("Objet touché : " + hit.collider.gameObject.name); // Affiche le nom de l'objet touché dans la console
                    Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);
                }

                if (hit.collider.CompareTag("P2")) // Vérifie si l'objet touché a le tag "Player"
                {
                    ScriptHolder.gameObject.GetComponent<BP_GameManager>().P2Die = true;
                    //Debug.Log("Objet touché : " + hit.collider.gameObject.name); // Affiche le nom de l'objet touché dans la console
                    Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);
                }

               
            }


        }








        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical" + (_playerId==0?"":2 )) : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal" + (_playerId == 0 ? "" : 2)) : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump" + (_playerId == 0 ? "" : 2)) && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y" + (_playerId == 0 ? "" : 2)) * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X" + (_playerId == 0 ? "" : 2)) * lookSpeed, 0);
        }
    }
}