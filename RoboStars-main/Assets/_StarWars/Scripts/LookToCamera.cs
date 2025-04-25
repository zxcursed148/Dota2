using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToCamera : MonoBehaviour
{
    private Camera myCamera;
    private void Awake(){
        myCamera = Camera.main;
    }
    private void LateUpdata(){
        transform.LookAt(myCamera.transform.position);
        transform.Rotate(Vector3.up * 180);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
