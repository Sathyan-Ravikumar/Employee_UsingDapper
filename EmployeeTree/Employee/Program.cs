using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Employee.Mapper;
using Employee.Model.Data;
using Employee.Repository.Interface;
using Employee.Repository;
using Employee.Service.Interface;
using Employee.Service;
using Employee.Service.NonActionMethod;

namespace YourNamespace
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<ContextFile>();
            builder.Services.AddAutoMapper(typeof(MapperContext));
            builder.Services.AddScoped<IEmployeeRepository, EmployeesRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<INonAction,Department_NonAction>();
            var app = builder.Build();

                var keyVaultURL = builder.Configuration.GetSection("KeyVault:KeyVaultURL").Value;
                var keyVaultClientId = builder.Configuration.GetSection("KeyVault:ClientID").Value;
                var keyVaultClientSecret = builder.Configuration.GetSection("KeyVault:ClientSecret").Value;
                var keyVaultDirectoryID = builder.Configuration.GetSection("KeyVault:DirectoryID").Value;

                var credential = new ClientSecretCredential(keyVaultDirectoryID, keyVaultClientId, keyVaultClientSecret);
                var secretClient = new SecretClient(new Uri(keyVaultURL), credential);

                // Retrieve the connection string from Azure Key Vault
                var connectionStringSecret = secretClient.GetSecret("DatabaseConnection");
                var serviceBusConnection = secretClient.GetSecret("ServiceBusConnection");
                var queueName = secretClient.GetSecret("DepartmentQueueName");
                // Update the connection string in the configuration
                builder.Configuration["ConnectionStrings:Connection"] = connectionStringSecret.Value.Value;
                builder.Configuration["ConnectionStrings:ServiceBus"] = serviceBusConnection.Value.Value;
                builder.Configuration["QueueName:DepartmentQueue"] = queueName.Value.Value;
        

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
