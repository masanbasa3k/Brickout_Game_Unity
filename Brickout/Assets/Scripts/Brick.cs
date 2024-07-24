using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Brick : MonoBehaviour
{
    [SerializeField] private AudioClip destroySound;
    public int health;
    public TMP_Text healthText;
    public GameManager gameManager;

    private void Start()
    {
        healthText.text = health.ToString();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (health <= 0)
        {
            if (gameManager != null)
            {
                gameManager.brickCount--;
                gameManager.InstantiateParticle(transform.position);
            }
            AudioSource.PlayClipAtPoint(destroySound, transform.position, 0.1f);
            Destroy(gameObject);
        }
        healthText.text = health.ToString();
    }
}
