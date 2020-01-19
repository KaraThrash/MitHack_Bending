using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controls response of objects that interact with elements
// fire burns or lights things on fire w/o destroying them
// air shrinks or just knocks things down
// water grows or just knocks things down

public class ResponsiveObject : MonoBehaviour
{
    public Vector3 bottomPoint;
    public GameObject splitObject;
    public Object flameEffect;
    public int tempElem = 2;

    public bool flammable, lightable, growable, splitable;

    private bool onFire = false;
    private int lastActiveElement = -1;

    private float targetScale = 2;
    private float growRate = 1;
    private float origHeight;
    private Vector3 origPos;

    private float startSizeChange = -1;

    void Start()
    {
        origHeight = transform.localScale.y;
        origPos = transform.position;
    }

    void Update()
    {
        if (startSizeChange > 0)
        {
            float delta = Time.time - startSizeChange;
            float newScale = Mathf.Lerp(origHeight, origHeight * targetScale, delta);
            transform.localScale = new Vector3(transform.localScale.x, newScale, transform.localScale.z);
            float newPos = Mathf.Lerp(origPos.y, origPos.y * (0.75f*targetScale), delta);
            transform.position = new Vector3(origPos.x, newPos, origPos.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject incoming = collision.gameObject;
        Debug.Log(incoming.name + " hit " + gameObject.name);

        if (IsElement(incoming)) {
            lastActiveElement = getElementType(incoming);
            switch (getElementType(incoming)) {
                case 2: //fire
                    if (flammable) Burn();
                    else if (lightable) SetFire();
                    break;
                case 3: //water
                    if (growable) Grow();
                    break;
                case 0: //air
                    if (splitable) Split();
                    break;
                case 1: //earth
                    break;
            }
        }
    }

    private void Burn() {
        SetFire();
        Destroy(gameObject, 2);
        //Object flames = Instantiate(flameEffect, transform.position, Quaternion.identity);
        //Object flames = Instantiate(Resources.Load("ObjectFire"), transform.position, Quaternion.identity);

    }

    private void SetFire() {
        //Destroy(flames, 2);
        Object flames = Instantiate(Resources.Load("ObjectFire"), transform.position, Quaternion.identity);
        onFire = true;
    }

    private void Grow() {
        targetScale = 2;
        startSizeChange = Time.time;
        return;
    }

    private void Split()
    { //shrinks lol
        targetScale = 0;
        startSizeChange = Time.time;
    }

    private bool IsElement(GameObject gobj)
    {
        return gobj.name == "HandCube";
        //return gobj.GetComponent<Element>() != null;
    }

    private int getElementType(GameObject gobj) {
        //return tempElem;
        return 0;
        //return gameObject.GetComponent<Element>().elementType;
    }

    public bool isFlaming() {
        return onFire;
    }

    public int LastActiveElement() {
        return lastActiveElement;
    }
}
