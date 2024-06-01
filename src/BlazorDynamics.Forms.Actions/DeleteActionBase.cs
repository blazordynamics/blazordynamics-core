﻿using BlazorDynamics.Core.Helpers;
using BlazorDynamics.Forms.Commons.Components;
using BlazorDynamics.Forms.Commons.ObjectHandlers;
using Microsoft.AspNetCore.Components;

namespace BlazorDynamics.Forms.Actions
{
    public class DeleteActionBase : InputFormComponent
    {
        [Parameter]
        public string Label { get; set; }

        public override string ValidationString => TokenReplacer.ReplaceTokens(InvalidMessage, this);

        public void DeleteItem()
        {
            DataObjectHelper.Remove(GetInstancePath(Path), Value);
            ValueChanged.InvokeAsync(null);
            // StateHasChanged();
        }
    }
}
