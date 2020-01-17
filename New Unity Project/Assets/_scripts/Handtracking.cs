using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Handtracking : MonoBehaviour
{
  public int element; //air,earth,fire,water
  public Transform leftHand,rightHand;
  public Vector3 lastposLeft,lastposRight;
  public List<GameObject> elements;//air,earth,fire,water
  public List<Vector3> handMovements;//if the hand keeps moving continue to construct a more complex move
  public GameObject heldElement;
  public float elementspeed = 1.0f;
  public float timer,distanceToCheckFor,timeIncrement = 0.2f;
  public Text debugMovementDisplay;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if(heldElement != null){

          heldElement.transform.position = Vector3.MoveTowards(heldElement.transform.position, leftHand.transform.position, elementspeed * Time.deltaTime);
          timer -= Time.deltaTime;
          if(timer <= 0)
          {
            timer = timeIncrement;
            HandMovementLogic();
          }
      }
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
            debugMovementDisplay.text = tempstring;
      }
      else
      {
        //hand moved very little, or is just minor hand tremors that we want to normalize for
        lastposLeft = leftHand.transform.position;
        Debug.Log("NONE");
        debugMovementDisplay.text = "None";
      }

    }
    public void SetElement(int newElement)
    {
      element = newElement;
      if(heldElement != null){Destroy(heldElement);}
      heldElement = Instantiate(elements[element],leftHand.position,leftHand.rotation) as GameObject;
      // heldElement.transform.parent = leftHand;

    }
}
