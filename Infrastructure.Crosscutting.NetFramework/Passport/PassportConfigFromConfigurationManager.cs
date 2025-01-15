using IdentityModel.Client;
using Infrastructure.Crosscutting.Passport;
using System.Configuration;
using System.Net;
using System.Text;


namespace Infrastructure.Crosscutting.NetFramework.Passport
{
    public sealed class PassportConfigFromConfigurationManager : IPassportConfig
    {
        public bool UsaPassport
        {
            get
            {
                var value = ConfigurationManager.AppSettings["usa_passport"];
                return value == "true";
            }
        }

        public string Authority
        {
            get
            {
                return ConfigurationManager.AppSettings["auth_authority"];
            }
        }

        public async Task<string> GenerateToken()
        {

            var client = new HttpClient();

            var response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = $"{Authority}",
                ClientId = "client",
                ClientSecret = "secret"
            });

            if (!response.IsError) return response.AccessToken;

            return null;

        }

        public void SincronizarPassports(string instituicao, List<string> passports, string token)
        {
            teste(instituicao, passports, token);
        }


        private void teste(string instituicao, List<string> passports, string token)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(Authority + "/api/instituicao/atualizar-vinculo-instituicao");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {

                var json = new StringBuilder();
                json.Append("{");
                json.AppendFormat("\"idInsituicao\": {0},", instituicao);
                json.AppendFormat("\"ids\": [{0}]", string.Join(",", passports));
                json.Append("}");

                streamWriter.Write(json.ToString());
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }
    }
}
