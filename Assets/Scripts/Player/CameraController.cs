using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; //vị trí nhân vật
    private Vector3 offset;  //Khoảng cách từ cam đến nhân vật

    void Start()
    {
        //Khoảng cách cam->nv 
        offset = transform.position - target.position;
       // Debug.Log(offset);
    }

    void LateUpdate()
    {
        //vị trí mới của camera
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z+target.position.z);
        transform.position = Vector3.Lerp(transform.position,newPosition,1f);
    }
}
