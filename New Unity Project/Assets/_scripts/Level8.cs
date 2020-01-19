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
    private Transform transform1, transform2, transform3, transform4;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        tree1 = GameObject.Find("tree1");
        tree2 = GameObject.Find("tree2");
        tree3 = GameObject.Find("tree3");
        tree4 = GameObject.Find("tree4");

        order = new GameObject[4];
        order[0] = tree3;
        order[1] = tree1;
        order[2] = tree2;
        order[3] = tree4;

        transform1 = tree1.transform;
        transform2 = tree2.transform;
        transform3 = tree3.transform;
        transform4 = tree4.transform;

        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelOver)
        {
            return;
        }
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

    void ResetLevel() {
        // play fail sound
        ResetTree(tree1, transform1);
        ResetTree(tree2, transform2);
        ResetTree(tree3, transform3);
        ResetTree(tree4, transform4);
    }

    void ResetTree(GameObject tree, Transform t) {
        tree.transform.position = new Vector3(t.position.x, t.position.y, t.position.z);
        tree.transform.localScale = new Vector3(t.localScale.x, t.localScale.y, t.localScale.z);
    }
}


