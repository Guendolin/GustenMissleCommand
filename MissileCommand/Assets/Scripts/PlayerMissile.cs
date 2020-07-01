using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private Vector2 origin = Vector2.zero;

    [SerializeField]
    private Vector2 target = Vector2.zero;

    [SerializeField]
    private LineRenderer projetileLineRenderer;

    [SerializeField]
    private GameObject explosion;

    [SerializeField]
    private GameObject targetMarker;

    [SerializeField]
    private GameObject projectile;

    //Ask Simon what's standard for public variables that shouldn't be edited
    //[NonSerialized] / [HideInInspector]

    //[NonSerialized]
    public bool isBeingFired = false;

    

    void Start()
    {
        origin = transform.position;

        projetileLineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        //var playerMissile = PlayerMissilePool.Instance.Get();

        Debug.Log(target + " " + origin);
        Debug.DrawLine(target, origin, Color.magenta);

        if (this.isBeingFired)
        {   
            this.projectile.transform.position = Vector3.MoveTowards(this.projectile.transform.position, this.targetMarker.transform.position, speed * Time.deltaTime);
            this.projetileLineRenderer.positionCount = 2;
            this.projetileLineRenderer.SetPosition(0, this.origin);
            this.projetileLineRenderer.SetPosition(1, this.projectile.transform.position);

            if ((this.targetMarker.transform.position - this.projectile.transform.position).sqrMagnitude < 0.1f)
            {
                this.projectile.SetActive(false);

                //TODO pool explosions
                Instantiate(explosion, this.targetMarker.transform.position, Quaternion.identity);

                this.targetMarker.SetActive(false);
                this.projetileLineRenderer.positionCount = 0;
                this.isBeingFired = false;

                PlayerMissilePool.Instance.ReturnToPool(this);
            }
        }
    }


    public void FireMissileInternal(Vector2 target, Vector2 origin)
    {
        this.origin = origin;
        this.target = target;
        this.isBeingFired = true;
        this.targetMarker.SetActive(true);
        this.targetMarker.transform.position = target;
        
        this.projectile.SetActive(true);
        this.projectile.transform.position = origin;
    }

    //TODO When pool and player scripts are up, just pass in the relevant variables
    //public void FireProjectile(Vector2 target)
    //{
    //    isBeingFired = true;
    //    targetMarker.SetActive(true);
    //    targetMarker.transform.position = target;

    //    projectile.SetActive(true);
    //    projectile.transform.position = origin;
    //}
}
