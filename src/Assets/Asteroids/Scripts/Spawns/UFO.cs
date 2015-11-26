using UnityEngine;

namespace Asteroids
{
	[AddComponentMenu("ASTEROIDS / UFO")]
	public class UFO : Obstacle 
	{
		private Vector3 m_moveDirection;
		
		void Start()
		{
			m_moveDirection = Random.insideUnitCircle.normalized;
		}

		void Update ()
		{
			transform.position += m_moveDirection * 1.5f * Time.deltaTime;
		}

		#region implemented abstract members of Obstacle

		protected override void DoExplosion ()
		{
			GlobalVariables.ExplosionPooling.DoExplosion(transform.position, transform.rotation, "EnemyDeath");
		}

		public override void DoImpact ()
		{
			GlobalVariables.PowerUpPooling.DoPowerUp (transform.position, transform.rotation);

			UIGame.UpdateScore (10);

			gameObject.SetActive (false);
		}

		#endregion
	}
}