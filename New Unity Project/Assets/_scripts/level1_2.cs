using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level1_2 : MonoBehaviour
{
    public LevelManager levelManager;
    public float targetTime = 2.0f;

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
    }
}
