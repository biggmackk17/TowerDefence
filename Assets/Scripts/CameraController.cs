using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    Vector3 move;
    [SerializeField]
    float xmin, xmax, zmin, zmax, ypos;
    Camera _cam;
    // Start is called before the first frame update
    void Start()
    {
        _cam = transform.GetComponent<Camera>();

    }

  

    // Update is called once per frame
    void Update()
    {

        var scroll = Input.mouseScrollDelta;
        _cam.fieldOfView -= scroll.y*5;
        _cam.fieldOfView = Mathf.Clamp(_cam.fieldOfView,10, 50);

    
        
        
        move.x = Input.GetAxis("Vertical");
        move.z = -Input.GetAxis("Horizontal");
        move.y = 0;

        transform.Translate(move, Space.World);
        var pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, xmin, xmax);
        pos.z = Mathf.Clamp(pos.z, zmin, zmax);
        pos.y = 20;

        transform.position = pos;
    }
}
