using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bending : MonoBehaviour
{
  public int elementBendState,lastaction;
  public GameObject windobj,rock,earthshield;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Push(Transform hand,Element element)
    {

      switch (element.elementType)
      {
        case 0://air
          if(lastaction == 0) //directional gust
          {
            GameObject clone = Instantiate(windobj,hand.position,hand.rotation) as GameObject;
            clone.GetComponent<DieInTime>().lifetime = element.currentStrength;
            clone.transform.localScale = new Vector3(clone.transform.localScale.x,clone.transform.localScale.y,clone.transform.localScale.z * element.currentStrength);
            clone.GetComponent<Element>().currentStrength = element.currentStrength;
            lastaction = -1;
          }
          else //small directional wind
          {

            GameObject clone3 = Instantiate(windobj,hand.position,hand.rotation) as GameObject;
            // clone.transform.LookAt(hand.position);
            clone3.GetComponent<DieInTime>().lifetime = 1.0f;
              clone3.GetComponent<Element>().currentStrength = element.currentStrength;
            lastaction = 0;
              element.currentStrength++;
          }
        break;
        case 1://earth

            GameObject clone2 = Instantiate(rock,hand.position,hand.rotation) as GameObject;
            clone2.GetComponent<DieInTime>().lifetime = element.currentStrength;
            clone2.GetComponent<Rigidbody>().AddForce(clone2.transform.forward * element.currentStrength);
            lastaction = 1;
              element.currentStrength++;

        break;
        default:
        break;
      }
    }

    public void Pull(Transform hand,Element element)
    {

      switch (element.elementType)
      {
        case 0://air
          if(lastaction == 0) //directional gust
          {
            GameObject clone = Instantiate(windobj,hand.position,hand.rotation) as GameObject;
            clone.GetComponent<DieInTime>().lifetime = element.currentStrength;
            clone.transform.localScale = new Vector3(clone.transform.localScale.x,clone.transform.localScale.y,clone.transform.localScale.z * element.currentStrength);
            clone.GetComponent<Element>().currentStrength = -element.currentStrength;
            lastaction = -1;
          }
          else //small directional wind
          {

            GameObject clone = Instantiate(windobj,hand.position,hand.rotation) as GameObject;
            // clone.transform.LookAt(hand.position);
            clone.GetComponent<DieInTime>().lifetime = 1.0f;
              clone.GetComponent<Element>().currentStrength = -element.currentStrength;
            lastaction = 0;
              element.currentStrength++;
          }
        break;
        case 1://earth

            GameObject clone2 = Instantiate(earthshield,hand.position,hand.rotation) as GameObject;
            clone2.GetComponent<DieInTime>().lifetime = element.currentStrength;
            lastaction = 1;
              element.currentStrength++;

        break;
        default:
        break;
      }
    }
    //back and left right/up down generates strength

}
