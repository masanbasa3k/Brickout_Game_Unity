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
    public float radius = 0.4f;
    public GameObject particlePrefab;
    [SerializeField] private AudioClip bounceSound;
    [SerializeField] private AudioClip gameOverSound;



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
        RaycastHit2D hit = Physics2D.Raycast(oldPos, rayCastDirection, Vector2.Distance(transform.position, oldPos), layerMask);
        if (hit)
        {
            if (hit.collider.isTrigger)
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("EZ"))
                {
                    float directionX = Random.value > 0.5f ? 1 : -1;
                    float directionY = -1;
                    direction = new Vector2(directionX, directionY).normalized;
                    transform.position = new Vector3(0, 0f, 0);

                    gameManager.lives--;
                    gameManager.updateLives();
                    AudioSource.PlayClipAtPoint(gameOverSound, transform.position);
                }
                else
                {
                    InstantiateParticle(transform.position);
                    Transform parent = (hit.collider.transform.parent).parent;
                    Brick brick = parent.GetComponent<Brick>();
                    brick.health--;
                    AudioSource.PlayClipAtPoint(bounceSound, transform.position);
                    transform.position = hit.point - direction * 0.1f;
                    direction = Vector2.Reflect(direction, hit.normal);
                }      
            }
            else
            {
                InstantiateParticle(transform.position);
                AudioSource.PlayClipAtPoint(bounceSound, transform.position);
                transform.position = hit.point - direction * 0.1f;
                direction = Vector2.Reflect(direction, hit.normal);
            }
        }
        oldPos = transform.position;
        nextPos = transform.position + (Vector3)direction;
    }

    public void InstantiateParticle(Vector3 position)
    {
        GameObject particle = Instantiate(particlePrefab, position, Quaternion.identity);
        Destroy(particle, particle.GetComponent<ParticleSystem>().main.duration + particle.GetComponent<ParticleSystem>().main.startLifetime.constantMax);
    }
}
