using chatApp.Services;

namespace chatApp;

public class MessageTemplateSelector: DataTemplateSelector
{
    public MessageService Service { get; set; }
    
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
        if (message.Sender == Service.Name)
        {
            return SenderMessageTemplate;
        }

        return ReceiverMessageTemplate;
    }
}