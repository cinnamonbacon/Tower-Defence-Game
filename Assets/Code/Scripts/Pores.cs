using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pores : MonoBehaviour
{
    private int pores;
    // Start is called before the first frame update
    void Start()
    {
        pores = 0;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = "Pores: " + pores;
    }

    public void changePores(int poresChange)
    {
        this.pores += poresChange;
    }
}
