using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameControl : MonoBehaviour
{
    public float distance = 5f;
    public GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camDir = character.GetComponent<Camera>().transform.forward;
        transform.position =  character.transform.position + camDir * distance;
    }
}
