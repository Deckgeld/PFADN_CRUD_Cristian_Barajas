using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using Ticket.Core;
using Ticket.WebApi.Models;
using Ubiety.Dns.Core;

namespace Ticket.WebApi
{
    public class Checker
    {
        public async Task<bool> JourneysWebApi(int idJourney)
        {
            string apiUrl = $"http://localhost:5001/api/Journey/{idJourney}";
            return await validater(idJourney, apiUrl, true, 5001);
        }


        public async Task<bool> PassagersWebApi(int idPassager)
        {

            string apiUrl = $"http://localhost:5000/api/Passengers/{idPassager}";
            return await validater(idPassager, apiUrl, false, 5000);
        }



        public async Task<bool> validater(int _id, string apiUrl, bool type, int port)
        {
            string loginUrl = $"http://localhost:{port}/api/Auth";
            LoginModel loginModel = new LoginModel()
            {
                Username = "admin@website.com",
                Password = "waP6KEfz%NJtTpvt"
            };
            string jsonLogin = JsonConvert.SerializeObject(loginModel);

            HttpClient client = new HttpClient();
            HttpResponseMessage loginResponse = await client.PostAsync(loginUrl, new StringContent(jsonLogin, System.Text.Encoding.UTF8, "application/json"));

            if (loginResponse.IsSuccessStatusCode)
            {
                var loginResponseData = await loginResponse.Content.ReadAsStringAsync();

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken token = tokenHandler.ReadJwtToken(loginResponseData);

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.RawData);

                
                HttpResponseMessage apiResponse = await client.GetAsync(apiUrl);
                if (apiResponse.StatusCode == HttpStatusCode.OK)
                {
                    string apiResponseData = await apiResponse.Content.ReadAsStringAsync();
                    
                    if (type) {
                        var enti = Newtonsoft.Json.JsonConvert.DeserializeObject<JourneyC>(apiResponseData);
                        if (enti.Id == _id)
                            return true;
                    }
                    else { 
                        var enti = Newtonsoft.Json.JsonConvert.DeserializeObject<PassengerC>(apiResponseData);
                        if (enti.Id == _id)
                            return true;
                    }
                }
            }
            return false;
        }
    }
}
