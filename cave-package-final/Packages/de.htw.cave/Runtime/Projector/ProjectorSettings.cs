using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Htw.Cave.Display;

namespace Htw.Cave.Projector
{
	[CreateAssetMenu(fileName = "New Projector Settings", menuName = "Htw.Cave/Projector Settings", order = 10)]
    public class ProjectorSettings : ScriptableObject
    {
		[SerializeField]
		private DisplayOutput output;
		public DisplayOutput Output
		{
			get { return output; }
			set { this.output = value; }
		}

		[SerializeField]
		private VSyncMode vSync;
		public VSyncMode VSync
		{
			get { return vSync; }
			set { this.vSync = value; }
		}

		[SerializeField]
		private ViewportAxis axis;
		public ViewportAxis Axis
		{
			get { return axis; }
			set { this.axis = value; }
		}

		[SerializeField]
		private float stereoSeparation;
		public float StereoSeparation
		{
			get { return stereoSeparation; }
			set { this.stereoSeparation = value; }
		}

		[SerializeField]
		private float stereoConvergence;
		public float StereoConvergence
		{
			get { return stereoConvergence; }
			set { this.stereoConvergence = value; }
		}

		[SerializeField]
		private bool useBimber;
		public bool UseBimber
		{
			get { return useBimber; }
			set { this.useBimber = value; }
		}

		[SerializeField]
		private bool useMask;
		public bool UseMask
		{
			get { return useMask; }
			set { this.useMask = value; }
		}
    }
}
