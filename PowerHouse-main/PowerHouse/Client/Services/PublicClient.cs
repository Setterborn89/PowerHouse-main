using Newtonsoft.Json;
using PowerHouse.Client.Shared;
using PowerHouse.Shared.DTO;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PowerHouse.Client.Services
{
    public class PublicClient
    {
        private HttpClient _client;


        public PublicClient(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public async Task<List<ConversationDTO>> GetPublicConversations()
        {
            try
            {
                var result = await _client.GetAsync("/api/conversation/public");
                var data = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<List<ConversationDTO>>(data) ?? new();
                return response;
            }
            catch
            {
                return new List<ConversationDTO>();
            }
        }
		public async Task<List<ConversationDTO>> GetMostPopularConversations()
		{
			try
			{
				var result = await _client.GetAsync("/api/conversation/public/mostpopular");
                var data = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<List<ConversationDTO>>(data) ?? new();
                return response;
			}
			catch(Exception e)
			{
				return new List<ConversationDTO>();
			}
		}
	}
}