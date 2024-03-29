using UnityEngine;
using UnityEditor;

public class TakeScreenshot : MonoBehaviour {

	[MenuItem("Window/Infinity PBR/Screenshot %y")]
	static void TakeScreenshotNow(){
		if (!EditorPrefs.HasKey("ScreenshotCount"))
		{
			EditorPrefs.SetInt("ScreenshotCount", 0);
		}
		ScreenCapture.CaptureScreenshot("Screenshot_" + EditorPrefs.GetInt("ScreenshotCount") + ".png", 3);
		EditorPrefs.SetInt("ScreenshotCount", EditorPrefs.GetInt("ScreenshotCount") + 1);
	}
}
