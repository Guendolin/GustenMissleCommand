﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    //this is speed is set in the EnemyManager
    private float Speed = 1f;

    private GameObject origin;

    private GameObject target;

    private Vector2 originPosition;

    [SerializeField]
    private LineRenderer projetileLineRenderer;

    private bool isBeingFired = false;

    private void OnEnable()
    {
        GameManager.Instance.audioManager.PlayWithRandomPitch("Incoming", 0.6f, 1.4f);
    }

    void Update()
    {
        if (isBeingFired)
        {
            Speed =  GameManager.Instance.enemyManager.MissileSpeed;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime);
            projetileLineRenderer.positionCount = 2;
            projetileLineRenderer.SetPosition(0, originPosition);
            projetileLineRenderer.SetPosition(1, transform.position);

            if ((target.transform.position - transform.position).sqrMagnitude < 0.1f)
            {
                target.SetActive(false);
                ExplodeAndReturnToPool();
                if (GameManager.Instance.isTheGameLost())
                {
                    GameEvents.Instance.GameOverEvent();
                }
            }
        }
    }

    public void FireMissileInternal(GameObject target, GameObject origin)
    {
        this.origin = origin;
        this.target = target;
        isBeingFired = true;

        originPosition = origin.transform.position;

        transform.position = origin.transform.position;
    }

    public void ExplodeAndReturnToPool()
    {
        var enemyExplosion = EnemyExplosionPool.Instance.Get();
        enemyExplosion.transform.position = transform.position;
        enemyExplosion.gameObject.SetActive(true);

        projetileLineRenderer.positionCount = 0;
        isBeingFired = false;

        EnemyMissilePool.Instance.ReturnToPool(this);
    }
}
