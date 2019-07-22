using Microsoft.Azure.CognitiveServices.ContentModerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogs.Data
{
    public static class Censurator
    {
        private static readonly string AzureRegion = "westeurope";
        //https://censurator.cognitiveservices.azure.com/contentmoderator
        private static readonly string AzureBaseURL =
            $"https://{AzureRegion}.api.cognitive.microsoft.com";
        private static readonly string CMSubscriptionKey = "90bef9c62e4b46709213c2b5f7e786ef";

        public static ContentModeratorClient NewClient()
        {
            ContentModeratorClient client = new ContentModeratorClient(new ApiKeyServiceClientCredentials(CMSubscriptionKey));

            client.Endpoint = AzureBaseURL;
            return client;
        }
    }
}
