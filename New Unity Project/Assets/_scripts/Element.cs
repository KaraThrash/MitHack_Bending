using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
  public bool currentlyHeld;
  public Transform targetpos;
  public float movespeed;
  public Handtracking handtracker;
  public ElementManager elementManager;
  public string elementtype;
  public bool myKinematic,myGravity;
    // Start is called before the first frame update
    void Start()
    {
      // handtracker = GameObject.Find("HandController").GetComponent<Handtracking>();
    elementManager  = GameObject.Find(elementtype + "Manager").GetComponent<ElementManager>();
    elementManager.AddElementToList(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
      if(currentlyHeld == true && targetpos != null)
      {Move();}
      else{
        GetComponent<Rigidbody>().useGravity = myGravity;
        currentlyHeld = false;
      }

    }
    public void Move()
    {
      GetComponent<Rigidbody>().AddForce((targetpos.position - transform.position) * movespeed * Time.deltaTime,ForceMode.Impulse);

    }
    public void Grab(Transform newtarget)
    {
      currentlyHeld = true;
      targetpos = newtarget;
      myGravity = GetComponent<Rigidbody>().useGravity;
      GetComponent<Rigidbody>().useGravity = false;
    }
}
