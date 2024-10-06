using ContentManager.Api.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ContentManager.Api.Persistence.Design;

public class DesignTimeFactory : IDesignTimeDbContextFactory<ApplicationContext> {
    ApplicationContext IDesignTimeDbContextFactory<ApplicationContext>.CreateDbContext(string[] args) {
        var config = new ConfigurationBuilder().AddUserSecrets<ApplicationContext>().Build();

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        optionsBuilder.ConfigureOptions(config.GetDataSource());

        return new ApplicationContext(optionsBuilder.Options);
    }
}