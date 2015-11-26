using UnityEngine;
using System.Collections;

namespace Asteroids
{
	[AddComponentMenu("ASTEROIDS / Game Manager")]
	[RequireComponent(typeof(AsteroidsSpawner))]
	[RequireComponent(typeof(EnemiesSpawner))]
	[RequireComponent(typeof(UFOSpawner))]
	public class GameManager : Singleton<GameManager>
	{
		private AsteroidsSpawner m_asteroidsSpawner;
		private EnemiesSpawner m_enemiesSpawner;
		private UFOSpawner m_ufoSpawner;

		[Header("Spawn Settings")]
		[Range(1, 10)] [SerializeField] private float m_spawnRadius;
		[Range(1, 10)] [SerializeField] private int m_minimumAsteroids;
		[Range(0, 1)] [SerializeField] private float m_additionalAsteroidsPerLevel;
		[Range(1, 5)] [SerializeField] private int m_asteroidPieces;
		[Range(5, 30)] [SerializeField] private float m_waitTimeToSpawnEnemy;
		[Range(5, 30)] [SerializeField] private float m_waitTimeToSpawnUFO;

		void Start()
		{
			m_asteroidsSpawner = GetComponent<AsteroidsSpawner> ();
			m_enemiesSpawner = GetComponent<EnemiesSpawner> ();
			m_ufoSpawner = GetComponent<UFOSpawner> ();

			StartCoroutine(Initialize ());
		}

		void Update()
		{
			if (m_asteroidsSpawner.AreAllDestroyed ())
			{
				NextLevel();
			}
		}

		void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere (transform.position, m_spawnRadius);
		}

		public static void SpawnAfterExplosion(Asteroid origin, AsteroidSize size)
		{
			Instance.m_asteroidsSpawner.SpawnPieces (origin, size, Instance.m_asteroidPieces);
		}

		public static void OnGameOver()
		{
			Instance.CancelInvoke ();

			SoundManager.PlaySoundEffect ("GameOver");

			UIGame.ShowGameOver();
		}

		private void NextLevel ()
		{
			CancelInvoke ();

			GlobalVariables.Player.Data.Level++;
			UIGame.UpdateLevel ();

			m_waitTimeToSpawnEnemy--;
			m_waitTimeToSpawnEnemy = Mathf.Max (m_waitTimeToSpawnEnemy, 5);
			m_waitTimeToSpawnUFO--;
			m_waitTimeToSpawnUFO = Mathf.Max (m_waitTimeToSpawnUFO, 5);

			StartCoroutine(Initialize ());
		}

		private IEnumerator Initialize()
		{
			yield return new WaitForSeconds (2.5f);

			StartCoroutine(m_asteroidsSpawner.SpawnCollection (GlobalVariables.Player.Data.Level, m_additionalAsteroidsPerLevel, m_minimumAsteroids, m_spawnRadius));

			Invoke ("SpawnEnemy", m_waitTimeToSpawnEnemy);
			Invoke ("SpawnUFO", m_waitTimeToSpawnUFO);
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