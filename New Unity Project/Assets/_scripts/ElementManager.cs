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
      int count = elements.Count - 1;
          while(count >= 0)
          {
                if(elements[count] == null){elements.RemoveAt(count); }
                else{
              if(elements[count].GetComponent<Element>().currentlyHeld == false && Vector3.Distance(elements[count].transform.position,playerPos.position) < distance)
              {closestElement = elements[count];}
              }

          }

    closestElement.GetComponent<Element>().Grab(playerPos);
    }

    public void GetAllInRangeAndPull(Transform playerPos,int range,float pullforce)
    {
      int count = elements.Count - 1;
          while(count >= 0)
          {
                if(elements[count] == null){elements.RemoveAt(count); }
                else{
                  if( elements[count].GetComponent<Rigidbody>() != null && Vector3.Distance(elements[count].transform.position,playerPos.position) < range &&  Vector3.Distance(elements[count].transform.position,playerPos.position) > 0.1f)
                  {

                    if(elementType == 1)//earth
                    {elements[count].GetComponent<Rigidbody>().AddForce((playerPos.position - elements[count].transform.position) * Mathf.Clamp(pullforce * 0.1f,1,maxPullForce),ForceMode.Impulse);}
                    if(elementType == 2)//water
                    {Destroy(elements[count]); elements.RemoveAt(count);}
                    if(elementType == 3)//earth
                    {elements[count].GetComponent<Rigidbody>().AddForce((playerPos.position - elements[count].transform.position) * Mathf.Clamp(pullforce * 0.1f,1,maxPullForce),ForceMode.Impulse);}
                  }
              }
              count--;
          }

    }
    public void GetAllInRangeAndPull(Transform playerPos,int range,float pullforce,Vector3 dir)
    {

      int count = elements.Count - 1;
          while(count >= 0)
          {
                if(elements[count] == null){elements.RemoveAt(count); }
                else{
                  if( elements[count].GetComponent<Rigidbody>() != null && Vector3.Distance(elements[count].transform.position,playerPos.position) < range &&  Vector3.Distance(elements[count].transform.position,playerPos.position) > 0.1f)
                  {

                    if(elementType == 1)//earth
                    {elements[count].GetComponent<Rigidbody>().AddForce(dir * Mathf.Clamp(pullforce * 0.1f,1,maxPullForce),ForceMode.Impulse);}
                    if(elementType == 2)//water
                    {Destroy(elements[count]); elements.RemoveAt(count); }
                    if(elementType == 3)//earth
                    {elements[count].GetComponent<Rigidbody>().AddForce(dir * Mathf.Clamp(pullforce * 0.1f,1,maxPullForce),ForceMode.Impulse);}
                  }
              }
              count--;
          }


    }
    public void AddElementToList(GameObject newelement)
    {elements.Add(newelement);}
}
