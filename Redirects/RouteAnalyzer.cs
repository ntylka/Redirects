namespace Redirects
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public class RouteAnalyzer : IRouteAnalyzer
	{
		private Dictionary<string, RouteNode> routes = new Dictionary<string, RouteNode>();

		public IEnumerable<string> Process(IEnumerable<string> routes)
		{
			var routeGraph = CreateRouteGraph(routes);
			return GetFinalizedRoutes(routeGraph);
		}

		private Dictionary<string, RouteNode> CreateRouteGraph(IEnumerable<string> routes)
		{
			var routeGraph = new Dictionary<string, RouteNode>();
			foreach (var route in routes)
			{
				var redirects = route.Split(" -> ");
				var previousPath = string.Empty;
				foreach (var redirect in redirects)
				{
					if (!routeGraph.Keys.Contains(redirect))
					{
						routeGraph.Add(
							redirect, 
							new RouteNode()
							{
								Path = redirect
							});
					}
					
					if (routeGraph.Keys.Contains(previousPath) && previousPath != redirect)
					{
						routeGraph[redirect].Previous = routeGraph[previousPath];
						routeGraph[previousPath].Next = routeGraph[redirect];
					}

					previousPath = redirect;
				}
			}

			return routeGraph;
		}

		private List<string> GetFinalizedRoutes(Dictionary<string, RouteNode>  routeGraph)
		{
			var output = new List<string>();
			var visitedCount = 0;
			foreach (var root in routeGraph.Values.Where(x => x.Previous == null))
			{
				var visited = new HashSet<string>();
				var currentNode = root;
				var sb = new StringBuilder();
				while (currentNode != null)
				{
					if (visited.Contains(currentNode.Path))
					{
						throw new Exception();
					}

					if (currentNode != root)
					{
						sb.Append(" -> ");
					}

					sb.Append(currentNode.Path);
					visited.Add(currentNode.Path);
					currentNode = currentNode.Next;
				}

				visitedCount += visited.Count;
				output.Add(sb.ToString());
			}

			if (visitedCount != routeGraph.Count)
			{
				throw new Exception();
			}

			return output;
		}
	}
}
