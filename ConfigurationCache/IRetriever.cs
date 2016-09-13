using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagOUtils.Messages
{
    /// <summary>
    /// Retrieve configuraiton values from some source, based on a category.
    /// </summary>
    /// <remarks>
    /// Some sources may keep configuration values in more than one location
    /// (e.g. different tables, different XML nodes, different files). The 
    /// category is used identify what source is used.
    /// </remarks>
    public interface ICategoryRetriever<TCategory>
    {
        string RetrieveValue(TCategory category, string key);
    }
}
