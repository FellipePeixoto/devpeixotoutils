namespace DevPeixoto.Mobile.Screens
{
    public interface IScreen : ISetup, IResume, IPause, IResetData, IScreenClose, ISubmit, IBack { }

    public interface ISetup
    {
        public void SetupScreen();
    }

    public interface IResume
    {
        public void ResumeScreen();
    }

    public interface IPause
    {
        public void OnPauseScreen();
    }

    public interface IResetData
    {
        public void ResetData();
    }

    public interface IScreenClose
    {
        public void OnScreenClose();
    }

    public interface ISubmit
    {
        public void Submit();
    }

    public interface IBack
    {
        public void Back();
    }
}
