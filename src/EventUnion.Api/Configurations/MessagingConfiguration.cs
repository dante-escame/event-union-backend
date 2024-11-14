using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SQS;
using MassTransit;

namespace EventUnion.Api.Configurations;

public static class MessagingConfiguration
{
    public static void ConfigureMessagingServices(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("MessageBroker");

        var amazonSqsSection = section.GetSection("AmazonSqs");
        var host = amazonSqsSection.GetValue<string>("Host");
        var accessKey = amazonSqsSection.GetValue<string>("AccessKey");
        var serviceUrl = amazonSqsSection.GetValue<string>("ServiceUrl");
        var secretKey = amazonSqsSection.GetValue<string>("SecretKey");
        var scopeName = GetScopeName(amazonSqsSection);

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            x.UsingAmazonSqs((context, configurator) =>
            {
                configurator.Host(new Uri(host!), h =>
                {
                    var hasLocalCredentials = !string.IsNullOrWhiteSpace(accessKey) && !string.IsNullOrEmpty(secretKey);
                    if (hasLocalCredentials)
                    {
                        h.AccessKey(accessKey);
                        h.SecretKey(secretKey);

                        h.Config(new AmazonSimpleNotificationServiceConfig { ServiceURL = serviceUrl });
                        h.Config(new AmazonSQSConfig { ServiceURL = serviceUrl });
                    }
                    else
                    {
                        h.Credentials(new EnvironmentVariablesAWSCredentials());
                    }
                });

                configurator.SendTopology.ErrorQueueNameFormatter =
                    new MessageBrokerErrorQueueNameFormatter();
                configurator.SendTopology.DeadLetterQueueNameFormatter =
                    new MessageBrokerDeadLetterQueueNameFormatter();

                configurator.MessageTopology.SetEntityNameFormatter(new CustomEntityNameFormatter(scopeName!));

                configurator.ConfigureEndpoints(context);
            });
        });
    }

    private static string? GetScopeName(IConfigurationSection amazonSqsSection)
    {
        var scopeName = amazonSqsSection.GetValue("ScopeName", string.Empty);

        return string.IsNullOrEmpty(scopeName)
            ? scopeName
            : $"{scopeName.ToLower()}-";
    }
}

public class MessageBrokerErrorQueueNameFormatter : IErrorQueueNameFormatter
{
    public string FormatErrorQueueName(string queueName)
        => $"{queueName}-dead";
}

public class MessageBrokerDeadLetterQueueNameFormatter : IDeadLetterQueueNameFormatter
{
    public string FormatDeadLetterQueueName(string queueName)
        => $"{queueName}-dead";
}

public class CustomEntityNameFormatter(string scopeName) : IEntityNameFormatter
{
    public string FormatEntityName<T>()
    {
        return $"{scopeName}{typeof(T).Name}";
    }
}