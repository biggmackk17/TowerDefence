using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Turret : MonoBehaviour
{ 
    [SerializeField]
    protected float _damage;
    [SerializeField]
    protected GameObject _target;
    [SerializeField]
    protected int _cost;
    protected List<GameObject> _targetList = new List<GameObject>();
    protected bool _firing;
    [SerializeField]
    protected float _fireDelay = .5f;
    protected WaitForSeconds _fireDelayWFS;
    public enum TurretType { gattling, doubleGattling, rocket, doubleRocket }
    TurretType _turretType;
    enum TurretState
    {
        idle,
        attacking,
        cooldown,
        destroy
    }

    TurretState turretState;





    public TurretType ReturnTurretType()
    {
        return _turretType;
    }

    protected virtual void Awake()
    {
        _fireDelayWFS = new WaitForSeconds(_fireDelay);
    }

    protected virtual void OnEnable()
    {
        Enemy.EnemyDeath += RemoveEnemyOnDeath;
        _firing = false; 
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if( other.TryGetComponent(out Enemy enemy))
        {

            _targetList.Add(other.gameObject);
            if (_targetList.Count <= 1)
            {
                SetTarget();
                if (!_firing)
                {
                    _firing = true;
                    StartCoroutine("Tracking");
                    StartCoroutine("Firing");
                }
            }
        }
    }

    protected virtual void SetTarget()
    {
        if (_targetList.Count > 0)
        {
            _target = _targetList[0];
        }
        else
        {
            _target = null;
            _firing = false;
        }

    }


    protected virtual void OnTriggerExit(Collider other)
    {

        if (other.TryGetComponent(out Enemy enemy))
        {
            _targetList.Remove(other.gameObject);
            if (_targetList.Count <= 0)
            {
                _firing = false;
            }
            SetTarget();
        }
    }


    protected virtual IEnumerator Firing()
    {
        while (_firing)
        {
            Fire();
           yield return _fireDelayWFS;
        }
    }

    protected virtual IEnumerator Tracking()
    {
        while (_firing)
        {
            transform.LookAt(_target.transform, Vector3.up);
            yield return null;
        }
    }

    protected virtual void Fire()
    {
       
    }

    protected virtual void RemoveEnemyOnDeath(GameObject enemy)
    {
        if(_targetList.Contains(enemy))
        {
           _targetList.Remove(enemy);
            SetTarget();
        }

    
    }

    private void SaveData(TurretData data)
    {

        SerializableVector3 pos = new SerializableVector3();
        pos.x = transform.position.x;
       // data = new TurretData(transform.position, transform.rotation, _turretType);
    }
}
