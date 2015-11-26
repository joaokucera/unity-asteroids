using UnityEngine;

namespace Asteroids
{
	[AddComponentMenu("ASTEROIDS/Movement Controller")]
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(RendererBehaviour))]
	public class MovementController : MonoBehaviour
	{
		[Header("Movement Settings")]
		[Range(1f, 250f)] public float RotationSpeed;
		[Range(1f, 250f)] public float ThrustForce;

		private Rigidbody2D m_rigidbody;
		public Rigidbody2D Rigidbody
		{
			get
			{
				if (m_rigidbody == null) m_rigidbody = GetComponent<Rigidbody2D> ();

				return m_rigidbody;
			}
		}
		
		private RendererBehaviour m_rendererBehaviour;
		public RendererBehaviour RendererBehaviour
		{
			get
			{
				if (m_rendererBehaviour == null) m_rendererBehaviour = GetComponent<RendererBehaviour> ();
				
				return m_rendererBehaviour;
			}
		}

		public void DoRotation(float baseVelocity)
		{
			Rigidbody.angularVelocity = baseVelocity * RotationSpeed;
		}
		
		public void DoTorque(float torque)
		{
			Rigidbody.AddTorque (torque * RotationSpeed);
		}

		public void DoForce(Vector2 direction)
		{
			Rigidbody.AddForce (direction * ThrustForce);
		}

		public void DOVelocity(Vector2 baseVelocity, float multiplier)
		{
			Rigidbody.velocity = baseVelocity + Random.insideUnitCircle * multiplier;
		}
	}
}