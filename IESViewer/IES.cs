using System;
using System.Collections.Generic;

namespace IESViewer
{
	public class IES
	{
		public SpecificationType identifier;

		public Dictionary<string, string> attributes;

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
			attributes = new Dictionary<string, string> ();
		}
	}
}

