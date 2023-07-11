using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SporeSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject spore;
    private float cooldown ;
    private float count;
    Transform[] tileLoc;
    ArrayList locations;
    void Start()
    {
        cooldown = 5;
        count = cooldown;
        tileLoc = GameObject.Find("/Tiles").GetComponentsInChildren<Transform>();
        locations = new ArrayList();
    }

    // Update is called once per frame
    void Update()
    {
        if (count <= 0)
        {
            Vector3 location = tileLoc[tileNum()].position;
            locations.Add(tileLoc[tileNum()]);
            Instantiate(spore, location, Quaternion.identity);
            count = cooldown;
        }
        else
        {
            count -= Time.deltaTime;
        }
        
    }

    int tileNum()
    {
        int num = Random.Range(0, tileLoc.Length-1);
        while (locations.Contains(tileLoc[num]))
        {
            num++;
            if(num== tileLoc.Length)
            {
                num = 0;
            }
        }
        return num;
    }
}
