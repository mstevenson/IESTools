using System;
using System.IO;

namespace IESTools
{
	/// <summary>
	/// An RGBE image file, originally developed for the Radiance renderer.
	/// Specification: http://paulbourke.net/dataformats/pic/
	/// </summary>
	public class RGBEFile
	{
		public string creatorName;

		IesTexture texture;

		public RGBEFile (IesTexture texture)
		{
			this.texture = texture;
		}

		public byte[] GetBytes ()
		{
			var sb = new System.Text.StringBuilder ();
			sb.AppendLine ("#?RADIANCE");
			if (!string.IsNullOrEmpty (creatorName)) {
				sb.AppendLine ("# Made with " + creatorName);
			}
			sb.AppendLine ("FORMAT=32-bit_rle_rgbe");
			sb.AppendFormat ("-Y {0} +X {1}", texture.Width, texture.Height);
			sb.AppendLine ();

			char[] data = new char[sb.Length];
			sb.CopyTo (0, data, 0, sb.Length);

			for (int y = 0; y < texture.Height; y++) {
				for (int x = 0; x < texture.Width; x++) {
					// RLE
					if (x > 0 && texture.ReadPixelIntensity (x - 1, y) == texture.ReadPixelIntensity (x, y)) {
						// TODO write RLE data
						// http://www.graphics.cornell.edu/%7Ebjw/rgbe/rgbe.c
					} else {
						// TODO write the pixel value
					}
				}
			}

			// TODO
			return new byte[0];
		}
	}
}

