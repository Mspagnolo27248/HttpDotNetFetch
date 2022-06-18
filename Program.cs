// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

Console.WriteLine("Hello, World!");



//HttpClient client = new HttpClient();
//client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
//var results  = await client.GetAsync("users").Result.Content.ReadAsStringAsync();
//Console.WriteLine(results);


string token = string.Empty;
string data = string.Empty ;


using (HttpClient client = new HttpClient())
{
    client.BaseAddress = new Uri("https://rackapi.opisnet.com/api/v1/");
    string json = JsonConvert.SerializeObject(new {Username="mspagnolo@amref.com",Password="Argreset123!" });
    var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
    var results = await client.PostAsync("Authenticate",httpContent).Result.Content.ReadAsStringAsync();
    Console.WriteLine(results);
    var obj = JsonConvert.DeserializeObject<dynamic>(results);
    
    token = obj.Data.ToString().Replace("Bearer","").Trim();
    Console.WriteLine(token.ToString());
    Console.WriteLine("-----------------");
}


using (HttpClient client = new HttpClient()){
    client.BaseAddress = new Uri("https://rackapi.opisnet.com/api/v1/");
    client.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer",token);
    var results = await client.GetAsync("Supplier").Result.Content.ReadAsStringAsync();
    Console.WriteLine(results);
    var obj = JsonConvert.DeserializeObject<dynamic>(results);


    //{
    //    "SupplierID": 16,
    //  "SupplierName": "Total"
    //},
    foreach (var item in obj.Data.Suppliers) {
        Console.WriteLine(item.SupplierID.ToString()); ;
    }
   
    Console.ReadLine();

} 



