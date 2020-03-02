using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public struct TransformInfo
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public TransformInfo(Transform transform)
    {
        position = transform.position;
        rotation = transform.rotation;
        scale = transform.localScale;
    }
}

public class SpecialWeapon : MonoBehaviour, IRangedWeapon
{
    public float radiusSpawnDistance;

    private GameObject cutsceneObject;
    private PlayableDirector weaponCutscene;
    private LockOnScript playerLockOn;
    private ArrayList enemies;
    private ArrayList originalEnemyTransforms;
    private GameObject enemyCutsceneLocation;
    private PauseManager pauseManager;

    private void Awake()
    {
        enemies = new ArrayList();
        originalEnemyTransforms = new ArrayList();
    }

    private void Start()
    {
        cutsceneObject = GameObject.FindGameObjectWithTag("RailGunCutscene");
        pauseManager = GameObject.FindGameObjectWithTag("PauseManager").GetComponent<PauseManager>();
        enemyCutsceneLocation = cutsceneObject.transform.GetChild(0).GetChild(0).gameObject;
        weaponCutscene = cutsceneObject.GetComponent<PlayableDirector>();
        playerLockOn = transform.root.GetChild(0).GetComponent<LockOnScript>();
    }

    public void Fire(Animator shooterAnimator)
    {
        enemies = playerLockOn.GetEnemiesInRange();
        if (enemies.Count > 0)
        {
            DisableEnemyMovement();
            StoreOriginalEnemyTransforms();
            MoveEnemyToCutsceneArea();
            PlayWeaponCutscene();
            PauseGameEnvironment();
        }
    }

    private void DisableEnemyMovement()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            GameObject enemy = (GameObject)enemies[i];
            if (enemy && enemy.GetComponent<EnemyHealth>().isAlive())
            {
                enemy.GetComponent<IAIController>().StopMovement();
            }
        }
    }

    private void PauseGameEnvironment()
    {
        pauseManager.EnablePausingVolume();
    }

    private void MoveEnemyToCutsceneArea()
    {
        int enemyCount = enemies.Count;
        float enemySpacing = 360 / enemyCount;

        for (int i = 0; i < enemies.Count; i++)
        {
            enemyCutsceneLocation.transform.rotation *= Quaternion.Euler(0f, enemySpacing, 0f);
            GameObject enemy = (GameObject)enemies[i];
            if (enemy && enemy.GetComponent<EnemyHealth>().isAlive())
            {
                enemy.transform.position = enemyCutsceneLocation.transform.position;
                enemy.transform.position += enemyCutsceneLocation.transform.forward * radiusSpawnDistance;
            }
        }
    }

    private void StoreOriginalEnemyTransforms()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy && enemy.GetComponent<EnemyHealth>().isAlive())
            {
                originalEnemyTransforms.Add(new TransformInfo(enemy.transform));
            }
        }
        SetTransformsInEnemyDetector();
    }

    private void PlayWeaponCutscene()
    {
        if (weaponCutscene)
            weaponCutscene.Play();
    }

    public void SetTransformsInEnemyDetector()
    {
        if (cutsceneObject)
        {
            GameObject enemyDetector = cutsceneObject.transform.GetChild(0).GetChild(1).GetChild(0).gameObject;
            enemyDetector.GetComponent<EnemyReturn>().SetEnemyOriginalPositions(originalEnemyTransforms);
        }
    }

    public void StopFiring()
    {

    }
}
