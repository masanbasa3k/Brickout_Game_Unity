using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public int level = 1;
    public int lives = 3;
    public TMP_Text levelText;
    public TMP_Text livesText;
    [Tooltip("The music")]
    [SerializeField] private AudioSource themeSong;

    [Tooltip("The brick")]
    public GameObject brickPrefab;
    public GameObject particlePrefab;
    public List<List<object>> level1 = new List<List<object>>(){
            new List<object> { 3, 11, 1.2f, 0.7f, new Vector2(-6f, 3.5f), 1 },
            new List<object> { 4, 11, 1.2f, 0.7f, new Vector2(-6f, 3.5f), 2 },
        };
    public int brickCount;
    private void Awake()
    {
        themeSong = GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
        LoadNextLevel();
    }

    void Update()
    {
        if (brickCount <= 0)
        {
            level++;
            LoadNextLevel();
        }
    }

    public void updateLives(int change)
    {
        lives += change;
        livesText.text = "Lives: " + lives;
    }

    public void LoadNextLevel()
    {
        themeSong.Play();
        SceneManager.LoadScene("Level" + level + "Scene");
        BrickCreator(level1[level-1]);
        levelText.text = "Level: " + level;
        livesText.text = "Lives: " + lives;
    }

    public void BrickCreator(List<object> level)
    {
        int rows = (int)level[0];
        int columns = (int)level[1];
        float xOffset = (float)level[2];
        float yOffset = (float)level[3];
        Vector2 startPosition = (Vector2)level[4];
        int health = (int)level[5];
        brickCount = rows * columns;
        
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector2 position = new Vector2(startPosition.x + col * xOffset, startPosition.y - row * yOffset);
                GameObject brick = Instantiate(brickPrefab, position, Quaternion.identity);
                brick.transform.parent = this.transform;
                brick.GetComponent<Brick>().health = health + row;
            }
        }
    }

    public void InstantiateParticle(Vector3 position)
    {
        GameObject particle = Instantiate(particlePrefab, position, Quaternion.identity);
        Destroy(particle, particle.GetComponent<ParticleSystem>().main.duration + particle.GetComponent<ParticleSystem>().main.startLifetime.constantMax);
    }
}
