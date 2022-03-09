using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{
    void Update() //automatically does funct every frame
    {
        FollowMouse();
        HandleInput();
    }

    void HandleInput()
    {
        if(Input.GetMouseButton(1)) //Get Mouse Button = holding mouse button
        {
            Thrust();
        }
        if (Input.GetMouseButtonDown(0)) //Get Mouse Button = holding mouse button
        {
            FireProjectile();
            FireProjectile2();
        }
    }

    void FollowMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        Vector2 directionToFace = new Vector2(
            mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = directionToFace; //always adding force to this direction
    }
}
