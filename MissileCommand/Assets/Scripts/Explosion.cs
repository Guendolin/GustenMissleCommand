using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    //TODO check distance instead of using collider, try with enemy missiles checking for explosions

    //TODO if not used remove delay
    private float delay = 0f;

    public float ExplosionRadius = 0f;

    [SerializeField]
    private float minExpolsionRadius = 0.05f;

    [SerializeField]
    private float maxExpolsionRadius = 0.3f;

    void Start()
    {
    }

    void Update()
    {
        //This feels a bit janky, rework to something simpler/non lerp
        //Debug.Log(ExplosionRadius);

        if (ExplosionRadius <= maxExpolsionRadius)
        {
            //Time is weird
            ExplosionRadius = Mathf.Lerp(minExpolsionRadius, maxExpolsionRadius, Time.deltaTime / 10f);
        }
        DestroyExplosion();

        //This only works if there's one explosion at a time
        if (!gameObject.activeInHierarchy)
        {
            ExplosionRadius = 0f;
        }
        
    }

    void DestroyExplosion()
    {

        //TODO When pool in place Return to pool instead
        //This destroys the explosion when animation is complete(with a delay if wanted)
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
    }
}
