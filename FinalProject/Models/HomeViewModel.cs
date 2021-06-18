using System.Collections.Generic;

namespace FinalProject.Models
{
    public class HomeViewModel
    {
        public IEnumerable<SearchResultsViewModel> Recommendations { get; set; }
        public SearchViewModel Search { get; set; }
    }
}
