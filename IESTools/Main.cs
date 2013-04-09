using System;

namespace IESTools
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			if (args.Length == 1) {
				var path = args [0];
				if (System.IO.File.Exists (path)) {
					var parser = new IesParser (path);
					var ies = parser.Parse ();
					DisplayIesData (ies);
				} else {
					System.Console.WriteLine ();
					System.Console.WriteLine ("Not a valid IES file");
					System.Console.WriteLine ();
				}
			} else {
				Console.WriteLine ("Please specify a path to an IES file");
			}
		}

		static void DisplayIesData (IesData ies)
		{
			System.Console.WriteLine ("");
			
			System.Console.WriteLine ("Identifier: " + ies.identifier);
			System.Console.WriteLine ("Keywords: ");
			System.Console.WriteLine ();
			foreach (var keyword in ies.keywords) {
				System.Console.WriteLine (string.Format ("   {0,-12} {1}", keyword.Key, keyword.Value));
			}
			
			System.Console.WriteLine ();
			
			System.Console.WriteLine ("Lamp Count: " + ies.lampCount);
			System.Console.WriteLine ("Lumens: " + ies.lumensPerLamp);
			System.Console.WriteLine ("Multiplier: " + ies.candelaMultiplier);
			System.Console.WriteLine ("Vertical Angles: " + ies.verticalAnglesCount);
			System.Console.WriteLine ("Horizontal Angles: " + ies.horizontalAnglesCount);
			System.Console.WriteLine ("Photometry Type: " + ies.photometryType);
			System.Console.WriteLine ("Units: " + ies.units);
			System.Console.WriteLine ("Size: " + ies.sizeX + ", " + ies.sizeY + ", " + ies.sizeZ);
			System.Console.WriteLine ("BallastFactor: " + ies.ballastFactor);
			System.Console.WriteLine ("InputWatts: " + ies.inputWatts);
			
			System.Console.WriteLine ();
			
			System.Console.WriteLine ("Candela Values:");
			System.Console.WriteLine ();
			
			for (int i = 0; i < ies.horizontalAnglesCount; i++) {
				for (int j = 0; j < ies.verticalAnglesCount; j++) {
					System.Console.Write (string.Format ("{0,5} | ", ies.angleCandelas [i, j].candela));
				}
				System.Console.WriteLine ();
			}
			
			System.Console.WriteLine ();
		}
	}
}
