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
    public List<List<object>> level1 = new List<List<object>>(){
            new List<object> { 5, 11, 1.2f, 0.7f, new Vector2(-6f, 3.5f) },
            new List<object> { 5, 5, 1.5f, 1f, new Vector2(-6f, 3.5f) },
        };
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        LoadNextLevel();

        // need to add a way to load the next level
        // find the prick count and if it is 0 then load the next level
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("Level" + level + "Scene");
        BrickCreator(level1[level-1]);
    }

    public void BrickCreator(List<object> level)
    {
        int rows = (int)level[0];
        int columns = (int)level[1];
        float xOffset = (float)level[2];
        float yOffset = (float)level[3];
        Vector2 startPosition = (Vector2)level[4];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector2 position = new Vector2(startPosition.x + col * xOffset, startPosition.y - row * yOffset);
                GameObject brick = Instantiate(brickPrefab, position, Quaternion.identity);
                brick.transform.parent = this.transform;
            }
        }
    }

}
