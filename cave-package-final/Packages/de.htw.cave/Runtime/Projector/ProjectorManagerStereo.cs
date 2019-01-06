using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Htw.Cave.Math;
using Htw.Cave.Display;

namespace Htw.Cave.Projector
{
	[AddComponentMenu("Htw.Cave/Projector/Projector Manager Stereo")]
    public class ProjectorManagerStereo : ProjectorManager
    {
		public override DisplayOutput DisplayOutput { get { return DisplayOutput.Stereo; } }

		public override void Render()
		{
			for(int i = base.data.Length - 1; i >= 0; --i)
			{
				base.data[i].RefreshPlane();

				Matrix4x4 holographicLeft = Projection.Holographic(
					base.data[i].plane[3],
					base.data[i].plane[2],
					base.data[i].plane[0],
					base.head.EyeLeft(),
					base.data[i].camera.nearClipPlane,
					base.data[i].camera.farClipPlane
				);
				Matrix4x4 holographicRight = Projection.Holographic(
					base.data[i].plane[3],
					base.data[i].plane[2],
					base.data[i].plane[0],
					base.head.EyeRight(),
					base.data[i].camera.nearClipPlane,
					base.data[i].camera.farClipPlane
				);

				base.data[i].camera.SetStereoProjectionMatrix(Camera.StereoscopicEye.Left, base.data[i].bimber * holographicLeft);
				base.data[i].camera.SetStereoProjectionMatrix(Camera.StereoscopicEye.Right, base.data[i].bimber * holographicRight);
			}
		}
    }
}
