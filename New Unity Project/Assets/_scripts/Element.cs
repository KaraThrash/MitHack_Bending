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
  public int elementType,currentStrength;
  public string elementName;
  public bool myKinematic,myGravity,primary;

    // Start is called before the first frame update
    void Start()
    {
      // handtracker = GameObject.Find("HandController").GetComponent<Handtracking>();
    elementManager  = GameObject.Find(elementName + "Manager").GetComponent<ElementManager>();
    elementManager.AddElementToList(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
      if(currentlyHeld == true)
      {
        if( targetpos != null){    Move();}
        else{Destroy(this.gameObject);}


      }
      else{
        GetComponent<Rigidbody>().useGravity = myGravity;
        currentlyHeld = false;
      }

    }
    public void Move()
    {
      GetComponent<Rigidbody>().AddForce((targetpos.position - transform.position).normalized * movespeed *  Vector3.Distance(targetpos.position , transform.position) * Time.deltaTime);

    }
    public void Grab(Transform newtarget)
    {
      currentlyHeld = true;
      targetpos = newtarget;
      myGravity = GetComponent<Rigidbody>().useGravity;
      GetComponent<Rigidbody>().useGravity = false;
    }
    public void OnTriggerStay(Collider col)
    {
            if(currentStrength != 0 && col.GetComponent<Rigidbody>() != null )
            {
              if(primary == true)
              {col.GetComponent<Rigidbody>().velocity = (col.transform.position - transform.position).normalized * currentStrength ;}
                else
                {col.GetComponent<Rigidbody>().AddForce((col.transform.position - transform.position).normalized * currentStrength * 10 * Time.deltaTime,ForceMode.Impulse );}


            }
    }
    public void OnTriggerEnter(Collider col)
    {
            if(currentStrength != 0 && elementType == 2 )//fire
            {
              if( col.GetComponent<Element>() == null)
              {
                GameObject clone = Instantiate(this.gameObject,col.transform.position,col.transform.rotation ) as GameObject;
                clone.GetComponent<DieInTime>().lifetime = 10.0f;
                clone.GetComponent<Element>().currentStrength = 0;
              }
            }
    }
}
