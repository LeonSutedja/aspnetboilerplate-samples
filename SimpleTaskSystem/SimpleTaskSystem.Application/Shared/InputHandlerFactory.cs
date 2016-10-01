namespace SimpleTaskSystem.Shared
{
    using Abp.Dependency;

    public interface IInputHandler<T>
    {
        void Handle(T input);
    }

    public interface IInputHandlerFactory
    {
        IInputHandler<T> CreateInputHandler<T>();
    }

    public class InputHandlerFactory : IInputHandlerFactory
    {
        private readonly IIocResolver _iocResolver;

        public InputHandlerFactory(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        public IInputHandler<T> CreateInputHandler<T>()
            => (IInputHandler<T>)_iocResolver.Resolve(typeof(IInputHandler<>).MakeGenericType(typeof(T)));
    }
}
