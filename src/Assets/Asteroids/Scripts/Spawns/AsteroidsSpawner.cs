using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Asteroids
{
	[AddComponentMenu("ASTEROIDS/Asteroids Spawner")]
	public class AsteroidsSpawner : Pooling<Asteroid> 
	{
		private List<Asteroid> m_asteroidsCollection = new List<Asteroid> ();

		[Header("Asteroids Spawn Settings")]
		[Range(1, 10)] [SerializeField] private int m_minimumAsteroids;
		[Range(0.1f, 2f)] [SerializeField] private float m_additionalAsteroidsPerLevel;
		[Range(1, 5)] [SerializeField] private int m_asteroidPieces;

		[Header("Asteroids Sprites")]
		[SerializeField] private Sprite[] m_sprites;

		public IEnumerator SpawnCollection (int level, float radius)
		{
			var length = Mathf.RoundToInt(level * m_additionalAsteroidsPerLevel) + m_minimumAsteroids;

			for(int i = 0; i < length; i++)
			{
				Vector2 spawnPosition = Random.insideUnitCircle * radius;

				while(Physics.CheckSphere(spawnPosition, 2f))
				{
					spawnPosition = Random.insideUnitCircle * radius;

					yield return null;
				}

				var asteroid = GetObjectFromPool();

				asteroid.Setup(spawnPosition, AsteroidSize.Large, m_sprites[Random.Range (0, m_sprites.Length)]);
				asteroid.SetMovementWithRandomForce();

				m_asteroidsCollection.Add(asteroid);
			}
		}
		
		public void TrySpawnInPieces (Asteroid origin, AsteroidSize size)
		{
			if (size != AsteroidSize.Null)
			{
				var position = (Vector2)origin.transform.position;

				for(int i = 0; i < m_asteroidPieces; i++)
				{
					var asteroid = GetObjectFromPool();

					asteroid.Setup(position + new Vector2(i * 1f, 0), size, m_sprites[Random.Range (0, m_sprites.Length)]);
					asteroid.SetMovementWithVelocity(origin.MovementController.Rigidbody.velocity, 2f);
					
					m_asteroidsCollection.Add(asteroid);
				}
			}

			m_asteroidsCollection.Remove (origin);

			SetObjectToPool (origin);
		}

		public bool CheckAllDestroyed ()
		{
			var allDestroyed = m_asteroidsCollection.Count == 0 || m_asteroidsCollection.Count(a => a.gameObject.activeInHierarchy) == 0;

			if (allDestroyed) m_asteroidsCollection.Clear();

			return allDestroyed;
		}
	}
}