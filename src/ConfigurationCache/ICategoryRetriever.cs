using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagOUtils.ConfigurationCache
{
    /// <summary>
    /// Retrieve configuraiton values from some source, based on a category.
    /// </summary>
    /// <remarks>
    /// Some sources may keep configuration values in more than one
    /// location (e.g. different tables, different XML nodes, different
    /// files). The category is used identify what source is used. By
    /// making the category a type, not only can retrievers use it as
    /// switch value, but they can also use the type as strategy. E.g.
    /// if the source is an xml file, the category class could provide
    /// the root node for its values. The retriever is generic and it
    /// responsible for any conversions from source to the desired type.
    /// </remarks>
    public interface ICategoryRetriever<TCategory>
    {
        T RetrieveValue<T>(TCategory category, string key);
    }
}