using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

// http://www.ltblight.com/English.lproj/LTBLhelp/pages/iesformat.html
// october2010traceprowebinar.pdf

namespace IESTools
{
	public enum SpecificationType
	{
		LM631986,
		LM631991,
		LM631995,
		LM632002
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
		enum Direction {
			Vertical,
			Horizontal
		}

		string path;
		IES ies;

		public IesParser (string path)
		{
			this.path = path;
		}

		public IES Parse ()
		{
			this.ies = new IES ();
			using (var fs = new FileStream (this.path, FileMode.Open, FileAccess.Read)) {
				using (var reader = new StreamReader (fs)) {
					ParseIdentifier (reader);
					ParseKeywords (reader);
					ParseTilt (reader);
					ParseLampData (reader);
					ParseBallastData (reader);
					ParseAngles (reader, Direction.Vertical);
					ParseAngles (reader, Direction.Horizontal);
					ParseCandelas (reader);
				}
			}

			return this.ies;
		}

		void ParseIdentifier (StreamReader reader)
		{
			var line = reader.ReadLine ().Trim ();
			if (line.StartsWith ("IESNA")) {
				switch (line) {
				case "IESNA:LM-63-1991":
					ies.identifier = SpecificationType.LM631991;
					break;
				case "IESNA:LM-63-1995":
					ies.identifier = SpecificationType.LM631995;
					break;
				case "IESNA:LM-63-2002":
					// FIXME unable to find documentation on this format
					ies.identifier = SpecificationType.LM632002;
					break;
				default:
					throw new System.Exception ("IES specification identifier not recognized");
				}
			} else {
				// we have to assume it's LM-63-1986 format since this format contains no identifier
				ies.identifier = SpecificationType.LM631986;
			}
		}

		void ParseKeywords (StreamReader reader)
		{
			switch (ies.identifier) {
			case SpecificationType.LM631986:
				// The 1986 specification does not contains user-defined keywords, rather the
				// file begins with comments requiring an arbitrary number of lines.
				break;
			case SpecificationType.LM631991:
				break;
			case SpecificationType.LM631995:
				string lastKey = "";
				while (reader.Peek () == '[') {
					var line = reader.ReadLine ().Trim ();
					int splitIndex = line.IndexOf (']') + 1;
					string key = line.Remove (splitIndex).Replace ("[", "").Replace ("]", "");
					string val = line.Substring (splitIndex).Trim ();
					if (!string.IsNullOrEmpty (val)) {
						if (key == "MORE" && !string.IsNullOrEmpty (lastKey)) {
							ies.keywords[lastKey] += "\n" + val;
						} else {
							ies.keywords.Add (key, val);
							lastKey = key;
						}
					}
				}
				break;
			}
		}

		void ParseTilt (StreamReader reader)
		{
			var line = reader.ReadLine ().Trim ();
			// Remove "Tilt" from the name
			string tilt = line.Split ('=')[1].Trim ();
			switch (tilt) {
			case "NONE":
				return;
			case "INHERIT":
				throw new System.Exception ("Can't use TILT type of INHERIT");
			default:
				// FIXME The next line contains data about the tilt, but we can't use it yet
				reader.ReadLine ();
				throw new System.Exception ("Can't use TILT type");
			}
		}

		void ParseLampData (StreamReader reader)
		{
			var line = reader.ReadLine ().Trim ();
			string[] data = line.Split (' ');
			ies.lampCount = int.Parse (data[0]);
			ies.lumensPerLamp = float.Parse (data[1]);
			ies.candelaMultiplier = float.Parse (data[2]);
			ies.verticalAnglesCount = int.Parse (data[3]);
			ies.horizontalAnglesCount = int.Parse (data[4]);
			ies.angleCandelas = new AngleCandela[ies.horizontalAnglesCount, ies.verticalAnglesCount];
			ies.photometryType = (PhotometryType)(int.Parse (data[5]));
			ies.units = (Units)(int.Parse (data[6]));
			ies.sizeX = float.Parse (data [7]);
			ies.sizeY = float.Parse (data [8]);
			ies.sizeZ = float.Parse (data [9]);
		}

		void ParseBallastData (StreamReader reader)
		{
			var line = reader.ReadLine ().Trim ();
			string[] val = line.Split (' ');
			ies.ballastFactor = float.Parse (val[0]);
			// note: data[1] isn't used, it's reserved for future use

			// FIXME data[1] used to be used for "BallastLampPhotometricFactor" in older formats

			ies.inputWatts = float.Parse (val[2]);
		}

		void ParseAngles (StreamReader reader, Direction direction)
		{
			List<float> allAngleValues = GetFloatValues (reader, (direction == Direction.Vertical) ? ies.verticalAnglesCount : ies.horizontalAnglesCount);
			for (int i = 0; i < ies.horizontalAnglesCount; i++) {
				for (int j = 0; j < ies.verticalAnglesCount; j++) {
					if (direction == Direction.Horizontal) {
						ies.angleCandelas[i,j].horizontalAngle = allAngleValues[i];
					} else if (direction == Direction.Vertical) {
						ies.angleCandelas[i,j].verticalAngle = allAngleValues[j];
					}
				}
			}
		}

		void ParseCandelas (StreamReader reader)
		{
			for (int horiz = 0; horiz < ies.horizontalAnglesCount; horiz++) {
				List<float> currentCandelaValues = GetFloatValues (reader, ies.verticalAnglesCount);
				for (int vert = 0; vert < ies.verticalAnglesCount; vert++) {
					ies.angleCandelas [horiz, vert].candela = currentCandelaValues [vert];
				}
			}
		}

		List<float> GetFloatValues (StreamReader reader, int totalValues)
		{
			List<float> allValues = new List<float> ();
			int count = 0;
			while (count < totalValues) {
				string line = reader.ReadLine ().Trim ();
				string[] lineValues = line.Split (new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				foreach (string val in lineValues) {
					allValues.Add (float.Parse (val));
					count++;
				}
			}
			return allValues;
		}
	}
}

