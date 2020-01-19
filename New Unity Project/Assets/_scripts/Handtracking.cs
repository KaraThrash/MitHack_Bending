using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Handtracking : MonoBehaviour
{
  public Bending bending;
  public int element,stationarycount,maxstationarycount; //air,earth,fire,water
  public Transform leftHand,rightHand,elementManagers;
  public Vector3 lastposLeft,lastposRight,lastKataAction;
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

      if(Vector3.Distance(lastposLeft, leftHand.position) >= distanceToCheckFor && Vector3.Distance(lastposRight, rightHand.position) <= Vector3.Distance(lastposLeft, leftHand.position))
      {
        //hand moved enough to not be hand shaking


        //check left or right movement
        string tempstring = "";
        Vector3 handmovement = new Vector3(0,0,0);
        if(Mathf.Abs(lastposLeft.x - leftHand.position.x) >= distanceToCheckFor){
          Debug.Log("X");tempstring += "x";
          handmovement = new Vector3(Mathf.Sign(lastposLeft.x - leftHand.position.x),0,0);
        }
        //check verticalmovement
          if(Mathf.Abs(lastposLeft.y - leftHand.position.y) >= distanceToCheckFor){
            handmovement = new Vector3(0,Mathf.Sign(lastposLeft.y - leftHand.position.y),0);
            Debug.Log("Y");
            tempstring += "y";
          }
          //check forward back movement
            if(Mathf.Abs(lastposLeft.z - leftHand.position.z) >= distanceToCheckFor){
                handmovement = new Vector3(0,0,Mathf.Sign(lastposLeft.z - leftHand.position.z));
              Debug.Log("Z");tempstring += "z";
            }


            lastposLeft = leftHand.position;
              lastposRight = rightHand.position;
            debugMovementDisplay2.text = tempstring + (handmovement.ToString());
            stationarycount = 0;


          if(handmovement != Vector3.zero)
          {
              leftHand.LookAt(2 * leftHand.position - rightHand.position);
              lastKataAction = handmovement;
             Bend();

          }
          else
          {
            leftHand.LookAt(2 * leftHand.position - leftHand.parent.position);


          }


      }
      else if( Vector3.Distance(lastposRight, rightHand.position) >= distanceToCheckFor)
      {

        lastposLeft = leftHand.position;
          lastposRight = rightHand.position;
            leftHand.LookAt( rightHand.position );
          // leftHand.LookAt(2 * leftHand.position - leftHand.parent.position);
        heldElement.GetComponent<Element>().currentStrength++;
      }
      else
      {

          if(stationarycount == 0)
          {
            timer = timeIncrement * 0.5f;
            stationarycount++;
            // Bend();
          }else{


              FinishBend();

          }
          lastposLeft = leftHand.position;
          lastposRight = rightHand.position;
      }

    }


      public void Bend()
      {
          // if(lastKataAction == Vector3.zero){return;}
          if(lastKataAction == new Vector3(0,0,-1))
          {
            bending.Push(true,leftHand,heldElement.GetComponent<Element>());
            Debug.Log("push");
          }
          else if(lastKataAction == new Vector3(0,0,1))
          {
              bending.Pull(true,leftHand,heldElement.GetComponent<Element>());
            Debug.Log("pull");
          }
          else if(lastKataAction == new Vector3(0,-1,0))
          {
            Debug.Log("up");
          }
          else if(lastKataAction == new Vector3(0,1,0))
          {
            Debug.Log("down");
          }
          else if(lastKataAction == new Vector3(-1,0,0))
          {
            Debug.Log("left-right");
          }
          else if(lastKataAction == new Vector3(1,0,0))
          {
            Debug.Log("right-left");
          }
          else if(lastKataAction == new Vector3(0,-1,-1))
          {
            Debug.Log("up forward");
          }
          else if(lastKataAction == new Vector3(0,1,-1))
          {
            Debug.Log("down forward");
          }
          else if(lastKataAction == new Vector3(-1,-1,0))
          {
            Debug.Log("right up");
          }
          else if(lastKataAction == new Vector3(1,-1,0))
          {
            Debug.Log("leftup");
          }
          else if(lastKataAction == new Vector3(-1,1,0))
          {
            Debug.Log("right down");
          }
          else if(lastKataAction == new Vector3(1,1,0))
          {
            Debug.Log("leftdown");
          }
          else
          {}
            // lastKataAction = Vector3.zero;
    }
    public void FinishBend()
    {
      // if(lastKataAction == Vector3.zero){return;}
      if(lastKataAction == new Vector3(0,0,-1))
      {
        bending.Push(false,leftHand,heldElement.GetComponent<Element>());
        Debug.Log("push");
      }
      else if(lastKataAction == new Vector3(0,0,1))
      {
          bending.Pull(false,leftHand,heldElement.GetComponent<Element>());
        Debug.Log("pull");
      }
      else if(lastKataAction == new Vector3(0,-1,0))
      {
        bending.DownUp(false,leftHand,heldElement.GetComponent<Element>());
        Debug.Log("up");
      }
      else if(lastKataAction == new Vector3(0,1,0))
      {
        bending.UpDown(false,leftHand,heldElement.GetComponent<Element>());
        Debug.Log("down");
      }
      else if(lastKataAction == new Vector3(-1,0,0))
      {
        bending.Push(false,leftHand,heldElement.GetComponent<Element>());
        Debug.Log("left-right");
      }
      else if(lastKataAction == new Vector3(1,0,0))
      {
        bending.Push(false,leftHand,heldElement.GetComponent<Element>());
        Debug.Log("right-left");
      }
      else if(lastKataAction == new Vector3(0,-1,-1))
      {
        bending.DownUp(false,leftHand,heldElement.GetComponent<Element>());
        Debug.Log("up forward");
      }
      else if(lastKataAction == new Vector3(0,1,-1))
      {
        bending.UpDown(false,leftHand,heldElement.GetComponent<Element>());
        Debug.Log("down forward");
      }
      else if(lastKataAction == new Vector3(-1,-1,0))
      {
        bending.DownUp(false,leftHand,heldElement.GetComponent<Element>());
        Debug.Log("right up");
      }
      else if(lastKataAction == new Vector3(1,-1,0))
      {
        bending.DownUp(false,leftHand,heldElement.GetComponent<Element>());
        Debug.Log("leftup");
      }
      else if(lastKataAction == new Vector3(-1,1,0))
      {
        bending.UpDown(false,leftHand,heldElement.GetComponent<Element>());
        Debug.Log("right down");
      }
      else if(lastKataAction == new Vector3(1,1,0))
      {
        bending.UpDown(false,leftHand,heldElement.GetComponent<Element>());
        Debug.Log("leftdown");
      }
      else if(lastKataAction == new Vector3(-1,1,1))
      {
        bending.UpDown(false,leftHand,heldElement.GetComponent<Element>());
        Debug.Log("right down");
      }
      else if(lastKataAction == new Vector3(1,1,-1))
      {
        bending.UpDown(false,leftHand,heldElement.GetComponent<Element>());
        Debug.Log("leftdown");
      }
      else if(lastKataAction == new Vector3(-1,-1,1))
      {
        bending.DownUp(false,leftHand,heldElement.GetComponent<Element>());
        Debug.Log("right down");
      }
      else if(lastKataAction == new Vector3(1,-1,-1))
      {
        bending.DownUp(false,leftHand,heldElement.GetComponent<Element>());
        Debug.Log("leftdown");
      }
      else
      {}
        lastKataAction = Vector3.zero;
          if(heldElement.GetComponent<Collider>() != null)
          {
            heldElement.GetComponent<Collider>().enabled = true;
          }

        if(heldElement != null){
          if(heldElement.GetComponent<DieInTime>() != null ){

            if( heldElement.GetComponent<DieInTime>().lifetime == -1){

              heldElement.GetComponent<DieInTime>().lifetime = 0.1f;
            }
          }else{  Destroy(heldElement);}

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
      timer = timeIncrement;
      // heldElement.transform.parent = leftHand;
      heldElement.GetComponent<Rigidbody>().isKinematic = true;
    }
  }
