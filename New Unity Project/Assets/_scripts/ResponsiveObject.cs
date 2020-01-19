using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controls response of objects that interact with elements
// fire burns or lights things on fire w/o destroying them
// air shrinks or just knocks things down
// water grows or just knocks things down

public class ResponsiveObject : MonoBehaviour
{
  public LevelManager levelmanager;
    public Vector3 bottomPoint,growSize,startPosition;
    public GameObject splitObject;
    public Object flameEffect;
    public int tempElem = 2;
    public int moveListenDistance = 1;

    public bool flammable, lightable, growable, splitable, movePuzzleObject;

    private bool onFire = false;
    private int lastActiveElement = -1;

    private float targetScale = 2;
    private float growRate = 5;
    private float origHeight,lifeTime;
    private Vector3 origPos;

    private float startSizeChange = 0;
    public int listenForElementType;
    void Start()
    {
      lifeTime = -1;
        origHeight = transform.localScale.y;
        origPos = transform.position;
        growSize =   transform.localScale;
        startPosition = transform.position;
    }

    void Update()
    {
        if (growable && startSizeChange != 0)
        {
          if(transform.localScale.y < growSize.y)
          {
            float delta = Time.time - startSizeChange;
            float newScale = Mathf.Lerp(origHeight, origHeight * targetScale, delta);
            transform.localScale = new Vector3(transform.localScale.x+ (2 * Time.deltaTime), transform.localScale.y + (growRate * 2 *  Time.deltaTime), transform.localScale.z+ (2 * Time.deltaTime));
            float newPos = Mathf.Lerp(origPos.y, origPos.y + 0.5f*targetScale , delta);
            transform.position = new Vector3(origPos.x, newPos, origPos.z);
          }else
          {
            growable = false;
            levelmanager.LevelObjectChanged(this.gameObject,false,false,true);
          }

        }
        if(movePuzzleObject == true && moveListenDistance < Vector3.Distance(startPosition, transform.position))
        {
          levelmanager.LevelObjectChanged(this.gameObject,false,true,false);
        }
        if(lifeTime != -1)
        {
          lifeTime -= Time.deltaTime;
          if(lifeTime <= 0)
          {BurnDown();}
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // GameObject incoming = collision.gameObject;
        // Debug.Log(incoming.name + " hit " + gameObject.name);


        if(collision.transform.GetComponent<Element>() != null)
        {

            switch (collision.transform.GetComponent<Element>().elementType) {
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
        // if (IsElement(incoming)) {
        //     lastActiveElement = getElementType(incoming);
        //     Debug.Log("element " + lastActiveElement);
        //     switch (getElementType(incoming)) {
        //         case 2: //fire
        //             if (flammable) Burn();
        //             else if (lightable) SetFire();
        //             break;
        //         case 3: //water
        //             if (growable) Grow();
        //             break;
        //         case 0: //air
        //             if (splitable) Split();
        //             break;
        //         case 1: //earth
        //             break;
        //     }
        // }
    }
    private void OnTriggerEnter(Collider collision)
    {
        GameObject incoming = collision.gameObject;
        Debug.Log(incoming.name + " hit " + gameObject.name);


        if(collision.transform.GetComponent<Element>() != null)
        {

            switch (collision.transform.GetComponent<Element>().elementType) {
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
        // if (IsElement(incoming)) {
        //     lastActiveElement = getElementType(incoming);
        //     Debug.Log("element " + lastActiveElement);
        //     switch (getElementType(incoming)) {
        //         case 2: //fire
        //             if (flammable) Burn();
        //             else if (lightable) SetFire();
        //             break;
        //         case 3: //water
        //             if (growable) Grow();
        //             break;
        //         case 0: //air
        //             if (splitable) Split();
        //             break;
        //         case 1: //earth
        //             break;
        //     }
        // }
    }
    private void Burn() {
        SetFire();
        lifeTime = 2;
        onFire = true;
        // Destroy(gameObject, 2);
        //Object flames = Instantiate(flameEffect, transform.position, Quaternion.identity);
        //Object flames = Instantiate(Resources.Load("ObjectFire"), transform.position, Quaternion.identity);

    }

    private void SetFire() {
        //Destroy(flames, 2);
        Debug.Log("setting on fire");
        //Object flames = Instantiate(Resources.Load("ObjectFire"), transform.position, Quaternion.identity);
        Object flames = Instantiate(flameEffect, transform.position, Quaternion.identity);
        onFire = true;
    }
    public void BurnDown()
    {
        levelmanager.LevelObjectChanged(this.gameObject,true,false,false);
        Destroy(this.gameObject);
    }
    private void Grow() {
      if(growSize == transform.localScale)
      {
        targetScale = 2;
        startSizeChange = Time.time;
        growSize = transform.localScale * 4;
        // growable = false;
      }

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
        ///return 0;
        return gobj.GetComponent<FlameControl>().elementType;
        //return gameObject.GetComponent<Element>().elementType;
    }

    public bool isFlaming() {
        return onFire;
    }

    public int LastActiveElement() {
        return lastActiveElement;
    }
}
