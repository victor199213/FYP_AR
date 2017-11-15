using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    //Define variable

    public GameObject enemy;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public float spawnTime;           
    public Transform[] spawnPoints;
    public int totalWave;
    public float waveDownTime;
    public float waveDuration;
    private float tmpWaveDuration;
    private float tmpWaveDownTime;
    private int waveCounter;
    private bool waveSwitch;
    private float TmpSpawnTimer;
    public float SmallDelay;
    private float tmpSmallDelay;
    public float SmallDelayDuration;
    private int spawnArea;
    public TextMesh timeLeftDisplay;
    public GameObject winPopup;
    public GameObject core;
    [HideInInspector]
    public bool gameWin;

    void Start()
    {
        //initializing variable
        InvokeRepeating("Spawn", spawnTime, spawnTime);
        tmpWaveDownTime = waveDownTime;
        tmpWaveDuration = waveDuration;
        waveCounter = 1;
        waveSwitch = true;
        TmpSpawnTimer = 0;
        gameWin = false;
        spawnArea = 1;
        tmpSmallDelay = SmallDelay;
    }

    private void Update()
    {
        //call spawn function
        Spawn();
    }

    void Spawn()
    {
        if (waveSwitch == true)
        {
            //how it spawn and how much small delay inbetween wave 
            TmpSpawnTimer += Time.deltaTime * 1;
            SmallDelay -= Time.deltaTime * 1;


            if (TmpSpawnTimer >= spawnTime && SmallDelay >= 0)
            {
                //random spawn spawn area and location
                switch (spawnArea)
                {
                    case 1:
                        {
                            int spawnPointIndex = Random.Range(0, 3);
                            var parent = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                            parent.transform.parent = gameObject.transform;
                            int typeSpawn = Random.Range(1, 4);

                            if (SmallDelay <= 1 && SmallDelay >= 0)
                            {
                                // last seconds it spawn a special enemy  
                                switch (typeSpawn)
                                {
                                    case 1:
                                        {
                                            int spawnPointIndex2 = Random.Range(0, 3);
                                            var parent2 = Instantiate(enemy2, spawnPoints[spawnPointIndex2].position, spawnPoints[spawnPointIndex2].rotation);
                                            parent2.transform.parent = gameObject.transform;
                                            break;
                                        }
                                    case 2:
                                        {
                                            int spawnPointIndex3 = Random.Range(0, 3);
                                            var parent3 = Instantiate(enemy3, spawnPoints[spawnPointIndex3].position, spawnPoints[spawnPointIndex3].rotation);
                                            parent3.transform.parent = gameObject.transform;
                                            break;
                                        }
                                    case 3:
                                        {
                                            int spawnPointIndex4 = Random.Range(0, 3);
                                            var parent4 = Instantiate(enemy4, spawnPoints[spawnPointIndex4].position, spawnPoints[spawnPointIndex4].rotation);
                                            parent4.transform.parent = gameObject.transform;
                                            break;
                                        }
                                }
                            }
                            break;
                        }
                    case 2:
                        {

                            int spawnPointIndex = Random.Range(3, 6);
                            var parent = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                            parent.transform.parent = gameObject.transform;
                            int typeSpawn = Random.Range(1, 4);

                            if (SmallDelay <= 1 && SmallDelay >= 0)
                            {
                                // last seconds it spawn a special enemy  
                                switch (typeSpawn)
                                {
                                    case 1:
                                        {
                                            int spawnPointIndex2 = Random.Range(3, 6);
                                            var parent2 = Instantiate(enemy2, spawnPoints[spawnPointIndex2].position, spawnPoints[spawnPointIndex2].rotation);
                                            parent2.transform.parent = gameObject.transform;
                                            break;
                                        }
                                    case 2:
                                        {
                                            int spawnPointIndex3 = Random.Range(3, 6);
                                            var parent3 = Instantiate(enemy3, spawnPoints[spawnPointIndex3].position, spawnPoints[spawnPointIndex3].rotation);
                                            parent3.transform.parent = gameObject.transform;
                                            break;
                                        }
                                    case 3:
                                        {
                                            int spawnPointIndex4 = Random.Range(3, 6);
                                            var parent4 = Instantiate(enemy4, spawnPoints[spawnPointIndex4].position, spawnPoints[spawnPointIndex4].rotation);
                                            parent4.transform.parent = gameObject.transform;
                                            break;
                                        }
                                }
                            }
                            break;
                        }
                    case 3:
                        {
                            int spawnPointIndex = Random.Range(6, 9);
                            var parent = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                            parent.transform.parent = gameObject.transform;
                            int typeSpawn = Random.Range(1, 4);

                            if (SmallDelay <= 1 && SmallDelay >= 0)
                            {
                                // last seconds it spawn a special enemy  
                                switch (typeSpawn)
                                {
                                    case 1:
                                        {
                                            int spawnPointIndex2 = Random.Range(6, 9);
                                            var parent2 = Instantiate(enemy2, spawnPoints[spawnPointIndex2].position, spawnPoints[spawnPointIndex2].rotation);
                                            parent2.transform.parent = gameObject.transform;
                                            break;
                                        }
                                    case 2:
                                        {
                                            int spawnPointIndex3 = Random.Range(6, 9);
                                            var parent3 = Instantiate(enemy3, spawnPoints[spawnPointIndex3].position, spawnPoints[spawnPointIndex3].rotation);
                                            parent3.transform.parent = gameObject.transform;
                                            break;
                                        }
                                    case 3:
                                        {
                                            int spawnPointIndex4 = Random.Range(6, 9);
                                            var parent4 = Instantiate(enemy4, spawnPoints[spawnPointIndex4].position, spawnPoints[spawnPointIndex4].rotation);
                                            parent4.transform.parent = gameObject.transform;
                                        }
                                        break;
                                }
                            }
                            break;
                        }
                    case 4:
                        {
                            int spawnPointIndex = Random.Range(9, spawnPoints.Length);
                            var parent = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                            parent.transform.parent = gameObject.transform;
                            int typeSpawn = Random.Range(1, 4);


                            if (SmallDelay <= 1 && SmallDelay >= 0)
                            {
                                // last seconds it spawn a special enemy  
                                switch (typeSpawn)
                                {
                                    case 1:
                                        {
                                            int spawnPointIndex2 = Random.Range(9, spawnPoints.Length);
                                            var parent2 = Instantiate(enemy2, spawnPoints[spawnPointIndex2].position, spawnPoints[spawnPointIndex2].rotation);
                                            parent2.transform.parent = gameObject.transform;
                                            break;
                                        }
                                    case 2:
                                        {
                                            int spawnPointIndex3 = Random.Range(9, spawnPoints.Length);
                                            var parent3 = Instantiate(enemy3, spawnPoints[spawnPointIndex3].position, spawnPoints[spawnPointIndex3].rotation);
                                            parent3.transform.parent = gameObject.transform;
                                            break;
                                        }
                                    case 3:
                                        {
                                            int spawnPointIndex4 = Random.Range(9, spawnPoints.Length);
                                            var parent4 = Instantiate(enemy4, spawnPoints[spawnPointIndex4].position, spawnPoints[spawnPointIndex4].rotation);
                                            parent4.transform.parent = gameObject.transform;
                                        }
                                        break;
                                }
                            }
                            break;
                        }
                }
                TmpSpawnTimer = 0;
            }
            if (SmallDelay <= -SmallDelayDuration)
            {
                //random spawn area
                spawnArea = Random.Range(1, 5);
                SmallDelay = tmpSmallDelay;
            }
        }
        if(waveCounter <= totalWave)
        {
            //check total wave and wave duration of each wave
            waveDuration -= Time.deltaTime * 1;

            if(waveDuration <= 0)
            {
                waveDownTime -= Time.deltaTime * 1;
                timeLeftDisplay.text = waveDownTime.ToString("F0");
                timeLeftDisplay.color = Color.blue;
                if (waveDownTime <= tmpWaveDownTime)
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
            else if(waveDuration > 0)
            {
                float tmpWaveDuration = waveDuration;
                if (tmpWaveDuration <= 0)
                    tmpWaveDuration = 0;
                timeLeftDisplay.text = tmpWaveDuration.ToString("F0");
                timeLeftDisplay.color = Color.red;
            }
        }
        if(waveCounter > totalWave)
        {
            //check if player manage to win the game 
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
