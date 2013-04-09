using System;
using System.Collections.Generic;

namespace IESTools
{
	public class IesCubemap
	{
		public readonly int resolution;
		public readonly Dictionary<CubeFace, IesTexture> textures;

		public IesCubemap (int resolution)
		{
			this.resolution = resolution;
			this.textures = new Dictionary<CubeFace, IesTexture> ();
			textures.Add (CubeFace.Right, new IESTools.IesTexture (resolution, resolution));
			textures.Add (CubeFace.Left, new IESTools.IesTexture (resolution, resolution));
			textures.Add (CubeFace.Top, new IESTools.IesTexture (resolution, resolution));
			textures.Add (CubeFace.Bottom, new IESTools.IesTexture (resolution, resolution));
			textures.Add (CubeFace.Front, new IESTools.IesTexture (resolution, resolution));
			textures.Add (CubeFace.Back, new IESTools.IesTexture (resolution, resolution));
		}

		/// <summary>
		/// Map a 2d point on a given face of a unit cube to a corresponding point on a unit sphere.
		/// </summary>
		/// <returns>The to sphere point.</returns>
		public static Vec3 CubeToSpherePoint (CubeFace face, double x, double y)
		{
			switch (face) {
			case CubeFace.Front:
				return CubeToSpherePoint (x, y, -0.5);
			case CubeFace.Back:
				return CubeToSpherePoint (-x, y, 0.5);
			case CubeFace.Left:
				return CubeToSpherePoint (-0.5, y, x);
			case CubeFace.Right:
				return CubeToSpherePoint (0.5, y, -x);
			case CubeFace.Top:
				return CubeToSpherePoint (x, 0.5, y);
			case CubeFace.Bottom:
				return CubeToSpherePoint (x, -0.5, -y);
			default:
				return new Vec3 (0, 0, 0);
			}
		}

		/// <summary>
		/// Map a point on the surface of a unit cube onto the surface of a unit sphere.
		/// </summary>
		public static Vec3 CubeToSpherePoint (double x, double y, double z)
		{
			Vec3 point = new Vec3 ();
			point.x = x * Math.Sqrt (1 - (y * y) / 2 - (z * z) / 2 + (y * y * z * z) / 3);
			point.y = y * Math.Sqrt (1 - (z * z) / 2 - (x * x) / 2 + (z * z * x * x) / 3);
			point.z = z * Math.Sqrt (1 - (x * x) / 2 - (y * y) / 2 + (x * x * y * y) / 3);
			return point;
		}
	}
}

