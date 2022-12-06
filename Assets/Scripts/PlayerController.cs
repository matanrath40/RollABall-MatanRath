using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject pickUpPrefab;
    public GameObject player;
    //public GameObject obstacle;
    public AudioClip pickUpSound;

    public float speed;
    public float jumpingSpeed;
    public Text countText;
    public Text winText;

    //public float spawnRadius = 5;
    //public float spawnCollisionCheckRadius;

    private Rigidbody rb;
    public int count;

    private PauseMenuScript pauseMenuScript;
    private GameOverMenu gameOverMenu;

    //private NewBehaviourScript

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
        //Spawner.SpawnObstacles();
        pauseMenuScript = gameObject.GetComponent<PauseMenuScript>();
        gameOverMenu = gameObject.GetComponent<GameOverMenu>();
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
            gameOverMenu.ShowMenu(count);
        }

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    gameOverMenu.ShowMenu();
        //}
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

    //private void SpawnObstacles()
    //{
    //    //for (int i = 0; i < 20; i++)
    //    //{
    //    //    Vector3 randomSpawnPosition = new Vector3(Random.Range(-9, 10), 0f, Random.Range(-9, 10));
    //    //    Instantiate(obstacle, randomSpawnPosition, Quaternion.Euler(0f, Random.Range(0, 360), 0f));

    //    //    //if (!Physics.CheckSphere(randomSpawnPosition, spawnCollisionCheckRadius))
    //    //    //{
    //    //    //    Instantiate(obstacle, randomSpawnPosition, Quaternion.Euler(0f, Random.Range(0, 360), 0f));
    //    //    //}
    //    //}

    //    Vector3 randomSpawnPosition = new Vector3(Random.Range(-9, 10), 0f, Random.Range(-9, 10));
    //    Instantiate(obstacle, randomSpawnPosition, Quaternion.Euler(0f, Random.Range(0, 360), 0f));

    //}

    //if (Input.GetKeyDown("space")) {

}
