using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public int level = 1;
    public int score;
    public int lives;

    [SerializeField]
    public GameObject brickPrefab;
    public int rows = 5;  // Satır sayısı
    public int columns = 10;  // Sütun sayısı
    public float xOffset = 1.1f;  // Brick'ler arasındaki yatay mesafe
    public float yOffset = 0.6f;  // Brick'ler arasındaki dikey mesafe
    public Vector2 startPosition = new Vector2(-5, 4);
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("Level" + level + "Scene");

        // if level 1 scene is loaded
        // create bricks
        // create 20 bricks
        // 5 rows and 4 columns
        // 1 unit space between bricks
        // 1 unit space between bricks and walls
        // 1 unit space between bricks and top wall

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector2 position = new Vector2(startPosition.x + col * xOffset, startPosition.y - row * yOffset);
                GameObject brick = Instantiate(brickPrefab, position, Quaternion.identity);
                brick.transform.parent = this.transform;
                // Burada brick'e can değeri veya diğer özellikleri atayabilirsiniz
            }
        }

    }
}
