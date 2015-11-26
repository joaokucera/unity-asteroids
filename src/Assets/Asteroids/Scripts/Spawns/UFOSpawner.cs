using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Asteroids
{
	[AddComponentMenu("ASTEROIDS / UFO Spawner")]
	public class UFOSpawner : Pooling<UFO> 
	{
		public void Spawn(float radius)
		{
			var ufo = GetObjectFromPool ();

			ufo.transform.position = Random.insideUnitCircle * radius;
		}
	}
}