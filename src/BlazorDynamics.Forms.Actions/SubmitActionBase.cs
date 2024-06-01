﻿using BlazorDynamics.Core.Helpers;
using BlazorDynamics.Forms.Commons.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorDynamics.Forms.Actions
{
    public class SubmitActionBase : InputFormComponent
    {
        [Parameter]
        public string Label { get; set; }
      
        public override string ValidationString => TokenReplacer.ReplaceTokens(InvalidMessage, this);

        public void Submit()
        {
            if (OnSubmitted.HasDelegate)
            {
                OnSubmitted.InvokeAsync(GetValue());
            }
        }
    }
}