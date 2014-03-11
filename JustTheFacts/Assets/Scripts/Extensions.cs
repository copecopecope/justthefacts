using UnityEngine;

public static class RendererExtensions
{
	public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
		return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
	}
}

public static class ApplicationExtensions
{
	public static bool isTouchEnabled(this Application app) 
	{
		#if UNITY_ANDROID | UNITY_IPHONE
			return true;
		#endif

		return false;
	}
}