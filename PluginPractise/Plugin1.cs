using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.Xrm.Sdk;

namespace PluginPractise
{
    public class Plugin1 : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext pluginExecutionContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory organizationServiceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService organizationService = (IOrganizationService)serviceProvider.GetService(typeof(IOrganizationService));
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            organizationService = organizationServiceFactory.CreateOrganizationService(pluginExecutionContext.UserId);

            if (pluginExecutionContext.MessageName != "Create")
                return;

            if (pluginExecutionContext.InputParameters.Contains("Target") && pluginExecutionContext.InputParameters["Target"] is Entity)
            {
                Entity entity = (Entity)pluginExecutionContext.InputParameters["Target"];//Obtain the target Entity from Imput parameters
                if (entity.LogicalName != "account")
                    return;

                try
                {
                    tracingService.Trace("under trace" + pluginExecutionContext.Stage.ToString());
                    //try for post operation
                    //if (pluginExecutionContext.Stage==40)// disadvantage is it is having multiple call to server
                    //{
                    //    tracingService.Trace("entered");
                    //    Entity updateaccount = new Entity("account");
                    //    tracingService.Trace("before" + entity.Id);
                    //    updateaccount.Id = entity.Id;
                    //    tracingService.Trace("id");
                    //    updateaccount["description"] = "Shilpi";
                    //    tracingService.Trace("des value");
                    //    organizationService.Update(updateaccount);   
                    //}
                   // Pre - Operation approach
                    entity.Attributes.Add("description", "Shilpi Pre-operation");

                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }
    }
}
