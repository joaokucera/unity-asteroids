﻿using UnityEngine;

namespace Asteroids
{
	[AddComponentMenu("ASTEROIDS / Enemy Weapon Pooling")]
	public class EnemyWeaponPooling : Pooling<MovementController>
	{
		[Header("Weapon Setttings")]
		[Range(0, 5)] [SerializeField] private float m_instantiationRadius = 2f;
		[Range(0, 5)] [SerializeField] private float m_accuracy = 1f;

		public void DoShoot (Vector2 position, Quaternion rotation)
		{
			GameObject target = GlobalVariables.Player.gameObject;
			
			if(target == null) return;
			
			Vector2 shotDirection = ((Vector2)target.transform.position - position).normalized;
			Vector2 modifiedShotDirection = (shotDirection + (Random.insideUnitCircle * (1f - m_accuracy))).normalized;

			var shot = GetObjectFromPool ();

			shot.transform.position = position + modifiedShotDirection * m_instantiationRadius;
			shot.transform.rotation = rotation;

			shot.DoForce (modifiedShotDirection);
		}
	}
}