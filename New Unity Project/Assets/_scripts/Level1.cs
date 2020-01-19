using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
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
        if (GameObject.Find("FireSphere").GetComponent<ResponsiveObject>().isFlaming())
            timerStart = true;
        if (timerStart)
            targetTime -= Time.deltaTime;
        if (targetTime <= 0.0f)
        {
            timerEnded();
            levelOver = true;
        }
    }

    void timerEnded()
    {
        Debug.Log("ended");
        levelManager.OpenDoor();
    }
}
