//using RestSharp;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;

//namespace GerarChaveAleatorio
//{
//    internal class Program
//    {
//        static async Task Main(string[] args)
//        {

//            var lista = GerarCodigo(1, 2500);
//            var lista2 = GerarCodigo(2501, 2500);
//            var lista3 = GerarCodigo(5001, 2500);
//            var lista4 = GerarCodigo(7501, 2500);

//            //var lista = GerarCodigo(1, 10);
//            //var lista2 = GerarCodigo(11, 10);
//            //var lista3 = GerarCodigo(21, 10);
//            //var lista4 = GerarCodigo(31, 10);

//            var modeloChamarApiLista1 = new List<(RestRequest request, string codigo)>();
//            var modeloChamarApiLista2 = new List<(RestRequest request, string codigo)>();
//            var modeloChamarApiLista3 = new List<(RestRequest request, string codigo)>();
//            var modeloChamarApiLista4 = new List<(RestRequest request, string codigo)>();

//            CarregarChamadas(lista, modeloChamarApiLista1);
//            CarregarChamadas(lista2, modeloChamarApiLista2);
//            CarregarChamadas(lista3, modeloChamarApiLista3);
//            CarregarChamadas(lista4, modeloChamarApiLista4);

//            using (var client = new RestClient("https://fiap-inaugural.azurewebsites.net/fiap"))
//            {

//                var tasks = new List<Task<(string mensagem, bool valido)>>();

//                foreach (var parametros in modeloChamarApiLista1)
//                {
//                    tasks.Add(EnviarCodigoAsync(client, parametros.request, parametros.codigo));
//                }

//                foreach (var parametros in modeloChamarApiLista2)
//                {
//                    tasks.Add(EnviarCodigoAsync(client, parametros.request, parametros.codigo));
//                }

//                foreach (var parametros in modeloChamarApiLista3)
//                {
//                    tasks.Add(EnviarCodigoAsync(client, parametros.request, parametros.codigo));
//                }

//                foreach (var parametros in modeloChamarApiLista4)
//                {
//                    tasks.Add(EnviarCodigoAsync(client, parametros.request, parametros.codigo));
//                }


//                await Task.WhenAll(tasks);

//                foreach (var task in tasks)
//                {
//                    if (task.Status == TaskStatus.RanToCompletion)
//                    {
//                        string mensagem = task.Result.mensagem;
//                        Console.WriteLine(mensagem);
//                        if (task.Result.valido)
//                        {
//                            EscreverArquivo(mensagem);
//                        }
//                    }
//                    else if (task.Status == TaskStatus.Faulted)
//                    {
//                        Console.WriteLine("Erro na chamada à API: " + task.Exception.Message);

//                    }
//                }
//            }
//            //var responses = await Task.WhenAll(taskList1); // returns IEmumerable<WalmartModel>

//            //var results = responses.Where(r => r != null); //filter out any null values

//        }

//        private static void EscreverArquivo(string mensagem)
//        {
//            try
//            {
//                StreamWriter sw = new StreamWriter("ResultadoComplete.txt");
//                sw.WriteLine(mensagem);
//                sw.Close();
//            }
//            catch (Exception ex)
//            {

//            }
//        }

//        private static void CarregarChamadas(List<string> lista, List<(RestRequest request, string codigo)> callApiModel)
//        {
//            foreach (var item in lista)
//            {
//                callApiModel.Add(CriarChamada(item));
//            }
//        }

//        //static List<String> GerarCodigo(int start, int total)
//        //{
//        //    string codigo = string.Empty;

//        //    Random r = new Random();
//        //    var listaNumerica = new List<int>();

//        //    for (int i = 0; i <= total; i++)
//        //    {
//        //        listaNumerica.Add(start+i);
//        //    }
//        //    listaNumerica = listaNumerica.OrderByDescending(x => x).ToList();
//        //    var codigos = new List<string>();
//        //    var alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzáçéóíúãõêâ";
//        //    for (int i = 0; i < listaNumerica.Count; i++)
//        //    {
//        //        for (int ii = 0; ii< alpha.Length; ii++)
//        //        {
//        //            codigos.Add(listaNumerica[i].ToString() + alpha[ii]);
//        //            codigos.Add(alpha[ii]+listaNumerica[i].ToString());

//        //        }
//        //    }
//        //    return codigos;
//        //}

//        static List<String> GerarCodigo(int start, int total)
//        {
//            var numeros = GerarNumeros(start, total);
//            var words = GerarLetras("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzáçéóíúãõêâ");

//            var codigos = new List<String>();

//            for (int i = 0; i < numeros.Count; i++)
//            {
//                for (int ii = 0; ii < words.Count; ii++)
//                {
//                    var word = words[ii];
//                    codigos.Add(numeros[i]);
                    
//                    codigos.Add($"{word}{numeros[i]}");
//                    codigos.Add($"{numeros[i]}{word}");
                    
//                    codigos.Add($"{numeros[i]}{word[0]}");
//                    //codigos.Add($"{numeros[i]}{word[1]}");
                    
//                    codigos.Add($"{word[0]}{numeros[i]}");
//                    //codigos.Add($"{word[1]}{numeros[i]}");

//                    codigos.Add($"{word[0]}{numeros[i]}{word[1]}");
//                    codigos.Add($"{word[1]}{numeros[i]}{word[2]}");
//                }
//            }

//            return codigos.Distinct().ToList();
//        }

//        static List<String> GerarNumeros(int start, int total)
//        {
//            string codigo = string.Empty;
//            var lista = new List<string>();

//            for (int i = 0; i <= total; i++)
//            {
//                lista.Add((start+i).ToString());
//            }
//            lista = lista.OrderByDescending(x => x).ToList();

//            return lista;
//        }

//        static List<String> GerarLetras(string words)
//        {
//            //var alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzáçéóíúãõêâ";
//            string codigo = string.Empty;
//            var lista = new List<string>();

//            for (int i = 0; i <= words.Length; i++)
//            {
//                for (int ii = 0; ii <= words.Length; ii++)
//                {
//                    lista.Add($"{words[i]}{words[ii]}");
//                    lista.Add($"{words[ii]}{words[i]}");
//                }
//            }
//            return lista.Distinct().ToList();
//        }

//        static (RestRequest request, string codigo) CriarChamada(string codigo)
//        {
//            var request = new RestRequest();
//            request.AddHeader("Content-Type", "application/json");
//            var json = "{\"key\": \""+codigo+"\"}";
//            request.AddParameter("application/json", json, ParameterType.RequestBody);

//            return (request, codigo);
//        }

//        static async Task<(string mensagem, bool valido)> EnviarCodigoAsync(RestClient client, RestRequest request, string codigo)
//        {
//            var response = await client.ExecutePostAsync(request);
//            string texto = string.Empty;
//            (string mensagem, bool valido) retorno;
//            if (response.StatusCode == System.Net.HttpStatusCode.OK)
//            {

//                texto = $"******** Código com sucesso é: {codigo} PARABÉNS; Response: {response.Content}";
//                retorno = (texto, true);
//                Console.WriteLine(texto);
//                Console.WriteLine(response.Content);
//            }
//            else
//            {
//                texto = $"Código: {codigo} inválido; Response: {response.Content}";
//                retorno = (texto, false);
//                Console.WriteLine(texto);

//            }
//            return retorno;
//        }
//    }
//}

