namespace NexusUserTest.Shared.NexusBlazor
{
    public interface INexusDialogService
    {
        public event Action<NexusDialogReference, NexusDialogSetting> OnShow;
        public event Action<Guid, NexusDialogResult?>? OnDialogCloseRequested;

        Task<NexusDialogResult?> Show(NexusDialogSetting setting);
        void Close(Guid dialogId, NexusDialogResult? result);
    }
}