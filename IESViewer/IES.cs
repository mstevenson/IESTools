using System;
using System.Collections.Generic;

namespace IESViewer
{
	public class IES
	{
		public SpecificationType specification;

		public string test;
		public string testDate;
		public string issueDate;
		public string testLab;
		public string manufacturer;
		public string luminaireCatalog;
		public string luminaire;
		public string lampCatalog;
		public string lamp;
		public string ballastCatalog;
		public string ballast;
		public string distribution;

		public Dictionary<string, string> customAttributes;

		public IES ()
		{
			customAttributes = new Dictionary<string, string> ();
		}
	}
}

