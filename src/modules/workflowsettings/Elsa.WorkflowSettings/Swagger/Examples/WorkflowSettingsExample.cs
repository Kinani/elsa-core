using System;
using Elsa.WorkflowSettings.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Elsa.WorkflowSettings.Swagger.Examples
{
    public class WorkflowSettingsExample : IExamplesProvider<WorkflowSetting>
    {
        public WorkflowSetting GetExamples()
        {
            return new()
            {
                Id = Guid.NewGuid().ToString("N"),
                WorkflowBlueprintId = Guid.NewGuid().ToString("N"),
                Key = "Disabled",
                Value = "true"
            };
        }
    }
}