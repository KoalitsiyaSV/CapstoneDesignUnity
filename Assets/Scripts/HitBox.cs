using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] Collider2D hitbox;
    [SerializeField] protected string targetLayerName;

    protected virtual void Awake()
    {
        hitbox = GetComponent<Collider2D>();
    }

    protected void OnTriggerEnter2D(Collider2D colliion)
    {
        if(colliion.gameObject.layer == LayerMask.NameToLayer(targetLayerName))
        {
            OnHit(colliion);
        }
    }

    protected virtual void OnHit(Collider2D colliion)
    {

    }
}