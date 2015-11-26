using System.Collections;
using UnityEngine;

namespace Asteroids
{
	[AddComponentMenu("ASTEROIDS/Screen Shake")]
    public class ScreenShake : Singleton<ScreenShake>
    {
		private Camera m_mainCamera;

		[Header("Shake Settings")]
		[Range(0f, 1f)] [SerializeField] private float m_shakeDecay = 0.005f;
		[Range(0f, 1f)] [SerializeField] private float m_shakeCoefIntensity = 0.05f;
		[Range(0f, 1f)] [SerializeField] private float m_multiplier = 0.5f;
		
		void Start()
		{
			m_mainCamera = Camera.main;
		}

        public static void Shake()
        {
			Instance.StartCoroutine (Instance.UpdateShake ());
        }

		private IEnumerator UpdateShake()
        {
			var originPosition = m_mainCamera.transform.position;
			var originRotation = m_mainCamera.transform.rotation;

			var shakeIntensity = m_shakeCoefIntensity;

			while (shakeIntensity > 0)
            {
				m_mainCamera.transform.position = m_mainCamera.transform.position + Random.insideUnitSphere * shakeIntensity;

				m_mainCamera.transform.rotation = new Quaternion
                (
					originRotation.x + Random.Range(-shakeIntensity, shakeIntensity) * m_multiplier,
					originRotation.y + Random.Range(-shakeIntensity, shakeIntensity) * m_multiplier,
					originRotation.z + Random.Range(-shakeIntensity, shakeIntensity) * m_multiplier,
					originRotation.w + Random.Range(-shakeIntensity, shakeIntensity) * m_multiplier
                );

				shakeIntensity -= m_shakeDecay;

                yield return null;
            }

			m_mainCamera.transform.position = originPosition;
			m_mainCamera.transform.rotation = originRotation;
        }
    }
}