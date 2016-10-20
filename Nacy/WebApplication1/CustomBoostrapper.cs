using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    using Nancy;
    using Nancy.Bootstrapper;
    using Nancy.Conventions;
    using Nancy.TinyIoc;
    using System.IO;

    public class CustomBoostrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("JavaScript", @"JavaScript")
            );
        }



        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            StaticConfiguration.EnableRequestTracing = true;
            StaticConfiguration.DisableErrorTraces = false;
        }
         protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
         {
             base.RequestStartup(container, pipelines, context);
             pipelines.BeforeRequest += ctx => 
             {

                 var a = ctx;
                 object obj = a;
               
                 return null;
             };
             pipelines.AfterRequest += ctx => 
             {
                 var a = ctx;
                 object obj = a;
                 obj = obj;
             
             };
             pipelines.OnError += (ctx,ex) =>
             {
                 string strd = ex.Message.ToString();
                 StreamWriter strdd = new StreamWriter(@"D:\w.txt",true);
                 strdd.Write(strd);
                 strdd.Flush();
                 strdd.Close();
                 return null;
             };
         }       
    }
}