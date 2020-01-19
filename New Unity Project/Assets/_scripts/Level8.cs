using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level8 : MonoBehaviour
{
    //0=air, 1=earth, 2=fire, 3=water
    public LevelManager levelManager;
    private bool levelOver = false;

    GameObject tree1, tree2, tree3, tree4;
    // order = 3124
    private int index;
    private GameObject[] order;
    private bool g1, g2, g3, g4;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        tree1 = GameObject.Find("tree1");
        tree2 = GameObject.Find("tree2");
        tree3 = GameObject.Find("tree3");
        tree4 = GameObject.Find("tree4");

        order = new GameObject[4];
        order[0] = tree1;
        order[1] = tree2;
        order[2] = tree3;
        order[3] = tree4;
    
        foreach (GameObject t in order) {
            t.GetComponent<ResponsiveObject>().growable = false;
        }

        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelOver)
        {
            return;
        }
        order[index].GetComponent<ResponsiveObject>().growable = true;
        Debug.Log(index);

        if (order[index].GetComponent<ResponsiveObject>().LastActiveElement() == 3)
        {
            Debug.Log(index);
            index++;
        }
        else {
            //ResetLevel();
        }

        if (index > 3) {
            levelManager.OpenDoor();
            levelOver = true;
        }
    }
}


