using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using OneEuroFilterUnity;

namespace Htw.Cave.Math
{
    public class OneEuroFilter2
    {
		public float Freq { get; private set; }
		public float MinCutoff { get; private set; }
		public float Beta { get; private set; }
		public float DCutoff { get; private set; }

		private OneEuroFilter filterX;
		private OneEuroFilter filterY;

		public OneEuroFilter2(float freq, float minCutoff = 1f, float beta = 0f, float dCutoff = 1f)
		{
			this.Freq = freq;
			this.MinCutoff = minCutoff;
			this.Beta = beta;
			this.DCutoff = dCutoff;

			this.filterX = new OneEuroFilter(this.Freq, this.MinCutoff, this.Beta, this.DCutoff);
			this.filterY = new OneEuroFilter(this.Freq, this.MinCutoff, this.Beta, this.DCutoff);
		}

		public void Update(float freq, float minCutoff = 1f, float beta = 0f, float dCutoff = 1f)
		{
			this.Freq = freq;
			this.MinCutoff = minCutoff;
			this.Beta = beta;
			this.DCutoff = dCutoff;

			this.filterX.UpdateParams(this.Freq, this.MinCutoff, this.Beta, this.DCutoff);
			this.filterY.UpdateParams(this.Freq, this.MinCutoff, this.Beta, this.DCutoff);
		}

		public Vector2 Filter(Vector2 input, float timestamp = -1f)
		{
			return new Vector2(
				this.filterX.Filter(input.x, timestamp),
				this.filterY.Filter(input.y, timestamp)
			);
		}
    }

	public class OneEuroFilter3
	{
		public float Freq { get; private set; }
		public float MinCutoff { get; private set; }
		public float Beta { get; private set; }
		public float DCutoff { get; private set; }

		private OneEuroFilter filterX;
		private OneEuroFilter filterY;
		private OneEuroFilter filterZ;

		public OneEuroFilter3(float freq, float minCutoff = 1f, float beta = 0f, float dCutoff = 1f)
		{
			this.Freq = freq;
			this.MinCutoff = minCutoff;
			this.Beta = beta;
			this.DCutoff = dCutoff;

			this.filterX = new OneEuroFilter(this.Freq, this.MinCutoff, this.Beta, this.DCutoff);
			this.filterY = new OneEuroFilter(this.Freq, this.MinCutoff, this.Beta, this.DCutoff);
			this.filterZ = new OneEuroFilter(this.Freq, this.MinCutoff, this.Beta, this.DCutoff);
		}

		public void Update(float freq, float minCutoff = 1f, float beta = 0f, float dCutoff = 1f)
		{
			this.Freq = freq;
			this.MinCutoff = minCutoff;
			this.Beta = beta;
			this.DCutoff = dCutoff;

			this.filterX.UpdateParams(this.Freq, this.MinCutoff, this.Beta, this.DCutoff);
			this.filterY.UpdateParams(this.Freq, this.MinCutoff, this.Beta, this.DCutoff);
			this.filterZ.UpdateParams(this.Freq, this.MinCutoff, this.Beta, this.DCutoff);
		}

		public Vector3 Filter(Vector3 input, float timestamp = -1f)
		{
			return new Vector3(
				this.filterX.Filter(input.x, timestamp),
				this.filterY.Filter(input.y, timestamp),
				this.filterZ.Filter(input.z, timestamp)
			);
		}
	}

	public class OneEuroFilter4
	{
		public float Freq { get; private set; }
		public float MinCutoff { get; private set; }
		public float Beta { get; private set; }
		public float DCutoff { get; private set; }

		private OneEuroFilter filterX;
		private OneEuroFilter filterY;
		private OneEuroFilter filterZ;
		private OneEuroFilter filterW;

		public OneEuroFilter4(float freq, float minCutoff = 1f, float beta = 0f, float dCutoff = 1f)
		{
			this.Freq = freq;
			this.MinCutoff = minCutoff;
			this.Beta = beta;
			this.DCutoff = dCutoff;

			this.filterX = new OneEuroFilter(this.Freq, this.MinCutoff, this.Beta, this.DCutoff);
			this.filterY = new OneEuroFilter(this.Freq, this.MinCutoff, this.Beta, this.DCutoff);
			this.filterZ = new OneEuroFilter(this.Freq, this.MinCutoff, this.Beta, this.DCutoff);
			this.filterW = new OneEuroFilter(this.Freq, this.MinCutoff, this.Beta, this.DCutoff);
		}

		public void Update(float freq, float minCutoff = 1f, float beta = 0f, float dCutoff = 1f)
		{
			this.Freq = freq;
			this.MinCutoff = minCutoff;
			this.Beta = beta;
			this.DCutoff = dCutoff;

			this.filterX.UpdateParams(this.Freq, this.MinCutoff, this.Beta, this.DCutoff);
			this.filterY.UpdateParams(this.Freq, this.MinCutoff, this.Beta, this.DCutoff);
			this.filterZ.UpdateParams(this.Freq, this.MinCutoff, this.Beta, this.DCutoff);
			this.filterW.UpdateParams(this.Freq, this.MinCutoff, this.Beta, this.DCutoff);
		}

		public Vector4 Filter(Vector4 input, float timestamp = -1f)
		{
			return new Vector4(
				this.filterX.Filter(input.x, timestamp),
				this.filterY.Filter(input.y, timestamp),
				this.filterZ.Filter(input.z, timestamp),
				this.filterW.Filter(input.w, timestamp)
			);
		}

		public Quaternion FilterQuaternion(Quaternion input, float timestamp = -1f)
		{
			// credit goes to https://github.com/DarioMazzanti/OneEuroFilterUnity/blob/master/Assets/Scripts/OneEuroFilter.cs
			// Workaround that take into account that some input device sends
			// quaternion that represent only a half of all possible values.
			// this piece of code does not affect normal behaviour (when the
			// input use the full range of possible values).
			if (Vector4.SqrMagnitude(new Vector4(this.filterX.currValue, this.filterY.currValue, this.filterZ.currValue, this.filterW.currValue).normalized
			- new Vector4(input.x, input.y, input.z, input.w).normalized) > 2)
				return new Quaternion(
					this.filterX.Filter(-input.x, timestamp),
					this.filterY.Filter(-input.y, timestamp),
					this.filterZ.Filter(-input.z, timestamp),
					this.filterW.Filter(-input.w, timestamp)
				);

			return new Quaternion(
				this.filterX.Filter(input.x, timestamp),
				this.filterY.Filter(input.y, timestamp),
				this.filterZ.Filter(input.z, timestamp),
				this.filterW.Filter(input.w, timestamp)
			);
		}
	}
}
