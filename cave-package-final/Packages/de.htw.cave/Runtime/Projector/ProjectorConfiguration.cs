using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Htw.Cave.Math;
using Htw.Cave.Util;

//https://github.com/Unity-Technologies/Unity.Mathematics/blob/master/src/Unity.Mathematics/bool3x2.gen.cs
//https://docs.unity3d.com/Manual/ExecutionOrder.html
//https://github.com/prime31/MessageKit/blob/master/Assets/MessageKit/MessageKit.cs
//https://github.com/Unity-Technologies/UnityCsReference/blob/master/Runtime/Export/Display.bindings.cs

namespace Htw.Cave.Projector
{
	[CreateAssetMenu(fileName = "New Projector Configuration", menuName = "Htw.Cave/Projector Configuration", order = 20)]
    public class ProjectorConfiguration : ScriptableObject
    {
		[SerializeField]
		private int order;
		public int Order
		{
			get { return this.order; }
			set { this.order = value; }
		}

		[SerializeField]
		private float width;
		public float Width
		{
			get { return this.width; }
			set { this.width = value; }
		}

		[SerializeField]
		private float height;
		public float Height
		{
			get { return this.height; }
			set { this.height = value; }
		}

		[SerializeField]
		private float near;
		public float Near
		{
			get { return this.near; }
			set { this.near = value; }
		}

		[SerializeField]
		private float far;
		public float Far
		{
			get { return this.far; }
			set { this.far = value; }
		}

		[SerializeField]
		private float fieldOfView;
		public float FieldOfView
		{
			get { return this.fieldOfView; }
			set { this.fieldOfView = value; }
		}

		[SerializeField]
		private Vector4[] bimber;
		public Matrix4x4 Bimber
		{
			get { return new Matrix4x4(this.bimber[0], this.bimber[1], this.bimber[2], this.bimber[3]); }
			set { this.bimber = new Vector4[]{value.GetColumn(0), value.GetColumn(1), value.GetColumn(2), value.GetColumn(3)}; }
		}

		[SerializeField]
		private Vector2[] correctionPlane;
		public Vector2[] CorrectionPlane
		{
			get { return this.correctionPlane; }
			set { this.correctionPlane = value; }
		}

		public void RecomputeBimber()
		{
			Vector2[] points = this.correctionPlane;
			Vector2[] destination = new Vector2[points.Length];

			for(int i = destination.Length - 1; i >= 0; --i)
				destination[i] = points[i] * 2f - Vector2.one;

			float[,] homography = Homography.Find(Homography.Quad, destination);
			this.Bimber = Projection.Bimber(homography);
		}

		public PixelMask GeneratePixelMask()
		{
			return new PixelMask(this.correctionPlane);
		}
    }
}
