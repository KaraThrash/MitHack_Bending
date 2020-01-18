using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieInTime : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifetime;
     // Start is called before the first frame update
     void Start()
     {

     }

     // Update is called once per frame
     void Update()
     {
         if (lifetime != -1)
         {
             lifetime -= Time.deltaTime;
             if (lifetime <= 0) { Destroy(this.gameObject); }
         }
     }
}
