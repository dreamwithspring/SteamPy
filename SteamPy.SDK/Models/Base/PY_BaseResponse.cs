using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamPy.SDK.Models.Base
{
    public class PY_BaseResponse<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long? timestamp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public T result { get; set; }
    }
}
