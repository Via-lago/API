using Client.Models;
using Client.Repositories.Interface;
using Client.ViewModels;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net.Http.Headers;

namespace Client.Repositories.Data
{
    public class EmployeRepository : GeneralRepository<Employee, Guid>, IEmployeRepository
    {
            private readonly HttpClient httpClient;
            private readonly string request;

        public EmployeRepository(string request = "Employee/") : base(request)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7274/api/")
            };
            this.request = request;
        }

        public async Task<ResponseListVM<GetAllEmployee>> GetAll()
        {
            ResponseListVM<GetAllEmployee> entityVM = null;
            //httpClient.DefaultRequestHeaders.Authorization =new AuthenticationHeaderValue("bearear",Token)
            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseListVM<GetAllEmployee>>(apiResponse);
            }
            return entityVM;
        }
    }
}

