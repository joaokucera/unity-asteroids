using UnityEngine;

namespace Asteroids
{
	[AddComponentMenu("ASTEROIDS/UFO Spawner")]
	public class UFOSpawner : Pooling<UFO> 
	{
		public void Spawn(float radius)
		{
			var ufo = GetObjectFromPool ();

			ufo.Setup (Random.insideUnitCircle * radius);
		}
	}
}