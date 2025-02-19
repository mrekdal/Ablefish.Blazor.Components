using Microsoft.Extensions.Logging;

namespace Ablefish.Blazor.Components
{
    public class SubjectBase : ISubject
    {
        private string _subjectName;
        protected ILogger<SubjectBase> _logger;
        protected ILoggerFactory _loggerFactory;

        public SubjectBase(string subjectName, ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<SubjectBase>();
            _subjectName = subjectName;
        }
        public bool LogNotifications { get; set; } = false;
        private readonly List<IObserver> _observers = new();

        public void Attach(IObserver observer)
        {
            if (_observers.Contains(observer))
            {
                _logger.LogError($"{_subjectName}: Observer {observer} is already attached.");
                return;
            }
            if (LogNotifications)
                _logger.LogInformation($"{_subjectName}: Attaching observer {observer} observes {observer.GuiElement}.");
            _observers.Add(observer);
        }

        public void DetachAll()
        {
            foreach (var observer in _observers)
                Detach(observer);
        }

        public void Detach(IObserver observer)
        {
            _logger.LogInformation($"{_subjectName}: Detaching observer {observer.ToString()} ).");
            _observers.Remove(observer);
        }

        public void Notify(GuiElement whatHasChanged)
        {
            int obsNo = 1;
            foreach (var observer in _observers)
            {
                if (observer.GuiElement == whatHasChanged)
                {
                    if (LogNotifications)
                        _logger.LogInformation($"{_subjectName}: Notify observer {observer} of {whatHasChanged} (#{obsNo}/{_observers.Count}).");
                    observer.HandleUpdate();
                }
                obsNo++;
            }
        }
    }
}