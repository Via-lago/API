﻿
using Client.Models;
using Client.Repositories.Interface;
using Client.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repositories.Data
{
    public class AccountRepository : GeneralRepository<Account, string>, IAccountRepository
    {
        private readonly HttpClient httpClient;
        private readonly string request;

        public AccountRepository(string request = "Accounts/") : base(request)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7274/api/")
            };
            this.request = request;
        }

        public async Task<ResponseViewModel<string>> Login(LoginVM entity)
        {
            ResponseViewModel<string> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request + "Login", content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseViewModel<string>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseMessageVM> Register(RegisterVM entity)
        {
            ResponseMessageVM entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            using (var response = httpClient.PostAsync(request + "Register", content).Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseMessageVM>(apiResponse);
            }
            return entityVM;
        }
    }
}