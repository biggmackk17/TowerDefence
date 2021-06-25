using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotateAround : MonoBehaviour
{
    [SerializeField]
    GameObject _target;
    [SerializeField]
    float _speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(_target.transform.position, Vector3.up,_speed);
    }
}
