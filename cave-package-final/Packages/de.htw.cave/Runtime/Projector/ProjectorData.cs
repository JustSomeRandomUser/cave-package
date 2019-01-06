using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Htw.Cave.Projector
{
    public class ProjectorData
    {
		public Camera camera;
		public Vector3[] plane;
		public Matrix4x4 bimber;
		public int order;

		private ProjectorEmitter projectorEmitter;
		private ProjectorPlane projectorPlane;
		private ProjectorConfiguration projectorConfiguration;

		public ProjectorData(ProjectorEmitter projectorEmitter, ProjectorPlane projectorPlane, ProjectorConfiguration projectorConfiguration)
		{
			this.projectorEmitter = projectorEmitter;
			this.projectorPlane = projectorPlane;
			this.projectorConfiguration = projectorConfiguration;

			RefreshAll();
		}

		public void RefreshAll()
		{
			RefreshCamera();
			RefreshPlane();
			RefreshBimber();
			RefreshOrder();
		}

		public void RefreshCamera()
		{
			this.camera = this.projectorEmitter.GetComponent<Camera>();
		}

		public void RefreshPlane()
		{
			this.plane = this.projectorPlane.Plane;
		}

		public void RefreshBimber()
		{
			this.bimber = this.projectorConfiguration.Bimber;
		}

		public void RefreshOrder()
		{
			this.order = this.projectorConfiguration.Order;
		}
    }
}
