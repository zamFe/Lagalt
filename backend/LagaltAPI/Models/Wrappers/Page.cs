using System.Collections.Generic;

namespace LagaltAPI.Models.Wrappers
{
    /// <summary> Data model used to split database contents into separate pages. </summary>
    /// <typeparam name="T"> Data to be contained in the page. </typeparam>
    public class Page<T>
    {
        /// <summary> A URI pointing to the next page of results, should there be any. </summary>
        public string Next { get; set; }

        /// <summary>
        ///     A URI pointing to the previous page of results, should there be any.
        /// </summary>
        public string Previous { get; set; }

        /// <summary> An enumerable containing the data requested from the database. </summary>
        public IEnumerable<T> Results { get; set; }

        // Constructor.
        public Page(ICollection<T> data, int totalEntities, PageRange range, string baseUri)
        {
            // TODO - make previous a bit better?

            Next = totalEntities <= range.Limit + range.Offset - 1
                ? ""
                : baseUri + $"?offset={range.Offset + range.Limit}&limit={range.Limit}";
            Previous = range.Offset == 1
                ? ""
                : baseUri + $"?offset={range.Offset - range.Limit}&limit={range.Limit}";
            Results = data;
        }
    }
}
