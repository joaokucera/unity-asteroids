using System.Collections;
using UnityEngine;

namespace Asteroids
{
	[AddComponentMenu("ASTEROIDS/Game Manager")]
	[RequireComponent(typeof(AsteroidsSpawner))]
	[RequireComponent(typeof(EnemiesSpawner))]
	[RequireComponent(typeof(UFOSpawner))]
	public class GameManager : Singleton<GameManager>
	{
		private const float MinWaitTime = 5f;

		private AsteroidsSpawner m_asteroidsSpawner;
		private EnemiesSpawner m_enemiesSpawner;
		private UFOSpawner m_ufoSpawner;

		[Range(0f, 2.5f)] [SerializeField] private float m_waitTimeToInitializeALevel;

		[Header("General Spawn Settings")]
		[Range(1f, 10f)] [SerializeField] private float m_spawnRadius;
		[Range(MinWaitTime, 30f)] [SerializeField] private float m_waitTimeToSpawnEnemy;
		[Range(MinWaitTime, 30f)] [SerializeField] private float m_waitTimeToSpawnUFO;

		void Start()
		{
			m_asteroidsSpawner = GetComponent<AsteroidsSpawner> ();
			m_enemiesSpawner = GetComponent<EnemiesSpawner> ();
			m_ufoSpawner = GetComponent<UFOSpawner> ();

			StartCoroutine(Initialize ());
		}

		public static void SetEnemyToPool(Enemy enemy)
		{
			Instance.m_enemiesSpawner.SetObjectToPool (enemy);
		}

		public static void SetUFOToPool(UFO ufo)
		{
			Instance.m_ufoSpawner.SetObjectToPool (ufo);
		}

		public static void CheckAsteroids(Asteroid origin, AsteroidSize size)
		{
			Instance.m_asteroidsSpawner.TrySpawnInPieces (origin, size);

			if (Instance.m_asteroidsSpawner.CheckAllDestroyed())
			{
				Instance.NextLevel();
			}
		}
		
		public static void OnGameOver()
		{
			Instance.CancelInvoke ();

			UIGame.ShowGameOver();
		}

		private IEnumerator Initialize()
		{
			yield return new WaitForSeconds (m_waitTimeToInitializeALevel);

			StartCoroutine(m_asteroidsSpawner.SpawnCollection (GlobalVariables.Player.Data.Level, m_spawnRadius));

			Invoke ("SpawnEnemy", m_waitTimeToSpawnEnemy);
			Invoke ("SpawnUFO", m_waitTimeToSpawnUFO);
		}

		private void NextLevel()
		{
			CancelInvoke ();
			
			GlobalVariables.Player.Data.NextLevel ();
			
			m_waitTimeToSpawnEnemy--;
			m_waitTimeToSpawnEnemy = Mathf.Max (m_waitTimeToSpawnEnemy, MinWaitTime);
			m_waitTimeToSpawnUFO--;
			m_waitTimeToSpawnUFO = Mathf.Max (m_waitTimeToSpawnUFO, MinWaitTime);
			
			StartCoroutine(Initialize ());
		}

		private void SpawnEnemy()
		{
			m_enemiesSpawner.Spawn (m_spawnRadius);

			Invoke ("SpawnEnemy", m_waitTimeToSpawnEnemy);
		}

		private void SpawnUFO()
		{
			m_ufoSpawner.Spawn (m_spawnRadius);

			Invoke ("SpawnUFO", m_waitTimeToSpawnUFO);
		}
	}
}