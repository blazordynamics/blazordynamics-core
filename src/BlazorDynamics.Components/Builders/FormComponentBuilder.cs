using BlazorDynamics.Common.Models;
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
            if (components == null) throw new ArgumentNullException(nameof(components));
            _component.SetComponents(components);
            return GetBuilderToReturn();

        }

        private TBuilder GetBuilderToReturn()
        {
            return this as TBuilder ?? throw new InvalidOperationException("Builder instance is not of the expected type");

        }

        public TBuilder WithTypeDefinition(string typeDefinitionName)
        {
            this.typeDefinitionName = typeDefinitionName;
            return GetBuilderToReturn();
        }

        public TBuilder WithPath(string path)
        {
            _component.SetPath(path);
            return GetBuilderToReturn();
        }

        public TBuilder WithInvalidMessage(string invalidMessage)
        {
            _component.SetInvalidMessage(invalidMessage);
            return GetBuilderToReturn();
        }

        public TBuilder WithStyle(string style)
        {
            _component.SetStyle(style);
            return GetBuilderToReturn();
        }

        public TBuilder WithClass(string className)
        {
            _component.SetClass(className);
            return GetBuilderToReturn();
        }

        public TBuilder WithParameter(string parameterName, object value)
        {
            _customParameters.Add(parameterName, value);
            return GetBuilderToReturn();
        }

        public TBuilder WithSubElements(params DynamicFormModel[] elements)
        {
            _component.FormModel.SubElements.AddRange(elements);
            return GetBuilderToReturn();
        }

        public TBuilder WithSubElement(DynamicFormModel element)
        {
            _component.FormModel.SubElements.Add(element);
            return GetBuilderToReturn();
        }

        public TBuilder WithString(string label, string path)
        {
            _component.FormModel.SubElements.Add(Factories.FormFactory.StringComponent(label, path));
            return GetBuilderToReturn();
        }

        public TBuilder WithValueChanged(EventCallback<object?> valueChanged)
        {
            if (_component?.ValueChanged != null)
            {
                _component.SetValueChanged(valueChanged);
            };
            return GetBuilderToReturn();
        }

        public TBuilder ConfigureFormModel(Action<DynamicFormModel> configure)
        {
            if (_component == null) { return GetBuilderToReturn(); }
            
            if(_component.FormModel == null)
            {
                var model = new DynamicFormModel(); 
                _component.SetFormModel(model);
                configure(model);
            }
            return GetBuilderToReturn();
        }


        public virtual DynamicFormModel Build()
        {
          
            var model = new DynamicFormModel();
            if (_component == null) { return model; }

            model.Parameters.AddRange(_customParameters);
            model.Parameters.Add(ParameterNames.Path, _component.Path);
            model.Parameters.Add(ParameterNames.InvalidMesssage, _component.InvalidMessage ?? string.Empty);
            model.SubElements = _component.FormModel.SubElements;
            return model;
        }
    }
}
