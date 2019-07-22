using Microsoft.Azure.CognitiveServices.Search.ImageSearch;
using Microsoft.Azure.CognitiveServices.Search.ImageSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogs.Data
{
    public class SearchPhoto
    {
        string subscriptionKey;

        ImageSearchClient client;

        public SearchPhoto(string subscriptionKey)
        {
            this.subscriptionKey = subscriptionKey;
            client = new ImageSearchClient(new ApiKeyServiceClientCredentials(subscriptionKey));
        }

        public Images GetPhotos(string searchTerm)
        {
            return client.Images.SearchAsync(query: searchTerm).Result;
        }
    }
}
