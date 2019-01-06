using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Htw.Cave.UI
{
	[AddComponentMenu("Htw.Cave/UI/UI Interrupt Menu")]
    public sealed class UIInterruptMenu : MonoBehaviour
    {
		public bool Shown { get; set; }

		public void Awake()
		{
			this.Shown = false;
		}

		public void Update()
		{
			if(Input.GetKey(KeyCode.Escape))
				if(this.Shown)
					Hide();
				else
					Show();
		}

		public void Show()
		{

		}

		public void Hide()
		{

		}

		public void AdjustDisplayCallback()
		{

		}

		public void QuitApplicationCallback()
		{
			Application.Quit();
		}
    }
}
