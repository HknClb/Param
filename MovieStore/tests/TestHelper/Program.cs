using Application;
using Application.Abstractions.UnitOfWorks.Base;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
IServiceProvider services = new ServiceCollection()
    .AddPersistenceServices(configuration)
    .AddApplicationServices()
    .BuildServiceProvider();

IUnitOfWork unitOfWork = services.GetRequiredService<IUnitOfWork>();
unitOfWork.WriteRepository<Star>().Add(new("Test", "Star"));
unitOfWork.WriteRepository<Director>().Add(new("Test", "Director"));
unitOfWork.WriteRepository<Genre>().Add(new("Test Genre"));
unitOfWork.WriteRepository<Test>().Add(new("Tets asdafg"));
//unitOfWork.Complete();