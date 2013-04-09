using System;

namespace IESTools
{
	public struct LatLon
	{
		/// <summary>
		/// The latitude in radians.
		/// </summary>
		public readonly double latitude;

		/// <summary>
		/// The longitude in radians.
		/// </summary>
		public readonly double longitude;
		
		public LatLon (double latitude, double longitude)
		{
			this.latitude = latitude;
			this.longitude = longitude;
		}

		public static LatLon FromSpherePoint (Vec3 point, float radius = 1)
		{
			double lat = Math.Acos (point.y / radius);
			double lon = Math.Atan (point.x / point.z);
			return new LatLon (lat, lon);
		}
	}
}

