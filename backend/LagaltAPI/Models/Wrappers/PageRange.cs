namespace LagaltAPI.Models.Wrappers
{
    /// <summary>
    ///     Simple data structure used to specify the range of entities included in a page.
    /// </summary>
    public class PageRange
    {
        public int Offset { get; set; }
        public int Limit { get; set; }

        // Constructor.
        public PageRange(int offset = 1, int limit = 10)
        {
            Offset = offset < 1 ? 1 : offset;
            Limit = limit < 1 || limit > 10 ? 10 : limit;
        }
    }
}
