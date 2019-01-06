using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Htw.Cave.Projector
{
	[AddComponentMenu("Htw.Cave/Projector/Projector Plane")]
    public sealed class ProjectorPlane : MonoBehaviour
    {
		public float Width { get; set; }
		public float Height { get; set; }
		public Vector3[] Plane { get; private set; }

		public void Awake()
		{
		}

		public void Update()
		{
			if(transform.hasChanged)
				FindPlane();
		}

		public void Resize(float width, float height)
		{
			this.Width = width;
			this.Height = height;
			FindPlane();
		}

		public void FindPlane()
		{
			this.Plane = new Vector3[] {
				transform.TransformPoint(new Vector3(-this.Width * 0.5f, this.Height * 0.5f, 0f)),
				transform.TransformPoint(new Vector3(this.Width * 0.5f, this.Height * 0.5f, 0f)),
				transform.TransformPoint(new Vector3(this.Width * 0.5f, -this.Height * 0.5f, 0f)),
				transform.TransformPoint(new Vector3(-this.Width * 0.5f, -this.Height * 0.5f, 0f))
			};
		}
    }
}
