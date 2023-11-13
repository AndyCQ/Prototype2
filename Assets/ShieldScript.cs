using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    private void Start()
    {
        int layerIndex = LayerMask.NameToLayer("Shield");
        gameObject.layer = layerIndex;

        //int playerLayerIndex = LayerMask.NameToLayer("Player");
        int groundLayerIndex = LayerMask.NameToLayer("Ground");
        //Physics2D.IgnoreLayerCollision(gameObject.layer, playerLayerIndex);
        Physics2D.IgnoreLayerCollision(gameObject.layer, groundLayerIndex);
    }
}
