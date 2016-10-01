namespace SimpleTaskSystem
{
    using System.Reflection;
    using Abp.AutoMapper;
    using Abp.Modules;
    using Castle.MicroKernel.Registration;
    using SimpleTaskSystem.Shared;

    /// <summary>
    /// 'Application layer module' for this project.
    /// </summary>
    [DependsOn(typeof(SimpleTaskSystemCoreModule), typeof(AbpAutoMapperModule))]
    public class SimpleTaskSystemApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            //This code is used to register classes to dependency injection system for this assembly using conventions.
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            
            IocManager.IocContainer.Register(Classes.FromThisAssembly()
                .BasedOn(typeof(IInputHandler<>))
                .WithService.AllInterfaces()
                .LifestyleTransient()
                .AllowMultipleMatches());

            IocManager.IocContainer.Register(Component.For(typeof(IInputHandlerFactory))
                .ImplementedBy(typeof(InputHandlerFactory))
                .LifestyleTransient());

            IocManager.IocContainer.Register(Classes.FromThisAssembly()
                .BasedOn(typeof(IQueryHandler<,>))
                .WithService.AllInterfaces()
                .LifestyleTransient()
                .AllowMultipleMatches());

            IocManager.IocContainer.Register(Component.For(typeof(IQueryHandlerFactory))
                .ImplementedBy(typeof(QueryHandlerFactory))
                .LifestyleTransient());

            //We must declare mappings to be able to use AutoMapper
            DtoMappings.Map();
        }
    }   
}