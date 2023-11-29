using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.VFX;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public PlayerMovement player;
    public TextMeshProUGUI levelCompleted;
    public TextMeshProUGUI level;

    public ParticleSystem vfx1;
    public ParticleSystem vfx2;
    public GameObject panel;
    public GameObject levelGrid;
    public GameObject gameOver;
    // public GameObject playerPrefab;


    public int amountOfFloor;
    // float startDelay = 0;
    //float intervalDelay = 2f;
    public bool isLevelCompleted;
    public bool isButtonClicked;
    bool ischecked;
    int index = 1;

    public Button[] button;

    // Start is called before the first frame update
    void Awake()
    {


        if (Instance != null)
        {

            Destroy(gameObject);

        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }


        isButtonClicked = false;

        level.text = "Level " + (SceneManager.GetActiveScene().buildIndex + 1).ToString();

        gameOver.gameObject.SetActive(false);

    }



    // Update is called once per frame
    void Update()
    {

        //Level Locekd-unlocked system
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < button.Length; i++)
        {
            button[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            button[i].interactable = true;
        }

        //Check If The Painteed Floor and The Amount Of The Floors Matched If So Display "Level Completed" UI...
        if (player.countPaintedFloor == amountOfFloor && isLevelCompleted == false)
        {
            levelCompleted.gameObject.SetActive(true);
            isLevelCompleted = true;
            
          



            Confetti();
            StartCoroutine(countDown());
            if (SceneManager.GetActiveScene().buildIndex == 9)
            {
                gameOver.gameObject.SetActive(true);
            }

            if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
            {
                PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
                PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
                PlayerPrefs.SetInt("Index", index);
                PlayerPrefs.Save();

            }




        }




    }



    void Confetti()
    {
        if (isLevelCompleted == true)
        {
            vfx1.Play();
            vfx2.Play();
        }

    }

    //Start The Game
    public void TapToCountinue()
    {
        panel.gameObject.SetActive(false);
        isButtonClicked = true;
    }


    public void HandleInputData(int val)
    {
        SceneManager.LoadScene(val);
        levelGrid.SetActive(false);
        if (val == 0)
        {
            PlayerPrefs.DeleteAll();
        }
        gameOver.gameObject.SetActive(false);

    }

    public void LoadLevelGrid()
    {
        levelGrid.gameObject.SetActive(true);
    }

    public void BackFromGrid()
    {
        levelGrid.gameObject.SetActive(false);
    }

    public void ResetLevels()
    {


        PlayerPrefs.DeleteAll();

    }









    IEnumerator countDown()
    {
        yield return new WaitForSeconds(1);




        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        level.text = "Level " + (SceneManager.GetActiveScene().buildIndex + 2).ToString();

        isLevelCompleted = false;
        panel.gameObject.SetActive(true);
        levelCompleted.gameObject.SetActive(false);
        isButtonClicked = false;
        gameOver.gameObject.SetActive(false);

    }



}
