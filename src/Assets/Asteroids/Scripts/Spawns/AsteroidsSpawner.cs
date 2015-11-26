using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Asteroids
{
	[AddComponentMenu("ASTEROIDS / Asteroids Spawner")]
	public class AsteroidsSpawner : Pooling<Asteroid> 
	{
		private List<Asteroid> m_asteroidsCollection = new List<Asteroid> ();
		private bool m_isReadyToPlay;

		[Header("Asteroids Sprites")]
		[SerializeField] private Sprite[] m_sprites;

		void OnGUI()
		{
			GUI.Label(new Rect(20, Screen.height - 40, 200, 100), "ASTEROIDS: " + m_asteroidsCollection.Count);
		}

		public IEnumerator SpawnCollection (int level, float additionalPerLevel, int minimum, float radius)
		{
			var length = Mathf.RoundToInt(level * additionalPerLevel) + minimum;

			for(int i = 0; i < length; i++)
			{
				Vector2 spawnPosition = Random.insideUnitCircle * radius;

				while(Physics.CheckSphere(spawnPosition, 1))
				{
					spawnPosition = Random.insideUnitCircle * radius;

					yield return null;
				}

				var asteroid = GetObjectFromPool();
				asteroid.Setup(spawnPosition, AsteroidSize.Large, m_sprites[Random.Range (0, m_sprites.Length)]);
				asteroid.SetMovement();

				m_asteroidsCollection.Add(asteroid);
			}

			m_isReadyToPlay = true;
		}
		
		public void SpawnPieces (Asteroid origin, AsteroidSize size, int pieces)
		{
			if (size != AsteroidSize.Null)
			{
				var position = (Vector2)origin.transform.position;

				for(int i = 0; i < pieces; i++)
				{
					var asteroid = GetObjectFromPool();
					asteroid.Setup(position + new Vector2(i * 1f, 0), size, m_sprites[Random.Range (0, m_sprites.Length)]);
					asteroid.SetMovement(origin.MovementController.Rigidbody.velocity);
					
					m_asteroidsCollection.Add(asteroid);
				}
			}

			m_asteroidsCollection.Remove (origin);

			origin.gameObject.SetActive (false);
		}

		public bool AreAllDestroyed ()
		{
			if (!m_isReadyToPlay) return false;

			var allDestroyed = m_asteroidsCollection.Count == 0 || m_asteroidsCollection.Count(a => a.gameObject.activeInHierarchy) == 0;

			if (allDestroyed) 
			{
				m_asteroidsCollection.Clear();

				m_isReadyToPlay = false;
			}

			return allDestroyed;
		}
	}
}