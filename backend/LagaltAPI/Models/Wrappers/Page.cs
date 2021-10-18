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
        public Page(string next, string previous, IEnumerable<T> data)
        {
            Next = next;
            Previous = previous;
            Results = data;
        }
    }
}
