using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using System.IO;

public class Aim : MonoBehaviour
{
   [SerializeField] private Transform spawnPosition;
    [SerializeField] private List<GameObject> allTarget = new List<GameObject>();
    [SerializeField] private GameObject targetCylinder;
    [SerializeField] private float range = 10f;

    private PlayerInput inputs;
    private PhotonView pv;
    private CharacterController controller;
    private GameObject targetObj;
    private bool canSearch = true;
    private int targetCount;

    private void Awake()
    {
        inputs = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();
        pv = GetComponentInParent<PhotonView>();
    }

    private void Start()
    {
        if (!pv.IsMine) return;
        targetCylinder.SetActive(false);
        inputs.CharacterControls.ChangeTarget.started += SelectNewTarget;
    }

    private void FixedUpdate()
    {
        if (!pv.IsMine) return;
        SelectTarget();
    }

    private void OnEnable()
    {
        inputs.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        inputs.CharacterControls.Disable();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void SetTargetStatus(bool isTarget)
    {
        targetCylinder.SetActive(isTarget);
    }

    private void SelectTarget()
    {
        if (controller.velocity == Vector3.zero)
        {
            if (canSearch)
            {
                InvokeRepeating(nameof(Calculate), 0f, 0.5f);
            }
        }
        else
        {
            if (targetObj != null)
            {
                targetObj.GetComponent<Aim>()?.SetTargetStatus(false);
                targetObj = null;
            }
            canSearch = true;
            CancelInvoke(nameof(Calculate));
        }
    }

    private void Calculate()
    {
        canSearch = false;
        allTarget.Clear();

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, range, Vector3.up, range);
        foreach (RaycastHit hit in hits)
        {
            GameObject tempObj = hit.collider.gameObject;
            if (tempObj.TryGetComponent(out CharacterController cc) && tempObj.GetComponentInParent<PhotonView>()?.IsMine == false)
            {
                allTarget.Add(tempObj);
            }
        }
        SelectNewTarget();
    }

    private void SelectNewTarget(InputAction.CallbackContext context)
    {
        SelectNewTarget();
    }

    private void SelectNewTarget()
    {
        if (allTarget.Count == 0) return;

        targetCount++;
        foreach (GameObject obj in allTarget)
        {
            obj.GetComponent<Aim>()?.SetTargetStatus(false);
        }
        if (targetCount >= allTarget.Count)
        {
            targetCount = 0;
        }
        targetObj = allTarget[targetCount];
        targetObj.GetComponent<Aim>()?.SetTargetStatus(true);
    }
}
