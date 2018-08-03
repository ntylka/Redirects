namespace Redirects
{
	using System.Collections.Generic;

	public interface IRouteAnalyzer
	{
		IEnumerable<string> Process(IEnumerable<string> routes);
	}
}
