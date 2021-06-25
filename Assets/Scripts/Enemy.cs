using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    int _worth;
    [SerializeField]
    float _speed;
    [SerializeField]
    int _id;
    [SerializeField]
    float _currentHealth;
    [SerializeField]
    float _TotalHealth;
    Animator _anim;
    NavMeshAgent _agent;
    Transform _destination;
    Collider _collider;

    public static Action EnemyDisable;
    public static Action<GameObject> EnemyDeath;
    public static Action<int> AddWarfund;

    //public delegate void EnemyDisable();
    //public static event EnemyDisable OnEnemyDisable;

    public int GetID()
    {
        return _id;
    }
    private void Awake()
    {
        _agent = transform.GetComponent<NavMeshAgent>();
        _destination = FindObjectOfType<EndPoint>().transform;//this is gross not sure why I left that in there.
        _collider = transform.GetComponent<Collider>();

        
        
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        _collider.enabled = true;
        _agent.SetDestination(_destination.position);
        _agent.speed = _speed;
        _currentHealth = _TotalHealth;
       
        

    }

    private void OnDisable()
    {
        EnemyDisable?.Invoke();
    }
    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }



    private void Die()
    {
        EnemyDeath?.Invoke(gameObject);
        AddWarfund?.Invoke(_worth);
        _collider.enabled = false;
        gameObject.SetActive(false);
       
  
    }
}
