using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using JoyconAPI;

namespace Htw.Cave.Joycon
{
	[AddComponentMenu("Htw.Cave/Joycon/Joycon Helper")]
	[RequireComponent(typeof(JoyconManager))]
	public class JoyconHelper : MonoBehaviour
    {
		public static JoyconManager Manager { get; private set; }

		public static List<JoyconAPI.Joycon> GetJoycons()
		{
			return Manager.joycons;
		}

		public static JoyconAPI.Joycon GetLeftJoycon()
		{
			foreach(JoyconAPI.Joycon joycon in GetJoycons())
			{
				if(joycon.isLeft)
					return joycon;
			}

			return null;
		}

		public static JoyconAPI.Joycon GetRightJoycon()
		{
			foreach(JoyconAPI.Joycon joycon in GetJoycons())
			{
				if(!joycon.isLeft)
					return joycon;
			}

			return null;
		}

		public static int GetJoyconsCount()
		{
			return GetJoycons().Count;
		}

		public void Awake()
		{
			Manager = base.GetComponent<JoyconManager>();
		}
	}
}
