using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemy2;
    public float spawnTime = 5f;           
    public Transform[] spawnPoints;
    public int totalWave;
    public float waveDownTime;
    public float waveDuration;
    private float tmpWaveDuration;
    private float tmpWaveDownTime;
    private int waveCounter;
    private bool waveSwitch;
    private float TmpSpawnTimer;

    public GameObject winPopup;
    public GameObject core;
    [HideInInspector]
    public bool gameWin;

    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
 
        //InvokeRepeating("Spawn", spawnTime, spawnTime);
        tmpWaveDownTime = waveDownTime;
        tmpWaveDuration = waveDuration;
        waveCounter = 1;
        waveSwitch = true;
        TmpSpawnTimer = 0;
        gameWin = false;
    }

    private void Update()
    {
        Spawn();
    }

    void Spawn()
    {
        if (waveSwitch == true)
        {
            TmpSpawnTimer += Time.deltaTime * 1;
            if (TmpSpawnTimer >= spawnTime)
            {
                int spawnPointIndex = Random.Range(0, spawnPoints.Length);
                int spawnPointIndex2 = Random.Range(0, spawnPoints.Length);
                var parent = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                //var parent2 = Instantiate(enemy2, spawnPoints[spawnPointIndex2].position, spawnPoints[spawnPointIndex2].rotation);
                parent.transform.parent = gameObject.transform;
                //parent2.transform.parent = gameObject.transform;
                TmpSpawnTimer = 0;
            }

        }
        if(waveCounter <= totalWave)
        {
            waveDuration -= Time.deltaTime * 1;

            if(waveDuration <= 0)
            {
                waveDownTime -= Time.deltaTime * 1;

                if(waveDownTime <= tmpWaveDownTime)
                {
                    waveSwitch = false;
                }
                if(waveDownTime <= 0)
                {
                    waveSwitch = true;
                    waveCounter += 1;
                    waveDownTime = tmpWaveDownTime;
                    waveDuration = tmpWaveDuration;
                    spawnTime = spawnTime * (float)0.8;
                }
            }
        }
        if(waveCounter > totalWave)
        {
            if(waveSwitch == true)
            {
                if (core.GetComponent<Core>().gameLose == false)
                {
                    Instantiate(winPopup, new Vector3(0, 5.0f, 0), core.transform.rotation, core.transform);
                    gameWin = true;
                }
            }
            waveSwitch = false;
        }
        if (waveCounter == totalWave)
        {
            waveDownTime = 0;
        }
    }
}
