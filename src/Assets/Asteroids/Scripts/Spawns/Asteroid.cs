using UnityEngine;
using System.Collections;

namespace Asteroids
{
	public enum AsteroidSize
	{
		Null = 0,
		Large = 1,
		Medium = 2,
		Small = 3
	}

	[AddComponentMenu("ASTEROIDS/Asteroid")]
	[RequireComponent(typeof(MovementController))]
	public class Asteroid : Obstacle
	{
		private AsteroidSize m_size;
		
		private MovementController m_movementController;
		public MovementController MovementController
		{
			get
			{
				if (m_movementController == null) m_movementController = GetComponent<MovementController> ();
				
				return m_movementController;
			}
		}

		public void Setup(Vector2 position, AsteroidSize size, Sprite newSprite)
		{
			transform.position = position;

			SetSize (size);

			MovementController.RendererBehaviour.TryChangeSprite (newSprite);
		}

		public void SetMovementWithRandomForce()
		{
			MovementController.DoForce(Random.insideUnitCircle);
			MovementController.DoTorque(Random.Range(-1, 1));
		}
		
		public void SetMovementWithVelocity (Vector2 velocity, float multiplier)
		{
			MovementController.DOVelocity (velocity, multiplier);
			MovementController.DoTorque(Random.Range(-1, 1));
		}

		private void SetSize(AsteroidSize size)
		{
			m_size = size;

			switch (m_size)
			{
				case AsteroidSize.Large:
					transform.localScale = Vector2.one * 2;
					break;
				case AsteroidSize.Medium:
					transform.localScale = Vector2.one * 1.5f;
					break;
				case AsteroidSize.Small:
					transform.localScale = Vector2.one;
					break;
			}
		}

		#region implemented abstract members of Obstacle

		public override void DoImpact()
		{
			UIGame.UpdateScore ((int)m_size);

			switch (m_size)
			{
				case AsteroidSize.Large:
					GameManager.CheckAsteroids(this, AsteroidSize.Medium);
					break;
				case AsteroidSize.Medium:
					GameManager.CheckAsteroids(this, AsteroidSize.Small);
					break;
				default:
					GameManager.CheckAsteroids(this, AsteroidSize.Null);
					break;
			}
		}
		
		protected override void DoExplosion ()
		{
			GlobalVariables.ExplosionPooling.DoExplosion(transform.position, transform.rotation, "AsteroidDeath");
		}

		#endregion
	}
}