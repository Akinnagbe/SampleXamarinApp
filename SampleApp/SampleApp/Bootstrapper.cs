using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp
{
    public class Bootstrapper
    {

        public static MapperConfiguration Configure()
        {
            // AutoMapper.Mapper.Reset();
            //Mapper.Initialize(cfg =>
            //{
            //    cfg.AddProfile<MapperProfile>();
            //});

            //Mapper.Configuration.AssertConfigurationIsValid();
            var configuration = new MapperConfiguration(config =>
            {
                config.AddProfile<MapperProfile>();
            });
            configuration.AssertConfigurationIsValid();
            return configuration;
        }
    }
}
