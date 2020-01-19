using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameControl : MonoBehaviour
{
    public GameObject[] activeElements;
    public GameObject activeElement;
    public float distance = 5f;
    public GameObject character;
    public float thrust;
    public int elementType;

    public GameObject flameEffect;

    private bool launched;
    private Rigidbody rb;
    //private GameObject gameObject;

    // Start is called before the first frame update
    void Start()
    {
        activeElements = new GameObject[4];
        launched = false;
        rb = GameObject.Find("HandCube").GetComponent<Rigidbody>();
        //meeple = this.gameObject.transform.GetChild(0);
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.activeSelf == true)
            {
                activeElement = gameObject.transform.GetChild(i).gameObject;
                Debug.Log(activeElement.name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camDir = character.GetComponent<Camera>().transform.forward;
        if (Input.GetMouseButtonDown(1))
        {
            print("space key was pressed");
            rb.AddForce(camDir * thrust);
            launched = true;
        }
        else if (!launched) {
                transform.position = character.transform.position + camDir * distance;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        return;
        Debug.Log("collision");
        if (activeElement.name == "Flame2")
        {
            Object flames = Instantiate(flameEffect, transform.position, Quaternion.identity);
            Destroy(flames, 2);
            if (other.gameObject.tag == "flammable")
                Destroy(other.gameObject, 2);
        }
    }
}
