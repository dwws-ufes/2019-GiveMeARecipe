using GiveMeARecipe.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;
using VDS.RDF.Query;

namespace GiveMeARecipe.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("categories")]
        public IActionResult GetCategories(string term)
        {
            var endpoint = new SparqlRemoteEndpoint(new Uri("http://dbpedia.org/sparql"), "http://dbpedia.org");
            var query =
                "PREFIX skos: <http://www.w3.org/2004/02/skos/core#>\r\n" +
                "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>\r\n" +
                "SELECT ?cat ?stripped_name\r\n" +
                "WHERE{\r\n" +
                    "?cat a skos:Concept.\r\n" +
                    "?cat rdfs:label ?name\r\n" +
                    $"filter contains(lcase(str(?name)), '{term}')\r\n" +
                    "FILTER(LANGMATCHES(LANG(?name), 'en'))\r\n" +
                    "BIND(STR(?name)  AS ?stripped_name)\r\n" +
                "} LIMIT 10";
            var results = endpoint.QueryWithResultSet(query);
            var categories = results.Select(r => new
            {
                Id = r["cat"].ToString(),
                Text = r["stripped_name"].ToString()
            }).ToList();

            return new JsonResult(categories);
        }

        [HttpGet("ingredients")]
        public IActionResult GetIngredients(string term)
        {
            var endpoint = new SparqlRemoteEndpoint(new Uri("http://dbpedia.org/sparql"), "http://dbpedia.org");
            var query =
                "PREFIX dbo: <http://dbpedia.org/ontology/>\r\n" +
                "PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>\r\n" +
                "SELECT ?ing ?stripped_name\r\n" +
                "WHERE{\r\n" +
                    "?ing a dbo:Food.\r\n" +
                    "?ing rdfs:label ?name\r\n" +
                    "filter exists { ?ing ^dbo:ingredient ?x }\r\n" +
                    $"filter contains(lcase(str(?name)), '{term}')\r\n" +
                    "FILTER(LANGMATCHES(LANG(?name), 'en'))\r\n" +
                    "BIND(STR(?name)  AS ?stripped_name)\r\n" +
                "} LIMIT 10";
            var results = endpoint.QueryWithResultSet(query);
            var ingredients = results.Select(r => new
            {
                Id = r["ing"].ToString(),
                Text = r["stripped_name"].ToString()
            }).ToList();

            return new JsonResult(ingredients);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
