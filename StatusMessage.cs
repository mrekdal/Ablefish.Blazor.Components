using Microsoft.Extensions.Logging;
using Ablefish.Blazor.Observer;

namespace Ablefish.Blazor.Status
{
    public enum StatusType { None, Success, Information, Warning, Error };

    public class StatusMessage : ISubjectMinimal, IDisposable, IStatusMessage
    {
        private readonly ILogger<StatusMessage>? _logger;
        private readonly SubjectLogged _subjectBase = new("StatusMessage", new LoggerFactory());

        public StatusMessage(ILogger<StatusMessage> logger)
        {
            _logger = logger;
        }

        public StatusType Status { get; internal set; }
        public string Text { get; internal set; } = string.Empty;
        public string Header { get; internal set; } = string.Empty;

        public bool IsEmpty { get => (Status == StatusType.None); }
        public bool HasData { get => (Status != StatusType.None || !string.IsNullOrEmpty(Text)); }

        public void Attach(IObserver observer)
        {
            _subjectBase.Attach(observer);
        }
        public void Detach(IObserver observer)
        {
            _subjectBase.Detach(observer);
        }

        public void Clear()
        {
            Update(string.Empty, string.Empty, StatusType.None);
        }

        public void Dispose()
        {
            _subjectBase.DetachAll();
        }

        public void SetException(Exception exception)
        {
            Update("An unexpected error occurred", exception.Message, StatusType.Error);
        }

        public void SetWarning(string statusHeader, string statusText)
        {
            Update(statusHeader, statusText, StatusType.Warning);
        }
        public void SetInformation(string statusHeader, string statusText)
        {
            Update(statusHeader, statusText, StatusType.Information);
        }

        public void SetSuccess(string statusHeader, string statusText)
        {
            Update(statusHeader, statusText, StatusType.Success);
        }

        public void Update(string statusHeader, string statusText, StatusType statusType)
        {
            if (_logger != null)
            {
                switch (statusType)
                {
                    case StatusType.Error:
                        _logger.LogError($"Error( {statusHeader} ): {statusText}");
                        break;
                    case StatusType.Information:
                        _logger.LogInformation($"Information ( {statusHeader} ): {statusText}");
                        break;
                    case StatusType.Success:
                        _logger.LogInformation($"Success ( {statusHeader} ): {statusText}");
                        break;
                    case StatusType.Warning:
                        _logger.LogWarning($"Warning ( {statusHeader} ): {statusText}");
                        break;
                    default:
                        _logger.LogInformation($"Status ( {statusHeader} ): {statusText}");
                        break;
                }
            }
            Status = statusType;
            Header = statusHeader;
            Text = statusText;
            _subjectBase.Notify();
        }
    }
}