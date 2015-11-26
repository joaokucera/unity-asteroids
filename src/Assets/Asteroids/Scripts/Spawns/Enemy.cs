using UnityEngine;
using System.Collections;

namespace Asteroids
{
	[AddComponentMenu("ASTEROIDS / Enemy")]
	public class Enemy : Obstacle 
	{
		private Vector3 m_moveDirection;

		void Start()
		{
			InvokeRepeating("DoShoot", 1f, 1f);
			
			ChangeDirection();
		}

		void Update ()
		{
			transform.position += m_moveDirection * 1f * Time.deltaTime;
		}

		private void DoShoot()
		{
			GlobalVariables.EnemyWeaponPooling.DoShoot (transform.position, transform.rotation);
		}

		private void ChangeDirection()
		{
			m_moveDirection = Random.insideUnitCircle.normalized;

			Invoke("ChangeDirection", Random.Range(1, 5));
		}

		#region implemented abstract members of Obstacle

		protected override void DoExplosion ()
		{
			GlobalVariables.ExplosionPooling.DoExplosion(transform.position, transform.rotation, "EnemyDeath");
		}

		public override void DoImpact ()
		{
			CancelInvoke ();

			UIGame.UpdateScore (5);
			
			gameObject.SetActive (false);
		}

		#endregion
	}
}