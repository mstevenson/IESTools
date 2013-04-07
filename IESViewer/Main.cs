using System;

namespace IESViewer
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			if (args.Length == 1) {
				var path = args [0];
				var parser = new IesParser (path);
				var ies = parser.Parse ();

				System.Console.WriteLine ("");

				System.Console.WriteLine ("Identifier: " + ies.identifier);
				System.Console.WriteLine ("Keywords: ");
				foreach (var keyword in ies.keywords) {
					System.Console.WriteLine ("   " + keyword.Key + ": " + keyword.Value);
				}

				System.Console.WriteLine ("");

				System.Console.WriteLine ("Lamp Count: " + ies.lampCount);
				System.Console.WriteLine ("Lumens: " + ies.lumens);
				System.Console.WriteLine ("Multiplier: " + ies.candelaMultiplier);
				System.Console.WriteLine ("Vertical Angles: " + ies.verticalAnglesCount);
				System.Console.WriteLine ("Horizontal Angles: " + ies.horizontalAnglesCount);
				System.Console.WriteLine ("Photometry Type: " + ies.photometryType);
				System.Console.WriteLine ("Units: " + ies.units);
				System.Console.WriteLine ("Size: " + ies.sizeX + ", " + ies.sizeY + ", " + ies.sizeZ);

				System.Console.WriteLine ("");

				System.Console.WriteLine ("BallastFactor: " + ies.ballastFactor);
				System.Console.WriteLine ("InputWatts: " + ies.inputWatts);

				System.Console.WriteLine ("");

			} else {
				Console.WriteLine ("Please specify a path to an IES file");
			}
		}
	}
}
