using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;

namespace SuperHero2Client
{
    //ヒーローのクラスを作成
    public class Hero
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
    class Program
    {

        static int id = 0;

        static void Main(string[] args) {
            Console.WriteLine("GETコマンドの実行");
            ListAllHeroes();
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("POSTコマンドの実行");
            AddHero();
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("DELETEコマンドの実行");
            DeleteHero(id);
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("PUTコマンドの実行");
            UpdateHero();
            Console.Read();
        }

        //Getコマンドの実行
        static async void ListAllHeroes() {
            //HttpClientのインスタンスを作成する。
            var client = new HttpClient();
            //↓ ASP.NET Web APIのライブラリではHTTPヘッダでJSONの指定がいらない
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //GETコマンドを非同期で実行する。
            HttpResponseMessage response = await client.GetAsync("http://localhost:35833/api/heroes");
            if (response.IsSuccessStatusCode) {
                Console.WriteLine("--Get Result--");
                //コンテンツの取得を非同期で実行
                var heroes = await response.Content.ReadAsAsync<List<Hero>>();
                foreach (var hero in heroes) {
                    Console.WriteLine("{0}\t: {1}", hero.Id, hero.Name);
                }
                id = heroes.OrderByDescending(h => h.Id).First().Id;
            }
            client.Dispose();
        }

        //POSTコマンドの実行
        static async void AddHero() {
            var client = new HttpClient();
            string name = "仮面ライダーアギト";
            //POSTコマンドをJSONを指定して非同期で実行
            HttpResponseMessage response = await client.PostAsJsonAsync<string>("http://localhost:35833/api/heroes", name);
            Console.WriteLine("--POST resut--");
            Console.WriteLine("Status: {0}", (int)response.StatusCode);
            if (response.IsSuccessStatusCode) {
                string m = await response.Content.ReadAsStringAsync();
                Console.WriteLine(m);
            }
            client.Dispose();
        }

        //DELETEコマンドの実行
        static async void DeleteHero(int id) {
            var client = new HttpClient();
            //DELETEコマンドを非同期で実行
            Console.WriteLine("== Delete Hero ID : {0} ==", id);
            var url = string.Format("http://localhost:35833/api/heroes?id={0}", id);
            var response = await client.DeleteAsync(url);
            Console.WriteLine("--DELETE Result Status Code--");
            Console.WriteLine("Status: {0}", (int)response.StatusCode);
            client.Dispose();
        }

        //PUTコマンドの実行
        static async void UpdateHero() {
            string url = "http://localhost:35833/api/heroes?id=2";
            string name = "モロボシダン";
            var client = new HttpClient();
            //PUTコマンドをJSONを指定して非同期で実行
            var response = await client.PutAsJsonAsync<string>(url, name);
            Console.WriteLine("-- PUT Result Status Code --");
            Console.WriteLine("Status: {0}", (int)response.StatusCode);
            if (response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("-- PUT result --");
                Console.WriteLine(content);
            }
            client.Dispose();
        }
    }
}
