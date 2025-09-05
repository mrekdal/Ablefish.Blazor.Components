using Microsoft.Extensions.Logging;

namespace Ablefish.Blazor.Observer
{

    public class SubjectBase: ISubject
    {
        protected readonly List<IObserver> _observers = new();
        public SubjectBase() { 
        }

        public virtual void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }
        public virtual void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void DetachAll()
        {
            foreach (var observer in _observers)
                Detach(observer);
        }
        public virtual void Notify()
        {
            foreach (var observer in _observers)
                    observer.HandleUpdate();
        }
    }

    public class SubjectLogged : SubjectBase,  ISubject
    {
        private string _subjectName;
        protected ILogger<SubjectBase> _logger;
        protected ILoggerFactory _loggerFactory;

        public SubjectLogged(string subjectName, ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<SubjectBase>();
            _subjectName = subjectName;
        }
        public bool LogNotifications { get; set; } = false;

        public override void Attach(IObserver observer)
        {
            if (_observers.Contains(observer))
            {
                _logger.LogError($"{_subjectName}: Observer {observer} is already attached.");
                return;
            }
            if (LogNotifications)
                _logger.LogInformation($"{_subjectName}: Attaching observer {observer} observes {this}.");
            base.Attach(observer);
        }


        public override void Detach(IObserver observer)
        {
            _logger.LogInformation($"{_subjectName}: Detaching observer {observer.ToString()} ).");
            base.Detach(observer);
        }

        public override void Notify()
        {
            int obsNo = 1;
            foreach (var observer in _observers)
            {
                {
                    if (LogNotifications)
                        _logger.LogInformation($"{_subjectName}: Notify observer {observer} of {this} (#{obsNo}/{_observers.Count}).");
                    observer.HandleUpdate();
                }
                obsNo++;
            }
        }
    }
}