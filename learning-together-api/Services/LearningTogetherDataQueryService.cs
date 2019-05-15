namespace learning_together_api.Services
{
    using System.Collections.Generic;
    using Data;
    using pathways_common.Interfaces;
    using pathways_common.Services;

    public abstract class LearningTogetherDataQueryService<T> : DataQueryService<T, DataContext>
        where T : IIdEntity
    {
        protected LearningTogetherDataQueryService(DataContext context, IEnumerable<T> collection) : base(context, collection)
        {
        }
    }
}