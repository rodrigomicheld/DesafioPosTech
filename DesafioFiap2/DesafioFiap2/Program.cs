using RestSharp;

Random random = new Random();
List<string> chaves = new List<string>();

for (int i = 1; i <= 10000; i++)
{
    string chave = GenerarChaves(random);
    chaves.Add(chave);
}

Console.WriteLine("Chaves geradas:");
foreach (string chave in chaves)
{
    Console.WriteLine(chave);
    enviar(chave);
}

static string GenerarChaves(Random random)
{
    var chave = "";

    char letras1 = (char)random.Next((int)'A', (int)'Z' + 1);
    char letras2 = (char)random.Next((int)'A', (int)'Z' + 1);

    int number = random.Next(1, 10001);

    // Gera um número aleatório de 1 a 3 para decidir o formato da string
    int format = random.Next(1, 4);

    if (format == 1)
    {
        // Uma letra no início + número + letra no final
        chave = letras1.ToString() + number.ToString() + letras2.ToString();
    }
    else if (format == 2)
    {
        // Uma letra no começo + número
        chave = letras1.ToString() + number.ToString();
    }
    else
    {
        // Uma letra somente no final + número
        chave = number.ToString() + letras2.ToString();
    }

    return chave;
}

void enviar(string chave)
{
    var client = new RestClient("https://fiap-inaugural.azurewebsites.net/fiap");
    var request = new RestRequest();
    request.AddHeader("Content-Type", "application/json");
    var json = "{ \"key\": \"" + chave + "\"}";
    request.AddParameter("application/json", json, ParameterType.RequestBody);
    var response = client.ExecutePost(request);

    if (response.IsSuccessStatusCode)
    {
        Console.WriteLine("Sucesso:" + chave + "  " + response.Content);
    }
}