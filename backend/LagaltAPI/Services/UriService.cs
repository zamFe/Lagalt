namespace LagaltAPI.Services
{
    public class UriService
    {
        private readonly string _baseUri;

        // Constructor.
        public UriService(string baseUri)
        {
            _baseUri = baseUri + "/";
        }

        /// <summary> Gets the base url for where the program is currently running. </summary>
        /// <returns> A string containing the base url. </returns>
        public string GetBaseUrl()
        {
            return _baseUri;
        }
    }
}
