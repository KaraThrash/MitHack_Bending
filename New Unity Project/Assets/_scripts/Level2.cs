using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    public LevelManager levelManager;
    private bool levelOver = false;

    public GameObject windmill1, windmill2;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        windmill1 = GameObject.Find("Windmill1");
        windmill2 = GameObject.Find("Windmill2");
    }

    // Update is called once per frame
    void Update()
    {
        if (levelOver) return;
        Debug.Log(windmill1.GetComponent<ResponsiveObject>().LastActiveElement());
        if (windmill1.GetComponent<ResponsiveObject>().LastActiveElement() == 0 && 
                windmill2.GetComponent<ResponsiveObject>().LastActiveElement() == 0) {
            levelOver = true;
            levelManager.OpenDoor();
        }
    }
}
