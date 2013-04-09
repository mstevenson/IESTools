using System;

namespace IESTools
{
	public class IesRenderer
	{
		public IesRenderer ()
		{
		}

		public IesCubemap RenderCubemap (int resolution, IesData iesData)
		{
			IesCubemap cubemap = new IesCubemap (resolution);
			foreach (var kvp in cubemap.textures) {
				CubeFace face = kvp.Key;
				IesTexture texture = kvp.Value;
				double offset = (1 / cubemap.resolution) / 2;
				for (int y = 0; y < cubemap.resolution; y++) {
					double yNorm = (y / cubemap.resolution) + offset;
					for (int x = 0; x < cubemap.resolution; x++) {
						double xNorm = (x / cubemap.resolution) + offset;
						Vec3 spherePoint = IesCubemap.CubePointToSpherePoint (face, xNorm, yNorm);
						LatLon latLongPoint = LatLon.FromSpherePoint (spherePoint);
						double candela = InterpolatedCandelaFromData (latLongPoint, iesData.angleCandelas);
						texture.WritePixelIntensity (x, y, candela);
					}
				}
			}
			return cubemap;
		}

		double InterpolatedCandelaFromData (LatLon position, AngleCandela[,] data)
		{
			// TODO interpolation function
			return 0;
		}
	}
}

