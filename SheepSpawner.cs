using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    public GameObject sheepPrefabSkin1; // Префаб овцы для скина 1.
    public GameObject sheepPrefabSkin2; // Префаб овцы для скина 2.
    public GameObject sheepPrefabSkin3; // Префаб овцы для скина 3.

    public Transform spawnPoint;
    public Transform despawnPoint;
    public float spawnInterval = 3; // Начальное значение интервала.
    private int score = 0;
    private bool isSpawning = false; // Флаг для отслеживания активности спавна.
    public SheepControl[] sheepControl; // Ссылка на объект с компонентом SheepControl.
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

        // Добавьте компонент SheepDespawner для овцы.
        SheepDespawner despawner = newSheep.AddComponent<SheepDespawner>();
        despawner.Initialize(despawnPoint.position);

        // Сохраните аниматор овцы в словаре.
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

    // Метод для обновления spawnInterval.
    public void UpdateSpawnInterval()
    {
        // Проверяем, делится ли счет на 10 без остатка.
        if (score % 10 == 0)
        {

            foreach (SheepControl sheep in sheepControl)
            {
                sheep.sheepSpeed += 0.3f;
            }

            // Уменьшаем spawnInterval на 0.1 каждые 10 очков.
            spawnInterval -= 0.2f;



            // Останавливаем вызов SpawnSheep и перезапускаем его с новым интервалом через задержку.
            CancelInvoke("SpawnSheepWithSelectedSkin");
            InvokeRepeating("SpawnSheepWithSelectedSkin", 2f, spawnInterval); // Добавляем небольшую задержку перед первым спавном. 
        }
    }

    // Метод для увеличения счета.
    public void IncreaseScore()
    {

        score++;
    }
}