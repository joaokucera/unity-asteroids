using UnityEngine;

namespace Asteroids
{
	[RequireComponent(typeof(SpriteRenderer))]
	public abstract class RendererBehaviour : MonoBehaviour
	{
		private SpriteRenderer m_spriteRenderer;
		public SpriteRenderer SpriteRenderer
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
			if (SpriteRenderer.sprite.name == newSprite.name) return;

			SpriteRenderer.sprite = newSprite;
		}
	}
}