using Newtonsoft.Json.Linq;
class mainku
{
    private static readonly HttpClient client = new HttpClient();

    public async static Task Main()
    {
        string text = "komputer adalah mesin yang dilengkapi CPU berkecapaan tinggi";
        string source = "auto";
        string target = "en";
        HttpClient client = new HttpClient();
        var values = new Dictionary<string, string>
            {
                { "q", text },
                { "source",  source },
                { "target", target }
            };

        var content = new FormUrlEncodedContent(values);
        var response = await client.PostAsync("https://libretranslate.de/translate", content);

        //JObject responseString = JObject.Parse(await response.Content.ReadAsStringAsync());
        System.Console.WriteLine(await response.Content.ReadAsStringAsync());
        //return responseString["translatedText"];

    }

}


