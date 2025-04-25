using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletInfo info;
    private Rigidbody rb;
    private PhotonView pv;

    private void Awake() // Исправлено Aweke -> Awake
    {
        rb = GetComponent<Rigidbody>();
        pv = GetComponent<PhotonView>();
        info.render = gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!pv.IsMine) return;

        if (other.gameObject.CompareTag("Player"))
        {
            PlayerSetting player = other.gameObject.GetComponentInParent<PlayerSetting>();
            if (player != null)
            {
                player.TakeDamage(info.damage); // Убедитесь, что метод TakeDamage в PlayerSetting public
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    public void StartMove(Vector3 dir)
    {
        rb.velocity = dir * info.speed;
    }

    void Start()
    {
    }

    void Update()
    {
    }
}
