namespace chatApp;

public class Message
{
    public string Sender { get; set; }
    public string Text { get; set; }
    public string Time { get; set; }

    public Message(string text, string sender)
    {
        Sender = sender;
        Text = text;
        Time = DateTime.Now.ToShortTimeString();
    }
}