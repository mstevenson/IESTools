using System;
using System.IO;

namespace IESViewer
{
	public class IesParser
	{
		// http://www.ltblight.com/English.lproj/LTBLhelp/pages/iesformat.html

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
					while (!sr.EndOfStream) {
						line = sr.ReadLine ().Trim ();

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
				}
			}
		}
	}
}

