using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level1_2 : MonoBehaviour
{
    public LevelManager levelManager;
    public float targetTime = 2.0f;
    public GameObject sphere1, sphere2, sphere3, sphere4;

    private bool timerStart = false;
    private bool levelOver = false;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    void Update()
    {
        if (levelOver) return;
        if (sphere1.GetComponent<ResponsiveObject>().isFlaming() &&
                sphere2.GetComponent<ResponsiveObject>().isFlaming() &&
                sphere3.GetComponent<ResponsiveObject>().isFlaming() &&
                sphere4.GetComponent<ResponsiveObject>().isFlaming()) {
            levelManager.OpenDoor();
            levelOver = true;
        }
        /*
        int count = 0;

        foreach (GameObject x in GameObject.FindGameObjectsWithTag("activatable"))
        {
            if (x.GetComponent<ResponsiveObject>().isFlaming())
            {
                count++;
            }
        }
        Debug.Log("count " + count);
        if (count >= 4)
        {
            levelManager.OpenDoor();
            levelOver = true;
        }
        */
    }
}
