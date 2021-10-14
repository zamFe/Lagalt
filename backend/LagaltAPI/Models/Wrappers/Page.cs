using System.Collections.Generic;

namespace LagaltAPI.Models.Wrappers
{
    /// <summary> Data model used to split database contents into separate pages. </summary>
    /// <typeparam name="T"> Data to be contained in the page. </typeparam>
    public class Page<T>
    {
        public IEnumerable<T> Results { get; set; }

        // Constructor.
        public Page(IEnumerable<T> data)
        {
            Results = data;
        }
    }
}
