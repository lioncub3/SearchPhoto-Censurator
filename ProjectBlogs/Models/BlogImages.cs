using Microsoft.Azure.CognitiveServices.Search.ImageSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBlogs.Models
{
    public class BlogImages
    {
        public Blog blog { get; set; }
        public IEnumerable<ImageObject> images { get; set; }
    }
}
