using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
  public float handspeed = 2.0f;
  public Transform hand;
  public Vector3 targetpos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.A)){ GetComponent<Handtracking>().SetElement(0);}
      if(Input.GetKeyDown(KeyCode.S)){ GetComponent<Handtracking>().SetElement(1);}
      if(Input.GetKeyDown(KeyCode.D)){ GetComponent<Handtracking>().SetElement(2);}
      if(Input.GetKeyDown(KeyCode.F)){ GetComponent<Handtracking>().SetElement(3);}
    MoveHandWithMouse();
    }

    public void MoveHandWithMouse()
    {
      targetpos = new Vector3(Mathf.Clamp(targetpos.x + (Input.GetAxis("HortMouse") * 0.01f), -1, 1) ,Mathf.Clamp(targetpos.y + (Input.GetAxis("VertMouse")* 0.01f), -1, 1),targetpos.z);
      if(Input.GetMouseButton(0)) {Debug.Log("leftmouse"); targetpos = new Vector3(targetpos.x,targetpos.y,Mathf.Clamp(targetpos.z - 0.1f, -1, 1) ); }
      if(Input.GetMouseButton(1)) {Debug.Log("rightmouse"); targetpos = new Vector3(targetpos.x,targetpos.y,Mathf.Clamp(targetpos.z + 0.1f, -1, 1)); }
      hand.localPosition = Vector3.MoveTowards(hand.localPosition, targetpos, handspeed * Time.deltaTime);

    }
}
