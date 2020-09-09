using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField]
    private Camera camera;

    public PlayerBase[] playerBases;

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

    }

    void Update()
    {
        OnMouseDown();
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePositionScreen = Input.mousePosition;
            Vector3 mousePositionWorld = camera.ScreenToWorldPoint(mousePositionScreen);

            int currentBase = -1;

            float closetDistance = float.MaxValue;

            for (int i = 0; i < playerBases.Length; i++)
            {
                //break this out as a global variable
                PlayerBase pBase = playerBases[i];
                float distanceToPosition = Mathf.Abs(pBase.transform.position.x - mousePositionWorld.x);

                if (pBase.HasMissiles() && distanceToPosition < closetDistance)
                {
                    currentBase = i;
                    closetDistance = distanceToPosition;
                }
            }
            if (currentBase >= 0 && playerBases[currentBase].HasMissiles())
            {
                FireMissile(mousePositionWorld, playerBases[currentBase].playerMissileLauncher[playerBases[currentBase].currentLauncher].transform.position);
                playerBases[currentBase].playerMissileLauncher[playerBases[currentBase].currentLauncher].gameObject.SetActive(false);
                playerBases[currentBase].currentLauncher--;
            }
        }
    }
    public void FireMissile(Vector2 target, Vector2 origin)
    {
        var playerMissile = PlayerMissilePool.Instance.Get();

        playerMissile.FireMissileInternal(target, origin);
        playerMissile.gameObject.SetActive(true);
    }
}