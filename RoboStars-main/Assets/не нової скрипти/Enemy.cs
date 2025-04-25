using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using ExitGames.Client.Photon;

public class Enemy : MonoBehaviourPunCallbacks
{
    [SerializeField] private float radiusSearch = 10f;
    [SerializeField] private float timeAttack;
    [SerializeFIeld] private int damage;
    private bool isAttack;
    private Transform targetTransform;

    void Start()
    {
        if (photonView.IsMine)
        {
            InvokeRepeating(nameof(SearchTargetRPC), 0f, 1f);
        }
    }

    void Update()
    {
        if (targetTransform == null)
        {
            photonView.RPC("SearchTarget", RpcTarget.All);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (targetTransform != null)
            Gizmos.DrawWireSphere(targetTransform.position, radiusSearch);
        else
            Gizmos.DrawWireSphere(transform.position, radiusSearch);
    }

    [PunRPC]
    private void SearchTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radiusSearch);
        foreach (var hitCollider in hitColliders)
        {
            CharacterController player = hitCollider.GetComponent<CharacterController>();
            if (player != null)
            {
                targetTransform = player.transform;
                break;
            }
        }
    }

    private void SearchTargetRPC()
    {
        photonView.RPC("SearchTarget", RpcTarget.AllBuffered);
    }

    private IEnumerator StartAttack()
}
