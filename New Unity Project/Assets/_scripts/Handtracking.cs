using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Handtracking : MonoBehaviour
{
  public int element,stationarycount,maxstationarycount; //air,earth,fire,water
  public Transform leftHand,rightHand,elementManagers;
  public Vector3 lastposLeft,lastposRight;
  public List<GameObject> elements,heldElements;//air,earth,fire,water
  public List<Vector3> handMovements;//if the hand keeps moving continue to construct a more complex move
  public GameObject heldElement;
  public float elementspeed = 1.0f;
  public float timer,distanceToCheckFor,timeIncrement = 0.2f;
  public Text debugMovementDisplay,debugMovementDisplay2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      // TestControls();
        CreateElement();
      if(heldElement != null){

          heldElement.transform.position = Vector3.MoveTowards(heldElement.transform.position, leftHand.transform.position, elementspeed * Time.deltaTime);
          timer -= Time.deltaTime;
          if(timer <= 0)
          {
            timer = timeIncrement;
            HandMovementLogic();
          }
      }else{
        CreateElement();
      }
    }

    public void TestControls()
    {

      if(OVRInput.Get(OVRInput.Button.One)){ debugMovementDisplay.text = "OVRInput.Button.One";}
            if(OVRInput.Get(OVRInput.Button.Two)){ debugMovementDisplay.text = "OVRInput.Button.two";}
            if(OVRInput.Get(OVRInput.Button.Three)){ debugMovementDisplay.text = "OVRInput.Button.three";}
                  if(OVRInput.Get(OVRInput.Button.Four)){ debugMovementDisplay.text = "OVRInput.Button.four";}
      if(OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTrackedRemote).x < 0){ debugMovementDisplay.text = "x PrimaryThumbstick y";}

      if(OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTrackedRemote).y < 0){ debugMovementDisplay.text = "y PrimaryThumbstick y";}

      if(  OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) < 0){ debugMovementDisplay.text = "SecondaryIndexTrigger";}
            if(  OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) < 0){ debugMovementDisplay.text = "LIndexTrigger";}

            if(  OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp)) { debugMovementDisplay.text = "PrimaryThumbstickUp";}
                  if(  OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) < 0){ debugMovementDisplay.text = "LIndexTrigger";}
    }
    public void CreateElement()
    {
      if(OVRInput.GetDown(OVRInput.Button.One)){ SetElement(0);debugMovementDisplay.text = "0";}
      else if(OVRInput.GetDown(OVRInput.Button.Two)){ SetElement(1);debugMovementDisplay.text = "1";}
      else if(OVRInput.GetDown(OVRInput.Button.Three)){ SetElement(2);debugMovementDisplay.text = "2";}
      else if(OVRInput.GetDown(OVRInput.Button.Four)){ SetElement(3);debugMovementDisplay.text = "3";}else{}

    }
    public void HandMovementLogic()
    {
      if(Vector3.Distance(lastposLeft, leftHand.position) >= distanceToCheckFor)
      {
        //hand moved enough to not be hand shaking


        //check left or right movement
        string tempstring = "";
        if(Mathf.Abs(lastposLeft.x - leftHand.position.x) >= distanceToCheckFor){Debug.Log("X");tempstring += "x";}
        //check verticalmovement
          if(Mathf.Abs(lastposLeft.y - leftHand.position.y) >= distanceToCheckFor){Debug.Log("Y");tempstring += "y";}
          //check forward back movement
            if(Mathf.Abs(lastposLeft.z - leftHand.position.z) >= distanceToCheckFor){Debug.Log("Z");tempstring += "z";}
            lastposLeft = leftHand.transform.position;
            debugMovementDisplay2.text = tempstring;
            stationarycount = 0;
            GameObject clone = Instantiate(elements[element],leftHand.position,leftHand.rotation) as GameObject;
            clone.GetComponent<DieInTime>().lifetime = 10.0f;
            clone.GetComponent<Rigidbody>().useGravity = true;
            clone.GetComponent<Element>().currentlyHeld = false;
          clone.GetComponent<Collider>().enabled = true;
      }
      else
      {
        //hand moved very little, or is just minor hand tremors that we want to normalize for
        stationarycount++;
        //can only hold still for a limited time, moving arms resets it
        if(stationarycount > maxstationarycount){
            if(heldElement != null){
                heldElement.GetComponent<DieInTime>().lifetime = 2.0f;
                heldElement.GetComponent<Element>().currentlyHeld = false;
              heldElement.GetComponent<Collider>().enabled = true;
            }

        }else{
        lastposLeft = leftHand.transform.position;
        Debug.Log("NONE");
        debugMovementDisplay2.text = "None";
        elementManagers.GetChild(element).GetComponent<ElementManager>().GetClosestElement(leftHand);
        }
      }

    }
    public void SetElement(int newElement)
    {
      element = newElement;
      if(heldElement != null){Destroy(heldElement);}
      heldElement = Instantiate(elements[element],leftHand.position,leftHand.rotation) as GameObject;
      // heldElement.GetComponent<Element>().currentlyHeld = true;
        heldElement.GetComponent<Element>().Grab(leftHand);
    heldElement.GetComponent<Collider>().enabled = false;
      lastposLeft = leftHand.transform.position;
      // heldElement.transform.parent = leftHand;

    }
  }
