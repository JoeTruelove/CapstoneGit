using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    public PlayerController player;
    public GameObject gameOver;

    public List<GameObject> allZombies = new List<GameObject>();

    private void Start()
    {
        foreach(GameObject gameObject in GameObject.FindGameObjectsWithTag("Zombie"))
        {
            allZombies.Add(gameObject);
        }
    }

    private void Update()
    {
        if(player.health <= 0)
        {
            gameOver.SetActive(true);
        }
        if(allZombies.Count == 0)
        {
            gameOver.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            RestartGame();
        }
    }

    public void ZombieDied(GameObject go)
    {
        allZombies.Remove(go);
    }

    public void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
        //Application.LoadLevel(0);
    }

}
