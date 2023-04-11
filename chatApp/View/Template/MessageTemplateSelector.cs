using chatApp.Services;

namespace chatApp;

public class MessageTemplateSelector: DataTemplateSelector
{
    public DataTemplate SenderMessageTemplate { get; set; }
    public DataTemplate ReceiverMessageTemplate { get; set; }
    public DataTemplate ServiceMessageTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        var message = (Message)item;

        if (message.IsService)
        {
            return ServiceMessageTemplate;
        }
        if (message.Sender != "")
        {
            return SenderMessageTemplate;
        }

        return ReceiverMessageTemplate;
    }
}