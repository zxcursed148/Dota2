using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;
public class FireBall : MonoBehaviour
{
    [SerializeField] private BulletInfo info;
    private Rigidbody rb;
    private PhotonView pv;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private GameObject targetObj;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pv = GetComponent<PhotonView>();
        info.render = gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!pv.IsMine) return;

        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInChildren<PlayerSetting>().TakeDamage(info.damage);
            PhotonNetwork.Destroy(gameObject);
        }
    }
    public void StartMove(Vector3 dir)
    {
        rb.velocity = dir * info.speed;
    }

    private void OnFire(InputAction.CallbackContext context)
    {
        if (spawnPosition == null)
        {
            Debug.LogError("Spawn position is not assigned!");
            return;
        }

        Vector3 dir;
        if (targetObj != null)
        {
            dir = (targetObj.transform.position - transform.position).normalized;
        }
        else
        {
            dir = transform.forward; // ‗ךשמ םולא÷ צ³כ³, סענ³כÿ÷למ גןונוה
        }

        GameObject temp = PhotonNetwork.Instantiate("FireBall", spawnPosition.position, Quaternion.identity);

        temp.GetComponent<Bullet>().StartMove(dir);
        Physics.IgnoreCollision(temp.GetComponent<Collider>(), transform.GetComponent<Collider>());
    }

}
