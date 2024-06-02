namespace BlazorDynamics.Forms.Commons.Components
{
    public abstract class InputFormComponent : FormComponentBase
    {
        public object? InputValue { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }

     
    }
}
