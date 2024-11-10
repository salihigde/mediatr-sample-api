using System.Collections.Generic;

namespace MediatrSample.Api.ViewModels
{
    /// <summary>
    /// used for both validation error and exception cases which
    /// doesn't have response to return
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// </summary>
        public List<string> Messages { get; set; } = new List<string>();
    }

    /// <summary>
    /// used when api returns 200 in which we expect another object with returned data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T> : ApiResponse where T : class
    {
        /// <summary>
        /// generic result
        /// </summary>
        public T Result { get; set; }
    }
}
