using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bending : MonoBehaviour
{
  public int elementBendState,lastaction;
  public GameObject windobj,rock,earthshield,flameThrower;
  public float bendcooldown,cooldowntimer;
  public ElementManager airManager,earthmanager,fireManager,waterManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if(cooldowntimer > 0){cooldowntimer -= Time.deltaTime;}
    }


    public void Push(bool midbending,Transform hand,Element element)
    {
      if(cooldowntimer <= 0){
      cooldowntimer = bendcooldown;
      switch (element.elementType)
      {
        case 0://air
          if(midbending == false) //directional gust
          {
            GameObject clone = Instantiate(windobj,hand.position,hand.rotation) as GameObject;
            clone.GetComponent<DieInTime>().lifetime = element.currentStrength;
            clone.transform.localScale = new Vector3(Mathf.Clamp(clone.transform.localScale.x +(0.2f * element.currentStrength),3,8),Mathf.Clamp(clone.transform.localScale.y +(0.2f * element.currentStrength),3,8),Mathf.Clamp(clone.transform.localScale.z * element.currentStrength,5,20));
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
          if(midbending == true){
            GameObject clone2 = Instantiate(rock,hand.position,hand.rotation) as GameObject;
            clone2.GetComponent<DieInTime>().lifetime = element.currentStrength;
            clone2.GetComponent<Rigidbody>().AddForce(clone2.transform.forward * element.currentStrength,ForceMode.Impulse);
            lastaction = 1;
              element.currentStrength++;
            }
        break;
        case 2://fire
            if(midbending == true)
            {//small flamethrower
                GameObject clonefire = Instantiate(flameThrower,hand.position,hand.rotation) as GameObject;
                clonefire.GetComponent<DieInTime>().lifetime = 1.0f;
                // clonefire.GetComponent<Rigidbody>().AddForce(clonefire.transform.forward * element.currentStrength);
                lastaction = 2;
                  element.currentStrength++;
              }else
              {
                GameObject clonefire = Instantiate(flameThrower,hand.position,hand.rotation) as GameObject;
                clonefire.GetComponent<DieInTime>().lifetime = element.currentStrength;
                clonefire.GetComponent<Rigidbody>().AddForce(clonefire.transform.forward * element.currentStrength);
              }
        break;
        default:
        break;
      }
    }
    }

    public void Pull(bool midbending,Transform hand,Element element)
    {
      if(cooldowntimer <= 0){
      cooldowntimer = bendcooldown;
      switch (element.elementType)
      {
        case 0://air
          if(midbending == false) //directional gust
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

            if(midbending == true)
            {
              element.currentStrength ++;
              earthmanager.GetAllInRangeAndPull(hand,element.currentStrength,element.currentStrength);
            }else
            {
              earthmanager.GetAllInRangeAndPull(hand,element.currentStrength,element.currentStrength * 2);
            }
            lastaction = 1;

        break;
        case 2://fire
            if(midbending == true)
            {//small flamethrower
                GameObject clonefire = Instantiate(flameThrower,hand.position,hand.rotation) as GameObject;
                clonefire.GetComponent<DieInTime>().lifetime = 1.0f;
                // clonefire.GetComponent<Rigidbody>().AddForce(clonefire.transform.forward * element.currentStrength);
                lastaction = 2;
                  element.currentStrength++;
              }else
              {
                GameObject clonefire = Instantiate(flameThrower,hand.position,hand.rotation) as GameObject;
                clonefire.GetComponent<DieInTime>().lifetime = element.currentStrength;
                clonefire.GetComponent<Rigidbody>().AddForce(clonefire.transform.forward * element.currentStrength);
              }
        break;
        default:
        break;
      }
    }
  }
    //back and left right/up down generates strength

}
