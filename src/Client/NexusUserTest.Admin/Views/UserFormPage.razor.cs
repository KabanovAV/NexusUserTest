using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using NexusUserTest.Common;
using System.ComponentModel;
using System.Reflection;

namespace NexusUserTest.Admin.Views
{
    public partial class UserFormPage
    {
        [Parameter, EditorRequired]
        public UserAdminDTO Data { get; set; }
        [Parameter]
        public IEnumerable<SelectItem>? GroupSelects { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }
        [Parameter]
        public EventCallback<UserAdminDTO> OnSave { get; set; }

        private EditContext? editContext;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            Data ??= new();
            editContext = new(Data);
        }

        private string GetDisplayName(string propertyName)
        {
            var property = typeof(UserAdminDTO).GetProperty(propertyName);
            var displayAttribute = property?.GetCustomAttribute<DisplayNameAttribute>();
            return displayAttribute?.DisplayName ?? propertyName;
        }

        private void CheckboxChange(int args)
        {
            if (Data.GroupUserItems!.FirstOrDefault(x => x.GroupId == args) is GroupUserAdminDTO group && group != null)
                Data.GroupUserItems!.Remove(group);
            else
            {
                var groupUser = new GroupUserAdminDTO { GroupId = args, Status = 1 };
                Data.GroupUserItems!.Add(groupUser);
            }
        }

        private async Task Save()
        {
            if (editContext!.Validate())
            {
                await OnSave.InvokeAsync(Data);
                ResetData();
            }
        }
        private async Task Cancel()
        {
            await OnCancel.InvokeAsync();
            ResetData();
        }

        private void ResetData()
        {
            if (Data.Id == 0)
                Data.GroupUserItems!.Clear();
        }
    }
}
