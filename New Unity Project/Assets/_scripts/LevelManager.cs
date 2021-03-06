﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
  public GameObject nextLevel,parentOfLevel;
    private float startDoorOpen = -1;
    private Vector3 doorOrigPos;
    public GameObject door;
    public bool listenForMovedObject,listenForBurnedObject,listenForPlacedObject,listenForGrownObject;
    public int burnedCount,movedCount,grownCount;
    public int elementTypeListening;
    public GameObject targetObject,aimObject;

    // Start is called before the first frame update
    void Start()
    {
        door = GameObject.Find("Door");
        doorOrigPos = door.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (startDoorOpen > 0)
        {
            float delta = Time.time - startDoorOpen;
            float newPos = Mathf.Lerp(doorOrigPos.y, doorOrigPos.y-7, delta/2);
            door.transform.position = new Vector3(doorOrigPos.x, newPos, doorOrigPos.z);
        }
    }
    public void LevelObjectChanged(GameObject obj,bool burned,bool moved,bool grown)
    {
      if(listenForBurnedObject == true && burned == true){
        burnedCount--;
        if(burnedCount <= 0){
          GameObject clone = Instantiate(nextLevel,transform.position,transform.rotation) as GameObject;
          clone.active = true;
          Destroy(parentOfLevel.gameObject);



        }
      }
      if(listenForPlacedObject == true && moved == true){

        if(Vector3.Distance(targetObject.transform.position,aimObject.transform.position) <= 1)
        {
          nextLevel.active = true; parentOfLevel.gameObject.active = false;
        }
      }
      if(listenForGrownObject == true && grown == true){
        grownCount --;
        if(grownCount <= 0){ nextLevel.active = true; parentOfLevel.gameObject.active = false;}
      }
    }
    public void OpenDoor() {
        Debug.Log("opening door");
        startDoorOpen = Time.time;
    }
    public void Restart() {
    }
}
