using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bending : MonoBehaviour
{
  public int elementBendState,lastaction;
  public GameObject windobj,rock,earthshield,flameThrower,waterBall;
    public GameObject currentWaterBall,lastSpawnedFire,lastSpawnedAir,lastSpawnedEarth;
  public float bendcooldown,cooldowntimer;
  public ElementManager airManager,earthmanager,fireManager,waterManager;
  public Text elementchargetext;
    public List<GameObject> waterList;
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
            clone.GetComponent<DieInTime>().lifetime = 1;
            clone.transform.localScale = new Vector3(Mathf.Clamp(clone.transform.localScale.x +(0.2f * element.currentStrength),3,8),Mathf.Clamp(clone.transform.localScale.y +(0.2f * element.currentStrength),3,8),Mathf.Clamp(clone.transform.localScale.z * element.currentStrength,10,40));
            clone.GetComponent<Element>().currentStrength = element.currentStrength;
            clone.GetComponent<Element>().primary = true;
            lastaction = -1;
          }
          else //small directional wind
          {
            if(lastSpawnedAir != null){Destroy(lastSpawnedAir);}

              lastSpawnedAir = Instantiate(windobj,hand.position,hand.rotation) as GameObject;
              // clone.transform.LookAt(hand.position);
              lastSpawnedAir.GetComponent<DieInTime>().lifetime = 1.0f;
                lastSpawnedAir.GetComponent<Element>().currentStrength = element.currentStrength;


            lastaction = 0;
              // element.currentStrength++;
          }
        break;
        case 1://earth
          if(midbending == true){
            GameObject clone2 = Instantiate(rock,hand.position,hand.rotation) as GameObject;
            clone2.GetComponent<DieInTime>().lifetime = element.currentStrength;
            clone2.GetComponent<Rigidbody>().AddForce(clone2.transform.forward * element.currentStrength,ForceMode.Impulse);
            lastaction = 1;
              element.currentStrength++;
            }else
            {
              GameObject clone2 = Instantiate(rock,hand.position,hand.rotation) as GameObject;
              clone2.GetComponent<DieInTime>().lifetime = element.currentStrength;
              clone2.transform.localScale = new Vector3(clone2.transform.localScale.x * element.currentStrength * 0.2f,clone2.transform.localScale.y * element.currentStrength * 0.2f,clone2.transform.localScale.z * element.currentStrength * 0.2f);
              clone2.GetComponent<Rigidbody>().AddForce(clone2.transform.forward * element.currentStrength,ForceMode.Impulse);
              clone2.GetComponent<Rigidbody>().mass = clone2.GetComponent<Rigidbody>().mass * element.currentStrength;
            }
        break;
        case 2://fire
            if(midbending == true)
            {//small flamethrower
                GameObject clonefire = Instantiate(flameThrower,hand.position,hand.rotation) as GameObject;
                clonefire.GetComponent<DieInTime>().lifetime = 1.2f;
                // clonefire.GetComponent<Rigidbody>().AddForce(clonefire.transform.forward * element.currentStrength);
                lastaction = 2;
                  element.currentStrength++;
              }else
              {
                GameObject clonefire = Instantiate(flameThrower,hand.position,hand.rotation) as GameObject;
                clonefire.GetComponent<DieInTime>().lifetime = 1.2f;
                clonefire.GetComponent<Rigidbody>().AddForce(clonefire.transform.forward * element.currentStrength);
              }
        break;
        case 3://Water
            if(midbending == true)
            {//water ball that adds to itself
                      if(currentWaterBall == null)
                      {
                        // currentWaterBall = Instantiate(waterBall,hand.position,hand.rotation) as GameObject;
                        //   currentWaterBall.GetComponent<DieInTime>().lifetime = 5;
                      }
                      else
                      {

                          currentWaterBall.GetComponent<DieInTime>().lifetime = 5;
                          currentWaterBall.GetComponent<Rigidbody>().AddForce((hand.position - currentWaterBall.transform.position) * Time.deltaTime,ForceMode.Impulse);
                        // GameObject clonewater = Instantiate(waterBall,currentWaterBall.transform.position + (currentWaterBall.transform.position - hand.position),hand.rotation) as GameObject;
                        // clonewater.GetComponent<Element>().Grab(currentWaterBall.transform);
                        // clonewater.transform.localScale = ( currentWaterBall.transform.localScale * 0.5f);
                        // clonewater.GetComponent<DieInTime>().lifetime = 5;
                        // clonewater.transform.parent = currentWaterBall.transform;
                      }

                      lastaction = 2;
                        // element.currentStrength++;
              }else
              {
                    if(currentWaterBall != null)
                    {
                      currentWaterBall.GetComponent<Collider>().enabled = true;
                      currentWaterBall.GetComponent<Rigidbody>().useGravity = true;
                      currentWaterBall.GetComponent<Rigidbody>().AddForce(hand.forward * element.currentStrength,ForceMode.Impulse);
                      currentWaterBall.GetComponent<DieInTime>().lifetime = 5;
                      currentWaterBall = null;
                    }
                      currentWaterBall = null;

              }
        break;
        default:
        break;
      }
    }
      elementchargetext.text = element.currentStrength.ToString();
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
            clone.GetComponent<DieInTime>().lifetime = 1;
           clone.transform.localScale = new Vector3(Mathf.Clamp(clone.transform.localScale.x +(0.2f * element.currentStrength),3,8),Mathf.Clamp(clone.transform.localScale.y +(0.2f * element.currentStrength),3,8),Mathf.Clamp(clone.transform.localScale.z * element.currentStrength,5,20));
            clone.GetComponent<Element>().currentStrength = -element.currentStrength;
            lastaction = -1;
              clone.GetComponent<Element>().primary = true;
          }
          else //small directional wind
          {

              if(lastSpawnedAir != null){Destroy(lastSpawnedAir);}
              else{
                lastSpawnedAir = Instantiate(windobj,hand.position,hand.rotation) as GameObject;
                // clone.transform.LookAt(hand.position);
                lastSpawnedAir.GetComponent<DieInTime>().lifetime = 0.2f;
                  lastSpawnedAir.GetComponent<Element>().currentStrength = -element.currentStrength;
              }

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
                clonefire.GetComponent<DieInTime>().lifetime = 1.2f;
                // clonefire.GetComponent<Rigidbody>().AddForce(clonefire.transform.forward * element.currentStrength);
                lastaction = 2;
                  element.currentStrength++;
              }else
              {
                GameObject clonefire = Instantiate(flameThrower,hand.position,hand.rotation) as GameObject;
                clonefire.GetComponent<DieInTime>().lifetime = 1.2f;
                clonefire.GetComponent<Rigidbody>().AddForce(clonefire.transform.forward * element.currentStrength);
              }
        break;
        case 3://Water
            if(midbending == true)
            {//water ball that adds to itself
                      if(currentWaterBall == null)
                      {
                        currentWaterBall = Instantiate(waterBall,hand.position,hand.rotation) as GameObject;
                        currentWaterBall.GetComponent<Element>().Grab(hand);
                      }
                      else
                      {
                          currentWaterBall.GetComponent<DieInTime>().lifetime = 5;
                        GameObject clonewater = Instantiate(waterBall,currentWaterBall.transform.position + (currentWaterBall.transform.position - hand.position),hand.rotation) as GameObject;
                        clonewater.GetComponent<Element>().Grab(currentWaterBall.transform);
                        clonewater.GetComponent<DieInTime>().lifetime = 10.0f;
                        clonewater.transform.localScale = ( currentWaterBall.transform.localScale * 0.2f);
                        // clonewater.transform.parent = currentWaterBall.transform;

                          clonewater.GetComponent<Rigidbody>().useGravity = true;
                          clonewater.GetComponent<Collider>().enabled = true;
                      }

                      lastaction = 2;
                        // element.currentStrength++;
              }else
              {
                    if(currentWaterBall != null)
                    {
                      currentWaterBall.GetComponent<Rigidbody>().useGravity = true;
                      currentWaterBall.GetComponent<Rigidbody>().AddForce(hand.forward * element.currentStrength,ForceMode.Impulse);
                      currentWaterBall.GetComponent<DieInTime>().lifetime = 5;
                    }else
                    {

                    }

                      currentWaterBall = null;
              }
        break;
        default:
        break;
      }
    }
    elementchargetext.text = element.currentStrength.ToString();
  }
    //back and left right/up down generates strength

}
