using Grpc.Service.Core.Dependency;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Util
{
    public class KafkaConfig
    {
        private static object normalCommandBusLock = new object();

        private static string normalCommandBusTopicTitle;

        public static string NormalCommandBusTopicTitle
        {
            get
            {
                lock (normalCommandBusLock)
                {
                    if (string.IsNullOrEmpty(normalCommandBusTopicTitle))
                    {
                        var config = IocManager.Instance.Resolve<IConfigurationRoot>();
                        string kafkaIP = string.Empty;
                        if (config != null)
                        {
                            normalCommandBusTopicTitle = config.GetSection("NormalCommandBus.TopicTitle").Value?.ToString() ?? string.Empty;
                        }
                    }

                    return normalCommandBusTopicTitle;
                }
            }
        }

        private static object commandBusLock = new object();

        private static string commandBusTopicTitle;

        public static string CommandBusTopicTitle
        {
            get
            {
                lock (commandBusLock)
                {
                    if (string.IsNullOrEmpty(commandBusTopicTitle))
                    {
                        var config = IocManager.Instance.Resolve<IConfigurationRoot>();
                        string kafkaIP = string.Empty;
                        if (config != null)
                        {
                            commandBusTopicTitle = config.GetSection("SortCommandBus.TopicTitle").Value?.ToString() ?? string.Empty;
                        }
                    }

                    return commandBusTopicTitle;
                }
            }
        }

        private static object eventBusLock = new object();

        private static string eventBusTopicTitle;

        public static string EventBusTopicTitle
        {
            get
            {
                lock (eventBusLock)
                {
                    if (string.IsNullOrEmpty(eventBusTopicTitle))
                    {
                        var config = IocManager.Instance.Resolve<IConfigurationRoot>();
                        string kafkaIP = string.Empty;
                        if (config != null)
                        {
                            eventBusTopicTitle = config.GetSection("EventBus.TopicTitle").Value?.ToString() ?? string.Empty;
                        }
                    }

                    return eventBusTopicTitle;
                }
            }
        }
    }
}
