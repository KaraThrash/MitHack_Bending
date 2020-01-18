using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlammableObject : MonoBehaviour
{
    public FlameControl flameController;

    // Start is called before the first frame update
    void Start()
    {
        flameController = GameObject.Find("HandCube").GetComponent<FlameControl>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
