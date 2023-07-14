using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SporeCollection : MonoBehaviour
{
    private float collectiontTime;
    private float timeToCollection;
    // Start is called before the first frame update
    void Start()
    {
        collectiontTime = 5;
        timeToCollection = collectiontTime;
        
    }
    void Update()
    {
        if (dist()<=1)
        {
            collect();
        }
    }


    private void collect()
    {
        if (collectiontTime <= 0)
        {
            Debug.Log("test");
            GameObject.Find("/Canvas/SporeCount").GetComponent<Spores>().changeSpores(5);
            collectiontTime += timeToCollection;
        }
        else
        {
            Debug.Log(collectiontTime);
            collectiontTime -= Time.deltaTime;
        }
    }

    private double dist()
    {
        Vector3 shroomPos = GameObject.Find("/Shroomie").GetComponent<Transform>().position;
        return Math.Pow(Math.Pow((shroomPos.x-transform.position.x),2)+Math.Pow((shroomPos.y - transform.position.y),2),(0.5f));
    }

    
}
