using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour
{
    [Header("Imports")]
    [SerializeField]
    private BP_GameManager _gameManager;
    [SerializeField]
    private Animation _anim;
    [SerializeField]
    private Camera _playerCamera;


    [Header("Values")]

    public bool _canMove = true;
    [SerializeField]
    private int _playerId;
    [SerializeField]
    private float _walkingSpeed = 7.5f;
    [SerializeField]
    private float _runningSpeed = 11.5f;
    [SerializeField]
    private float _jumpSpeed = 8.0f;
    [SerializeField]
    private float _gravity = 20.0f;
    [SerializeField]
    private float _lookSpeed = 2.0f;
    [SerializeField]
    private float _lookXLimit = 45.0f;
    [SerializeField,Range(0.05f,3)]
    private float _timeBeforeShot;

    private bool _fireReady = true;

    

    private CharacterController _characterController;
    private Vector3 _moveDirection = Vector3.zero;
    private float _rotationX = 0;
    
    void Start()
    {
        //anim = gameObject.GetComponent<Animation>();

        //Vector3 direction = Vector3.forward;

        _characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Fire();
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = _canMove ? (isRunning ? _runningSpeed : _walkingSpeed) * Input.GetAxis("Vertical" + (_playerId==0?"":2 )) : 0;
        float curSpeedY = _canMove ? (isRunning ? _runningSpeed : _walkingSpeed) * Input.GetAxis("Horizontal" + (_playerId == 0 ? "" : 2)) : 0;
        float movementDirectionY = _moveDirection.y;
        _moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump" + (_playerId == 0 ? "" : 2)) && _canMove && _characterController.isGrounded)
        {
            _moveDirection.y = _jumpSpeed;
        }
        else
        {
            _moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!_characterController.isGrounded)
        {
            _moveDirection.y -= _gravity * Time.deltaTime;
        }

        // Move the controller
        _characterController.Move(_moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (_canMove)
        {
            _rotationX += -Input.GetAxis("Mouse Y" + (_playerId == 0 ? "" : 2)) * _lookSpeed;
            _rotationX = Mathf.Clamp(_rotationX, -_lookXLimit, _lookXLimit);
            _playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X" + (_playerId == 0 ? "" : 2)) * _lookSpeed, 0);
        }
    }

    private void Fire()
    {
        if (!_fireReady)
            return;
        if (Input.GetAxis("Fire1" + (_playerId == 0 ? "" : 2)) > 0)
        {
            //Debug.Log("SUUUUUUUUUU");
            Vector3 characterPosition = _playerCamera.transform.position;
            Quaternion characterRotation = _playerCamera.transform.rotation;

            // Crée un rayon depuis le centre du personnage orienté vers l'avant
            Ray ray = new Ray(characterPosition, characterRotation * Vector3.forward);

            RaycastHit hit;


            if (Physics.Raycast(ray, out hit)) // Effectue le raycast et vérifie s'il y a une collision
            {
                Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green, 1f);
                if (hit.collider.CompareTag("P1")) // Vérifie si l'objet touché a le tag "Player"
                {
                    _gameManager.P1Die = true;
                    Scores.Instance.IncreaseScore(1);
                    Debug.Log("Objet touché : " + hit.collider.gameObject.name); // Affiche le nom de l'objet touché dans la console
                    Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);
                }

                if (hit.collider.CompareTag("P2")) // Vérifie si l'objet touché a le tag "Player"
                {
                    _gameManager.P2Die = true;
                    Scores.Instance.IncreaseScore(0);
                    Debug.Log("Objet touché : " + hit.collider.gameObject.name); // Affiche le nom de l'objet touché dans la console
                    Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);
                }


            }
            StartCoroutine(ShotCooldown());
        }
    }

    private IEnumerator ShotCooldown()
    {
        _fireReady = false;
        yield return new WaitForSeconds(_timeBeforeShot);
        _fireReady = true;
    }
}