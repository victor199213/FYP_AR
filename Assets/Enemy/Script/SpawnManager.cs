using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemy2;
    public float spawnTime = 3f;           
    public Transform[] spawnPoints;        


    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        int spawnPointIndex2 = Random.Range(0, spawnPoints.Length);
        var parent = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        var parent2 = Instantiate(enemy2, spawnPoints[spawnPointIndex2].position, spawnPoints[spawnPointIndex2].rotation);
        parent.transform.parent = gameObject.transform;
    }
}
