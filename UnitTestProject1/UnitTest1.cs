using Microsoft.VisualStudio.TestTools.UnitTesting;
using Redirects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SimpleRedirects()
        {
			var sampleInput = new List<string>()
			{
				"/home",
				"/our-ceo.html -> /about-us.html",
				"/about-us.html -> /about",
				"/product-1.html -> /seo"
			};

			var output = new RouteAnalyzer().Process(sampleInput);
			var expected = new List<string>()
			{
				"/home",
				"/our-ceo.html -> /about-us.html -> /about",
				"/product-1.html -> /seo"
			};

			Assert.IsTrue(output.SequenceEqual(expected));
		}

		[TestMethod]
		public void RedirectToSelf()
		{
			var sampleInput = new List<string>()
			{
				"/home -> /home"
			};

			var output = new RouteAnalyzer().Process(sampleInput);
			var expected = new List<string>() { "/home" };

			Assert.IsTrue(output.SequenceEqual(expected));
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void CircularRefWithRoot()
		{
			var sampleInput = new List<string>()
			{
				"/our-ceo.html -> /about-us.html",
				"/about-us.html -> /about",
				"/about -> /about-us.html"
			};

			var output = new RouteAnalyzer().Process(sampleInput);
			Assert.Fail();
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void CircularRefNoRoot()
		{
			var sampleInput = new List<string>()
			{
				"/our-ceo.html -> /about-us.html",
				"/about-us.html -> /about",
				"/about -> /our-ceo.html"
			};

			var output = new RouteAnalyzer().Process(sampleInput);
			Assert.Fail();
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void OverwroteRoute_NodeUnReachable()
		{
			var sampleInput = new List<string>()
			{
				"/our-ceo.html -> /about-us.html",
				"/about-us.html -> /about",
				"/about-us.html -> //about-us"
			};

			var output = new RouteAnalyzer().Process(sampleInput);
			Assert.Fail();
		}
	}
}
