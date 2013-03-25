using System;
using System.IO;

namespace IESViewer
{
	public enum IESParsingSection {
		Identifier,
		Keywords,
		Tilt,
		LampData,
		BallastData,
		VerticalAngles,
		HorizontalAngles,
		CandelaData
	}

	public class IESReader : IDisposable
	{
		public IESParsingSection CurrentSection { get; private set; }
		public SpecificationType Specification { get; private set; }

		StreamReader sr;
		string currentToken;

		int line;
		int cursor;

		public IESReader (string path)
		{
			sr = new StreamReader (path);
			CurrentSection = IESParsingSection.Identifier;
			ParseIdentifier ();
		}

		string ReadLine ()
		{
			string s = sr.ReadLine ();
			line++;
			return s;
		}

		char ReadChar ()
		{
			char c = (char)sr.Read ();
			cursor++;
			// FIXME if c is a newline, then increment the current line
		}

		SpecificationType ParseIdentifier ()
		{


			if (token.StartsWith ("IESNA")) {
				switch (token) {
				case "IESNA:LM-63-1991":
					Specification = SpecificationType.LM631991;
					break;
				case "IESNA:LM-63-1995":
					Specification = SpecificationType.LM631995;
					break;
				default:
					throw new System.Exception ("IES specification identifier not recognized");
					break;
				}
			} else {
				// we have to assume it's LM-63-1986 format since this format contains no identifier
				Specification = SpecificationType.LM631986;
			}
		}

		public bool MoveNext ()
		{
		}

		public void Dispose ()
		{
			sr.Dispose ();
		}
	}
}

