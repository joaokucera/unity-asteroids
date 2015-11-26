using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids
{
	[AddComponentMenu("ASTEROIDS / Movement Controller")]
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(RendererBehaviour))]
	public class MovementController : MonoBehaviour
	{
		[Header("Movement Settings")]
		[Range(0, 500)] public float RotationSpeed;
		[Range(0, 500)] public float ThrustForce;

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

		public void DoRotation(float direction)
		{
			Rigidbody.angularVelocity = direction * RotationSpeed;
		}
		
		public void DoTorque(float direction)
		{
			Rigidbody.AddTorque (direction * RotationSpeed);
		}

		public void DoForce(Vector2 direction)
		{
			Rigidbody.AddForce (direction * ThrustForce);
		}

		public void DOVelocity(Vector2 parentVelocity)
		{
			Rigidbody.velocity = parentVelocity + Random.insideUnitCircle * 2;
		}

		public void Stop(Action callback)
		{
			Rigidbody.velocity = Vector2.zero;

			StartCoroutine(RendererBehaviour.Blink(callback));
		}
	}
}