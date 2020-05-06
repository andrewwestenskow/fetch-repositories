using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApiClient
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        private static async Task ProcessRepositories(string orgName, int sinceId)
        {
            
            var repositories = await FetchRepositories(orgName, sinceId);
            foreach (var repo in repositories)
            {
                Console.WriteLine($"id: {repo.repoId}; Name: {repo.repoName}; pushed: {repo.lastPush}");

            }

            Console.WriteLine("-------------------------------");
            Console.WriteLine("Press right arrow for more or any other key to exit");
            var keyPress = Console.ReadKey().Key.ToString();

            Console.WriteLine(keyPress);

            if(keyPress == "RightArrow")
            {
                await ProcessRepositories(orgName, repositories[repositories.Count - 1].repoId);
            }
        }
        private static async Task<List<Repository>> FetchRepositories(string orgName, int sinceId)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask = client.GetStreamAsync($"https://api.github.com/orgs/{orgName}/repos?sort=updated&since={sinceId}");
            var repositories = await JsonSerializer.DeserializeAsync<List<Repository>>(await streamTask);

            return repositories;
        }
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter the name of the github org you want to see.");
            var input = Console.ReadLine();

            await ProcessRepositories(input, 0);

            

            


        }

    }
}
