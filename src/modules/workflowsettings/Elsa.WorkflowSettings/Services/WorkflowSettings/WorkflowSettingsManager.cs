using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Elsa.WorkflowSettings.Abstractions.Providers;
using Elsa.WorkflowSettings.Abstractions.Services.WorkflowSettings;
using Elsa.WorkflowSettings.Models;

namespace Elsa.WorkflowSettings.Services.WorkflowSettingsContexts
{
    public class WorkflowSettingsManager : IWorkflowSettingsManager
    {
        private readonly IEnumerable<IWorkflowSettingsProvider> _workflowSettingsProviders;

        public WorkflowSettingsManager(IEnumerable<IWorkflowSettingsProvider> workflowSettingsProviders)
        {
            _workflowSettingsProviders = workflowSettingsProviders;
        }

        public async ValueTask<WorkflowSetting> LoadSettingAsync(WorkflowSetting workflowSetting, CancellationToken cancellationToken = default)
        {
            var providers = _workflowSettingsProviders;

            var value = new ValueTask<WorkflowSetting>(new WorkflowSetting());

            foreach (var provider in providers)
            {
                var providerValue = await provider.GetWorkflowSettingAsync(workflowSetting.WorkflowBlueprintId, workflowSetting.Key, cancellationToken);
                if (providerValue.Value != null)
                {
                    value = new ValueTask<WorkflowSetting>(providerValue);
                }
            }

            return await value;
        }
    }
}