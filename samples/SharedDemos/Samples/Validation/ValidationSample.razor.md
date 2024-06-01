﻿# Validation

### Parameters

## Sample code

````csharp
@inherits SampleBasePage
@page "/samples/components/validation"

<SampleDocumentation MarkDownFile="Samples.Components.ValidationSample.razor.md" Data="@data">
    <BDForm FormModel="@model" Components="@UserSettingsService.Components" Value="@data" ValueChanged="@update"></BDForm>
</SampleDocumentation>


@code {

    private void update()
    {
        StateHasChanged();
    }

    public ValidationModel data = new ValidationModel
        {
            First = "First text",
            Second = "Second text",
            Third = "Third Text"
        };

    public DynamicFormModel model = FormFactory.GroupComponent("Validation",
        FormFactory.StringComponentBuilder("$.First")
                .WithLabel("regular string")
                .WithMinimumLength(4)
                .WithMaximumLength(10)
                .WithInvalidMessage("The length should be between {MinimumLength} and {MaximumLength}")
                .Build(),
        FormFactory.StringComponentBuilder("$.Second")
                .WithLabel("normal string")
                .WithMaximumLength(4)
                .WithInvalidMessage("The length should less than {MaximumLength}")
                .Build(),
        FormFactory.StringComponentBuilder("$.Third")
                .WithLabel("string")
                .WithMaximumLength(10)
                .Build()
    );

    public class ValidationModel
    {
       
        public string First { get; set; }
       
        public string Second { get; set; }
       
        public string Third { get; set; }
    }
}

````
