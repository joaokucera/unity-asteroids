using UnityEngine;
using UnityEngine.UI;

namespace Asteroids
{
	[AddComponentMenu("ASTEROIDS / UI Menu")]
	public class UIMenu : Singleton<UIMenu> 
	{
		[Header("Menu UI Componenets")]
		[SerializeField] private Text m_highScoreText;

		void Start()
		{
			var highScore = PlayerPrefs.GetInt ("highscore");

			m_highScoreText.text = string.Format("HIGH SCORE: <color=#FFC000FF>{0}</color>", highScore);
		}
		
		public void Play()
		{
			SoundManager.PlaySoundEffect ("ButtonClick");

			Application.LoadLevel ("GameScene");
		}
	}
}