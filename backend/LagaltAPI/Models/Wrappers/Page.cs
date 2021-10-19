using System.Collections.Generic;

namespace LagaltAPI.Models.Wrappers
{
    /// <summary> Data model used to split database contents into separate pages. </summary>
    /// <typeparam name="T"> Data to be contained in the page. </typeparam>
    public class Page<T>
    {
        public string Next { get; set; }
        public string Previous { get; set; }
        public IEnumerable<T> Results { get; set; }

        // Constructor.
        public Page(ICollection<T> data, PageRange filter, string baseUri)
        {
            // TODO - Fix next page sometimes being empty.
            //        Have to instead get total valid results,
            //        and then see if there is enough for another page
            Next = data.Count < filter.Limit
                ? ""
                : baseUri + $"?offset={filter.Offset + filter.Limit}&limit={filter.Limit}";
            Previous = filter.Offset == 1
                ? ""
                : baseUri + $"?offset={filter.Offset - filter.Limit}&limit={filter.Limit}";
            Results = data;
        }
    }
}
