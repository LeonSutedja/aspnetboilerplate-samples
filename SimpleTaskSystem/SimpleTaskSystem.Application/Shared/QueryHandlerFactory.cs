namespace SimpleTaskSystem.Shared
{
    using Abp.Dependency;

    public interface IQueryHandler<in T, out T1>
    {
        T1 Handle(T input);
    }

    public interface IQueryHandlerFactory
    {
        IQueryHandler<T, T1> CreateHandler<T, T1>();
    }

    public class QueryHandlerFactory : IQueryHandlerFactory
    {
        private readonly IIocResolver _iocResolver;

        public QueryHandlerFactory(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        public IQueryHandler<T, T1> CreateHandler<T, T1>()
            => (IQueryHandler<T, T1>)_iocResolver.Resolve(typeof(IQueryHandler<,>).MakeGenericType(typeof(T), typeof(T1)));
    }
}
