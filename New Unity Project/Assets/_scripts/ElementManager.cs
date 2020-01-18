using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour
{
  public List<GameObject> elements;
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
    public void AddElementToList(GameObject newelement)
    {elements.Add(newelement);}
}
