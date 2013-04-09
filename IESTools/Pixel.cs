using System;

namespace IESTools
{
	public struct Pixel
	{
		public float r;
		public float g;
		public float b;

		public Pixel (float r, float g, float b)
		{
			this.r = r;
			this.g = g;
			this.b = b;
		}

		public override bool Equals (System.Object obj)
		{
			if (obj == null) {
				return false;
			}
			if (obj is Pixel == false) {
				return false;
			}
			var p = (Pixel)obj;
			return (p.r == r && p.g == g && p.b == b);
		}

		public static bool operator ==(Pixel a, Pixel b)
		{
			if (System.Object.ReferenceEquals (a, b)) {
				return true;
			}
			if ((object)a == null || (object)b == null) {
				return false;
			}
			return (a.r == b.r && a.g == b.g && a.b == b.b);
		}

		public static bool operator !=(Pixel a, Pixel b)
		{
			return !(a == b);
		}

		public override int GetHashCode ()
		{
			return r.GetHashCode () ^ g.GetHashCode () ^ b.GetHashCode ();
		}
	}
}

