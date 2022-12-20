using Newtonsoft.Json;
using Fluent.LibreTranslate;

class langLists
{
    public string code { get; set; }
    public string name { get; set; }
    public string[] targets { get; set; }
}

class libretranslateExtern
{
    private HttpClient _httpClient;
    public libretranslateExtern(LibreTranslateServer ServerSelect)
    {
        this._httpClient = new HttpClient()
        {
            BaseAddress = new Uri(ServerSelect.ToString())
        };
    }

    public async Task<langLists[]> GetListsLanguage()
    {
        return JsonConvert.DeserializeObject<langLists[]>(await this._httpClient.GetStringAsync("/languages"));
    }
}

class mainku
{
    private static readonly HttpClient client = new HttpClient();

    public async static Task Main()
    {
        libretranslateExtern libre = new libretranslateExtern(LibreTranslateServer.Libretranslate_de);

        langLists[] data = await libre.GetListsLanguage();

        string raws = string.Empty;
        foreach (langLists data_literate in data)
        {
            raws += $"{data_literate.code} = {data_literate.name}\n";
        }

        Console.WriteLine(raws);
        // string text = "komputer adalah mesin yang dilengkapi CPU berkecapaan tinggi";
        // string source = "auto";
        // string target = "en";
        // HttpClient client = new HttpClient();
        // var values = new Dictionary<string, string>
        //     {
        //         { "q", text },
        //         { "source",  source },
        //         { "target", target }
        //     };

        // var content = new FormUrlEncodedContent(values);
        // var response = await client.PostAsync("https://libretranslate.de/translate", content);

        // //JObject responseString = JObject.Parse(await response.Content.ReadAsStringAsync());
        // System.Console.WriteLine(await response.Content.ReadAsStringAsync());
        // //return responseString["translatedText"];

    }

}


