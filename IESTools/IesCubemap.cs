using System;
using System.Collections.Generic;

namespace IESTools
{
	public class IESCubemap
	{
		public readonly int resolution;
		public readonly Dictionary<CubeFace, IesTexture> textures;

		public IESCubemap (int resolution)
		{
			this.resolution = resolution;
			this.textures = new Dictionary<CubeFace, IesTexture> ();
			textures.Add (CubeFace.Right, new IESTools.IesTexture (resolution));
			textures.Add (CubeFace.Left, new IESTools.IesTexture (resolution));
			textures.Add (CubeFace.Top, new IESTools.IesTexture (resolution));
			textures.Add (CubeFace.Bottom, new IESTools.IesTexture (resolution));
			textures.Add (CubeFace.Front, new IESTools.IesTexture (resolution));
			textures.Add (CubeFace.Back, new IESTools.IesTexture (resolution));
		}

		public static Vec3 CubePointToSpherePoint (CubeFace face, float x, float y)
		{
			switch (face) {
			case CubeFace.Front:
				return CubePointToSpherePoint (x, y, -0.5);
			case CubeFace.Back:
				return CubePointToSpherePoint (-x, y, 0.5);
			case CubeFace.Left:
				return CubePointToSpherePoint (-0.5, y, x);
			case CubeFace.Right:
				return CubePointToSpherePoint (0.5, y, -x);
			case CubeFace.Top:
				return CubePointToSpherePoint (x, 0.5, y);
			case CubeFace.Bottom:
				return CubePointToSpherePoint (x, -0.5, -y);
			default:
				return new Vec3 (0, 0, 0);
			}
		}

		public static Vec3 CubePointToSpherePoint (double x, double y, double z)
		{
			Vec3 point = new Vec3 ();
			point.x = x * Math.Sqrt (1 - (y * y) / 2 - (z * z) / 2 + (y * y * z * z) / 3);
			point.y = y * Math.Sqrt (1 - (z * z) / 2 - (x * x) / 2 + (z * z * x * x) / 3);
			point.z = z * Math.Sqrt (1 - (x * x) / 2 - (y * y) / 2 + (x * x * y * y) / 3);
			return point;
		}
	}
}

