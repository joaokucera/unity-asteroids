using UnityEngine;
using System.Collections;

namespace Asteroids
{
	[AddComponentMenu("ASTEROIDS/Background Screen")]
	public class BackgroundScreen : MonoBehaviour 
	{
		void Start()
		{
			var mainCamera = Camera.main;

			transform.localScale = new Vector3 (mainCamera.orthographicSize * mainCamera.aspect, mainCamera.orthographicSize, 1);
		}
	}
}