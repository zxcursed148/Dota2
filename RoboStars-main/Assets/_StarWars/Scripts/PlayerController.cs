using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField] private float rotateSpeed;
    private PlayerInput inputActions;
    private CharacterController controller;
    private Animator animator;
    private Vector2 movmentInput;
    private Vector3 currentMovment;
    private Quaternion rotateDir;
    private bool isRun;
    private bool isWalk;
    [SerializeField] private cameraFollow myCamScript;

    private PhotonView pv;

    private void OnMovementAction(InputAction.CallbackContext context)
    {
        movmentInput = context.ReadValue<Vector2>();

        currentMovment.x = movmentInput.x;
        currentMovment.z = movmentInput.y;

        isWalk = movmentInput.x !=0 || movmentInput.y !=0;
    }
    private void Awake()
    {
        pv = GetComponent<PhotonView>();

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        inputActions = new PlayerInput();

        inputActions.CharacterControls.Movement.started += OnMovementAction;
        inputActions.CharacterControls.Movement.performed += OnMovementAction;
        inputActions.CharacterControls.Movement.canceled += OnMovementAction;

        inputActions.CharacterControls.Movement.started += OnCameraMovement;
        inputActions.CharacterControls.Movement.performed += OnCameraMovement;
        inputActions.CharacterControls.Movement.canceled += OnCameraMovement;
    
        //if(!pv.IsMine)
        //{
         //   Destroy(myCamScript.gameObject);
        //}//
    }
    public void Respawn(){
        controller.enabled = false;
        transform.position = Vector3.up;
        controller.enabled = true;
    }
    private void OnEnable()
    {
        inputActions.CharacterControls.Enable();
    }
    private void OnDisable()
    {
        inputActions.CharacterControls.Disable();
    }
    private void PlayerRotate()
    {
        if (isWalk)
        {
            rotateDir = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(currentMovment), Time.deltaTime * rotateSpeed);
            transform.rotation = rotateDir;
        }
    }
    private void AnimateControl()
    {
        animator.SetBool("isWalk",isWalk);
        animator.SetBool("isRun", isRun);
    }
    private void Update()
    {
        //if (!pv.IsMine) return;
        AnimateControl();
        PlayerRotate();
        inputActions.CharacterControls.Run.started += OnRun;
        inputActions.CharacterControls.Run.canceled += OnRun;
    }
    private void FixedUpdate()
    {
        //if (!pv.IsMine) return;
        controller.Move(currentMovment * Time.fixedDeltaTime);
    }
    private void OnRun(InputAction.CallbackContext context)
    {
        isRun = context.ReadValueAsButton(); 
    }
    private void OnCameraMovement(InputAction.CallbackContext context) 
    {
        myCamScript.SetOffset(currentMovment);
    }
}
