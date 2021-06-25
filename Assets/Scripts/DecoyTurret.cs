using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoyTurret : MonoBehaviour
{
    Renderer[] _rends;
    [SerializeField]
    Turret.TurretType _turretType;

    private void OnEnable()
    {
        TurretBuilder.OnValidSpot += TurnGreen;
        TurretBuilder.OnInvalidSpot += TurnRed;
    }

    private void OnDisable()
    {
        TurretBuilder.OnValidSpot -= TurnGreen;
        TurretBuilder.OnInvalidSpot -= TurnRed;
    }


    public Turret.TurretType ReturnTurretType()
    {
        return _turretType;
    }

    private void Start()
    {
        _rends = transform.GetComponentsInChildren<Renderer>();
    }


    void TurnRed()
    {
        foreach (Renderer rend in _rends)
        {
            rend.material.color = Color.red;
        }
        Debug.Log("TurnedRed");
    }

    void TurnGreen()
    {
        foreach (Renderer rend in _rends)
        {
            rend.material.color = Color.green;
        }
    }
}
