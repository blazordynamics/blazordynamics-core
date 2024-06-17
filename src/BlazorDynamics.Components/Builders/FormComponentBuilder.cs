using BlazorDynamics.Core.Models;
using BlazorDynamics.Core.Models.ParameterModels;
using BlazorDynamics.Forms.Commons.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorDynamics.Forms.Components.Builders
{
    public abstract class FormComponentBuilder<TComponent, TBuilder>
        where TComponent : FormComponentBase, new()
        where TBuilder : FormComponentBuilder<TComponent, TBuilder>, new()
    {
        internal string? typeDefinitionName = null;
        internal readonly TComponent _component = new TComponent();
        private readonly ParameterList _customParameters = new ParameterList();
        public TBuilder WithComponentDefinitions(ComponentsList components)
        {
            _component.Components = components;
            return this as TBuilder;
        }

        public TBuilder WithTypeDefinition(string typeDefinitionName)
        {
            this.typeDefinitionName = typeDefinitionName;
            return this as TBuilder;
        }

        public TBuilder WithPath(string path)
        {
            _component.Path = path;
            return this as TBuilder;
        }

        public TBuilder WithInvalidMessage(string invalidMessage)
        {
            _component.InvalidMessage = invalidMessage;
            return this as TBuilder;
        }

        public TBuilder WithStyle(string style)
        {
            _component.Style = style;
            return this as TBuilder;
        }

        public TBuilder WithClass(string className)
        {
            _component.Class = className;
            return this as TBuilder;
        }

        public TBuilder WithParameter(string parameterName, object value)
        {
            _customParameters.Add(parameterName, value);
            return this as TBuilder;
        }

        //public TBuilder WithFormModel(DynamicFormModel formModel)
        //{
        //    _component.FormModel = formModel;
        //    return this as TBuilder;
        //}

        public TBuilder WithSubElements(params DynamicFormModel[] elements)
        {
            _component.FormModel.SubElements.AddRange(elements);
            return this as TBuilder;
        }

        public TBuilder WithSubElement(DynamicFormModel element)
        {
            _component.FormModel.SubElements.Add(element);
            return this as TBuilder;
        }

        public TBuilder WithString(string label, string path)
        {
            _component.FormModel.SubElements.Add(Factories.FormFactory.StringComponent(label, path));
            return this as TBuilder;
        }

        public TBuilder WithValueChanged(EventCallback<object?> valueChanged)
        {
            _component.ValueChanged = valueChanged;
            return this as TBuilder;
        }

        //public FormComponentBuilder<TComponent, TBuilder> AddElement(Action<TBuilder> configure)
        //{
        //    // Create a new builder of the same type
        //    var childBuilder = new TBuilder();

        //    // Configure the child builder using the provided action
        //    configure(childBuilder);

        //    WithFormModel(childBuilder.Build());

        //    // Return the original builder
        //    return this;
        //}


        public TBuilder ConfigureFormModel(Action<DynamicFormModel> configure)
        {
            if (_component.FormModel == null)
            {
                _component.FormModel = new DynamicFormModel();
            }

            configure(_component.FormModel);
            return this as TBuilder;
        }


        public virtual DynamicFormModel Build()
        {
            var model = new DynamicFormModel();
            model.Parameters.AddRange(_customParameters);
            model.Parameters.Add(ParameterNames.Path, _component.Path);
            model.Parameters.Add(ParameterNames.InvalidMesssage, _component.InvalidMessage);
            model.SubElements = _component.FormModel.SubElements;
            return model;
        }
    }
}
