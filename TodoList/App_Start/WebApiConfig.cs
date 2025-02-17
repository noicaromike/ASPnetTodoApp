﻿using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http.Cors;
namespace TodoList
{
    public static class WebApiConfig
    {
        public class CustomJsonFormatter : JsonMediaTypeFormatter
        {
            public CustomJsonFormatter()
          {
              this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
          }
            public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediatype)
           {
               base.SetDefaultContentHeaders(type, headers, mediatype);
                headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
        }
        
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.Add(new CustomJsonFormatter());
            //config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            
            // 1st params origin https://localhost:44360/
            // 2nd params header content type
            // 3rd params method get post put delete etc
            EnableCorsAttribute cors = new EnableCorsAttribute("*","*","*");
            config.EnableCors(cors);
        }
    }
}
