using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurretBuilder : MonoBehaviour
{
    public static Action OnEnterBuildMode;  
    public static Action OnExitBuildMode;
    public static Action OnValidSpot;
    public static Action OnInvalidSpot;





    public static Action<TurretNode, Turret.TurretType> OnBuildTurret;
    bool _buildMode = false;  //are we in build mode?
    [SerializeField]
    Turret.TurretType _turretType; // what type of turret are we planning on building?
    [SerializeField]
    DecoyTurret[] _decoys; //array of the decoy turrets
    [SerializeField]
    DecoyTurret _activeDecoy;//which decoy are we showing right now? 

    void SelectTurretType(Turret.TurretType type) //set the current turret type. Will probably be set through event
    {
        //_turretType = type; //set our turret type to the param 
    }

    void EnterBuildMode(Turret.TurretType turretType) //enter build mode, starts raycast while loop, sends out message to turn on availible beacons
    {
        _turretType = turretType;
        _buildMode = true; //we are now in build mode
        StartCoroutine("BuildMode");//start the build mode while loop, keeps raycast from constantly being called
        OnEnterBuildMode?.Invoke(); // send out event "we are building"
    }

    void ExitBuildMode() // exit build mode, sends out message to turn off availible beacons
    {
        _buildMode = false; //we are not in build mode. Also cancels while loop
        _activeDecoy.gameObject.SetActive(false);
        _activeDecoy = null;
        OnExitBuildMode?.Invoke(); //send out event "We are no longer building"
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //these are for testing purposes only
        {
           // SelectTurretType(Turret.TurretType.gattling);
            EnterBuildMode(Turret.TurretType.gattling);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitBuildMode();
        }
    }

    IEnumerator BuildMode() //this starts when we enter build mode
    {
        foreach (DecoyTurret decoy in _decoys) // go through all decoys, activate the one the matches our turret type
        {
            if (decoy.ReturnTurretType() == _turretType)
            {
                decoy.gameObject.SetActive(true);
                _activeDecoy = decoy;  //get refrence to this decoy
            }
            else { decoy.gameObject.SetActive(false); } //turn off any decoys that dont match the type
        }

        while (_buildMode) //while we are in buildmode...
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition); //ray point from camera
            RaycastHit hit; //holds ray hit data
            if (Physics.Raycast(raycast, out hit)) //did the ray hit anything?
            {
                _activeDecoy.transform.position = hit.point; //place the decoy at that hit point
                if (hit.collider.TryGetComponent<TurretNode>(out var node)) //is the hitpoint a turret node?
                {
                    if (!node.ReturnOccupied()) //if the node is not occupied
                    {
                        OnValidSpot?.Invoke(); //send out the valid node event
                        if (Input.GetMouseButtonDown(0)) //if you press the left mouse 
                        {
                            OnBuildTurret?.Invoke(node, _turretType); //send out an event to build a tower, pass in who should be listening and the turret type. 
                            ExitBuildMode();//exit build mode
                        }
                    }
                }
                    else OnInvalidSpot?.Invoke(); //spot is invalid

                if (Input.GetMouseButtonDown(1)) // if we right click get out of build mode
                {
                    ExitBuildMode();//leave build mode
                }
                
            }
            

                
            yield return null;
        } // if we get past here, we are no longer in build mode. Reset everything, disable decoy
        yield return null;
    }

}
