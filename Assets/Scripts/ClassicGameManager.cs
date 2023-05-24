using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Windows;

public class ClassicGameManager : MonoBehaviour
{
    [Header("Text")]
    public TMP_Text scoreText;
    public TMP_Text comboText;
    public TMP_Text missesText;

    public float maxComboDuration = 2f;

    [Header("Objects")]
    //public GameObject fruit;
    public Transform[] spawnBounds;

    public float minXVelocity = 0.5f;
    public float maxXVelocity = 3f;

    public float minYVelocity = 10f;
    public float maxYVelocity = 15f;

    private int score;
    private int comboCounter = 0;
    float comboTimer = 0f;

    private int misses;

    public static ClassicGameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

    }

    private void Start()
    {
        InvokeRepeating("SpawnObject", 1f, 1f);
    }

    private void Update()
    {
        comboTimer += Time.deltaTime;
        if (comboTimer > maxComboDuration)
        {
            FinishCombo();
        }
    }

    public void UpdateScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
        comboCounter++;
        
    }

    private void FinishCombo()
    {
        comboTimer = 0;

        if (comboCounter >= 3)
        {
            comboText.text = "+" +  comboCounter.ToString();
            UpdateScore(comboCounter);
        }
        comboCounter = 0;


    }

    public void SpawnObject()
    {
        float spawnX = Random.Range(spawnBounds[0].position.x, spawnBounds[1].position.x);

        Vector3 spawnPosition = new Vector3(spawnX, spawnBounds[0].position.y, spawnBounds[0].position.z);

        float xVelocity = Random.Range(minXVelocity, maxXVelocity);
        if (spawnX > (spawnBounds[0].position.x + spawnBounds[1].position.x)/2)
        {
            xVelocity *= -1;
        }
        float yVelocity = Random.Range(minYVelocity, maxYVelocity);
        //Debug.Log("X : " + xVelocity + "Y: " + yVelocity);

        GameObject fruit = RandomSelectorWeighted.Instance.GetRandomObject();

        GameObject fruitObject =  ObjectPooler.Instance.GetPooledItem(fruit);
        fruitObject.transform.position = spawnPosition;

        Rigidbody fruitRB = fruitObject.GetComponent<Rigidbody>();
        Vector3 fruitVelocity = new Vector3(xVelocity, yVelocity, 0);
        fruitRB.velocity = fruitVelocity;
        


    }

    public void DestroyOutBounds(GameObject gameObject)
    {
        gameObject.SetActive(false);
        gameObject.transform.position = Vector3.zero;
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;

        if (gameObject.CompareTag("Fruit"))
        {
            misses += 1;
            missesText.text = misses.ToString();
            if (misses >= 3)
            {
                Time.timeScale = 0;
            }
        }
    }

}
