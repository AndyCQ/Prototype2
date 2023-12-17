using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerCode player;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerCode>();
    }

    // Update is called once per frame
    void Update()
    {
        
        for (int i = 0; i < hearts.Length; i++ ){
            if(i < player.currHealth){
                hearts[i].sprite = fullHeart;
            }
            else{
                hearts[i].sprite = emptyHeart;
            }
            if(i < player.maxHealth){
                hearts[i].enabled = true;
            }
            else{
                hearts[i].enabled = false;
            }
        }
    }
}
