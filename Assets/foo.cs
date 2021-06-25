using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class foo : MonoBehaviour
{
    public Func<int, int> OnFoo;
    // Start is called before the first frame update

    private void OnEnable()
    {
        OnFoo += subscriber;
    }



    private int subscriber(int number)
    {
        return number;
    }

    private void Start()
    {
        Test(4);
    }

    private int Test(int num)
    {

        return OnFoo.Invoke(num);
     // return (int)OnFoo?.Invoke(num);
        //return OnFoo?.Invoke(num);

    }
}
