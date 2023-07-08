using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    //A boolean to keep track of whether the turret is selected
    private Boolean selected;

    //A float for the time the cooldown for repositioning takes
    public float repositioningTime;
    //The cooldown for repositioning
    private float repositioningCooldown;

    // Start is called before the first frame update
    void Start()
    {
        selected = false;
        repositioningCooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {

        //Counts down the cooldown for repositioning
        if (repositioningCooldown > 0)
        {
            repositioningCooldown -= Time.deltaTime;
        }


        //Runs if mouse is clicked
        if (Input.GetMouseButtonDown(0))
        {
            //Uses ray cast to hit a collision2d object
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);


            if(hit.collider == null)
            {
                //Sets selected back to false when clicking on no object
                selected = false;
            }
            else if (!selected) {
                if (hit.collider.gameObject == this.gameObject) {
                    //Sets selected to true
                    selected = true;
                }  
            }else if (hit.collider.gameObject.name.Substring(0,4) == "Tile" && repositioningCooldown<=0)
            {
                //Sets position to the tile clicked but at z level -1 so it gets hit by raycast over the tile
                this.transform.position = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y, -1);

                //Resets repositioning cooldown
                repositioningCooldown = repositioningTime;

                selected = false;
            }
            else
            {
                //Sets selected back to false when clicking anywhere else
                selected = false;
            }
        }
    }
}
