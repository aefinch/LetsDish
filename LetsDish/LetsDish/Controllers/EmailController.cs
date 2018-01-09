using System;
using System.IO;
using System.Web.Http;
using RestSharp;
using RestSharp.Authenticators;

namespace LetsDish.Controllers
{
    public class EmailController : ApiController
    {
    }
}


public class SendComplexMessageChunk
{

	public static void Main(string[] args)
	{
		Console.WriteLine(SendComplexMessage().Content.ToString());
	}

	public static IRestResponse SendComplexMessage()
	{
		RestClient client = new RestClient();
		client.BaseUrl = new Uri("https://api.mailgun.net/v3");
		client.Authenticator =
			new HttpBasicAuthenticator("api", "${ MAILGUN_CONFIG.apiKey }");
		RestRequest request = new RestRequest();
		request.AddParameter("domain", "YOUR_DOMAIN_NAME", ParameterType.UrlSegment);
		request.Resource = "{domain}/messages";
		request.AddParameter("from", "Excited User <YOU@YOUR_DOMAIN_NAME>");
		request.AddParameter("to", "foo@example.com");
		request.AddParameter("subject", "Hello");
		request.AddParameter("text", "Testing some Mailgun awesomness!");
		request.AddParameter("html",
							  "<html>HTML version of the body</html>");
		request.AddFile("attachment", Path.Combine("files", "test.jpg"));
		request.AddFile("attachment", Path.Combine("files", "test.txt"));
		request.Method = Method.POST;
		return client.Execute(request);
	}

}