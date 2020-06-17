using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{

    //TODO remove [SerializedField] from variables where not needed
    //Check spelling and Coding standards
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private Vector2 origin = Vector2.zero;

    [SerializeField]
    private Vector2 target = Vector2.zero;

    [SerializeField]
    private LineRenderer projetileLineRenderer;

    [SerializeField]
    private GameObject targetMarker;

    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private GameObject explosion;

    private bool isBeingFired = false;

    private bool isExploding = false;
  

    void Start()
    {
        origin = transform.position;
    }


    void Update()
    {
        
        OnMouseDown();

        Vector3 normalizedTarget = Vector3.Normalize(target - origin);

        if (isBeingFired)
        {
            projectile.transform.Translate(normalizedTarget * speed * Time.deltaTime);
            projetileLineRenderer.positionCount = 2;
            projetileLineRenderer.SetPosition(0, origin);
            projetileLineRenderer.SetPosition(1, projectile.transform.position);

            if (Vector3.Distance(projectile.transform.position, targetMarker.transform.position) < 0.1f)
            {
                projectile.SetActive(false);

                //TODO pool explosions
                Instantiate(explosion, targetMarker.transform.position, Quaternion.identity);

                targetMarker.SetActive(false);
                projetileLineRenderer.positionCount = 0;
                isBeingFired = false;
                
                //TODO Use the pool for projectiles
                //Put projectile back in pool
                
            }
        }




        void FireProjectile()
        {
            isBeingFired = true;
            targetMarker.SetActive(true);
            targetMarker.transform.position = target;

            projectile.SetActive(true);
            projectile.transform.position = origin;
        }

        void OnMouseDown()
        {
            //TODO move to relevant script (player/base)
            if (Input.GetMouseButtonDown(0))
            {
                //TODO ask Simon if this is a good/normal way to get this
                //Get our mouse position
                Vector3 mousePosition;


                //TODO cache camera instead of Camera.main cuz it's slow
                mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

                target = mousePosition;
                FireProjectile();
            }
        }
    }

}
