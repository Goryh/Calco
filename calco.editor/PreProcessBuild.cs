using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.Callbacks;
#if UNITY_EDITOR && UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif

namespace calco
{
	public class CppMathIncludePreProcessBuild : IPreprocessBuildWithReport
	{
		public int callbackOrder { get { return 0; } }
		public void OnPreprocessBuild(BuildReport report)
		{
			var packageInfo = UnityEditor.PackageManager.PackageInfo.FindForPackageName("com.flowers.calco");
			string sourcePackagePath = packageInfo.resolvedPath;

			if( report.summary.platform == BuildTarget.StandaloneWindows || report.summary.platform == BuildTarget.StandaloneWindows64 )
			{
				var createSolutionBuildSettings = EditorUserBuildSettings.GetPlatformSettings("Standalone", "CreateSolution");
				if( createSolutionBuildSettings == "true" )
				{
					var addlArgs = "--compiler-flags=\"/FI $(ProjectDir)Source\\il2cppOutput\\cppMath.h\"";
					addlArgs += " --emit-comments";
					System.Environment.SetEnvironmentVariable("IL2CPP_ADDITIONAL_ARGS", addlArgs);
				}
				else
				{
					var pchCppPath = Path.Combine(sourcePackagePath, "calco/pch-cpp.hpp").Replace('\\','/');
					var addlArgs = $"--additional-cpp=\"{pchCppPath}\"";
					PlayerSettings.SetAdditionalIl2CppArgs(addlArgs);
				}
			}
			else if( report.summary.platform == BuildTarget.StandaloneOSX )
			{
				var addlArgs = "--compiler-flags=\"-include$PROJECT_DIR/Il2CppOutputProject/Source/il2cppOutput/cppMath.h\"";
				PlayerSettings.SetAdditionalIl2CppArgs(addlArgs);
			}
			else if( report.summary.platform == BuildTarget.Android )
			{
				// the only way to properly include the cppMath.h file is to reference to its base location as on the temporal location it will be available with a delay so some files won't find it
				var pchCppPath = Path.Combine(sourcePackagePath, "calco/pch-cpp.hpp").Replace('\\','/');
				var addlArgs = $"--additional-cpp=\"{pchCppPath}\"";
				PlayerSettings.SetAdditionalIl2CppArgs(addlArgs);
			}
		}
	}

	public class CppMathIncludePostProcessBuild : IPostprocessBuildWithReport
	{
		public int callbackOrder { get { return 0; } }
		public void OnPostprocessBuild(BuildReport report)
		{
			System.Environment.SetEnvironmentVariable("IL2CPP_ADDITIONAL_ARGS", null);
			PlayerSettings.SetAdditionalIl2CppArgs("");
		}

#if UNITY_EDITOR && UNITY_IOS
		[PostProcessBuild]
		public static void CppMathIncludePostProcessBuildIos(BuildTarget buildTarget, string path)
		{
			if( buildTarget != BuildTarget.iOS )
				return;

			string destPath = path + "/Il2CppOutputProject/Source/il2cppOutput/";

			var packageInfo = UnityEditor.PackageManager.PackageInfo.FindForPackageName("com.flowers.calco");
			string sourcePath = packageInfo.resolvedPath;

			File.Copy(Path.Combine(sourcePath, "calco/pch-cpp.hpp")	.Replace('\\','/'), 	destPath + "pch-cpp.hpp", 	true);
			File.Copy(Path.Combine(sourcePath, "calco/cppMath.h")	.Replace('\\','/'), 	destPath + "cppMath.h", 	true);
			File.Copy(Path.Combine(sourcePath, "calco/vecILMath.h")	.Replace('\\','/'),		destPath + "vecILMath.h", 	true);
			File.Copy(Path.Combine(sourcePath, "calco/vecMath.h")	.Replace('\\','/'),		destPath + "vecMath.h", 	true);
			File.Copy(Path.Combine(sourcePath, "calco/vecMathNeon.h").Replace('\\','/'),	destPath + "vecMathNeon.h", true);
		}
#endif
	}
}
