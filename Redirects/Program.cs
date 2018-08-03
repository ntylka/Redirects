using System;
using System.Collections.Generic;
using System.Linq;

namespace Redirects
{
    class Program
    {
        static void Main(string[] args)
        {
			var sampleInput = new List<string>() {
				"/home",
				"/our-ceo.html -> /about-us.html",
				"/about-us.html -> /about",
				"/product-1.html -> /seo",
				"/seo -> /seo-central"
			};

			var output = new RouteAnalyzer().Process(sampleInput);
			foreach (var s in output)
			{
				Console.WriteLine(s);
			}

			Console.WriteLine();
		}
    }
}
