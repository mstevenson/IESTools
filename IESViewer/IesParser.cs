using System;
using System.IO;

namespace IESViewer
{
	public enum SpecificationType
	{
		LM631986,
		LM631991,
		LM631995
	}

	public enum PhotometryType
	{
		C = 1,
		B = 2,
		A = 3
	}

	public enum Units
	{
		Feet = 1,
		Meters = 2
	}

	public class IesParser
	{
		enum ParseMode {
			Identifier,
			Keywords,
			Tilt,
			LampData,
			BallastData,
			VerticalAngles,
			HorizontalAngles,
			CandelaData
		}

		// http://www.ltblight.com/English.lproj/LTBLhelp/pages/iesformat.html
		// october2010traceprowebinar.pdf

		string path;

		public IesParser (string path)
		{
			this.path = path;
		}

		void Parse ()
		{
			var ies = new IES ();

			string line;
			using (var fs = new FileStream (this.path, FileMode.Open, FileAccess.Read)) {
				using (var sr = new StreamReader (path)) {
					var mode = ParseMode.Identifier;

					while (!sr.EndOfStream) {
						line = sr.ReadLine ().Trim ();

						if (mode == ParseMode.Identifier) {
							ies.identifier = line;
							mode = ParseMode.Keywords;
							continue;
						}

						if (mode == ParseMode.Keywords && !line.StartsWith ("[")) {
							mode = ParseMode.Tilt;
						}

						if (mode == ParseMode.Keywords) {
							if (line.StartsWith ("[") && line.EndsWith ("]")) {
								int splitIndex = line.IndexOf (']');
								string key = line.Remove (splitIndex).Replace ("[", "").Replace ("]", "");
								string val = line.Substring (splitIndex).Trim ();
								switch (key) {
								case "TEST":
									ies.test = val;
									break;
								case "TESTDATE":
									ies.testDate = val;
									break;
								case "ISSUEDATE":
									ies.issueDate = val;
									break;
								case "TESTLAB":
									ies.testLab = val;
									break;
								case "MANUFAC":
									ies.manufacturer = val;
									break;
								case "LUMCAT":
									ies.luminaireCatalog = val;
									break;
								case "LUMINAIRE":
									ies.luminaire = val;
									break;
								case "LAMPCAT":
									ies.lampCatalog = val;
									break;
								case "LAMP":
									ies.lamp = val;
									break;
								case "BALLAST":
									ies.ballast = val;
									break;
								case "BALLASTCAT":
									ies.ballastCatalog = val;
									break;
								case "DISTRIBUTION":
									ies.distribution = val;
									break;
								default:
									if (!string.IsNullOrEmpty (val)) {
										ies.customAttributes.Add (key, val);
									}
									break;
								}
							}
						}

						if (mode == ParseMode.Tilt) {
							if (line.StartsWith ("IILT")) {
								string tilt = line.Substring (5).Trim ();
								switch (tilt) {
								case "NONE":
									break;
								case "INHERIT":
									throw new System.Exception ("Can't use TILT type");
									break;
								default:
									// FIXME The next line contains data about the tilt, but we can't use it yet
									sr.ReadLine ();
									throw new System.Exception ("Can't use TILT type");
									break;
								}
								mode = ParseMode.LampData;
								continue;
							}
						} else if (mode == ParseMode.LampData) {
							string[] data = line.Split (' ');
							ies.lampCount = int.Parse (data[0]);
							ies.lumens = float.Parse (data[1]);
							ies.multiplier = float.Parse (data[2]);
							ies.verticalAngles = int.Parse (data[3]);
							ies.horizontalAngles = int.Parse (data[4]);
							ies.photometryType = (PhotometryType)(int.Parse (data[5]));
							ies.units = (Units)(int.Parse (data[6]));
							ies.size = new Vec3 (float.Parse (data[7]), float.Parse (data[8]), float.Parse (data[9]));
							mode = ParseMode.BallastData;
						} else if (mode == ParseMode.BallastData) {
							string[] data = line.Split (' ');
							ies.ballastFactor = data[0];
							// note: data[1] isn't used, it's reserved for future use
							ies.inputWatts = data[2];
							mode = ParseMode.VerticalAngles;
						} else if (mode == ParseMode.VerticalAngles) {
							// TODO read the given number of vertical angles which may span multiple lines
							mode = ParseMode.HorizontalAngles;
						} else if (mode == ParseMode.HorizontalAngles) {
							// TODO read the given number of horizontal angles which may span multiple lines
						} else if (mode == ParseMode.CandelaData) {
							// TODO read data for verticle angles first, then for horizontal angles
						}
					}
				}
			}
		}
	}
}

