using UnityEngine;

namespace Asteroids
{
	[AddComponentMenu("ASTEROIDS/UFO")]
	public class UFO : Obstacle 
	{
		private const int ImpactPoints = 10;

		private Vector3 m_moveDirection;
		private float m_movementSpeed = 1.5f;

		void Update ()
		{
			transform.position += m_moveDirection * m_movementSpeed * Time.deltaTime;
		}

		public void Setup (Vector2 position)
		{
			transform.position = position;

			m_moveDirection = Random.insideUnitCircle.normalized;
		}

		#region implemented abstract members of Obstacle

		public override void DoImpact ()
		{
			GlobalVariables.PowerUpPooling.DoPowerUp (transform.position, transform.rotation);

			UIGame.UpdateScore (ImpactPoints);

			GameManager.SetUFOToPool (this);
		}
		
		protected override void DoExplosion ()
		{
			GlobalVariables.ExplosionPooling.DoExplosion(transform.position, transform.rotation, "UFODeath");
		}

		#endregion
	}
}