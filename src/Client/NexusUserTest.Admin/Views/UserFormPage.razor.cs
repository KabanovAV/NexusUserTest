using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using NexusUserTest.Common.DTOs;
using NexusUserTest.Shared;

namespace NexusUserTest.Admin.Views
{
    public partial class UserFormPage
    {
        [Parameter]
        public UserDTO Data { get; set; }
        [Parameter]
        public IEnumerable<SelectItem> GroupSelects { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }
        [Parameter]
        public EventCallback<UserDTO> OnSave { get; set; }

        private EditContext? editContext;
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            Data ??= new();
            editContext = new(Data);
        }
        private void CheckboxChange(int args)
        {
            if (Data.GroupUserItems!.FirstOrDefault(x => x.GroupId == args) is GroupUserCreateDTO group && group != null)
                Data.GroupUserItems!.Remove(group);
            else
            {
                var groupUser = new GroupUserCreateDTO { GroupId = args };
                Data.GroupUserItems!.Add(groupUser);
            }
        }
        private async Task Save()
        {
            if (editContext!.Validate())
            {
                await OnSave.InvokeAsync(Data);
                if (Data.Id == 0)
                    Data.GroupUserItems!.Clear();
            }
        }
        private async Task Cancel()
        {
            await OnCancel.InvokeAsync();
            if (Data.Id == 0)
                Data.GroupUserItems!.Clear();
        }
    }
}
