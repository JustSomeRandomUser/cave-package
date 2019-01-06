using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Htw.Cave.Kinect
{
	public static class KinectAddinPostBuild
	{
		private const string PackagesDirName = KinectAddinData.PackagesDirName;
		private const string PackageDirName = KinectAddinData.PackageDirName;
		private const string KinectDirName = KinectAddinData.KinectDirName;
		private const string PluginsDirName = KinectAddinData.PluginsDirName;

	    [PostProcessBuild(1000)]
	    public static void OnPostProcessBuild(BuildTarget target, string path)
	    {
			// moving kinect dlls from assets space back to package space
			// after they where exported in the building process.
			var separator = Path.DirectorySeparatorChar;
			var packageDir = Path.Combine(Application.dataPath, ".." + separator + PackagesDirName + separator + PackageDirName);
			var kinectDir = Path.Combine(packageDir, KinectDirName);
			var targetDir = Application.dataPath + separator + PluginsDirName;

			KinectAddinData.Move(new DirectoryInfo(targetDir), new DirectoryInfo(kinectDir));
			KinectAddinData.Import();
		}
	}
}
