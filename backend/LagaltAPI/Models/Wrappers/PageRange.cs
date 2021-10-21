namespace LagaltAPI.Models.Wrappers
{
    /// <summary>
    ///     Simple data structure used to specify the range of entities included in a page.
    /// </summary>
    public class PageRange
    {
        /// <summary>
        ///     Specifies the database index (base 1) of the first entity to be included.
        /// </summary>
        public int Offset { get; set; }

        /// <summary> Specifies the upper limit for how many entities to include. </summary>
        public int Limit { get; set; }

        // Constructor.
        public PageRange(int offset = 1, int limit = 10)
        {
            Offset = offset < 1 ? 1 : offset;
            Limit = limit < 1 || limit > 10 ? 10 : limit;
        }
    }
}
