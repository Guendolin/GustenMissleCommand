using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public CircleCollider2D ExplosionCollider;

    private Vector3 explosionScale;
    private Vector3 colliderScale;

    public float lerper;

    public Vector3 tempScale;

    //TODO if not used remove delay
    private float delay = 0f;

    public Projectile projectileRef;

    public GameObject clone = null;

    void Start()
    {
        
        ExplosionCollider = GetComponent<CircleCollider2D>();
        ExplosionCollider.radius = 0.05f;

        //clone = projectileRef.explosionClone;

        //Vector3 tempScale = clone.transform.localScale;
    }

    void Update()
    {
        //TODO Rework this, the radius growth is inconsistent 

        #region oldCode
        //-----------------------------------------------------------------------------------------
        //lerper = Mathf.Lerp(0.5f, 1.5f, 100);

        //Vector3 scaleChange;
        //scaleChange = new Vector3(-0.01f, -0.01f, -0.01f);

        //transform.localScale += scaleChange;

        //if (transform.localScale.x < 0.1f|| transform.localScale.x > 1.0f)
        //{
        //    scaleChange = -scaleChange;
        //}
        //-----------------------------------------------------------------------------------------
        ////Get the scale
        //explosionScale = gameObject.transform.localScale;
        ////multiply the collider radius with the scale
        //colliderScale = ExplosionCollider.radius * explosionScale;
        ////Set radius to the new value
        //ExplosionCollider.radius = colliderScale.x;

        //transform.localScale += explosionScale * 0.5f;
        //-----------------------------------------------------------------------------------------
        #endregion

        
        //tempScale.x += Time.time;
        //clone.transform.localScale = tempScale;

        DestroyExplosion();
    }

    void DestroyExplosion()
    {
        gameObject.transform.localScale = Vector3.one;
        ExplosionCollider.radius = 0.05f;
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
    }
}
