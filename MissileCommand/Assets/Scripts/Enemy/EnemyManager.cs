using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    public EnemyMissileLauncher enemyMissileLauncher;

    public GameObject missileTarget;

    public float MissileSpeed = 3f;


    [SerializeField]
    [Tooltip("Set the Y-position of the EnemyMissileLaunchers")]
    private float enemyMissileSpawnY = 6f;

    [SerializeField]
    [Tooltip("Lowest possible time between shots")]
    private const float minFireTime = 1f;

    [SerializeField]
    [Tooltip("Highest possible time between shots")]
    private const float maxFireTime = 3f;

    [SerializeField]
    [Range(minFireTime, maxFireTime)]
    [Tooltip("Initial time between shots")]
    private float fireTime = 2f;

    private float fireTimer = 0f;

    public int MissileCount = 10;

    //some logic for if the missiles should be splitting

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SetMissileLauncherPosition();
    }

    void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireTime && MissileCount > 1)
        {
            fireTimer = 0;
            SetMissileTarget();
            enemyMissileLauncher.FireMissile(missileTarget, enemyMissileLauncher.gameObject);
            SetMissileLauncherPosition();
            MissileCount--;
        }
    }

    void SetMissileTarget()
    {
        int targetBase = Random.Range(0, 8);
        missileTarget = GameManager.Instance.Enemytargets[targetBase];
    }

    void SetMissileLauncherPosition()
    {
        enemyMissileLauncher.transform.position = new Vector3(Random.Range(-10.25f, 10.25f), enemyMissileSpawnY);
        fireTime = Mathf.Clamp(Random.Range((fireTime * 0.5f), fireTime * 1.5f), 1f, 3f);
    }
}
