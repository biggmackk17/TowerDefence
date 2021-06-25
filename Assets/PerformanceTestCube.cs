using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class PerformanceTestCube : MonoBehaviour
{
    MeshRenderer meshRenderer;


    private void Start()
    {
        meshRenderer = transform.GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        PerformanceTest.OnTest += Test2;
        
        
    }

    public void Test()
    {
        meshRenderer.material.color = Color.red;
    }

    public void Test2(GameObject cube)
    {
        Profiler.BeginSample("MyTestAction");
        if (cube == gameObject)
        {
            meshRenderer.material.color = Color.red;
        }
        Profiler.EndSample();
    }


    

    private void OnDisable()
    {
        
    }
}
