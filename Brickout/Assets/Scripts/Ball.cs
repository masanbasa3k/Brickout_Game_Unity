using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;
    public Vector2 direction;
    public Vector3 oldPos;
    public Vector3 nextPos;
    private LayerMask layerMask;
    public GameManager gameManager;


    void Awake()
    {
        layerMask = LayerMask.GetMask("Wall","Brick","EZ");
        float directionX = Random.value > 0.5f ? 1 : -1;
        float directionY = -1;

        direction = new Vector2(directionX, directionY).normalized;
        oldPos = transform.position;

        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        Vector3 rayCastDirection = (transform.position - oldPos).normalized;
        RaycastHit2D hit = Physics2D.Raycast(oldPos, rayCastDirection, Vector3.Distance(transform.position, oldPos), layerMask);

        if (hit)
        {
            transform.position = hit.point - direction * 0.1f;
            direction = Vector2.Reflect(direction, hit.normal);
            if (hit.collider.isTrigger)
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("EZ"))
                {
                    gameManager.lives--;
                    Destroy(gameObject);
                }
                else
                {
                    Transform parent = (hit.collider.transform.parent).parent;
                    Brick brick = parent.GetComponent<Brick>();
                    brick.health--;
                }      
            }
        }
        oldPos = transform.position;
        nextPos = transform.position + (Vector3)direction;
    }
}
