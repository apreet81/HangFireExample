using Hangfire;
using Owin;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace HangFireExample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //GlobalConfiguration.Configuration
            //    .UseSqlServerStorage("Data Source=.;Initial Catalog=HangFireExample;User ID=sa;Password=12345");
            GlobalConfiguration.Configuration
                    .UseSqlServerStorage("Data Source=self-dzone.database.windows.net;Initial Catalog=test;User ID=Amanpreet;Password=Mentis123");

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            RecurringJob.AddOrUpdate(() => ExecuteWebhookRequest(), "*/10 * * * *");
        }

        public void ExecuteWebhookRequest()
        {
            var client = new RestClient("http://webhook.dzone.in/6709d976");
            var request = new RestRequest(Method.POST);
            string currentDateTime = DateTime.Now.ToString();
            request.AddJsonBody(currentDateTime + "From Godaddy hosting");
            IRestResponse response = client.Execute(request);
        }
    }
}