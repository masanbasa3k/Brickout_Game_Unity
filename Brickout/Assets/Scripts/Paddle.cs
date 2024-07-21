using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    void Update()
    {

        // take mouse position
        // move paddle to mouse position

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        // transform.position = new Vector3(mousePos.x, 0, 0);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePos, 1f, LayerMask.GetMask("Wall"));
        if (!hit)
        {
            transform.position = new Vector3(mousePos.x, 0, 0);
        }
    }
}
