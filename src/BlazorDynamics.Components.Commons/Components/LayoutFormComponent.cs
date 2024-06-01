namespace BlazorDynamics.Forms.Commons.Components
{
    public abstract class LayoutFormComponent : FormComponentBase
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();
            AllowElements = true;
        }

    }
}
