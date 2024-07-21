using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Brick : MonoBehaviour
{
    public int health;
    public TMP_Text healthText;

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        healthText.text = health.ToString();
    }
}
