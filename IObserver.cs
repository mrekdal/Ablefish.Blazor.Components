namespace Ablefish.Blazor.Observer
{

    public interface IObserver
    {
        void HandleUpdate(); 
    }

    public interface IObserverGui : IObserver
    {
        GuiElement GuiElement { get; }
    }

    public interface ISubjectMinimal
    {
        void Attach(IObserver observer); // Add an observer
    }

    public interface ISubject: ISubjectMinimal
    {
        void Detach(IObserver observer); // Remove an observer
        void Notify(); // Notify all observers
    }

    public interface ISubjectGui : ISubject
    {
        void NotifyGui(GuiElement whatChanged); // Notify all observers
    }

}
