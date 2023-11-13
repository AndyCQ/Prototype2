using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public PlayerCode player;
    public int neededDucks = 3;
    public string nextLevel;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCode>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && player.ducksInLine == neededDucks){
            SceneManager.LoadScene(nextLevel);
        }
    }
}
