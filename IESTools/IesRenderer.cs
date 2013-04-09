using System;

namespace IESTools
{
	public class IesRenderer
	{
		public IesRenderer ()
		{
		}

		public IESCubemap RenderCubemap (int resolution, IesData iesData)
		{
			IESCubemap cubemap = new IESCubemap (resolution);

			foreach (var kvp in cubemap.textures) {
				CubeFace face = kvp.Key;
				IesTexture texture = kvp.Value;
				float offset = (1 / cubemap.resolution) / 2;
				for (int y = 0; y < cubemap.resolution; y++) {
					float yNorm = (y / cubemap.resolution) + offset;
					for (int x = 0; x < cubemap.resolution; x++) {
						float xNorm = (x / cubemap.resolution) + offset;
						var spherePoint = IESCubemap.CubePointToSpherePoint (face, xNorm, yNorm);
						var latLongPoint = LatLon.FromSpherePoint (spherePoint);
						var candela = GetInterpolatedCandela (latLongPoint, iesData.angleCandelas[x, y]);
						var pixel = CandelaToPixel (candela);
						texture.WritePixel (x, y, pixel);
					}
				}
			}

			return cubemap;
		}

		double GetInterpolatedCandela (LatLon position, AngleCandela data)
		{
			// TODO interpolation function
			return 0;
		}

		IesPixel CandelaToPixel (double candela)
		{
			// TODO
			return new IesPixel (0, 0, 0);
		}

	}
}

