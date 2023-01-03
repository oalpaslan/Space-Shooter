using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private WaitForSeconds _waitForEnemy = new WaitForSeconds(1.75f);

    private bool _stopSpawning;
    private float _enemyCount;
    

    void Start()
    {
        
        StartCoroutine(SpawnRoutine());
    }

    private void Update()
    {
        _enemyCount = _enemyContainer.transform.childCount;
    }

    IEnumerator SpawnRoutine()
    {


        while (_stopSpawning == false )
        {
            if (_enemyCount < 8)
            {
                float randomX = Random.Range(-9.5f, 9.5f);
                Vector3 enemyPos = new Vector3(randomX, 7.5f, 0);
                Quaternion enemyRot = new Quaternion(0, 0, 0, 0);
                //We create a variable to be able to reference to the instantiated object later.
                GameObject newEnemy = Instantiate(_enemyPrefab, enemyPos, enemyRot);
                //We put the newObject into the EnemyContainer, so it will hold the enemy objects.
                newEnemy.transform.parent = _enemyContainer.transform;


                yield return _waitForEnemy;
            }
            else
                yield return _waitForEnemy;
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
