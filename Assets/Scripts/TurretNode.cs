using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretNode : MonoBehaviour
{
    [SerializeField]
    Turret.TurretType _turretType;
    [SerializeField]
    bool _occupied;
    [SerializeField]
    GameObject _availibleMarker;
    [SerializeField]
    Turret[] _turrets;

    private void OnEnable()
    {
        TurretBuilder.OnEnterBuildMode += CheckAvailiblity;
        TurretBuilder.OnExitBuildMode += DisableMarkers;
        TurretBuilder.OnBuildTurret += BuildTurret;
    }

    private void CheckAvailiblity()
    {
        if (!_occupied)
        {
            _availibleMarker.SetActive(true);
        }
    }

    private void DisableMarkers()
    {
        _availibleMarker.SetActive(false);
    }

    public bool ReturnOccupied()
    {
        return _occupied;
    }

    private void BuildTurret(TurretNode node, Turret.TurretType turret)
    {
        if (node == this)
        {
            _turretType = turret;
            switch (turret)
            {

                case Turret.TurretType.gattling:
                    foreach (Turret myturret in _turrets)
                    {
                        if (myturret.ReturnTurretType() == Turret.TurretType.gattling)
                        {
                            myturret.gameObject.SetActive(true);
                        }
                        else myturret.gameObject.SetActive(false);
                    }
                    break;

                case Turret.TurretType.rocket:
                    foreach (Turret myturret in _turrets)
                    {
                        if (myturret.ReturnTurretType() == Turret.TurretType.rocket)
                        {
                            myturret.gameObject.SetActive(true);
                        }

                        else myturret.gameObject.SetActive(false);
                    }
                    break;
            }
            _occupied = true;
        }
    }

    private void OnMouseDown()
    {
        
    }


}
