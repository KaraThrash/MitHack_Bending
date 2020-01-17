using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
  public Transform hand;
        public Vector3 targetpos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

      targetpos = new Vector3(Mathf.Clamp(targetpos.x + (Input.GetAxis("HortMouse") * 0.01f), -1, 1) ,Mathf.Clamp(targetpos.y + (Input.GetAxis("VertMouse")* 0.01f), -1, 1),targetpos.z);
      if(Input.GetMouseButton(0)) {Debug.Log("leftmouse"); targetpos = new Vector3(targetpos.x,targetpos.y,Mathf.Clamp(targetpos.z + 0.3f, -1, 1) ); }
      if(Input.GetMouseButton(1)) {Debug.Log("rightmouse"); targetpos = new Vector3(targetpos.x,targetpos.y,Mathf.Clamp(targetpos.z - 0.3f, -1, 1)); }
      hand.localPosition = Vector3.MoveTowards(hand.localPosition, targetpos, 1.0f * Time.deltaTime);

      // }
    }
}
