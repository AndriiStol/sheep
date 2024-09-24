using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    public GameObject sheepPrefabSkin1; 
    public GameObject sheepPrefabSkin2; 
    public GameObject sheepPrefabSkin3; 

    public Transform spawnPoint;
    public Transform despawnPoint;
    public float spawnInterval = 3; 
    private int score = 0;
    private bool isSpawning = false; 
    public SheepControl[] sheepControl; 
    private Dictionary<GameObject, Animator> sheepAnimators = new Dictionary<GameObject, Animator>();


    private void Start()
    {
        StartSpawn();
    }

    private void StartSpawn()
    {
        isSpawning = true;
        InvokeRepeating("SpawnSheepWithSelectedSkin", 0f, spawnInterval);

    }

    private void SpawnSheepWithSelectedSkin()
    {
        GameObject newSheep = null;

        int selectedSkin = PlayerPrefs.GetInt("selectSkin1", 1);

        switch (selectedSkin)
        {
            case 1:
                newSheep = Instantiate(sheepPrefabSkin1, spawnPoint.position, Quaternion.identity);
                break;
            case 2:
                newSheep = Instantiate(sheepPrefabSkin2, spawnPoint.position, Quaternion.identity);
                break;
            case 3:
                newSheep = Instantiate(sheepPrefabSkin3, spawnPoint.position, Quaternion.identity);
                break;
            default:
                newSheep = Instantiate(sheepPrefabSkin1, spawnPoint.position, Quaternion.identity);
                break;
        }

       
        SheepDespawner despawner = newSheep.AddComponent<SheepDespawner>();
        despawner.Initialize(despawnPoint.position);

    
        Animator sheepAnimator = newSheep.GetComponent<Animator>();
        sheepAnimators[newSheep] = sheepAnimator;
    }


    private class SheepDespawner : MonoBehaviour
    {
        private Vector3 despawnPosition;

        public void Initialize(Vector3 position)
        {
            despawnPosition = position;
        }

        private void Update()
        {
            if (transform.position.x > despawnPosition.x)
            {
                Destroy(gameObject);
            }
        }
    }

   
    public void UpdateSpawnInterval()
    {
      
        if (score % 10 == 0)
        {

            foreach (SheepControl sheep in sheepControl)
            {
                sheep.sheepSpeed += 0.3f;
            }

           
            spawnInterval -= 0.2f;



            
            CancelInvoke("SpawnSheepWithSelectedSkin");
            InvokeRepeating("SpawnSheepWithSelectedSkin", 2f, spawnInterval); 
        }
    }


    public void IncreaseScore()
    {

        score++;
    }
}
