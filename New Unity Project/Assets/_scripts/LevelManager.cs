using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private float startDoorOpen = -1;
    private Vector3 doorOrigPos;
    public GameObject door;
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

    public void OpenDoor() {
        Debug.Log("opening door");
        startDoorOpen = Time.time;
    }
    public void Restart() {
    }
}
