using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using System;


public class PerformanceTest : MonoBehaviour
{
    // Start is called before the first frame update
    public static Action<GameObject> OnTest;
    [SerializeField]
    GameObject cube, cube2;

    private void Start()
    {
        
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Test();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Test();
        }
    }


    void Test()
    {

        Profiler.BeginSample("MyTestGetComponent --- Null Check --- Not Null");
            var mycube = cube.GetComponent<PerformanceTestCube>();
            if (mycube != null)
            {
                mycube.Test();
            }
            Profiler.EndSample();
        List<Enemy> enemylist = new List<Enemy>();


      


        Profiler.BeginSample("MyTestGetComponent --- Null Check --- is Null");
        var mycube2 = cube2.GetComponent<PerformanceTestCube>();
        if (mycube2 != null)
        {
            mycube2.Test();
        }
        Profiler.EndSample();


        Profiler.BeginSample("MyTestActionInvoke");
        OnTest?.Invoke(cube);
        Profiler.EndSample();

       
            Profiler.BeginSample("MyTestTryGetComponent --- Not Null");
            if (cube.TryGetComponent<PerformanceTestCube>(out var _cube))
            {
                _cube.Test();
            }
        Profiler.EndSample();


        Profiler.BeginSample("MyTestTryGetComponent --- Is Null");
        if (cube2.TryGetComponent<PerformanceTestCube>(out var _cube2))
        {
            _cube.Test();
        }
        Profiler.EndSample();


        Profiler.BeginSample("MyTestNoNullCheckGetComponenet -- Not Null");
            {
            cube.GetComponent<PerformanceTestCube>().Test();
        }
        Profiler.EndSample();

        Profiler.BeginSample("MyTestNoNullCheckGetComponenet -- Null");
        {
            cube2.GetComponent<PerformanceTestCube>().Test();
        }
        Profiler.EndSample();

    }
}
