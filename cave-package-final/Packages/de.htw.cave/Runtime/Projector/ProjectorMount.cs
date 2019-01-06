using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Htw.Cave.Projector
{
	[AddComponentMenu("Htw.Cave/Projector/Projector Mount")]
    public sealed class ProjectorMount : MonoBehaviour
    {
		[SerializeField]
		private Transform snapOn;
		public Transform SnapOn
		{
			get { return this.snapOn; }
			set { this.snapOn = value; }
		}

		public void LateUpdate()
		{
			transform.position = this.snapOn.position;
		}
    }
}
