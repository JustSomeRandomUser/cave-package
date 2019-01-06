using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Htw.Cave.Projector
{
	[AddComponentMenu("Htw.Cave/Projector/Projector Head")]
    public sealed class ProjectorHead : MonoBehaviour
    {
		private Transform eyeLeft;
		private Transform eyeRight;

		public void Awake()
		{
			InitializeEyes();
		}

		public void SetEyeSeparation(float separation)
		{
			Vector3 s = new Vector3(separation * 0.5f, 0f, 0f);

			this.eyeLeft.position = transform.TransformPoint(-s);
			this.eyeRight.position = transform.TransformPoint(s);
		}

		public Vector3 EyeAnchor()
		{
			return transform.position;
		}

		public Vector3 EyeLeft()
		{
			return eyeLeft.position;
		}

		public Vector3 EyeRight()
		{
			return eyeRight.position;
		}

		private void InitializeEyes()
		{
			GameObject left = new GameObject("Left Eye");
			this.eyeLeft = left.GetComponent<Transform>();
			this.eyeLeft.parent = transform;

			GameObject right = new GameObject("Right Eye");
			this.eyeRight = right.GetComponent<Transform>();
			this.eyeRight.parent = transform;
		}
    }
}
