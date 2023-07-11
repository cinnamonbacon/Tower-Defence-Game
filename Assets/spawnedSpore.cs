using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnedSpore : MonoBehaviour
{
    private float counter;
    private float lifeSpan;
    // Start is called before the first frame update
    void Start()
    {
        lifeSpan = 17;
        counter = lifeSpan;
    }

    // Update is called once per frame
    void Update()
    {
        if (counter <= 0)
        {
            GameObject.Find("/Manager").GetComponent<SporeSpawner>().removeLoc(this.transform);
            Destroy(this.gameObject);
        }
        else
        {
            counter -= Time.deltaTime;
        }
    }
}
