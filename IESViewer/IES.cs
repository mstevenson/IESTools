using System;
using System.Collections.Generic;

namespace IESViewer
{
	public class IES
	{
		public SpecificationType specification;

		public string identifier;

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

		public int lampCount;
		public float lumens;
		public float multiplier;
		public int verticalAnglesCount;
		public int horizontalAnglesCount;
		public PhotometryType photometryType;
		public Units units;
		public Vec3 size;

		public float ballastFactor;
		public float inputWatts;

		public AngleCandela[] verticalAngleCandelas;
		public AngleCandela[] horizontalAngleCandelas;

		public IES ()
		{
			customAttributes = new Dictionary<string, string> ();
		}
	}
}

