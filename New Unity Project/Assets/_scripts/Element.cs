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
      if(Vector3.Distance(targetpos.position , transform.position) > 0.5f)
      {

      transform.position = Vector3.MoveTowards(transform.position, targetpos.position, movespeed  * 2 * Time.deltaTime);
      GetComponent<Rigidbody>().drag = 1.0f;
      }
        else
        {

          GetComponent<Rigidbody>().AddForce((targetpos.position - transform.position) * movespeed  * 5 * Time.deltaTime,ForceMode.Impulse);
          GetComponent<Rigidbody>().drag = 0;
        }


    }
    public void Grab(Transform newtarget)
    {
      currentlyHeld = true;
      targetpos = newtarget;
      if(GetComponent<Collider>() != null)
      {GetComponent<Collider>().enabled = false;}
      myGravity = GetComponent<Rigidbody>().useGravity;
      GetComponent<Rigidbody>().useGravity = false;
    }
    public void LetGo()
    {
      currentlyHeld = false;
      targetpos = null;
      if(GetComponent<Collider>() != null)
      {GetComponent<Collider>().enabled = true;}
      GetComponent<Rigidbody>().useGravity = myGravity;
    }
    public void OnTriggerStay(Collider col)
    {
            if( elementType == 0 && currentStrength != 0 && col.GetComponent<Rigidbody>() != null && col.GetComponent<Element>() == null )
            {
              if(primary == true)
              {col.GetComponent<Rigidbody>().AddForce(Vector3.up * currentStrength *  Time.deltaTime,ForceMode.Impulse );}



            }
    }
    public void OnTriggerEnter(Collider col)
    {
      if( elementType == 0 && currentStrength != 0 && col.GetComponent<Rigidbody>() != null )
      {
        if(primary == true)
        {col.GetComponent<Rigidbody>().velocity = (col.transform.position - transform.position).normalized * currentStrength * 10;}
          else
          {col.GetComponent<Rigidbody>().AddForce((col.transform.position - transform.position).normalized * currentStrength * 10 * Time.deltaTime,ForceMode.Impulse );}


      }
            if(currentStrength > 0 && elementType == 2 )//fire
            {
              if( col.GetComponent<Element>() == null && col.transform.Find("OnFireObj") == null)
              {
                  // if(col.GetComponent<Renderer>() != null )
                  // {  col.GetComponent<Renderer>().material = this.GetComponent<Renderer>().material;}

                      GameObject clone = Instantiate(this.gameObject,col.transform.position  ,Quaternion.identity ) as GameObject;
                      // clone.transform.localScale = clone.transform.localScale * 4;
                      clone.transform.name = "OnFireObj";
                      clone.GetComponent<DieInTime>().lifetime = 10.0f;
                      clone.GetComponent<Element>().currentStrength = -1;
                      clone.transform.parent = col.transform;

                    currentStrength--;
                    if(currentStrength < 0){ Destroy(this.gameObject);}
              }
              else
              {
                if( col.GetComponent<Element>().elementType == 3){Destroy(this.gameObject);}
              }
            }

    }
}
