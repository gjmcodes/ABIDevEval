using Ambev.DeveloperEvaluation.Application;
using Ambev.DeveloperEvaluation.BUS;
using Ambev.DeveloperEvaluation.Common.HealthChecks;
using Ambev.DeveloperEvaluation.Common.Logging;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Queries;
using Ambev.DeveloperEvaluation.IoC;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.ReadOnlyRepositories;
using Ambev.DeveloperEvaluation.WebApi.Faker;
using Ambev.DeveloperEvaluation.WebApi.Middleware;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Ambev.DeveloperEvaluation.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Log.Information("Starting web application");

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.AddDefaultLogging();
            builder.Logging.AddConsole();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.AddBasicHealthChecks();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DefaultContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
                )
            );
            builder.Services.AddScoped<ReadOnlyContext>(sp =>
            {
                var nosqlConnString = builder.Configuration.GetConnectionString("ReadOnlyConnection").Split(";");
                var server = nosqlConnString[0];
                var database = nosqlConnString[1];

                var ctx = new ReadOnlyContext(server, database);

                return ctx;
            });

            builder.Services.AddJwtAuthentication(builder.Configuration);
            builder.RegisterDependencies();

            builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(ApplicationLayer).Assembly,
                    typeof(Program).Assembly
                );
            });

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            builder.Services.AddScoped<IBUS, Bus>();
            var app = builder.Build();
            app.UseMiddleware<ValidationExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseBasicHealthChecks();

            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetRequiredService<DefaultContext>();
                ctx.Database.EnsureCreated();

                var rdCtx = scope.ServiceProvider.GetRequiredService<ReadOnlyContext>();

                var prodColl = rdCtx.GetCollection<ProductReadOnlyRepository>(ProductReadOnlyRepository.COLLECTION_NAME);
                if(prodColl.EstimatedDocumentCount() <= 0)
                {
                    var prods = FakeDataGenerator.GenerateFakeProductsQuery(50);
                    rdCtx.CreateCollection(ProductReadOnlyRepository.COLLECTION_NAME, prods);
                }

                var userColl = rdCtx.GetCollection<UserExternalQuery>(UserReadOnlyRepository.COLLECTION_NAME);
                if (userColl.EstimatedDocumentCount() <= 0)
                {
                    var users = FakeDataGenerator.GenerateFakeUsersQuery(5);
                    rdCtx.CreateCollection(UserReadOnlyRepository.COLLECTION_NAME, users);
                }

                var branchColl = rdCtx.GetCollection<BranchExternalQuery>(BranchReadOnlyRepository.COLLECTION_NAME);
                if (branchColl.EstimatedDocumentCount() <= 0)
                {
                    var branches = FakeDataGenerator.GenerateFakeBranchesQuery(2);
                    rdCtx.CreateCollection(BranchReadOnlyRepository.COLLECTION_NAME, branches);
                }
            }
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
