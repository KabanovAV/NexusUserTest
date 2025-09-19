namespace NexusUserTest.Common.DTOs
{
    public class GroupInfoDetailsDTO : GroupInfoDTO
    {
        public SettingDTO Setting { get; set; }
        public List<GroupUserDTO> User { get; set; }
    }
}
