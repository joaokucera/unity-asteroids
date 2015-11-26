using UnityEngine;
using System.Collections;
using System;

namespace Asteroids
{
	[RequireComponent(typeof(SpriteRenderer))]
	public abstract class RendererBehaviour : MonoBehaviour
	{
		private SpriteRenderer m_spriteRenderer;
		private SpriteRenderer SpriteRenderer
		{
			get 
			{
				if (m_spriteRenderer == null)
				{
					m_spriteRenderer = GetComponent<SpriteRenderer>();
				}

				return m_spriteRenderer;
			}
		}

		public abstract void OnBecameInvisible();

		public void TryChangeSprite(Sprite newSprite)
		{
			//if (SpriteRenderer.sprite.name == newSprite.name) return;

			SpriteRenderer.sprite = newSprite;
		}

		public IEnumerator Blink(Action callback)
		{
			SpriteRenderer.enabled = false;

			yield return new WaitForSeconds (1f);

			for (int i = 0; i < 10; i++)
			{
				SpriteRenderer.enabled = !SpriteRenderer.enabled;

				yield return new WaitForSeconds (.1f);
			}

			SpriteRenderer.enabled = false;

			callback ();
		}

		public void Show()
		{
			SpriteRenderer.enabled = true;
		}
	}
}