using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject pickUpPrefab;
    public GameObject player;
    public AudioClip pickUpSound;

    public float speed;
    public float jumpingSpeed;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    public int count;
    private static int topScore = 0;

    private PauseMenuScript pauseMenuScript;

    [SerializeField] GameObject gameOverMenu;
    public Text finalScoreText;
    public Text highScoreText;

    void Start()
    {
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
        //Spawner.SpawnObstacles();
        pauseMenuScript = gameObject.GetComponent<PauseMenuScript>();
        //gameOverMenu = gameObject.GetComponent<GameOverMenu>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        rb.AddForce(movement * speed);


    }

    private void Update()
    {

        Vector3 ballJump = new Vector3(0, 10f, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(ballJump * jumpingSpeed);
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            pauseMenuScript.Pause();
        }

        if (player.transform.position.y < 0)
        {
            Debug.Log("Ball has fallen");
            ShowGameOverMenu();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (count > topScore)
            {
                topScore = count;
            }
            print($"Top Score: {topScore}");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            print("The reset button is working");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            Debug.Log("ball hit pick up");

            other.gameObject.SetActive(false);
            count++;
            SetCountText();
            AudioSource.PlayClipAtPoint(pickUpSound, transform.position);

            Vector3 randomSpawnPosition = new Vector3(Random.Range(-9, 10), 1, Random.Range(-9, 10));
            Instantiate(pickUpPrefab, randomSpawnPosition, Quaternion.identity);


        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("ball hit obstacle");
            rb.AddForce(new Vector3(0, 14f, 0) * jumpingSpeed);
        }
    }

    void SetCountText()
    {
        countText.text = "Count:" + count.ToString();
        if (count >= 12)
        {
            winText.text = "You Win!";
        }
    }

    public void ShowGameOverMenu()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;

        if (count > topScore)
        {
            topScore = count;
        }

        SetGameOverMenuTexts();
    }

    void SetGameOverMenuTexts()
    {
        finalScoreText.text = $"{count} POINTS";
        highScoreText.text = $"HIGH SCORE: {topScore} POINTS";
    }

    public void RestartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        print("The reset button is working");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

}
