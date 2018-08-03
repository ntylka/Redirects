# Welcome! 

Thanks for applying to Boostability. 

The following problem has no *right* answer. We just want to know what you would do. This exercise is not timed but we expect it to: 

- Be complete (using C#)
- Have a useful commit history (don't make one big commit at the end)
- Be tested (using unit tests)

## The Scenario

Pretend you have a list of routes in an application (/home, /about, and so on). But, you also have routes that redirect to new routes (/home.html -> /home). 

Make an applicationt that takes a list of routes and prints out the paths in the application without any duplicates. 

For example, if input will look like this: 

```csharp
new [] 
{ 
    "/home",
    "/our-ceo.html -> /about-us.html",
    "/about-us.html -> /about",
    "/product-1.html -> /seo"    
}
```

We would expect the output to look like this: 

```csharp
new [] 
{
    "/home",
    "/our-ceo.html -> /about-us.html -> /about",
    "/product-1.html -> /seo"
}
```

But, if we have a circular reference, we want to throw an exception: 

```csharp
new []
{
    "/about-us.html -> /about",
    "/about -> about-us.html"
}

// Oh no! Throw an exception (/about-us.html -> /about -> /about-us.html -> /about -> /about-us.html -> /about). 

```

## Hints

- We don't care about the order the routes are returned
- We don't really care about string parsing (you can assume "->" as a delimiter)
- You shouldn't assume the redirects inputed have any order to them
- You should write a program that implements the following interface: 

```csharp
interface RouteAnalyzer 
{
    IEnumerable<string> Process(IEnumerable<string> routes);
}
```