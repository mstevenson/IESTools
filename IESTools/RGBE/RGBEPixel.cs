using System;

namespace IESTools
{
	/// <summary>
	/// A pixel in RGBE format.
	/// </summary>
	public struct RGBEPixel
	{
		public byte r;
		public byte g;
		public byte b;
		public byte e;

		public RGBEPixel (float r, float g, float b)
		{
			// Based on code by Greg Ward and Bruce Walter
			// http://www.graphics.cornell.edu/%7Ebjw/rgbe/rgbe.c

			float val = r;
			if (g > val)
				val = g;
			if (b > val)
				val = b;

			if (val < 1e-32) {
				this.r = this.g = this.b = this.e = 0;
			} else {
				int exponent;
				val = Frexp (val, out exponent) * 256f / val;
				this.r = (byte)(r * val);
				this.g = (byte)(g * val);
				this.b = (byte)(b * val);
				this.e = (byte)(exponent + 128);
			}
		}

		/// <summary>
		/// Extract the mantissa and exponent from a value
		/// </summary>
		static float Frexp (float value, out int exponent)
		{
			// FIXME
			exponent = 1;
			return 1;

//			// http://stackoverflow.com/questions/389993/extracting-mantissa-and-exponent-from-double-in-c-sharp/390072#390072
//			long bits = BitConverter.DoubleToInt64Bits (value);
//			bool negative = (bits < 0);
//			exponent = (int) ((bits >> 52) & 0x7ffL);
//			long mantissa = bits & 0xfffffffffffffL;
//			if (exponent==0) {
//				exponent++;
//			} else {
//				mantissa = mantissa | (1L<<52);
//			}
//			exponent -= 1075;
//			if (mantissa == 0)  {
//				return 0;
//			}
//			while ((mantissa & 1) == 0) 
//			{
//				mantissa >>= 1;
//				exponent++;
//			}
//			return mantissa;
		}

		public override bool Equals (System.Object obj)
		{
			if (obj == null) {
				return false;
			}
			if (obj is RGBEPixel == false) {
				return false;
			}
			var p = (RGBEPixel)obj;
			return (p.r == r && p.g == g && p.b == b && p.e == e);
		}
		
		public static bool operator ==(RGBEPixel a, RGBEPixel b)
		{
			if (System.Object.ReferenceEquals (a, b)) {
				return true;
			}
			if ((object)a == null || (object)b == null) {
				return false;
			}
			return (a.r == b.r && a.g == b.g && a.b == b.b && a.e == b.e);
		}
		
		public static bool operator !=(RGBEPixel a, RGBEPixel b)
		{
			return !(a == b);
		}

		public override int GetHashCode ()
		{
			return r ^ g ^ b ^ e;
		}
	}
}

