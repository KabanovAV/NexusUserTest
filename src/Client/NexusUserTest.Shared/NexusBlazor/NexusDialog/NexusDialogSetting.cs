namespace NexusUserTest.Shared.NexusBlazor
{
    public class NexusDialogSetting
    {
        public string HeadingTitle { get; set; }
        public bool IsHeadingCloseButton { get; set; }
        public string Message { get; set; }
        public string CancelButtonTitle { get; set; }
        public string ApplyButtonTitle { get; set; }
        public string BaseClass { get; set; }
        public string AdditionalClasses { get; set; }
        public string IconClass { get; set; }

        public NexusDialogSetting(string headingTitle, string message, string applyButtonTitle)
            : this(headingTitle, false, message, "", applyButtonTitle, "", "", "") { }

        public NexusDialogSetting(string headingTitle, bool isHeadingCloseButton, string message, string applyButtonTitle)
            : this(headingTitle, isHeadingCloseButton, message, "", applyButtonTitle, "", "", "") { }

        public NexusDialogSetting(string headingTitle, string message, string cancelButtonTitle, string applyButtonTitle)
            : this(headingTitle, false, message, cancelButtonTitle, applyButtonTitle, "", "", "") { }

        public NexusDialogSetting(string headingTitle, bool isHeadingCloseButton, string message, string cancelButtonTitle, string applyButtonTitle)
            : this(headingTitle, isHeadingCloseButton, message, cancelButtonTitle, applyButtonTitle, "", "", "") { }

        public NexusDialogSetting(string headingTitle, bool isHeadingCloseButton, string message, string cancelButtonTitle, string applyButtonTitle,
            string baseClass, string additionalClasses, string iconClass)
        {
            HeadingTitle = headingTitle;
            IsHeadingCloseButton = isHeadingCloseButton;
            Message = message;
            CancelButtonTitle = cancelButtonTitle;
            ApplyButtonTitle = applyButtonTitle;
            BaseClass = baseClass;
            AdditionalClasses = additionalClasses;
            IconClass = iconClass;
        }
    }
}
