using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndPoint : MonoBehaviour
{

   public static Action RemoveLife;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other + "Hit the endpoint");
        if (other.TryGetComponent( out Enemy enemy))
        {
           enemy.gameObject.SetActive(false);
            Debug.Log(other + "Disables Enemy");
            RemoveLife?.Invoke();
        }
    }
}
