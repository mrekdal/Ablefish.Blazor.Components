namespace Ablefish.Blazor.Observer
{

    public interface IObserver
    {
        void HandleUpdate(); // Called when the subject notifies
        GuiElement GuiElement { get; }
    }

    public interface ISubjectMinimal
    {
        void Attach(IObserver observer); // Add an observer
    }
    public interface ISubject: ISubjectMinimal
    {
        void Detach(IObserver observer); // Remove an observer
        void Notify(GuiElement whatChanged); // Notify all observers
    }

}
