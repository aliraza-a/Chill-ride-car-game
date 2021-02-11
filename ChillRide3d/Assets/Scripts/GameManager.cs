using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource backgroundMusic;

    public GameObject Obsticle;
    public Transform obsticleSpawnPoint;
    public float maxSpawnPointX;

    public GameObject Tree;
    public Transform treeSpawnPoint;
    public float maxTreeSpawnPointLeft;
    public float minTreeSpawnPointLeft;
    public float maxTreeSpawnPointRight;
    public float minTreeSpawnPointRight;

    public Text scoreText;
    public Text highscoreText;

    public GameObject startPanel;
    public GameObject gamePanel;

    int score = 0;
    int highscore = 0;

    bool gameStarted = false;

    public static GameManager instance;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("highscore"))
        {
            highscore = PlayerPrefs.GetInt("highscore");
            highscoreText.text = "high score: " + highscore.ToString();
        }
    }

    void Update()
    {
        if(Input.anyKeyDown && !gameStarted)
        {
            startPanel.gameObject.SetActive(false);
            gamePanel.gameObject.SetActive(true);

            StartCoroutine("SpawnObsticles");
            StartCoroutine("SpawnTrees");

            gameStarted = true;

            backgroundMusic.Play();
        }
    }

    IEnumerator SpawnObsticles()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            SpawnObsticle();
        }
    }

    IEnumerator SpawnTrees()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            SpawnTreeRight();
            SpawnTreeLeft();
        }
    }

    public void SpawnObsticle()
    {
        float randomObsticleSpawnX = Random.Range(-maxSpawnPointX, maxSpawnPointX);

        Vector3 obsticleSpawnPos = obsticleSpawnPoint.position;
        obsticleSpawnPos.x = randomObsticleSpawnX;

        Instantiate(Obsticle, obsticleSpawnPos, Quaternion.identity);
    }

    public void SpawnTreeLeft()
    {
        float randomTreeSpawnXLeft = Random.Range(minTreeSpawnPointLeft, maxTreeSpawnPointLeft);

        Vector3 treeSpawnPos = treeSpawnPoint.position;
        treeSpawnPos.x = randomTreeSpawnXLeft;

        Instantiate(Tree, treeSpawnPos, Quaternion.identity);
    }

    public void SpawnTreeRight()
    {
        float randomTreeSpawnXRight = Random.Range(minTreeSpawnPointRight, maxTreeSpawnPointRight);

        Vector3 treeSpawnPos = treeSpawnPoint.position;
        treeSpawnPos.x = randomTreeSpawnXRight;

        Instantiate(Tree, treeSpawnPos, Quaternion.identity);
    }

    public void Restart()
    {
        if(score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("highscore", highscore);
        }
        SceneManager.LoadScene(0);
    }

    public void ScoreUp()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
