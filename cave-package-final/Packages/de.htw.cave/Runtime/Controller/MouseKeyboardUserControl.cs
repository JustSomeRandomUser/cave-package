using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Htw.Cave.Controller
{
	[AddComponentMenu("Htw.Cave/Controller/Mouse and Keyboard Control")]
	[RequireComponent(typeof(GenericController))]
	public class MouseKeyboardUserControl : MonoBehaviour
    {
		private GenericController controller;

		public void Awake()
		{
			this.controller = base.GetComponent<GenericController>();
		}

		public void Update()
		{
			float strafe = Input.GetAxis("Horizontal");
			float forward = Input.GetAxis("Vertical");
			float mouseX = Input.GetAxis("Mouse X");

			this.controller.Move(strafe, forward);
			this.controller.Rotate(mouseX);
		}
	}
}
