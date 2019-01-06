using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Htw.Cave.Projector
{
	[AddComponentMenu("Htw.Cave/Projector/Projector Gizmos")]
	[ExecuteInEditMode]
	[RequireComponent(typeof(ProjectorEmitter))]
    public sealed class ProjectorGizmos : MonoBehaviour
    {
		public static bool ShowViewport { get; set; }
		public static bool ShowPlane { get; set; }

		[SerializeField]
		private bool showViewport;

		[SerializeField]
		private bool showPlane;

		private ProjectorEmitter emitter;

		public void Awake()
		{
#if UNITY_EDITOR
			this.emitter = base.GetComponent<ProjectorEmitter>();
#else
			Destroy(this);
#endif
		}

#if UNITY_EDITOR
		public void OnValidate()
		{
			ShowViewport = this.showViewport;
			ShowPlane = this.showPlane;
		}
#endif

#if UNITY_EDITOR
		public void OnDrawGizmos()
		{
			if(this.emitter.Plane == null)
				return;

			this.emitter.ResizePlane();

			this.showViewport = ShowViewport;
			this.showPlane = ShowPlane;

			if(this.showViewport)
			{
				Gizmos.color = Color.yellow;
				Gizmos.DrawLine(this.emitter.transform.position, this.emitter.Plane.Plane[0]);
				Gizmos.DrawLine(this.emitter.transform.position, this.emitter.Plane.Plane[1]);
				Gizmos.DrawLine(this.emitter.transform.position, this.emitter.Plane.Plane[2]);
				Gizmos.DrawLine(this.emitter.transform.position, this.emitter.Plane.Plane[3]);
			}


			Gizmos.color = Color.yellow;
			Gizmos.DrawLine(this.emitter.Plane.Plane[0], this.emitter.Plane.Plane[1]);
			Gizmos.DrawLine(this.emitter.Plane.Plane[1], this.emitter.Plane.Plane[2]);
			Gizmos.DrawLine(this.emitter.Plane.Plane[2], this.emitter.Plane.Plane[3]);
			Gizmos.DrawLine(this.emitter.Plane.Plane[3], this.emitter.Plane.Plane[0]);

			if(this.showPlane)
			{
				Color softYellow = Color.yellow;
				softYellow.a = 0.2f;
				Gizmos.color = softYellow;
				Gizmos.DrawLine(this.emitter.Plane.Plane[0], this.emitter.Plane.Plane[2]);
				Gizmos.DrawLine(this.emitter.Plane.Plane[1], this.emitter.Plane.Plane[3]);
			}
		}
#endif
    }
}
