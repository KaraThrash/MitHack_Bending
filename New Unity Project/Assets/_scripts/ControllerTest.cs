using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

      //axis 1 2 left trach pad
      //axis 4 5 right track pad
      //axis 3 trigger
      // 11 left grip 12 right grip
      if (Input.GetAxis("HTC_VIU_UnityAxis1") != 0)
      {

         Debug.Log("HTC_VIU_UnityAxis1");
      }
      if (Input.GetAxis("HTC_VIU_UnityAxis4") != 0)
      {
         Debug.Log("HTC_VIU_UnityAxis4");
      }

      if (Input.GetAxis("HTC_VIU_UnityAxis5") != 0)
      {
         Debug.Log("HTC_VIU_UnityAxis5");
      }
      if (Input.GetAxis("HTC_VIU_UnityAxis2") != 0)
      {
         Debug.Log("HTC_VIU_UnityAxis2");

      }
      if (Input.GetAxis("HTC_VIU_UnityAxis6") != 0)
      {
         Debug.Log("HTC_VIU_UnityAxis6");
      }
      if (Input.GetAxis("HTC_VIU_UnityAxis12") != 0)
      {
         Debug.Log("HTC_VIU_UnityAxis12");
      }
      if (Input.GetAxis("HTC_VIU_UnityAxis11") != 0)
      {
         Debug.Log("HTC_VIU_UnityAxis11");
      }

      if (Input.GetAxis("Vertical") != 0)
      {
         Debug.Log("vert");
      }


      if (Input.GetKeyDown(KeyCode.JoystickButton8))
      {
         Debug.Log("b8");
      }
      if (Input.GetKeyDown(KeyCode.JoystickButton7))
      {
         Debug.Log("b7");
      }
      if (Input.GetKeyDown(KeyCode.JoystickButton6))
      {
         Debug.Log("b6");
      }
      if (Input.GetKeyDown(KeyCode.JoystickButton5))
      {
         Debug.Log("b5");

      }
      if (Input.GetKeyDown(KeyCode.JoystickButton4))
      {
         Debug.Log("b4");
      }
      if (Input.GetKeyDown(KeyCode.JoystickButton3))
      {
         Debug.Log("b3");
      }
      if (Input.GetKeyDown(KeyCode.JoystickButton2))
      {
         Debug.Log("b2");

      }
      if (Input.GetKeyDown(KeyCode.JoystickButton1))
      {
         Debug.Log("b1");
      }
    }
}
