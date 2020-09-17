using System;
using System.Collections.Generic;
using System.Text;

namespace Philips.Chatbots.Data.Models.Interfaces
{
    /// <summary>
    /// Dummy interface to differentiate true data models from other classes. 
    /// </summary>
    public interface IDataModel
    {
        public string _id { get; set; }
    }
}
