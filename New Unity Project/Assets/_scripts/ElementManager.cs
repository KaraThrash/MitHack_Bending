using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour
{
  public List<GameObject> elements;
  public int elementType;
  public int maxPullForce;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GetClosestElement(Transform playerPos)
    {
      int distance = 99;
      GameObject closestElement = elements[0];
      foreach(GameObject go in elements)
      {
        if(go.GetComponent<Element>().currentlyHeld == false && Vector3.Distance(go.transform.position,playerPos.position) < distance)
        {closestElement = go;}

      }
    closestElement.GetComponent<Element>().Grab(playerPos);
    }

    public void GetAllInRangeAndPull(Transform playerPos,int range,float pullforce)
    {

      foreach(GameObject go in elements)
      {
        if(go != null && go.GetComponent<Rigidbody>() != null && Vector3.Distance(go.transform.position,playerPos.position) < range)
        {go.GetComponent<Rigidbody>().AddForce((playerPos.position - go.transform.position) * Mathf.Clamp(pullforce * 0.1f,1,maxPullForce),ForceMode.Impulse);}

      }

    }
    public void GetAllInRangeAndPull(Transform playerPos,int range,float pullforce,Vector3 dir)
    {

      foreach(GameObject go in elements)
      {
        if(go != null && go.GetComponent<Rigidbody>() != null && Vector3.Distance(go.transform.position,playerPos.position) < range)
        {
          if(elementType == 1)//earth
          {go.GetComponent<Rigidbody>().AddForce(dir * Mathf.Clamp(pullforce * 0.1f,1,maxPullForce),ForceMode.Impulse);}
          if(elementType == 3)//fire
          {Destroy(go);}

        }

      }

    }
    public void AddElementToList(GameObject newelement)
    {elements.Add(newelement);}
}
