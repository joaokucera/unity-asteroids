using System.Collections;
using UnityEngine;

namespace Asteroids
{
	public abstract class Obstacle : MonoBehaviour 
	{
		void OnTriggerEnter2D(Collider2D collider)
		{
			if (collider.IsPlayerShot())
			{
				collider.gameObject.SetActive(false);

				DoExplosion();

				DoImpact();
			}
		}

		protected abstract void DoExplosion();

		public abstract void DoImpact();
	}
}