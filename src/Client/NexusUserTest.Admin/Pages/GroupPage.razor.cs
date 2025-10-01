using Microsoft.AspNetCore.Components;
using NexusUserTest.Common;
using NexusUserTest.Shared;
using NexusUserTest.Shared.NexusBlazor;
using NexusUserTest.Shared.Services;

namespace NexusUserTest.Admin.Pages
{
    public partial class GroupPage
    {
        [Inject]
        public IAPIService? ServiceAPI { get; set; }
        [Inject]
        public INexusNotificationService? NotificationService { get; set; }
        [Inject]
        public INexusDialogService? DialogService { get; set; }

        private NexusTableGrid<GroupEditDTO>? NexusTable;
        private readonly NexusTableGridEditMode EditMode = NexusTableGridEditMode.Single;
        private readonly NexusTableGridSelectionMode SelectMode = NexusTableGridSelectionMode.Single;

        private List<GroupEditDTO>? Items;
        private IEnumerable<SelectItem>? SpecializationSelects;

        public bool IsCrud => NexusTable != null
            && (NexusTable.InsertedItems.Count > 0 || NexusTable.EditedItems.Count > 0);
        public bool IsSelected => IsCrud || !NexusTable!.IsRowsSelected;
        public bool IsSaveCancel => !IsCrud;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadData();
                await InvokeAsync(StateHasChanged);
            }
        }

        private async Task LoadData()
        {
            var g = await ServiceAPI!.GroupService.GetAllEditGroup("Specialization,GroupUser");
            Items = [.. g];
        }

        public async Task Insert()
        {
            var selects = await ServiceAPI!.SpecializationService.GetSpecializationSelect();
            SpecializationSelects = selects.Data;
            await NexusTable!.InsertRow(new GroupEditDTO { Begin = DateTime.Now, End = DateTime.Now });
        }

        public async Task Edit()
        {
            if (NexusTable != null && NexusTable.SelectedRows.Count != 0)
            {
                //SpecializationSelects = await ServiceAPI!.SpecializationService.GetSpecializationSelect();
                if (EditMode == NexusTableGridEditMode.Multiple
                    && SelectMode == NexusTableGridSelectionMode.Multiple)
                {
                    foreach (var selectRow in NexusTable.SelectedRows)
                    {
                        NexusTable.EditRow(selectRow);
                    }
                }
                else
                {
                    var data = NexusTable.SelectedRows.First();
                    NexusTable.EditRow(data);
                }
            }
        }

        public async Task Save()
        {
            if (NexusTable!.InsertedItems.Count == 0 && NexusTable!.EditedItems.Count > 0)
            {
                var data = NexusTable!.EditedItems.First();
                await Update(data);
            }
            else
            {
                var data = NexusTable!.InsertedItems.First();
                await Add(data);
            }
            await NexusTable.Reload();
        }

        public async Task Add(GroupEditDTO entity)
        {
            var data = await ServiceAPI!.GroupService.AddGroup(entity, "Specialization,GroupUser");
            if (data != null)
            {
                NexusTable!.Data.Add(data);
                await NexusTable.SelectRow(data);
                NotificationService!.ShowSuccess("Группа добавлена", "Успех");
            }
        }

        public async Task Update(GroupEditDTO entity)
        {
            var data = await ServiceAPI!.GroupService.UpdateGroup(entity, "Specialization,GroupUser");
            if (data != null)
            {
                var index = NexusTable!.Data.FindIndex(s => s.Id == data.Id);
                if (index >= 0)
                    NexusTable.Data[index] = data;
                await NexusTable.SelectRow(data);
                await NexusTable.CancelEditRow(data);
                NotificationService!.ShowSuccess("Группа изменена", "Успех");
            }
        }

        public async Task Delete()
        {
            if (NexusTable != null && NexusTable.SelectedRows.Count != 0)
            {
                var data = NexusTable.SelectedRows.First();
                var settings = new NexusDialogSetting("Удаление группы", $"Вы уверены, что хотите удалить \"{data.Title}\" группу?", "Отменить", "Удалить");
                var result = await DialogService!.Show(settings);
                if (result?.Canceled == false)
                {
                    await ServiceAPI!.GroupService.DeleteGroup(data.Id);
                    NexusTable.RemoveRow(data);
                    NotificationService!.ShowSuccess("Группа удалена", "Успех");
                }
            }
        }

        public async Task Cancel()
        {
            var data = NexusTable!.SelectedRows.First();
            await NexusTable.CancelEditRow(data);
        }

        //public void OnSelecChange(ChangeEventArgs args, GroupDTO item)
        //{
        //    var result = Int32.Parse(args.Value.ToString());
        //    item.SpecializationId = result;
        //    item.SpecializationTitle = SpecializationSelectItems.First(s => s.Value == result).Text;
        //}
    }
}
