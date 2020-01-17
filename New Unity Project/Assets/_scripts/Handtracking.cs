using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handtracking : MonoBehaviour
{
  public int element; //air,earth,fire,water
  public Transform leftHand,rightHand;
  public Vector3 lastposLeft,lastposRight;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetElement(int newElement)
    {
      element = newElement;

    }
}
