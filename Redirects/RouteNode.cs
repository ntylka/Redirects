namespace Redirects
{
	internal class RouteNode
	{
		internal string Path { get; set; }

		internal RouteNode Previous { get; set; }

		internal RouteNode Next { get; set; }
	}
}