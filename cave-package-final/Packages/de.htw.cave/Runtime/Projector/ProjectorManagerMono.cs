using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Htw.Cave.Math;
using Htw.Cave.Display;

namespace Htw.Cave.Projector
{
	[AddComponentMenu("Htw.Cave/Projector/Projector Manager Mono")]
    public class ProjectorManagerMono : ProjectorManager
    {
		public override DisplayOutput DisplayOutput { get { return DisplayOutput.Mono; } }

		public override void Render()
		{
			for(int i = base.data.Length - 1; i >= 0; --i)
			{
				base.data[i].RefreshPlane();

				Matrix4x4 holographic = Projection.Holographic(
					base.data[i].plane[3],
					base.data[i].plane[2],
					base.data[i].plane[0],
					base.head.EyeAnchor(),
					base.data[i].camera.nearClipPlane,
					base.data[i].camera.farClipPlane
				);

				base.data[i].camera.projectionMatrix = base.data[i].bimber * holographic;
			}
		}
    }
}
