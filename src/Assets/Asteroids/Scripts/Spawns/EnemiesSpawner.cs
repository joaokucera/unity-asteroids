using UnityEngine;

namespace Asteroids
{
	[AddComponentMenu("ASTEROIDS/Enemies Spawner")]
	public class EnemiesSpawner : Pooling<Enemy> 
	{
		public void Spawn(float radius)
		{
			var enemy = GetObjectFromPool ();

			enemy.Setup (Random.insideUnitCircle * radius);
		}
	}
}