using UnityEngine;

public class Monster : MonoBehaviour
{   
    public float speed = 4;
    public float startSpd = 4;
    public int currHealth;
    public int maxHealth = 4;
    public Rigidbody2D Mrb;
    public bool moving = true;

    void Start(){
        Mrb = GetComponent<Rigidbody2D>();
        speed = startSpd;
    }



}