using UnityEngine;
using UnityEditor;
using PixelCrushers.DialogueSystem.Examples;

namespace PixelCrushers.DialogueSystem.Editors {

	/// <summary>
	/// This class defines menu items for the example scripts in the Dialogue System menu.
	/// </summary>
	static public class ExampleMenuItems {
		
		[MenuItem("Window/Dialogue System/Component/Example/Usable", false, 300)]
		public static void AddComponentUsable() {
			DialogueSystemMenuItems.AddComponentToSelection<Usable>();
		}
		
		[MenuItem("Window/Dialogue System/Component/Example/Selector", false, 301)]
		public static void AddComponentSelector() {
			DialogueSystemMenuItems.AddComponentToSelection<Selector>();
		}
		
		[MenuItem("Window/Dialogue System/Component/Example/Proximity Selector", false, 302)]
		public static void AddComponentProximitySelector() {
			DialogueSystemMenuItems.AddComponentToSelection<ProximitySelector>();
		}
		
		[MenuItem("Window/Dialogue System/Component/Example/Always Face Camera", false, 303)]
		public static void AddComponentAlwaysFaceCamera() {
			DialogueSystemMenuItems.AddComponentToSelection<AlwaysFaceCamera>();
		}
		
		[MenuItem("Window/Dialogue System/Component/Example/Simple Controller", false, 304)]
		public static void AddComponentSimpleController() {
			DialogueSystemMenuItems.AddComponentToSelection<SimpleController>();
		}
		
		[MenuItem("Window/Dialogue System/Component/Example/Navigate On Mouse Click", false, 305)]
		public static void AddComponentNavigateOnMouseClick() {
			DialogueSystemMenuItems.AddComponentToSelection<NavigateOnMouseClick>();
		}
		
		[MenuItem("Window/Dialogue System/Component/Example/Range Trigger", false, 304)]
		public static void AddComponentRangeTrigger() {
			DialogueSystemMenuItems.AddComponentToSelection<RangeTrigger>();
		}
		
		[MenuItem("Window/Dialogue System/Component/Example/Unity Community/Smooth Camera With Bumper", false, 353)]
		public static void AddComponentSmoothCameraWithBumper() {
			DialogueSystemMenuItems.AddComponentToSelection<SmoothCameraWithBumper>();
		}
		
	}
		
}
