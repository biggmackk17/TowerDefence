using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tryget : MonoBehaviour
{
    [SerializeField]

    int i;
    // Start is called before the first frame update
    void Start()
    {
       

        foreach (Transform child in transform)
        {
            i++;
            var childCheck = child;
          /*  if (childCheck.TryGetComponent<Renderer>(out var rend))
            {
                rend.enabled = false;
           } */

           
            while (childCheck.childCount > 0)
            {
                foreach (Transform grandchild in childCheck)
                {
                    i++;
                   /* if (grandchild.TryGetComponent<Renderer>(out var childrend))
                    {
                        childrend.enabled = false;
                    }
                    */
                    childCheck = grandchild;
                }
            }
        }
            Debug.Log(i);
        



    }
}



          

     /*   var rends=  GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in rends)
        {
            rend.enabled = false;
        }
        

    }
    */

    // Update is called once per frame


