using System;
using System.Collections.Generic;

namespace IESTools
{
	public class IesData
	{
		public SpecificationType identifier;

		public Dictionary<string, string> keywords;

		public int lampCount;
		public float lumensPerLamp;
		public float candelaMultiplier;
		public int verticalAnglesCount;
		public int horizontalAnglesCount;
		public PhotometryType photometryType;
		public Units units;
		public float sizeX;
		public float sizeY;
		public float sizeZ;

		public float ballastFactor;
		public float inputWatts;

		/// <summary>
		/// A lat-long array of degree angles and their associated candela values.
		/// </summary>
		public AngleCandela[,] angleCandelas;

		public IesData ()
		{
			keywords = new Dictionary<string, string> ();
		}
	}
}

