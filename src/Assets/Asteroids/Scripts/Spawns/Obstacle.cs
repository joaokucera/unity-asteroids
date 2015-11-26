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
				GlobalVariables.Player.SetShotToPool(collider.GetComponent<MovementController>());

				DoExplosion();

				DoImpact();
			}
		}

		public abstract void DoImpact();
		
		protected abstract void DoExplosion();
	}
}