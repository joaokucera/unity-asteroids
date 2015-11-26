using System.Collections;
using UnityEngine;

namespace Asteroids
{
	[AddComponentMenu("ASTEROIDS/Enemy")]
	public class Enemy : Obstacle 
	{
		private const int ImpactPoints = 5;

		private Vector3 m_moveDirection;
		private float m_movementSpeed = 1f;
		private float m_minTimeToChangeDirection = 1f;
		private float m_maxTimeToChangeDirection = 5f;

		void Update ()
		{
			transform.position += m_moveDirection * m_movementSpeed * Time.deltaTime;
		}

		public void Setup (Vector2 position)
		{
			transform.position = position;

			InvokeRepeating("DoShoot", 1.5f, 1.5f);
			
			ChangeDirection();
		}

		private void DoShoot()
		{
			GlobalVariables.EnemyWeaponPooling.DoShoot (transform.position, transform.rotation);
		}

		private void ChangeDirection()
		{
			m_moveDirection = Random.insideUnitCircle.normalized;

			Invoke("ChangeDirection", Random.Range(m_minTimeToChangeDirection, m_maxTimeToChangeDirection));
		}

		#region implemented abstract members of Obstacle

		public override void DoImpact ()
		{
			CancelInvoke ();

			UIGame.UpdateScore (ImpactPoints);

			GameManager.SetEnemyToPool (this);
		}
		
		protected override void DoExplosion ()
		{
			GlobalVariables.ExplosionPooling.DoExplosion(transform.position, transform.rotation, "EnemyDeath");
		}

		#endregion
	}
}