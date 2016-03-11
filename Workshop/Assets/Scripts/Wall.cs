using UnityEngine;
using System.Collections;

namespace CompletedWorkshop
{
	public class Wall : MonoBehaviour
	{
		public Sprite damagedSprite;
		public int hp = 4;
		public AudioClip[] chopSounds;

		private SpriteRenderer spriteRenderer;

		void Awake()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
		}

		public void DamageWall(int dmg)
		{
			hp -= dmg;

			SoundManager.instance.RandomizeSfx(chopSounds);

			if(hp <= 0)
				gameObject.SetActive(false);
			else
				spriteRenderer.sprite = damagedSprite;
		}
	}
}
