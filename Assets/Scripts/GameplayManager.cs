namespace AFSInterview
{
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;

    public class GameplayManager : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private Vector2 boundsMin;
        [SerializeField] private Vector2 boundsMax;
        [SerializeField] private float enemySpawnRate;

        [Header("UI")] 
        [SerializeField] private GameObject enemiesCountText;
        [SerializeField] private GameObject scoreText;

        [SerializeField] private GameObject ground;

        private List<Enemy> enemies;
        private int score;

        [SerializeField] private ObjectPool enemyPool;
        [SerializeField] private ObjectPool towerPool;
        [SerializeField] private ObjectPool flurryTowerPool;

        private void Awake()
        {
            enemies = new List<Enemy>();
            InvokeRepeating(nameof(SpawnEnemy), enemySpawnRate, enemySpawnRate);
            enemyPool.CreatePool();
            towerPool.CreatePool();
            flurryTowerPool.CreatePool();

        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit, LayerMask.GetMask("Ground")))
                {
                    var spawnPosition = hit.point;
                    spawnPosition.y = ground.transform.position.y;

                    SpawnTower(spawnPosition, towerPool.GetFromPool<SimpleTower>());
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit, LayerMask.GetMask("Ground")))
                {
                    var spawnPosition = hit.point;
                    spawnPosition.y = ground.transform.position.y;

                    SpawnTower(spawnPosition, flurryTowerPool.GetFromPool<FlurryTower>());
                }
            }

            scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
            enemiesCountText.GetComponent<TextMeshProUGUI>().text = "Enemies: " + enemies.Count;
        }

        private void SpawnEnemy()
        {
            var position = new Vector3(Random.Range(boundsMin.x, boundsMax.x), ground.transform.position.y, Random.Range(boundsMin.y, boundsMax.y));

            var enemy = enemyPool.GetFromPool<Enemy>();
            enemy.transform.position = position;
            enemy.OnEnemyDied += Enemy_OnEnemyDied;
            enemy.Initialize(boundsMin, boundsMax);

            enemies.Add(enemy);
        }

        private void Enemy_OnEnemyDied(Enemy enemy)
        {
            enemies.Remove(enemy);
            score++;
        }

        private void SpawnTower(Vector3 position, Tower tower)
        {
            tower.transform.position = position;
            tower.Initialize(enemies);
        }
    }
}