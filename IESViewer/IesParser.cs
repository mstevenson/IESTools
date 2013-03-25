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
//		enum ParsingStage {
//			Identifier,
//			Keywords,
//			Tilt,
//			LampData,
//			BallastData,
//			VerticalAngles,
//			HorizontalAngles,
//			CandelaData
//		}

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
				using (var sr = new StreamReader (fs) {
					var mode = ParsingStage.Identifier;

					while (!sr.EndOfStream) {
						line = sr.ReadLine ().Trim ();

						if (mode == ParsingStage.Identifier) {
							if (line.StartsWith ("IESNA")) {
								switch (line) {
								case "IESNA:LM-63-1991":
									ies.identifier = SpecificationType.LM631991;
									break;
								case "IESNA:LM-63-1995":
									ies.identifier = SpecificationType.LM631995;
									break;
								default:
									throw new System.Exception ("IES specification identifier not recognized");
									break;
								}
							} else {
								// we have to assume it's LM-63-1986 format since this format contains no identifier
								ies.identifier = SpecificationType.LM631986;
							}
							ies.identifier = line;
							mode = ParsingStage.Keywords;
							continue;
						}

						if (mode == ParsingStage.Keywords && !line.StartsWith ("[")) {
							mode = ParsingStage.Tilt;
						}

						if (mode == ParsingStage.Keywords) {
							if (ies.identifier == SpecificationType.LM631986) {
								// The 1986 specification does not contains user-defined keywords, rather the
								// file begins with comments requiring an arbitrary number of lines.

							} else {
								if (line.StartsWith ("[") && line.EndsWith ("]")) {
									int splitIndex = line.IndexOf (']');
									string key = line.Remove (splitIndex).Replace ("[", "").Replace ("]", "");
									string val = line.Substring (splitIndex).Trim ();
									if (!string.IsNullOrEmpty (val)) {
										ies.attributes.Add (key, val);
									}
								}
							}
						}

						if (mode == ParsingStage.Tilt) {
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
								mode = ParsingStage.LampData;
								continue;
							}
						} else if (mode == ParsingStage.LampData) {
							string[] data = line.Split (' ');
							ies.lampCount = int.Parse (data[0]);
							ies.lumens = float.Parse (data[1]);
							ies.multiplier = float.Parse (data[2]);
							ies.verticalAnglesCount = int.Parse (data[3]);
							ies.horizontalAnglesCount = int.Parse (data[4]);
							ies.photometryType = (PhotometryType)(int.Parse (data[5]));
							ies.units = (Units)(int.Parse (data[6]));
							ies.size = new Vec3 (float.Parse (data[7]), float.Parse (data[8]), float.Parse (data[9]));
							mode = ParsingStage.BallastData;
						} else if (mode == ParsingStage.BallastData) {
							string[] data = line.Split (' ');
							ies.ballastFactor = data[0];
							// note: data[1] isn't used, it's reserved for future use
							ies.inputWatts = data[2];
							mode = ParsingStage.VerticalAngles;
						} else if (mode == ParsingStage.VerticalAngles) {
							// TODO read the given number of vertical angles which may span multiple lines
							mode = ParsingStage.HorizontalAngles;
						} else if (mode == ParsingStage.HorizontalAngles) {
							// TODO read the given number of horizontal angles which may span multiple lines
						} else if (mode == ParsingStage.CandelaData) {
							// TODO read data for verticle angles first, then for horizontal angles
						}
					}
				}
			}
		}
	}
}

