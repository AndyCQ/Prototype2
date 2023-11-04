using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public float immunityTime = 1.5f; // Time in seconds for which the player is immune
    public float flashDuration = 0.1f;
    
    int layer1;
    int layer2;
    
    void Start(){
        layer1 = LayerMask.NameToLayer("Player");
        layer2 = LayerMask.NameToLayer("Enemy");

    }

    void Update(){
        if(PlayerCode.Instance.IsImmune){
            Physics2D.IgnoreLayerCollision(layer1, layer2, true);
        }
        else{Physics2D.IgnoreLayerCollision(layer1, layer2, false);}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerCode.Instance.IsImmune) return;
        else if (collision.CompareTag("EnemyBullet"))
        {
            Debug.Log(PlayerCode.Instance.IsImmune);
            PlayerCode.Instance.IsImmune = true;
            StartFlashing();
            Invoke("StopFlashing", immunityTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerCode.Instance.IsImmune) return;
        else if (collision.collider.CompareTag("Enemy"))
        {
            //Debug.Log(PlayerCode.Instance.IsImmune);
            PlayerCode.Instance.IsImmune = true;
            
            StartFlashing();
            Invoke("StopFlashing", immunityTime);
        }
    }




    private void StartFlashing()
    {
        InvokeRepeating("ToggleVisibility", 0, flashDuration);
    }

    private void StopFlashing()
    {
        PlayerCode.Instance.IsImmune = false;
        CancelInvoke("ToggleVisibility");
        PlayerCode.Instance.PlayerRenderer.enabled = true;  // Ensure the player is visible at the end
    }

    private void ToggleVisibility()
    {
        PlayerCode.Instance.PlayerRenderer.enabled = !PlayerCode.Instance.PlayerRenderer.enabled;
    }

}
