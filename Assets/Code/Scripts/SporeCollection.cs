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

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collectiontTime == 0)
        {
            GameObject.Find("/Canvas/SporeCount").GetComponent<Spores>().changeSpores(5);
            collectiontTime += timeToCollection;
        }
        else
        {
            collectiontTime -= Time.deltaTime;
        }
    }
}
