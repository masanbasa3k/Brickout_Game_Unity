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

    private void Awake()
    {
        healthText.text = health.ToString();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(destroySound, transform.position, 1f);
            if (gameManager != null)
            {
                gameManager.brickCount--;
                gameManager.InstantiateParticle(transform.position);
            } 
            Destroy(gameObject);
        }
        healthText.text = health.ToString();
    }
}
