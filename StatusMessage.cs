namespace Ablefish.Blazor.Components
{
    public enum StatusType { None, Success, Information, Warning, Error };

    public class StatusMessage
    {
        public StatusType Status { get; internal set; }
        public string Text { get; internal set; } = string.Empty;
        public string Header { get; internal set; } = string.Empty;

        public bool IsEmpty { get => (Status == StatusType.None); }
        public bool HasData { get => (Status != StatusType.None || !string.IsNullOrEmpty(Text)); }
        public void Clear()
        {
            Header = string.Empty;
            Text = string.Empty;
            Status = StatusType.None;
        }

        public void SetException(Exception exception)
        {
            Status = StatusType.Error;
            Header = "Unexpected Error";
            Text = exception.Message;
        }
        public void Update(string statusHeader, string statusText, StatusType statusType)
        {
            Status = statusType;
            Header = statusHeader;
            Text = statusText;
        }
    }
}