using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spores : MonoBehaviour
{
    private int spores;
    // Start is called before the first frame update
    void Start()
    {
        spores = 0;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = "Spores: " + spores;
    }

    public void changeSpores(int sporesChange)
    {
        this.spores += sporesChange;
    }
}
