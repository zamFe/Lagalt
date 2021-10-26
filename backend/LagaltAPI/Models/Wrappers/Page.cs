using System.Collections.Generic;

namespace LagaltAPI.Models.Wrappers
{
    /// <summary> Data model used to split database contents into separate pages. </summary>
    /// <typeparam name="T"> Data to be contained in the page. </typeparam>
    public class Page<T>
    {
        /// <summary> How many total entities are available for the query. </summary>
        public int TotalEntities { get; set; }

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
            TotalEntities = totalEntities;

            Next = TotalEntities <= range.Limit + range.Offset - 1
                ? ""
                : baseUri + $"?offset={range.Offset + range.Limit}&limit={range.Limit}";
            Previous = range.Offset == 1
                ? ""
                : baseUri + $"?offset={range.Offset - range.Limit}&limit={range.Limit}";
            Results = data;
        }
    }
}
