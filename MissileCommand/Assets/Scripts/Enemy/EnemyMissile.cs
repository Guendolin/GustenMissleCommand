using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    
    public float Speed = 5f;

    private GameObject origin;

    private GameObject target;

    private Vector2 originPosition;

    [SerializeField]
    private LineRenderer projetileLineRenderer;

    private bool isBeingFired = false;

    void Update()
    {
        if (isBeingFired)
        {
            Speed = EnemyManager.Instance.MissileSpeed;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime);
            projetileLineRenderer.positionCount = 2;
            projetileLineRenderer.SetPosition(0, originPosition);
            projetileLineRenderer.SetPosition(1, transform.position);

            if ((target.transform.position - transform.position).sqrMagnitude < 0.1f)
            {
                target.SetActive(false);
                ExplodeAndReturnToPool();
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
