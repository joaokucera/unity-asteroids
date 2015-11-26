using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Asteroids
{
	[AddComponentMenu("ASTEROIDS / Enemies Spawner")]
	public class EnemiesSpawner : Pooling<Enemy> 
	{
		public void Spawn(float radius)
		{
			var enemy = GetObjectFromPool ();

			enemy.transform.position = Random.insideUnitCircle * radius;
		}
	}
}